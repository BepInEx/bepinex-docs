# BepInEx Documentation

This is the repo for storing documentation related to BepInEx.  

## [View the docs](https://docs.bepinex.dev/)

## Contributing

All contributions either via PRs or issues are welcome!

This project uses [DocFX](https://dotnet.github.io/docfx/) to render the API documentation and the articles.  
Please refer to DocFX documentation for information on using [DocFX-flavoured markdown](https://dotnet.github.io/docfx/spec/docfx_flavored_markdown.html?tabs=tabid-1%2Ctabid-a).

In general, you should be able to update pages with a simple markdown editor.

### Testing docs locally

If you want to preview the docs locally, you need .NET 5 or newer installed.  
After that, do the following:

1. Clone this repo with `git clone`
2. In the cloned directory, run
    ```
    git worktree add --checkout common common
    ```
    A folder named `common` should appear.
2. Write documentation into `api` or `articles` folder. Refer to [docfx guide](https://dotnet.github.io/docfx/tutorial/docfx_getting_started.html) and [DFM syntax guide](https://dotnet.github.io/docfx/spec/docfx_flavored_markdown.html) for info on writing the guides using DocFX
3. Run `common/build.bat --target=Build` to build the docs. The generated docs will appear in `_site` folder
