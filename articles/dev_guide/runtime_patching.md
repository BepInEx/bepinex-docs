---
uid: runtime_patching
title: Patching methods during runtime
---

# Patching methods during runtime

> [!NOTE]
> This guide is still WIP.

Runtime patching is the process of modifying methods without permanently 
patching them. Runtime patching happens while the game is running and on .NET 
can be done very extensively.

BepInEx ships with HarmonyX and MonoMod.RuntimeDetour to perform runtime patching. 
You can use either or both libraries -- both of them have different API but it 
does not matter which one you use.

## HarmonyX 

BepInEx uses [HarmonyX](https://github.com/BepInEx/HarmonyX) to perform runtime 
patching. HarmonyX is a fork of [Harmony](https://github.com/pardeike/Harmony) 
with changes to specifically allow interop with MonoMod.RuntimeDetour. 

HarmonyX is attribute-based, which means you define patches by applying attributes 
to a method.

Refer to the following guides on how to use HarmonyX:

* [HarmonyX wiki](https://github.com/BepInEx/HarmonyX/wiki) - gives basic examples and outlines differences from normal Harmony. Still WIP at the moment
* [Original Harmony wiki](https://harmony.pardeike.net/articles/intro.html) - HarmonyX API is similar to that of Harmony, so you can use the official wiki without much issue

## MonoMod.RuntimeDetour

[MonoMod.RuntimeDetour](https://github.com/MonoMod/MonoMod/blob/master/README-RuntimeDetour.md) 
is a helper library that allows to apply runtime patches as C# objects.

Alternatively, RuntimeDetour supports defining patches as events.

Some useful guides

* [Brief introduction into using RuntimeDetour](https://github.com/MonoMod/MonoMod/blob/master/README-RuntimeDetour.md)
* [Introduction to IL hooks](https://github.com/risk-of-thunder/R2Wiki/wiki/Working-with-IL)