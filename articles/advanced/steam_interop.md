---
uid: steam_interop
title: Running native Unix games through Steam
---

## Running native Unix games through Steam

To make a native game work with BepInEx you need to run it through a script, 
which can cause issues with Steam since it will want to run the game executable 
directly and can restart the game if you try to run it from outside of Steam, 
preventing BepInEx from being used.

Thankfully, Steam provides a way to run launch scripts directly before running 
the original game. This guide will use this feature to fix the above issue.

The process is similar for both Linux and macOS, but with one key difference.

### 1. Download and install BepInEx

First, download and install BepInEx binaries if you hadn't done so.  
For that, follow steps 1-2 in the [installation guide](<xref:installation>#installing-bepinex)

> [!TIP]
> To easily find the game folder of a Steam game, go into properties of the game:  
> ![Right-click the game and press Properties](images/steam_props.png)  
>  
> And Select `Browse local files` from `Local files` tab:  
> ![Click Browse local files to open the game folder](images/steam_local_files.png)

> [!NOTE]
> Don't run the script yet as it will run the game without Steam integration.
> You don't need to configure the script either, as it will be done by Steam.

### 2. Set up permissions

On Unix systems, you first need to give the run script permission to run.  
At this moment it has to be done manually.

Open the game folder in terminal and add execution permission to run script:

```sh
chmod u+x run_bepinex.sh
```

This will add needed permissions to run BepInEx.

### 3. Configure Steam to run the script

Finally, configure Steam to run the script.  
Open the game's properties on Steam:

![Open game properties on Steam by right-clicking the game name](images/steam_props.png)

Next, click `Set launch options` button which will open a new window:

![Click Set launch options to set launch options](images/steam_launch_opts.png)

Now, change the launch options depending on your OS:

#### [Linux](#tab/tabid-1)
Set the launch option to
```
./run_bepinex.sh %command%
```

#### [macOS](#tab/tabid-2)
First, open a terminal in the game folder and run
```sh
pwd
```
This will print the full path to the game folder. Copy it.

Next, set launch option to
```
"<PWD>/run_bepinex.sh" %command%
```
**where `<PWD>` is the full path to the game folder you got above.**
***

### 4. Run first time to generate configuration

Finally, run the game via Steam normally. 
This will generate BepInEx config, but the game might not run.

### 5. Configure BepInEx to suit your needs.

Open `BepInEx/config/BepInEx.cfg` in a text editor of your choice. 
All options are documented directly in the configuration file.

Additionally, refer to refer to the [configuration guide](<xref:configuration>) for more information.
