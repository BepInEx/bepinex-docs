---
uid: plugin_dev_index
title: "Writing a basic plugin"
---

# Writing a basic plugin

The primary purpose of BepInEx is to load user-made code into various games.  
There are a few ways of doing it, but writing *plugins* is the most commonly used approach.
Plugins are classes that are annotated with @BepInEx.BepInPlugin annotation.
BepInEx provides a variety of helpers to facilitate logging, configuration, path management and dependency management.
Plugins are compiled into .NET DLL files and put into `BepInEx/plugins` folder for BepInEx to load.

BepInEx provides some starter templates to make plugin development easier.

In this guide, we will

* install tools necessary for plugin development,
* set up a basic C# plugin project,
* use plugin logger to write messages to the console, and
* read and write configuration files.

> [!NOTE]
> Although this is an introductory guide, an elementary understanding of C# is required.  
> If you are not familiar with C#, [.NET Academy](https://dotnetcademy.net/) provides a simple step-by-step tutorial.
>
> On the contrary, basic knowledge of using command line prompt on your OS is **strongly encouraged**.

The following topics will be covered:

1. [Setting up the development environment](./1_setup.md)
2. [Creating a new plugin project](./2_plugin_start.md)
3. [Using loggers to simplify debugging](./3_logging.md)
4. [Reading and writing configuration files](./4_configuration.md)