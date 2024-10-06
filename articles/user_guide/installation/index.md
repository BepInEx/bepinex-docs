---
uid: installation
---

# Installing BepInEx

## Requirements

* *Supported Operating Systems*
    - Windows 7, 8, 8.1 and 10 (both x86 and x64 are supported)
    - Linux distros with GCC 10 or newer, preferably GNU/Linux distro (x86_64 and x86 archs are supported)
    - macOS 10.13 High Sierra or newer
    - Other OSes for which usage via [hardpatching](hardpatching.md) is possible
      - Any OS that has support for [Wine](https://www.winehq.org/) (Linux, macOS, FreeBSD, ...)
      - Any OS that has support for [Mono](https://www.mono-project.com/) (Windows, Linux, macOS)
* *Supported Unity games*
    - Unity 3 or newer

> [!IMPORTANT]
> Games built with IL2CPP are not supported at the moment.
> However, support for it is planned as the tooling has gotten better thanks to projects like [Il2CppAssemblyUnhollower](https://github.com/knah/Il2CppAssemblyUnhollower).  

## Where to download BepInEx

BepInEx is distributed in two builds: *stable* and *bleeding edge*.

**Stable builds** are available on [GitHub](https://github.com/BepInEx/BepInEx/releases).  
Stable builds are released once a new iteration of BepInEx is considered feature-complete.  
They have the least bugs, but some newest features might not be available.  
**It is recommended to use stable builds in most cases.**

**Bleeding edge builds** are available on [BepisBuilds](https://builds.bepis.io/projects/bepinex_be).
Bleeding edge builds are always the latest builds of the source code. Thus they are the opposite to stable builds: they have the newest features and bugfixes available, but usually tend to be the most buggy.  
Therefore you should use bleeding edge builds only if you are asked to or if you want to preview the upcoming version of BepInEx.


## Installing BepInEx

Currently, BepInEx can be installed manually.

1. Download the correct version of BepInEx.

    [Download BepInEx from one of the available sources.](#where-to-download-bepinex)  
    Pick a version depending on your OS:
    # [Windows](#tab/tabid-win)
    Download one of the following versions:
    * `x86` for games with **32-bit executables**
    * `x64` for games with **64-bit executables**

    # [Linux/macOS](#tab/tabid-nix)
    Download archive with designation `nix`. The archive contains everything needed
    to run BepInEx on both 32- and 64-bit executables.
    ***

2. Extract the contents into the game root.

    After you have downloaded the correct game version, extact the contents of 
    the archive into the game folder.
    # [Windows](#tab/tabid-win)
    The game root folder is where the game executable is located.

    # [Linux/macOS](#tab/tabid-nix)
    On Linux, the game root folder is where the executable `<Game>.x86` or 
    `<Game>.x86_64` is located.

    On macOS, the root folder is where the game `<Game>.app` is located.
    ***

3. Do a first run to generate configuration files

    # [Windows](#tab/tabid-win)
    Simply run the game executable. This should generate BepInEx configuration 
    file into `BepInEx/config` folder and an initial log file `BepInEx/LogOutput.log`.

    # [Linux/macOS](#tab/tabid-nix)
    > [!NOTE]
    > If you are modding a Steam game, you need to [configure Steam to run BepInEx](<xref:steam_interop>)
    
    First, open the included run script `run_bepinex.sh` in a text editor of 
    your choice. Edit the line
    ```sh
    executable_name="";
    ```
    to be **the name of the game executable**:
    
    * On Linux, this is simply the name of the game executable
    * On macOS, this is the name of the game app **with** `.app` extension, for example `HuniePop.app`

    Finally, open the terminal in the game folder and make `run_bepinex.sh` script 
    executable:
    ```bash
    chmod u+x run_bepinex.sh
    ```

    You can now run BepInEx by executing the run script:
    ```bash
    ./run_bepinex.sh
    ```
    This should generate BepInEx configuration 
    file into `BepInEx/config` folder and an initial log file `BepInEx/LogOutput.log`.
    ***
    
4. Configure BepInEx to suit your needs. 

   Open `BepInEx/config/BepInEx.cfg` in a text editor of your choice. 
   All options are documented directly in the configuration file.

   Additionally, refer to refer to the [configuration guide](<xref:configuration>) for more information.

## Further steps and troubleshooting

Some games require some additional changes in order to work around specific 
limitations of different Unity versions. 

Please refer to the 
[troubleshooting](<xref:troubleshooting>) section for more information about 
additional installation steps.
