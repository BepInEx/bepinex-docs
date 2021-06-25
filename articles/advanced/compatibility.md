---
uid: compatibility
title: Compatibility
---

BepInEx allows for easy integration of other Unity modding frameworks. That way 
you can get the benefit of installing only one framework without worrying about 
potential compatibility issues. Moreover, by using BepInEx to power plugins 
from other frameworks you get the benefits of

* Easy installation: BepInEx installation is as simple as a drag-and-drop
* Support: BepInEx is actively maintained and supported while being fully open-source
* High compatibility: BepInEx strives to support as many platforms as it can as stable as it can

As of right now, BepInEx has loaders for the following frameworks and tools:

| **Framework/Loader**               | **BepInEx Loader**                                                            | **Status**                                 |
| ---------------------------------- | ----------------------------------------------------------------------------- | ------------------------------------------ |
| IPA (Illusion Plugin Architecture) | [IPALoaderX](https://github.com/BepInEx/IPALoaderX)                           | Stable, Maintained, Full interop           |
| BSIPA (Beat Saber IPA)             | [BepInEx.BSIPA.Loader](https://github.com/BepInEx/BepInEx.BSIPA.Loader)       | Stable, Maintained, Supports most features |
| Sybaris 2                          | [SybarisLoader](https://github.com/BepInEx/BepInEx.SybarisLoader.Patcher)     | Stable, Maintained, Full interop           |
| UnityInjector                      | [UnityInjectorLoader](https://github.com/BepInEx/BepInEx.UnityInjectorLoader) | Stable, Maintained, Full interop           |
| MonoMod Patches                    | [MonoModLoader](https://github.com/BepInEx/BepInEx.MonoMod.Loader)            | Stable, Maintained, Supports most featues  |
| Unity Mod Manger                   | [Yan.UMMLoader](https://github.com/hacknet-bar/Yan.UMMLoader)                 | WIP, Maintaned by community, Full interop  |