---
uid: troubleshooting
title: Troubleshooting
---

# Troubleshooting

This page outlines some specifics related to running BepInEx of various 
platforms and Unity versions.  
Whenever you have a problem starting up BepInEx, most commonly, it's either because of a missing core file or a wrong entry point. 

## Common

### Enable console

First of all, it's suggested to enable the console. That way, you'll see load progress and potential errors live.

Open `BepInEx/config/BepInEx.cfg`, locate and change the following settings accordingly:

```ini
[Logging.Console]

Enabled = true
```

### There is no BepInEx/config/BepInEx.cfg or the console doesn't open

This means either doorstop or the preloader has failed or did not run at all. Check the game root for preloader log files and continue reading this guide.

#### Check version of BepInEx and the bitness of the game

BepInEx has separate builds for x64 and x86 games, and separate builds for different operating systems. Make sure you have picked the correct build for your game.

The target operating system of BepInEx must be the same as the game's! It doesn't matter what OS you are actually running on! If you are running a Windows game on Linux, check the Advanced/Wine guide.

To see what bittness the game is, run the game and check it via Task Manager. If you see `(32 bit)` after the game process name like below, the game requires a **x86** build of BepInEx.
![ThomasWasAlone.exe (32 bit)](images/x86process_example.png)  

If you don't see such addition as below, the game requires **x64** build of BepInEx.
![Koikatu.exe](images/x64process_example.png)  

#### Extremely long paths with non-ASCII characters

Some versions of Mono bundled with Unity games cannot handle non-ASCII characters in paths or too long path names.
Because of that, it's suggested that:

* Your game executable path is not too long. Under 1024 will work on most systems, under 256 on all.
* Attempt to remove "exotic" characters from the game path. Make sure any of the game folders have only the following characters:
  * A-Z, a-z or numbers 0-9
  * Common punctuation (`.:;,!"#%&()[]{}=?*'_-`)

### Some characters are displayed incorrectly in console

If the application or your code is outputting non-ascii characters to the console or unity log, you may encounter some display issues.

#### Garbled text (mojibake)
<img width="451" height="33" alt="image" src="https://github.com/user-attachments/assets/348726a1-80b9-4f9e-91ff-3c0ae585c413" />

If you see something like the above in console it means that somewhere there are mismatched encodings.

Usually this is caused by a bug, so make sure you are on the latest version of BepInEx first.
You can attempt to fix this by changing your system locale settings or by forcing the game to run under a specific locale with some tool.

#### Unknown characters
<img width="445" height="41" alt="image" src="https://github.com/user-attachments/assets/75174ac4-fbfe-4342-8f0d-d33d8bd47684" />

If you instead see the above, where all of the characters are replaced by a placeholder, it means that you your console font is missing those characters.

To fix this you have to change the console font to one that contains the required characters.
Right click on the console title bar and go to Properties, then pick a different font from the font list and apply.
Once you find a font that has the required characters they should start displaying correctly. 

These settings will only last until the console window is closed. If you want this font to be used by default, right click on title bar and then "Defaults" and set it there.

### Preloader runs without errors but chainloader doesn't start / plugins are not loaded

This can happen if the chosen entry point is never called by the game. Edit `BepInEx.cfg` and try setting a different entry point under `[Preloader.Entrypoint]`.
You can pick any constructor or method in the game assemblies, e.g. `Camera..cctor`.

Note that different entry points will have different side effects, and generally the earlier is is called the better.

### Plugins are loaded without errors but do not work afterwards

This can happen if the BepInEx Manager GameObject is destroyed by the game. Try setting `HideManagerGameObject = true` in `BepInEx.cfg`.

### If the game was modded before BepInEx, restore original `Managed` folder

Suppose you're upgrading from an old version of BepInEx v4 and below, or you used a different modding framework before.
In that case, there might be some incompatible DLLs installed into the game's `Managed` folder.  

If the game is on Steam, go to `<Game Folder>\<Game Name>_Data` folder and delete `Managed` folder.
Finally, go to Steam and [verify game integrity](https://support.steampowered.com/kb_article.php?ref=2037-QEUH-3335).   
This procedure will cause Steam to redownload a clean copy of `Managed` folder.

If the game is not on Steam, you can try obtaining the clean `Managed` folder or reinstall the game altogether.

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

### Harmony backend

On Unity versions 2017 and newer (especially 2018), Harmony and MonoMod.RuntimeDetour may error when trying to patch anything. Here's an example error message:

```
[Error  : Unity Log] NotImplementedException: Derived classes must implement it
Stack trace:
System.Reflection.Module.get_Assembly () (at <e1319b7195c343e79b385cd3aa43f5dc>:0)
MonoMod.Utils._DMDEmit.Generate (MonoMod.Utils.DynamicMethodDefinition dmd, System.Reflection.MethodBase _mb, System.Reflection.Emit.ILGenerator il) (at <041d70ff506f4f089a67adab0245e45d>:0)
MonoMod.Utils.DMDEmitMethodBuilderGenerator.GenerateMethodBuilder (MonoMod.Utils.DynamicMethodDefinition dmd, System.Reflection.Emit.TypeBuilder typeBuilder) (at <041d70ff506f4f089a67adab0245e45d>:0)
MonoMod.Utils.DMDEmitMethodBuilderGenerator._Generate (MonoMod.Utils.DynamicMethodDefinition dmd, System.Object context) (at <041d70ff506f4f089a67adab0245e45d>:0)
...
```

This is due to the System.Runtime.Emit implementation in the version of Mono that is bundled with the game being incomplete. This can be fixed by setting the `Preloader.HarmonyBackend` setting to `cecil`, as such:

```ini
[Preloader]

## Specifies which MonoMod backend to use for Harmony patches. Auto uses the best available backend.
## This setting should only be used for development purposes (e.g. debugging in dnSpy). Other code might override this setting.
# Setting type: MonoModBackend
# Default value: auto
# Acceptable values: auto, dynamicmethod, methodbuilder, cecil
HarmonyBackend = cecil
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
