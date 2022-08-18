---
title: "Basic plugin: Reading and writing configuration files"
---

# Using configuration files

Usually, you may want to allow the user of the plugin to change the specifics of its behavior.
Moreover, your plugin might need to have a permanent data store for some of its internal data.

Whatever the reason, BepInEx provides a built-in @BepInEx.Configuration.ConfigFile class for simple configuration files. 
The format is based on INI with some syntax from TOML, which allows you to save most of the basic data types.

> [!IMPORTANT]
> The configuration parser is undergoing changes in BepInEx 6.
> As such, expect the syntax to change in the near future.

> [!NOTE]
> Using BepInEx's configuration API is optional.
> You can always provide a custom way to serialize and deserialize data for your plugin.

In this part, we will go through the core API for reading and writing configuration files.

## Using configuration files in plugins

Inside the plugin, you get access to @BepInEx.BaseUnityPlugin.Config property that is a preconfigured configuration file.  
The file is saved in `BepInEx\config\<GUID>.cfg` where `<GUID>` is the GUID of your plugin.

To access and create configuration values, you first need to define them with @BepInEx.Configuration.ConfigFile.Bind``1(System.String,System.String,``0,System.String).
Configuration initialization is often done in plugin startup code.

Example:

# [Unity (Mono)](#tab/tabid-unitymono)

```cs
using BepInEx;
using BepInEx.Unity.Mono;

namespace MyFirstPlugin;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    private ConfigEntry<string> configGreeting;
    private ConfigEntry<bool> configDisplayGreeting;

    private void Awake()
    {
        configGreeting = Config.Bind("General",      // The section under which the option is shown
                                        "GreetingText",  // The key of the configuration option in the configuration file
                                        "Hello, world!", // The default value
                                        "A greeting text to show when the game is launched"); // Description of the option to show in the config file

        configDisplayGreeting = Config.Bind("General.Toggles", 
                                            "DisplayGreeting",
                                            true,
                                            "Whether or not to show the greeting text");
        // Test code
        Logger.LogInfo("Hello, world!");
    }
}
```

# [Unity (Il2Cpp)](#tab/tabid-unityil2cpp)

```cs
using BepInEx;
using BepInEx.Unity.IL2CPP;

namespace MyFirstPlugin;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    private ConfigEntry<string> configGreeting;
    private ConfigEntry<bool> configDisplayGreeting;

    public override void Load()
    {
        configGreeting = Config.Bind("General",      // The section under which the option is shown
                                        "GreetingText",  // The key of the configuration option in the configuration file
                                        "Hello, world!", // The default value
                                        "A greeting text to show when the game is launched"); // Description of the option to show in the config file

        configDisplayGreeting = Config.Bind("General.Toggles", 
                                            "DisplayGreeting",
                                            true,
                                            "Whether or not to show the greeting text");
        // Test code
        Log.LogInfo("Hello, world!");
    }
}
```

# [.NET Framework](#tab/tabid-netfw)

```cs
using BepInEx;
using BepInEx.NET.Common;

namespace MyFirstPlugin;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    private ConfigEntry<string> configGreeting;
    private ConfigEntry<bool> configDisplayGreeting;

    public override void Load()
    {
        configGreeting = Config.Bind("General",      // The section under which the option is shown
                                        "GreetingText",  // The key of the configuration option in the configuration file
                                        "Hello, world!", // The default value
                                        "A greeting text to show when the game is launched"); // Description of the option to show in the config file

        configDisplayGreeting = Config.Bind("General.Toggles", 
                                            "DisplayGreeting",
                                            true,
                                            "Whether or not to show the greeting text");
        // Test code
        Log.LogInfo("Hello, world!");
    }
}
```

# [.NET Core](#tab/tabid-coreclr)

```cs
using BepInEx;
using BepInEx.NET.Common;

namespace MyFirstPlugin;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    private ConfigEntry<string> configGreeting;
    private ConfigEntry<bool> configDisplayGreeting;

    public override void Load()
    {
        configGreeting = Config.Bind("General",      // The section under which the option is shown
                                        "GreetingText",  // The key of the configuration option in the configuration file
                                        "Hello, world!", // The default value
                                        "A greeting text to show when the game is launched"); // Description of the option to show in the config file

        configDisplayGreeting = Config.Bind("General.Toggles", 
                                            "DisplayGreeting",
                                            true,
                                            "Whether or not to show the greeting text");
        // Test code
        Log.LogInfo("Hello, world!");
    }
}
```

***

> [!TIP]
> Instead of using the plugin startup method, you can also define wrappers inside the constructor.
> Moreover, you do not need to define all options at once and instead create them on demand!

After defining the values, you can use them right away with @BepInEx.Configuration.ConfigEntry`1.Value:

# [Unity (Mono)](#tab/tabid-unitymono)

```cs
// Instead of just Debug.Log("Hello, world!")
if(configDisplayGreeting.Value)
    Logger.LogInfo(configGreeting.Value);
```

# [Unity (Il2Cpp)](#tab/tabid-unityil2cpp)

```cs
// Instead of just Debug.Log("Hello, world!")
if(configDisplayGreeting.Value)
    Log.LogInfo(configGreeting.Value);
```

# [.NET Framework](#tab/tabid-netfw)

```cs
if(configDisplayGreeting.Value)
    Log.LogInfo(configGreeting.Value);
```

# [.NET Core](#tab/tabid-coreclr)

```cs
if(configDisplayGreeting.Value)
    Log.LogInfo(configGreeting.Value);
```

***

When you compile your plugin and run the game with it for the first time, the configuration file will be automatically generated.  
In the case of this example, the following configuration file is created in `BepInEx\config\MyFirstPlugin.cfg`:

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

Notice the similarities between the calls to @BepInEx.Configuration.ConfigFile.Bind``1(System.String,System.String,``0,System.String) and the generated configuration file.

## Creating configuration files manually

In some cases (e.g. preloader patchers, non-plugin DLLs), you may want to create a configuration file manually.

This can be done quickly by creating a new instance of @BepInEx.Configuration.ConfigFile:

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
> In general, it is **recommended** to use the paths provided in @BepInEx.Paths instead of manually trying to locate the directories.

## Summary

In this part, we briefly overviewed the use of configuration files.

Next, you should get better accustomed to the additional API provided in @BepInEx.Configuration.ConfigFile and @BepInEx.Configuration.ConfigEntry`1 if you want to use configuration files supplied by BepInEx.  
The additional API allows you to manually save and reload configuration as well.

This part concludes the basic plugin tutorial.
Feel free to refer to [BepInEx API Docs](~/api/index.md) for extensive information on all methods that BepInEx provides.
Check through some of the advanced guides for information on how to use BepInEx:

* [Patching methods at runtime](<xref:runtime_patching>)