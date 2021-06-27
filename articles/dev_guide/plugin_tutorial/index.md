---
uid: plugin_dev_index
title: "How to: create a BepInEx plugin"
---

# How to: create a BepInEx plugin

The main purpose of BepInEx is to load user-made code into Unity.
This is done by writing *plugins* -- classes that inherit from @BepInEx.BaseUnityPlugin.
In addition to being loaded by BepInEx and being able to use Unity's and game's APIs, plugins have access to various BepInEx functionalities, such as logging, dependency management and configuration file management.
Plugins are compiled into .NET DLL files and put into `BepInEx/plugins` folder for BepInEx to load.

In this guide, we will create a simple BepInEx plugin that uses most important features provided by BepInEx.

> [!NOTE]
> This guide assumes basic knowledge of programming in C#.
> In addition, the APIs provided by Unity will not be covered in this guide.  
> For more information on how to use Unity's scripting API, refer to [Unity User Manual](https://docs.unity3d.com/Manual/index.html).

Making a BepInEx plugin consists of the following steps:

1. [Setting up the development environment](./1_setup.md)
2. [Creating plugin base](./2_plugin_start.md)
3. (Optional) [Reading and writing configuration files](./3_configuration.md)
4. (Optional) [Using loggers to simplify debugging](./4_logging.md)
5. WIP: Packaging and distributing the plugin
