#addin nuget:?package=Cake.DocFx&version=1.0.0
#tool nuget:?package=docfx.console&version=2.58.0
#addin nuget:?package=Cake.DoInDirectory&version=4.0.2
#addin nuget:?package=Cake.Json&version=6.0.1
#addin nuget:?package=Newtonsoft.Json&version=13.0.1
#addin nuget:?package=Cake.FileHelpers&version=4.0.1
#addin nuget:?package=Cake.SemVer&version=4.0.0
#addin nuget:?package=semver&version=2.0.4

var target = Argument("target", "Build");
var buildVersion = Argument("build_version", GetVersionString());
var repoUrl = Argument("repo_url", "https://github.com/BepInEx/bepinex-docs");

Information($"Version to build: {buildVersion}");

string GetVersionString() {
    var res = "";
    DoInDirectory("..", () =>
    {
        res = RunGit("rev-parse --abbrev-ref HEAD");
    });
    return res;
}

string RunGit(string command, string separator = "") 
{
    using(var process = StartAndReturnProcess("git", new() { Arguments = command, RedirectStandardOutput = true })) 
    {
        process.WaitForExit();
        return string.Join(separator, process.GetStandardOutput());
    }
}

void CleanDir(DirectoryPath path) 
{
    if(DirectoryExists(path))
        DeleteDirectory(path, new() { Force = true, Recursive = true });
}

Task("Cleanup")
    .Does(() => 
{
    DoInDirectory("..", () => 
    {
        CleanDir("_site");
        // if (DirectoryExists("gh-pages"))
        // {
        //     CleanDir("gh-pages");
        //     RunGit("worktree remove gh-pages");
        // }
    });
});

Task("CleanDeps")
    .Does(() =>
{
    DoInDirectory("..", () => 
    {
        CleanDir("src");
        
    });
});

Task("PullDeps")
    .Does(() =>
{
    DoInDirectory("..", () =>
    {
        if (!DirectoryExists("src"))
        {
            Information("Pulling BepInEx");
            CreateDirectory("src");
            RunGit("clone https://github.com/BepInEx/BepInEx.git src");

            DoInDirectory("src", () => 
            {
                if(buildVersion != "master")
                    RunGit($"checkout {buildVersion}");
                RunGit("submodule update --init --recursive");
            });
        }

        if (!DirectoryExists("template"))
        {
            Information("Pulling template");
            CreateDirectory("template");
            RunGit("clone https://github.com/BepInEx/bepinex-docs-template.git template");
        }
    });
});

Task("LoadGHPages")
    .Does(() =>
{
    DoInDirectory("..", () => 
    {
        if (DirectoryExists("gh-pages"))
        {
            Information("Cleaning up previous worktree");
            // RunGit("worktree remove gh-pages");
            return;
        }
        Information("Loading GH Pages as a worktree");
        RunGit("fetch");
        RunGit("worktree add --checkout gh-pages gh-pages");
    });
});

Task("GenDocs")
    .Does(() => 
{
    DoInDirectory("..", () =>
    {
        Information("Generating metadata");
        DocFxMetadata("./docfx.json");

        Information("Generating docs");
        var gitInfo = RunGit("log --pretty=\"%h; %ci\" -1");

        var ghMeta = SerializeJson(new Dictionary<string, string>{
            ["repo"] = repoUrl,
            ["branch"] = buildVersion
        }).Replace("\"", "\\\"");

        Information(ghMeta);

        DocFxBuild("./docfx.json", new() {
            GlobalMetadata = {
                ["_docsVersion"] = buildVersion,
                ["_buildInfo"] = gitInfo,
                ["_a"] = $"\\\", \\\"_gitContribute\\\": {ghMeta}, \\\"_b\\\": \\\"" // A hack to bypass Cake.DocFx restrictuin on globalmetadata type
            }
        });
    });
});

Task("PublishGHPages")
    .IsDependentOn("Cleanup")
    .IsDependentOn("CleanDeps")
    .IsDependentOn("PullDeps")
    .IsDependentOn("LoadGHPages")
    .IsDependentOn("GenDocs")
    .Does(() => 
{
    DoInDirectory("..", () =>
    {
        var ghPages = Directory("gh-pages");
        var buildDir = ghPages + Directory(buildVersion);

        Information($"Copying docs to {buildDir}");
        CleanDir(buildDir);
        CreateDirectory(buildDir);
        CopyDirectory("_site", buildDir);

        bool IsVersionDir(DirectoryPath d) => d.GetDirectoryName().StartsWith("v");

        var allVersions = GetDirectories("gh-pages/*")
                .Where(IsVersionDir)
                .Select(d => (d.GetDirectoryName(), ParseSemVer(d.GetDirectoryName()[1..])));
        var (latestTag, latestVersion) = allVersions.OrderByDescending((v) => v.Item2).FirstOrDefault();

        // Version target
        if (buildVersion.StartsWith("v"))
        {
            var curVersion = ParseSemVer(buildVersion[1..]);

            Information($"Current version: {curVersion}; Latest built version: {latestVersion}");

            if (curVersion >= latestVersion)
            {
                Information("Updating index version");

                var deleteDirs = GetDirectories("gh-pages/*")
                    .Where(d => !IsVersionDir(d) && d.GetDirectoryName() != "master" && d.GetDirectoryName() != "gh-pages");

                DeleteDirectories(deleteDirs, new() {
                    Force = true,
                    Recursive = true
                });

                CopyDirectory("_site", ghPages);
            }
        }

        Information($"Generating versions file");
        FileWriteText(ghPages + File("versions.json"),
            SerializeJsonPretty(new Dictionary<string, object> {
                ["versions"] = allVersions.Select((v) => new { tag = v.Item1, version = v.Item2.ToString()}).Concat(new[] { new { tag = "master", version = "master" } }),
                ["latestTag"] = latestTag?.ToString() ?? ""
        }));
    });
});

Task("Build")
    .IsDependentOn("Cleanup")
    .IsDependentOn("PullDeps")
    .IsDependentOn("GenDocs");

Task("Serve")
    .Does(() =>
{
    DoInDirectory("..", () =>
    {
         DocFxServe("./_site");
    });
});

Task("BuildServe")
    .IsDependentOn("Build")
    .IsDependentOn("Serve");

RunTarget(target);