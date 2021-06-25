---
title: Upgrading
---

# Upgrading

## Migration from previous versions of BepInEx

### Upgrading from 4.x

> [!IMPORTANT]
> pre-BepInEx 5 plugins are not compatible with BepInEx 5!  
> This guide only provides a temporary upgrade solution.  
> It is suggested that you do a clean install of BepInEx instead: remove all BepInEx files and start over fresh.

1. Delete `BepInEx/core` folder if it exists.
2. Download and install BepInEx 5 [according to the installation guide](<xref:installation>).
3. Download [latest version of `BepInEx.BepInEx4Upgrader`](https://github.com/BepInEx/BepInEx.BepInEx4Upgrader/releases) and place it in `BepInEx/patchers` folder.
4. Run the game normally.

### Upgrading from 3.x

To migrate from a previous version of BepInEx, do the following:

1. Delete `UnityEngine.dll`, `0Harmony.dll` and `BepInEx.dll` from the `*_Data\Managed` folder for your game
  
  > [!IMPORTANT]
  > Check **all** game folders for their respective `Managed` folders.
  > BepInEx 3 creates the files above for each valid Unity executable it finds, which means that you may have to repeat this process multiple times.

2. Rename `UnityEngine.dll.bak` to `UnityEngine.dll`
3. **Delete `BepInEx.Patcher.exe` from the game's root folder.**
4. Delete your `config.ini` file in your BepInEx folder
5. Follow the upgrading guide for 4.x

## Migrating from Sybaris 2.x

1. Delete **all occurrences** of the following DLLs in the game's folder:
    * `ExIni.dll`
    * `UnityInjector.dll`
    * `Mono.Cecil.dll`
    * `Sybaris.Loader.dll`
    * `COM3D2.UnityInjector.Patcher` (and other UnityInjector patchers)
    * `opengl32.dll`  
  Use Windows' search tool if you cannot find those.
2. [Install BepInEx 4 normally](<xref:installation>)
3. Download and install [UnityInjectorLoader](https://github.com/BepInEx/BepInEx.UnityInjectorLoader/releases) and [SybarisLoader](https://github.com/BepInEx/BepInEx.SybarisLoader.Patcher/releases) to enable UnityInjector and Sybaris compatibility