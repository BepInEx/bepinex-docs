---
uid: preloader_patchers
title: Using preloader patchers
---

# Using preloader patchers

## Preface

As of version 4.0, BepInEx allows to write *preload-time patchers* that modify 
assemblies before the game loads them.  
While most plug-ins can use Harmony to do runtime patching, using preload-time 
patchers provides more fine control over how the assembly is patched.

It is still recommended that *you use Harmony wherever possible* because 
Harmony makes sure all patches are compatible with each other. Use Mono.Cecil 
only if something cannot be done by Harmony (more info below).

Note: The contract for preloader patchers has changed between BepInEx v5 and v6.

## Difference from runtime patchers

Because preload-time patchers are run *before* the assemblies are loaded into 
memory, the patchers have more fine-grained control over how to modify the 
assemblies.

| Feature                                     | Preload-time patcher                                  | Runtime patcher                                  |
| ------------------------------------------- | ----------------------------------------------------- | ------------------------------------------------ |
| **Used library**                            | Mono.Cecil                                            | Harmony                                          |
| **Used contract**                           | Written in a separate DLL, uses a special contract    | Written in plug-in DLL, uses Harmony's API       |
| **Application time**                        | Applied on raw assemblies before the game initializes | Applied on assemblies already loaded in memory   |
| **Can apply hooks**                         | Yes                                                   | Yes, as long as the target is not inlined by JIT |
| **Can reference game assembly directly**    | No                                                    | Yes                                              |
| **Can rewrite methods' IL**                 | Yes                                                   | Yes                                              |
| **Can modify field/method propeties**       | Everything                                            | Partially                                        |
| **Can add new classes, methods and fields** | Yes                                                   | No                                               |
| **Can replace assemblies**                  | Yes                                                   | No                                               |

Thus, use preload-time patchers only if you must modify the structure of 
the assembly. For hooking methods use Harmony.

> [!WARNING]
> Preloader-time patching comes with its own caveats! 
> Refer to the [notes below for more information](#notes-and-tips).

## Writing a patcher

### Requirements

Assuming you know how to use an IDE of your choice, you will need to

* Create an assembly (DLL) project targeting the same .NET version as regular plugins for your game
* Remove references to all unused imports
* Add a reference to Mono.Cecil (use 0.10.3 for Unity Mono, otherwise the latest version). You can get it on NuGet, for instance, 
  or use the one prepackaged with BepInEx
* Add one or more patcher classes (example below)

### Patcher plugin

A patcher plugin's skeleton is similar to a regular plugin:

```csharp
[PatcherPluginInfo("io.bepis.mytestplugin", "My Test Plugin", "1.0")]
class EntrypointPatcher : BasePatcher
{
    public override void Initialize() { }

    public override void Finalizer() { }

    ...
}
```

Notable things:
* Instead of using `[BepInPlugin]`, you use `[PatcherPluginInfo]` instead.
* The base class is `BasePatcher`.
* There are two methods you can override related to the patching engine lifecycle.
* There is no constructor (or if there is, it has no parameters).
* Patches are declared as additional methods (see below).

You have access to the same base properties that regular plugins do; i.e. `Log`, `Config` and and `Info`. You also have access to `Context`, which is an object that contains the current information that the assembly patcher engine within BepInEx is currently using. For example, you can use it to find out which other patcher plugins are loaded, which assemblies can be patched, which patches have already been applied etc.

**Note that your patcher plugin GUID must be unique, even against regular plugins!** Because patcher plugins have their own configuration files now, they must also have a unique GUID so that there aren't any conflicts when loading / saving configuration settings.

### Lifecycle

This is the lifecycle of the patcher engine within BepInEx:

1. All `.dll` files within `BepInEx/patchers` are examined to see if they contain any patcher plugins. The ones that do are loaded as assemblies.
2. Every discovered patcher plugin is instantiated once (by calling the constructor).
3. All patcher plugins have their `Initialize()` function called.
4. Every patching method within each patcher plugin is executed, against the targeted type / assembly. Any unhandled exceptions are logged.
5. All patcher plugins have their `Finalizer()` function called.
6. Patcher engine unloads all loaded `AssemblyDefinition` and `TypeDefinition` objects.

Use your `Initialize` method for code that needs to run first exactly once, and your `Finalizer` method for code that needs to run last exactly once.

### Patch methods

Patch methods are much more declarative now, very similar to declaring Harmony patches. Here is an example declaration:

```csharp
[TargetAssembly("Assembly-CSharp.dll")]
public void PatchAssembly(AssemblyDefinition assembly)
{
    ...
}
```

You can target assemblies, or specific types (detailed below).

Patch methods must not be static or abstract. They can be any visibility, however.

They can have `void` or `bool` as a return type. In the case of `bool`, the return value specifies if the targeted assembly or type has been modified by the patcher. This is important, because if you tell BepInEx that the patch method hasn't actually patched anything, then it won't mark the assembly / types you've requested as modified. With a `void` return type, BepInEx will always assume that you have performed modifications.

If you have an `AssemblyDefinition` as the first parameter, then you can also define it as `ref` if you wish to replace it with another definition entirely. This is useful if you want to replace an assembly with another one you have shipped yourself, for example.

You can also provide a second `string` parameter, which will contain the (relative) filename of the assembly. If you are targeting a type, then it will return the filename of the assembly that the type belongs to.

For patch methods that target assemblies, you can specify multiple assemblies:

```csharp
[TargetAssembly("Assembly-CSharp.dll")]
[TargetAssembly("UnityEngine.dll")]
public void PatchAssembly(AssemblyDefinition assembly, string filename)
{
    ...
}
```

Which will then run that patch method twice, once for each assembly. There is also the option of specifying all available assemblies:

```csharp
[TargetAssembly(TargetAssemblyAttribute.AllAssemblies)]
public void PatchAssembly(AssemblyDefinition assembly, string filename)
{
    ...
}
```

As stated above you also have the option of specifying specific types. For example:

```csharp
[TargetType("Assembly-CSharp.dll", "GameNamespace.GameClass")]
public void PatchAssembly(TypeDefinition type)
{
    ...
}
```

The first parameter of the attribute is the filename of the assembly where the type belongs, and the second parameter is the full name of the type you wish to patch (including namespaces).

* You're able to specify additional `[TargetType]` attributes to specify more types to run the patcher for, however you **cannot** mix-and-match `[TargetType]` and `[TargetAssembly]`.
* You're also able to specify an additional `string` parameter for the assembly filename, however you cannot specify the first parameter as `ref`.

## Notes and tips

* **Do not reference any DLLs that you will want to patch!** Doing so will 
  load them into memory prematurely, which will make patching impossible!
* **Do not mix plug-in DLL with patcher DLL!** Plugins often reference 
  assemblies that must be patched, which will cause the assemblies to be 
  loaded prematurely.
* You cannot patch some assemblies, as they are required for the assembly patcher to execute.
  The list of assemblies that cannot be patched are (BepInEx 6.0): `mscorlib.dll`, `System.dll`, `System.Core.dll`.
  Either use Harmony or edit these assemblies permanently.
