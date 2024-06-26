---
title: "Basic plugin: Using loggers to simplify debugging"
---

# Using loggers to simplify debugging

One of the most essential parts of any plugin is logging messages. Be it a 
piece of information, a warning or a more significant error, BepInEx provides functionality to log it all.  
With BepInEx, you can use the following logging APIs:

* **(Recommended)** @BepInEx.Logging.Logger APIs
* `UnityEngine.Debug` APIs (for Unity Mono)
* `System.Diagnostics.Trace` APIs
* `System.Console` APIs

Whichever API you will use, BepInEx will write the logs to the console,
Unity's `output_log.txt` and to `BepInEx/LogOutput.log` file.

## Using @BepInEx.Logging.Logger APIs

**This is the recommended way for logging in plugins.**

All plugin instances have a logger property:

# [Unity (Mono)](#tab/tabid-unitymono)

```cs
using BepInEx;
using BepInEx.Unity.Mono;

namespace MyFirstPlugin;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    private void Awake()
    {
        Logger.LogInfo("This is information");
        Logger.LogWarning("This is a warning");
        Logger.LogError("This is an error");
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
    public override void Load()
    {
        Log.LogInfo("This is information");
        Log.LogWarning("This is a warning");
        Log.LogError("This is an error");
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
    public override void Load()
    {
        Log.LogInfo("This is information");
        Log.LogWarning("This is a warning");
        Log.LogError("This is an error");
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
    public override void Load()
    {
        Log.LogInfo("This is information");
        Log.LogWarning("This is a warning");
        Log.LogError("This is an error");
    }
}
```

***


This will print the following messages to BepInEx console:

```txt
[Info   : Logger Test] This is information
[Warning: Logger Test] This is a warning
[Error  : Logger Test] This is an error
```

Notice that the log reports the message type and the message source.  
When using BepInEx's own logging API, the log source (i.e. the plugin name is automatically logged).

Check out @BepInEx.Logging.ManualLogSource for all available logging methods.

## Advanced: Log sources and log listeners

BepInEx's logging system mimics that of `System.Diagnostics.Trace` API.  
BepInEx allows creating *log sources* that can generate log events (i.e. the messages) and *log listeners* that receive and process those log events.  
All sources are linked to listeners via @BepInEx.Logging.Logger class.

In most cases, you don't have to care about how the API works. However, in some cases, you may want to register your own log sources to log messages.  
In addition, sometimes, you might need to process the log events to write them somewhere. This is where you use the manual APIs.

### Registering log sources

A log source is a class that inherits from @BepInEx.Logging.ILogSource.  
The most basic implementation is @BepInEx.Logging.ManualLogSource which exposes
various convenience logging functions.

To register a log source, add it to @BepInEx.Logging.Logger.Sources collection:

```csharp
var myLogSource = new ManualLogSource("MyLogSource"); // The source name is shown in BepInEx log

// Register the source
BepInEx.Logging.Logger.Sources.Add(myLogSource);

myLogSource.LogInfo("Test"); // Will print [Info: MyLogSource] Test

// Remove the source to free resources
BepInEx.Logging.Logger.Sources.Remove(myLogSource);
```

Because @BepInEx.Logging.ManualLogSource is so useful, you can use @BepInEx.Logging.Logger.CreateLogSource(System.String) to automatically create and register a @BepInEx.Logging.ManualLogSource. 

That way, the above example becomes

```csharp
var myLogSource = BepInEx.Logging.Logger.CreateLogSource("MyLogSource");
myLogSource.LogInfo("Test");
BepInEx.Logging.Logger.Sources.Remove(myLogSource);
```

### About log listeners

Log listeners are used to processing messages from log sources. To create a log source, create a class that inherits @BepInEx.Logging.ILogListener.  
After that, register a log listener by adding it to @BepInEx.Logging.Logger.Listeners.

By default, BepInEx itself registers the following listeners:

* @BepInEx.Logging.ConsoleLogListener - writes all log messages to BepInEx 
  console
* @BepInEx.Logging.DiskLogListener - writes all log messages to 
  `BepInEx/LogOutput.log`
* @BepInEx.Unity.Mono.Logging.UnityLogListener - writes all log messages to Unity's 
  `output_log.txt` (only in Unity Mono)
* @BepInEx.Unity.IL2CPP.Logging.IL2CPPUnityLogSource - writes all log messages to Unity's 
  `output_log.txt` (only in Unity Il2Cpp)

If you need to write a custom log listener, consider using the above ones as 
examples.

## Advanced: global plugin logger

If you have multiple classes in your plugin but only one plugin, you might want to use the same plugin logger in the other class as well.

This can be done with with a *global plugin logger* pattern. To apply the pattern, do the following:

* Create an internal static @BepInEx.Logging.ManualLogSource field inside the plugin class
* In plugin's startup code, assign plugin's logger to the field
* In your other classes, use the static logger field from your plugin class

Example:

# [Unity (Mono)](#tab/tabid-unitymono)

```cs
using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.Mono;

namespace MyFirstPlugin;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Log;

    private void Awake()
    {
        Log = base.Logger;
    }
}

// Some other class in the plugin assembly
class SomeOtherAssembly
{
    public void SomeMethod()
    {
        Plugin.Log.LogInfo("Plugin message!");
    }
}
```

# [Unity (Il2Cpp)](#tab/tabid-unityil2cpp)

```cs
using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;

namespace MyFirstPlugin;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    internal static new ManualLogSource Log;

    public override void Load()
    {
        Log = base.Log;
    }
}

// Some other class in the plugin assembly
class SomeOtherAssembly
{
    public void SomeMethod()
    {
        Plugin.Log.LogInfo("Plugin message!");
    }
}
```

# [.NET Framework](#tab/tabid-netfw)

```cs
using BepInEx;
using BepInEx.Logging;
using BepInEx.NET.Common;

namespace MyFirstPlugin;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    internal static new ManualLogSource Log;

    public override void Load()
    {
        Log = base.Log;
    }
}

// Some other class in the plugin assembly
class SomeOtherAssembly
{
    public void SomeMethod()
    {
        Plugin.Log.LogInfo("Plugin message!");
    }
}
```

# [.NET Core](#tab/tabid-coreclr)

```cs
using BepInEx;
using BepInEx.Logging;
using BepInEx.NET.Common;

namespace MyFirstPlugin;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    internal static new ManualLogSource Log;

    public override void Load()
    {
        Log = base.Log;
    }
}

// Some other class in the plugin assembly
class SomeOtherAssembly
{
    public void SomeMethod()
    {
        Plugin.Log.LogInfo("Plugin message!");
    }
}
```

***

## Summary

BepInEx provides simple logging methods for plugins.
Additionally, you are free to extend BepInEx logging facilities to suit your needs.

Next: [Reading and writing configuration files](4_configuration.md)
