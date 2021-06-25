---
title: "Plugin walkthrough: Logging messages"
---

# Logging messages

One of the most important parts of any plugin is logging messages. Be it a 
piece of information, a warning or a bigger error, BepInEx provides 
functionality to log it all.  
With BepInEx, you can use the following logging APIs:

* **(Recommended)** @BepInEx.Logging.Logger APIs
* `UnityEngine.Debug` APIs
* `System.Diagnostics.Trace` APIs
* `System.Console` APIs

Whichever API you will use, BepInEx will write the logs to the console,
Unity's `output_log.txt` and to `BepInEx/LogOutput.log` file.

## Using @BepInEx.Logging.Logger APIs

**This is the recommended way for logging in plugins.**

All @BepInEx.BaseUnityPlugin instances have @BepInEx.BaseUnityPlugin.Logger 
property for logging:

```csharp
[BepInPlugin("org.bepinex.myplugin", "Logger Test", "1.0.0.0")]
public class MyPlugin : BaseUnityPlugin
{
    void Awake()
    {
        Logger.LogInfo("This is information");
        Logger.LogWarning("This is a warning");
        Logger.LogError("This is an error");
    }
}
```

If BepInEx console is enabled via [the configuration](<xref>configuration), 
this will print

```
[Info   : Logger Test] This is information
[Warning: Logger Test] This is a warning
[Error  : Logger Test] This is an error
```

Notice that the log reports the message type and the message source.  
When using BepInEx's own logging API, the log source (i.e. the plugin name 
is automatically reported).

Refer to @BepInEx.Logging.ManualLogSource for all available logging API.

## Advanced: Log sources and log listeners

BepInEx's logging system mimics that of `System.Diagnostics.Trace` API.  
BepInEx allows to create *log sources* that can generate log events (i.e 
the messages) and *log listeners* that receive and process those log events.  
All sources are linked to listeners via @BepInEx.Logging.Logger class.

In most cases, you don't have to care about how the API works. However, in some
cases you may want to register your own log sources to log messages.  
In  addition, sometimes you might need to process the log events to write them
somewhere. This is where you use the manual APIs.

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

Because @BepInEx.Logging.ManualLogSource is so useful, you can use 
@BepInEx.Logging.Logger.CreateLogSource(System.String) to automatically create
and register a @BepInEx.Logging.ManualLogSource. 

That way the above example becomes

```csharp
var myLogSource = BepInEx.Logging.Logger.CreateLogSource("MyLogSource");
myLogSource.LogInfo("Test");
BepInEx.Logging.Logger.Sources.Remove(myLogSource);
```

### About log listeners

Log listeners are used to process messages from log sources. To create a log 
source, create a class that inherits @BepInEx.Logging.ILogListener.  
After that, register a log listener by adding it to @BepInEx.Logging.Logger.Listeners.

By default, BepInEx itself registers the following listeners:

* @BepInEx.Logging.ConsoleLogListener - writes all log messages to BepInEx 
  console
* @BepInEx.Logging.DiskLogListener - writes all log messages to 
  `BepInEx/LogOutput.log`
* @BepInEx.Logging.UnityLogListener - writes all log messages to Unity's 
  `output_log.txt`

If you need to write a custom log listener, consider using the above ones as 
examples.