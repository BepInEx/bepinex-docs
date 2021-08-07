---
uid: preloader_patches
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

* Create an assembly (DLL) project targeting .NET 3.5
* Remove references to all unused imports
* Add a reference to Mono.Cecil 0.10 (you can get it on NuGet, for instance, 
  or use the one prepackaged with BepInEx)
* Add one or more patcher classes (example below)

### Patcher contract

BepInEx considers a patcher *any class* that has the following members:

* Property `public static IEnumerable<string> TargetDLLs { get; }` that 
  contains a list of assembly names (including the extension).
* Method `public static void Patch(AssemblyDefinition assembly)` that applies 
  the changes to the assembly itself.

Here is an example of a valid patcher:

```csharp
using System.Collections.Generic;
using Mono.Cecil;

public static class Patcher
{
    // List of assemblies to patch
    public static IEnumerable<string> TargetDLLs { get; } = new[] {"Assembly-CSharp.dll"};

    // Patches the assemblies
    public static void Patch(AssemblyDefinition assembly)
    {
        // Patcher code here
    }
}
```

### Specifying target DLLs

To specify which assemblies are to be patched, create a 
`public static IEnumerable<string> TargetDLLs` getter property.  

Note that `TargetDLLs` is enumerated during *patching*, not before. That means 
the following enumerator is valid:

```csharp
public static IEnumerable<string> TargetDLLs => GetDLLs();

public static IEnumerable<string> GetDLLs()
{
    // Do something before patching Assembly-CSharp.dll

    yield return "Assembly-CSharp.dll";

    // Do something after Assembly-CSharp has been patched, and before UnityEngine.dll has been patched

    yield return "UnityEngine.dll";

    // Do something after patching is done
}
```

### Patch method

A valid patcher method has **one** of the following signatures:

```csharp
public static void Patch(AssemblyDefinition assembly);
public static void Patch(ref AssemblyDefinition assembly);
```

In the latter case, the *reference* to the AssemblyDefinition is passed. That means it is possible to fully swap an assembly for a different one.

### Patcher initialiser and finaliser

In addition, the patchers are allowed to have the following methods:

```csharp
// Called before any patching occurs
public static void Initialize();

// Called after preloader has patched all assemblies and loaded them in
// At this point it is fine to reference patched assemblies
public static void Finish();
```

### Logging

BepInEx allows to either use the Standard Output (provided through `Console` 
class) or -- more fittingly -- the methods provided by [`System.Diagnostics.Trace`](https://msdn.microsoft.com/en-us/library/system.diagnostics.trace(v=vs.110).aspx) 
class.

With BepInEx 5 you can also use @BepInEx.Logging.Logger.CreateLogSource(System.String)
to use BepInEx's own logging system.

### Deploying and using

Build the project as a separate DLL from the plug-in. Place the DLL in 
`BepInEx/patchers` and run the game.

## Notes and tips

* **Do not reference any DLLs that you will want to patch!** Doing so will 
  load them into memory prematurely, which will make patching impossible!
* **Do not mix plug-in DLL with patcher DLL!** Plugins often reference 
  assemblies that must be patched, which will cause the assemblies to be 
  loaded prematurely.
* You cannot patch `mscorlib.dll`. In addition,the following assemblies cannot 
  be patched or replaced (BepInEx 4.0): `System.dll`, `System.Core.dll`. Either 
  use Harmony or edit these assemblies permanently.
* Because `TargetDLLs` is iterated only once, you can do initialization work 
  there (i.e. reading a configuration file).
    Note that you don't have to specify the target DLLs on compile time:
    ```csharp
    public static IEnumerable<string> TargetDLLs 
    { 
        get 
        {
            // Do whatever pre-patcher work...
            
            string[] assemblies = // Get asseblies dynamically (i.e from configuration file);
            return assemblies;
        } 
    }
    ```
* When you specify many target DLLs, you can change patching behaviour by 
  checking the assembly's name:
    ```csharp
    public static void Patch(AssemblyDefinition assembly)
    {
        if (assembly.Name.Name == "Assembly-CSharp")
        {
            // The assembly is Assembly-CSharp.dll
        }
        else if (assembly.Name.Name == "UnityEngine")
        {
            // The assembly is UnityEngine.dll
        }
    }
    ```
* You can use `Config` class provided by BepInEx to read and save configuration 
  options.
