---
uid: api
---

# BepInEx API documentation

This page contains documentation for BepInEx API.

## Structure of BepInEx

BepInEx is divided into subassemblies that provide game-specific support.

Main assemblies are:

* `BepInEx.Core.dll` -- contains common utilities of @BepInEx namespace and base classes for engine-specific loaders
* `BepInEx.Unity.dll` -- contains @BepInEx.Unity namespace responsible for Mono Unity support
* `BepInEx.IL2CPP.dll` -- contains @BepInEx.IL2CPP namespace responsible for Il2Cpp Unity support
* `BepInEx.NetLauncher.exe` -- contains @BepInEx.NetLauncher namespace and code responsible for .NET Framework modding support

### Support assembly structure

Each assembly exports at least two classes

* A class that every plugin must inherit to be counted as a plugin (e.g. @BepInEx.BaseUnityPlugin for Mono or @BepInEx.IL2CPP.BasePlugin for Il2Cpp)
* An implementation of @BepInEx.Bootstrap.BaseChainloader`1 that is responsible for loading the plugins

Additionally, there are some general useful classes and namespaces:

* @BepInEx.BepInPlugin, @BepInEx.BepInDependency and @BepInEx.BepInProcess attributes -- used to specify necessary metadata about each plugin class.
* @BepInEx.Configuration -- access to configuration.
* @BepInEx.Paths -- all standard file and directory locations BepInEx and plugins rely on.
* @BepInEx.Logging -- classes used for logging