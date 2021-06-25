---
title: BepInEx Guide Index
---

# BepInEx Guide Index

Welcome to BepInEx documentation pages! Please refer to the navigation menu 
on the left for all specific guides!

## What is BepInEx

BepInEx is a .NET plugin framework aimed at Unity games running 
on [Mono](https://www.mono-project.com/) runtime.  
The main goal of BepInEx is to be *simple to install and use for end-users* 
and to *provide absolutely necessary tools for modding* all while being 
*small and easily portable* to as many different Unity games as possible.

While BepInEx is mainly aimed at PC games running on Windows, BepInEx can be 
installed on Linux, macOS and any other operating system that either has 
support for Mono or Windows emulation.

## What BepInEx is not

Currently BepInEx does not aim to be the solution for modding all games 
with .NET support on all platforms. This limitation allows BepInEx to be 
small and simple to install while still working on as many games as possible.

As of version 5.0, support for more general .NET games is being planned.

## Getting started with BepInEx

To start with BepInEx, you should [download and install it](<xref:installation>).  
Next, you might want to [configure it and any of the plugins you install](<xref:configuration>).

> [!NOTE]  
> While BepInEx should work with default configuration on the majority of Unity games,
> some games might require specific entry point configuration.
> Refer to [troubleshooting information](<xref:troubleshooting>) for info on 
> how to set up entrypoints in exceptional cases.

## Developing plugins

If you are a developer, you can check the 
[plugin creation walkthrough](<xref:plugin_dev_index>) to get acquainted 
with most of the API of BepInEx.
Additionally, you should check out 
[how to use Harmony to patch game methods](<xref:runtime_patching>) and 
[how to patch game assemblies with Cecil](<xref:preloader_patches>).

For more exact documentation on each of BepInEx's feature, consult the 
[API documentation](~/api/index.md)

Finally, the [advanced guides](<xref:advanced>) contain information on how to 
debug plugins with dnSpy and elaborate on technical details of BepInEx and 
Unity modding.