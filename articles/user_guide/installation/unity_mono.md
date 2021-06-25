# Installing BepInEx on Mono Unity

1. Download the correct version of BepInEx.

    [Download BepInEx from one of the available sources.](index.md#where-to-download-bepinex)  
    Pick a version depending on your OS:
    # [Windows](#tab/tabid-win)
    Download one of the following versions:
    * `UnityMono_x86` for games with **32-bit executables**
    * `UnityMono_x64` for games with **64-bit executables**

    # [Linux/macOS](#tab/tabid-nix)
    Download archive with designation `UnityMono_nix`. The archive contains everything needed to run BepInEx on both 32- and 64-bit executables.
	
    ***

2. Extract the contents into the game root.

    After you have downloaded the correct game version, extract the archive contents into the game folder.
    # [Windows](#tab/tabid-win)
    The game root folder is where the game executable is located.

    # [Linux/macOS](#tab/tabid-nix)
    On Linux, the game root folder is where the executable `<Game>.x86` or 
    `<Game>.x86_64` is located.

    On macOS, the root folder is where the game `<Game>.app` is located.
    ***

3. Do the first-time run to generate configuration files

    # [Windows](#tab/tabid-win)
    Run the game executable. This step should generate the BepInEx configuration file into the `BepInEx/config` folder and an initial log file `BepInEx/LogOutput.txt`.

    # [Linux/macOS](#tab/tabid-nix)
    > [!NOTE]
    > If you are modding a Steam game, you need to [configure Steam to run BepInEx](<xref:steam_interop>)
    
    First, open the included run script `run_bepinex.sh` in a text editor of your choice. Edit the line
    ```sh
    executable_name="";
    ```
    to be **the name of the game executable**:
    
    * On Linux, this is simply the name of the game executable
    * On macOS, this is the name of the game app **with** `.app` extension, for example, `HuniePop.app`.

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