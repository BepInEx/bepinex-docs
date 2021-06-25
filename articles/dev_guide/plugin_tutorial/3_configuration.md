---
title: "Plugin walkthrough: Using configuration files"
---

# Using configuration files

Usually you may want to allow the user of the plug-in to change the specifics 
of its behaviour.
Moreover, your plug-in might need to have a permanent data store for some of 
its internal data.

Whatever the reason, BepInEx provides a built-in @BepInEx.Configuration.ConfigFile 
class for simple configuration files. 
The format is based on INI with some syntax from TOML, which allows you to save 
most of the basic data types.

This chapter provides information on how to add and use the configuration 
files. We use the example plug-in as in the previous chapters.

> [!NOTE]
> Using BepInEx's configuration API is optional. You can always provide a 
> custom way to serialise and deserialise data for your plug-in.

## Using configuration files in plug-ins

Inside the plug-in you get access to @BepInEx.BaseUnityPlugin.Config property 
that is preconfigured to be saved in `BepInEx\config\<GUID>.cfg` where `<GUID>` 
is the GUID of your plug-in.

In order to use the configuration file, you have to define the values you want 
to save programmatically with @BepInEx.Configuration.ConfigFile.Bind``1(System.String,System.String,``0,System.String):

```csharp
[BepInPlugin("org.bepinex.plugins.exampleplugin", "Example Plug-In", "1.0.0.0")]
public class ExamplePlugin : BaseUnityPlugin
{
    private ConfigEntry<string> configGreeting;
    private ConfigEntry<bool> configDisplayGreeting;

    void Awake()
    {
        configGreeting = Config.Bind("General",   // The section under which the option is shown
                                     "GreetingText",  // The key of the configuration option in the configuration file
                                     "Hello, world!", // The default value
                                     "A greeting text to show when the game is launched"); // Description of the option to show in the config file

        configDisplayGreeting = Config.Bind("General.Toggles", 
                                            "DisplayGreeting",
                                            true,
                                            "Whether or not to show the greeting text");
        Logger.LogInfo("Hello, world!");
    }
}
```

> [!TIP]
> Instead of `Awake()`, you can also define wrappers inside the constructor.
> Moreover, you don't need to define all options at once and instead have them 
> created on demand!

After you have defined the values, you can use them right away with 
@BepInEx.Configuration.ConfigEntry`1.Value:

```csharp
// Instead of just Debug.Log("Hello, world!")
if(configDisplayGreeting.Value)
    Logger.LogInfo(configGreeting.Value);
```

When you compile your plug-in and run the game with it for the first time, the 
configuration file will be automatically generated.  
In the case of this example, the following configuration file is created in 
`BepInEx\config\org.bepinex.plugins.exampleplugin.cfg`:

```ini
[General]

## A greeting text to show when the game is launched
# Setting type: String
# Default value: Hello, world!
GreetingTest = Hello, world!

[General.Toggles]

## Whether or not to show the greeting text
# Setting type: Boolean
# Default value: True
DisplayGreeting = true
```

Notice the similarities between the calls to 
@BepInEx.Configuration.ConfigFile.Bind``1(System.String,System.String,``0,System.String)
and the generated configuration file.

## Creating configuration files manually

In some cases (e.g. [preloader patchers](<xref:preloader_patches>), non-plugin 
DLLs) you may want to create a configuration file manually.

This can be done easily be creating a new instance of 
@BepInEx.Configuration.ConfigFile:

```csharp
// Create a new configuration file.
// First argument is the path to where the configuration is saved
// Second arguments specifes whether to create the file right away or whether to wait until any values are accessed/written
var customFile = new ConfigFile(Path.Combine(Paths.ConfigPath, "custom_config.cfg"), true);

// You can now create configuration wrappers for it
var userName = customFile.Bind("General",
    "UserName",
    "Deuce",
    "Name of the user");

// In plug-ins, you can still access the default configuration file
var configGreeting = Config.Bind("General", 
    "GreetingTest",
    "Hello, world!", 
    "A greeting text to show when the game is launched");
```

> [!NOTE]
> Notice that we use @BepInEx.Paths class to get the path to `BepInEx\config`.
> In general, it is **recommended** to use the paths provided in @BepInEx.Paths 
> instead of manually trying to locate the directories.

## Next steps

Next, you should get better accustomed with additional API provided in 
@BepInEx.Configuration.ConfigFile and @BepInEx.Configuration.ConfigEntry`1 if 
you want to use configuration files provided by BepInEx.  
The additional API allows you to manually save and reload configuration as well 
as listen for configuration change events.

In the next chapter we will look at the logging API provided by BepInEx.