---
title: Hardpatching with BepInEx.Patcher
---

# Hardpatching with BepInEx.Patcher

> [!IMPORTANT]
> The hardpatcher permanently edits the game assembly to inject BepInEx into it.  
> Use it **only if the normal installation methods don't work**!

## Differences between proxy and hardpatcher

The hardpatcher provides mainly the following benefits:

* Works on any system with any security settings
* Works when normal proxy entrypoint doesn't

Meanwhile hardpatcher comes with these downsides

* You'll have to reapply it on game update or reinstall
* Uninstalling requires removing and reinstalling game files
* Preloader patching is not available

## Installing hardpatch


1. Download the latest hardpatcher and BepInEx core from [BepisBuilds](https://builds.bepis.io/projects/bepinex_be)
    * The hardpatcher archive name starts with `BepInEx_Patcher`
    * The core archive name starts with `BepInEx_x64`
2. Extract the core and patcher into the game folder
3. Remove unnecessary core files
    * `winhttp.dll`
    * `doorstop_config.ini`
4. Run the hardpatcher and wait until it finishes running
5. Run the game