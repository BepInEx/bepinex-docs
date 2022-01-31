---
uid: installation
---

# Installing BepInEx

## Requirements

* *Supported Operating Systems*
    - Windows 7, 8, 8.1, and 10 (both x86 and x64 are supported)
    - Linux distros with GCC 10 or newer, preferably GNU/Linux distro (x86_64 and x86 archs are supported)
    - macOS 10.13 High Sierra or newer
* *Supported game engines*
    - Unity 3 or newer
    - XNA, FNA, MonoGame, and other engines running on .NET Framework

## Where to download BepInEx

Official BepInEx binaries are distributed in two variations: *stable builds* and *bleeding edge (BE) builds*.

**Stable builds** are available on [GitHub](https://github.com/BepInEx/BepInEx/releases).  
Stable builds are released once a new iteration of BepInEx is considered feature-complete.  
They may have only minor bugs, but some newest features might not be available.  
**It is recommended to use stable builds in most cases.**

**Bleeding edge builds** are available on [BepInBuilds](https://builds.bepinex.dev/projects/bepinex_be).
Bleeding edge builds are always the latest builds of the source code. Thus they are the opposite of stable builds: they have the newest features and bugfixes available but usually tend to be the most buggy.  
Therefore you should only use bleeding edge builds if you are asked to or if you want to preview the upcoming version of BepInEx.

There also exist *unofficial 3rd party* distributions often preconfigured and set up to work with specific games.


## Installing BepInEx

Currently, BepInEx can be installed manually.

BepInEx has separate binaries for different game engines. Refer to separate installation guides for the specific engine your game is using:

* Unity games using *mono* runtime: these games usually have a folder named `Managed` somewhere in their install
    * [Install guide for Mono Unity](unity_mono.md)
* Unity games using *Il2Cpp* runtime: these games usually have a folder named `il2cpp_data` somewhere in their install
    * [Install guide for Il2Cpp Unity](unity_il2cpp.md)
* .NET Framework games
    * [Install guide for .NET Framework games](net_fw.md)


## Further steps and troubleshooting

Some games require some additional changes to work around specific limitations of different Unity versions. 

Please refer to the [troubleshooting](<xref:troubleshooting>) section for more information about additional installation steps.
