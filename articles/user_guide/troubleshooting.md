---
uid: troubleshooting
title: Troubleshooting
---

This page outlines some specifics related to running BepInEx of various 
platforms and Unity versions.  
Whenever you have a problem starting up BepInEx, most commonly, it's either because of a missing core file or a wrong entry point. 

## Common

### Enable console

In many cases, it's suggested to enable the console. That way, you'll see load progress and potential errors live.

Open `BepInEx/config/BepInEx.cfg`, locate and change the following settings accordingly:

```ini
[Logging.Console]

Enabled = true
```

### Remove `Managed` folder and verify files

Suppose you're upgrading from an older version of BepInEx or a different modding framework.
In that case, there might be some incompatible DLLs installed into the game's `Managed` folder.  

If the game is on Steam, go to `<Game Folder>\<Game Name>_Data` folder and delete `Managed` folder.
Finally, go to Steam and [verify game integrity](https://support.steampowered.com/kb_article.php?ref=2037-QEUH-3335).   
This procedure will cause Steam to redownload a clean copy of `Managed` folder.

If the game is not on Steam, you can try obtaining the clean `Managed` folder 
or reinstall the game altogether.

### (Windows) Check the bitness of the game

Currently, Windows builds of BepInEx ship separately for x64 and x86 games.  
Because of that, make sure the version of BepInEx is for the correct architecture. 

To do that, run the game and check it via Task Manager.  
If you see `(32 bit)` after the game process name:  
![ThomasWasAlone.exe (32 bit)](images/x86process_example.png)  
the game requires **x86** build of BepInEx.

If you don't see such addition:  
![Koikatu.exe](images/x64process_example.png)  
the game requires **x64** build of BepInEx.

### Extremely long paths with non-ASCII characters

Some versions of Mono bundled with Unity games cannot handle non-ASCII characters in paths or too long path names.
Because of that, it's suggested that

* Your game executable path is not too long. Under 1024 will work on most systems, under 256 on all.
* Attempt to remove "exotic" characters from the game path. Make sure any of the game folders have only the following characters:
  * A-Z, a-z or numbers 0-9
  * Common punctuation (`.:;,!"#%&()[]{}=?*'_-`)

## Unity 2017 and newer

### Change the entry point

In some games, the default entry point is too early for BepInEx to load up properly.
For that, try an alternative entry point:

Open `BepInEx/config/BepInEx.cfg`, locate and change the following settings accordingly:

```ini
[Preloader.Entrypoint]

Assembly = UnityEngine.CoreModule.dll

Type = MonoBehaviour

Method = .cctor
```

## Unity 5 and older

### Change the entry point

In some games, the default entry point is too early for BepInEx to load up properly.
For that, try an alternative entry point:

Open `BepInEx/config/BepInEx.cfg`, locate and change the following settings accordingly:

```ini
[Preloader.Entrypoint]

Assembly = UnityEngine.dll

Type = MonoBehaviour

Method = .cctor
```

In some cases, another option works better

```ini
[Preloader.Entrypoint]

Assembly = UnityEngine.dll

Type = Camera

Method = .cctor
```

Future versions of BepInEx should automate the process of setting an early enough entry point.

## Unity 4 and older

### Ensure core libraries are included

Some older Unity games strip away unused core libraries. Specifically, BepInEx 
requires the following two libraries to be bundled

* `System.dll`
* `System.Core.dll`

Ensure they have been included in the `<Game Name>_Data/Managed` folder of your game.  
If not, you have to obtain such libraries yourself *at the moment*.

1. Head to [Unity download archive](https://unity3d.com/get-unity/download/archive)
2. Find an old version of Unity. 5.0.0 is suggested as a fitting middle ground
3. Download its Unity Editor and install it
4. Go to `<unity-install-dir>\Editor\Data\PlaybackEngines\windowsstandalonesupport\Variations\win32_development_mono` where `<unity-install-dir>` is the directory where you installed Unity to
5. In the folder, locate `System.Core.dll` (should be in `Data\Managed`) and copy it to your game's `Managed` folder
6. Try rerunning the game. BepInEx should now launch

### Rename `winhttp.dll` to `version.dll`

While `winhttp.dll` proxy works best on more platforms (especially older versions of Wine on Linux), older Unity games might not work correctly with it.  

Try renaming `winhttp.dll` that comes with BepInEx to `version.dll` and run the game.