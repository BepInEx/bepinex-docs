---
uid: configuration
title: Configuration
---

# Configuration

BepInEx 5 contains all configuration files in `BepInEx/config` folder.  
All configuration files have `.cfg` extension and follow 
[TOML](https://github.com/toml-lang/toml)-like syntax.

## Configuring BepInEx

The main BepInEx configuration is located in `BepInEx/config/BepInEx.cfg`.  
If you don't have the file, run the game with BepInEx at least once and BepInEx 
with automatically generate the file.

Open the file in any text editor of your choice. All configuration options 
are documented.

## Configuring plugins

Most plugins have their configuration options in `BepInEx/config` folder.  
The configuration files are named by the GUID of the plugin.  
Options are usually documented, but that depends on the plugin developer.

It is suggested to download and use [BepInEx.ConfigurationManager](https://github.com/BepInEx/BepInEx.ConfigurationManager) 
which provides a simple, in-game UI for editing the plugin configuration.