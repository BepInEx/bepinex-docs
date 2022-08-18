# Installing BepInEx on Il2Cpp Unity

> [!IMPORTANT]
> Builds for this guide are only available via [Bleeding Edge](https://builds.bepinex.dev/projects/bepinex_be) builds!  

1. Download the correct version of BepInEx.

    [Download BepInEx from one of the available sources.](index.md#where-to-download-bepinex)  

    Pick a version depending on your OS:
    # [Windows](#tab/tabid-win)
    Download one of the following versions:

    * `Unity.IL2CPP-win-x86` for games with **32-bit executables**
    * `Unity.IL2CPP-win-x64` for games with **64-bit executables**

    # [Linux](#tab/tabid-linux)
    Download archive with designation `Unity.IL2CPP-linux-x64`.

    # [macOS](#tab/tabid-macos)
    Download archive with designation `Unity.IL2CPP-macos-x64`.
	
    ***

2. Extract the contents into the game root.

    After you have downloaded the correct game version, extract the archive contents into the game folder.

    # [Windows](#tab/tabid-win)
    The game root folder is where the game executable is located.

    # [Linux](#tab/tabid-linux)
    On Linux, the game root folder is where the executable `<Game>.x86` or 
    `<Game>.x86_64` is located.

    # [macOS](#tab/tabid-macos)

    On macOS, the root folder is where the game `<Game>.app` is located.
    ***

3. Do the first-time run to generate configuration files

    > [!NOTE]
    > First run in Il2Cpp games may take some time as it requires generating files necessary for modding.

    # [Windows](#tab/tabid-win)
    Run the game executable. This step should generate the BepInEx configuration file into the `BepInEx/config` folder and an initial log file `BepInEx/LogOutput.txt`.

    # [Linux](#tab/tabid-linux)
    > [!NOTE]
    > If you are modding a Steam game, you need to [configure Steam to run BepInEx](<xref:steam_interop>)
    
    First, open the included run script `run_bepinex.sh` in a text editor of your choice. Edit the line
    ```sh
    executable_name="";
    ```
    to be **the name of the game executable**. On Linux, this is simply the name of the game executable.

    Finally, open the terminal in the game folder and make the `run_bepinex.sh` script executable:
    ```bash
    chmod u+x run_bepinex.sh
    ```

    You can now run BepInEx by executing the run script:
    ```bash
    ./run_bepinex.sh
    ```
    This command should generate a BepInEx configuration file into the `BepInEx/config` folder and an initial log file `BepInEx/LogOutput.txt`.

    # [macOS](#tab/tabid-macos)
    > [!NOTE]
    > If you are modding a Steam game, you need to [configure Steam to run BepInEx](<xref:steam_interop>)
    
    First, open the included run script `run_bepinex.sh` in a text editor of your choice. Edit the line
    ```sh
    executable_name="";
    ```
    to be **the name of the game executable**. On macOS, this is the name of the game app **with** `.app` extension, for example, `HuniePop.app`.

    Finally, open the terminal in the game folder and make the `run_bepinex.sh` script executable:
    ```bash
    chmod u+x run_bepinex.sh
    ```

    You can now run BepInEx by executing the run script:
    ```bash
    ./run_bepinex.sh
    ```
    This command should generate a BepInEx configuration file into the `BepInEx/config` folder and an initial log file `BepInEx/LogOutput.txt`.

    ***

4. Configure BepInEx to suit your needs. 

   Open `BepInEx/config/BepInEx.cfg` in a text editor of your choice. 
   All options are documented directly in the configuration file.

   Additionally, refer to refer to the [configuration guide](<xref:configuration>) for more information.
