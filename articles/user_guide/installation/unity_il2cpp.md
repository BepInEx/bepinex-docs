# Installing BepInEx on Il2Cpp Unity

> [!IMPORTANT]
> Currently Il2Cpp builds are only available via [Bleeding Edge](https://builds.bepis.io/projects/bepinex_be) builds!  
> Do not try using stable builds for Il2Cpp games.

> [!IMPORTANT]
> At the moment, Il2Cpp builds are available only for Windows and Wine.

1. Download the correct version of BepInEx.

    [Download BepInEx from one of the available sources.](index.md#where-to-download-bepinex)  
    
    Download one of the following versions:
    * `UnityIL2CPP_x86` for games with **32-bit executables**
    * `UnityIL2CPP_x64` for games with **64-bit executables**

2. Extract the contents into the game root.

    After you have downloaded the correct game version, extract the archive contents into the game folder.

    The game root folder is where the game executable is located.

3. Do the first-time run to generate configuration files

    Run the game executable. This step should generate the BepInEx configuration file into the `BepInEx/config` folder and an initial log file `BepInEx/LogOutput.txt`.

    > [!NOTE]
    > First run in Il2Cpp games may take some time as it requires generating files necessary for modding.
    
4. Configure BepInEx to suit your needs. 

   Open `BepInEx/config/BepInEx.cfg` in a text editor of your choice. 
   All options are documented directly in the configuration file.

   Additionally, refer to refer to the [configuration guide](<xref:configuration>) for more information.
