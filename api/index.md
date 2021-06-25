---
uid: api
---

# BepInEx API documentation

This page contains documentation for BepInEx API.

## Main namespaces of BepInEx

### @BepInEx

Contains most commonly used API provided by BepInEx.

Important classes:

* @BepInEx.BaseUnityPlugin -- base class your plugins should inherit from.
* @BepInEx.BepInPlugin, @BepInEx.BepInDependency and @BepInEx.BepInProcess attributes -- used to specify important metadata about each plugin class.
* @BepInEx.Configuration -- access to configuration.
* @BepInEx.Paths -- all common file and directory locations BepInEx and plugins rely on.

### @BepInEx.Bootstrap

Contains internals of BepInEx plugin loader. Allows access to other loaded plugins.

### @BepInEx.Logging

All classes related to logging in BepInEx.