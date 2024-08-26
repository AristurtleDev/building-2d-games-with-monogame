# Chapter 2-2: The Content Project File

- [Global Properties Section](#global-properties-section)
- [References Section](#references-section)
- [Content Section](#content-section)

---

The *Content.mgcb* content project file, located in the */Content/* directory in the project root directory, defines the assets and configurations to use when compiling assets using the **MonoGame Content Builder**. Each line is actually a flag that is passed to the **MonoGame Content Builder** executable to build the content.

```sh
#----------------------------- Global Properties ----------------------------#

/outputDir:bin/$(Platform)
/intermediateDir:obj/$(Platform)
/platform:DesktopGL
/config:
/profile:Reach
/compress:False

#-------------------------------- References --------------------------------#


#---------------------------------- Content ---------------------------------#
```

This is not a file you would typically edit manually.  Instead you would load this file inside the **MonoGame Content Builder Editor**, which provides a visual interface for adding and managing assets which will then write the appropriate configurations to this file for you. We'll cover using the **MonoGame Content Builder Editor** in [Chapter 03: The Content Pipeline](../chapter-03-the-content-pipeline/03-00-the-content-pipeline.md).

However, it can still be useful to know how to read this file, so we'll cover it briefly below.

## Global Properties Section
The global properties section defines configurations used by the **MonoGame Content Builder** when building the assets defined in the content section of this file.

- `/outputDir` specifies the directory where the compile content is output to.  The `$(Platform)` variable is replaced by the value used in the `/platform` flag.
- `/intermediateDir` specifies the directory where intermediate files are written to during a build.  The `$(Platform)` variable is replaced by the value used in the `/platform` flag.
- `/platform` specifies the target platform that content is being built for so that hte content can be optimized for that platform.  Available values are
  - `Android`
  - `DesktopGL`
  - `iOS`
  - `PlayStation4`
  - `PlayStation5`
  - `Switch`
  - `Windows`
  - `WindowsStoreApp`
  - `XBoxOne`

> [!NOTE]
> Support for the `PlayStation4`, `PlayStation5`, `Switch`, and `XBoxOne` platforms is only available for licensed console developers.

- `/config` is an optional flag that can be used to specify a build configuration name.  This value is sometimes used as a hint in content processors.
- `/profile` specifies the target graphics profile to build for.  Available values are
  - `HiDef`
  - `Reach`
- `/compress` specifies whether compiled content should be compressed with LZ4 compression.  If `True`, content build times will increases.  Enabling this is not recommended when targeting Android since the *.apk* application package is already compressed.

## References Section
The references section defines references to third party assemblies that contain content pipeline extensions for the **MonoGame Content Builder** to provide content importers and processors that are not part of the standard ones provided. The syntax for including a reference is as follows:

```sh
/reference:<assembly_path>
```
For each assembly referenced, a new `/reference` line would be added. The `<assembly_path>` can be either an absolute path or a relative path.  When it is a relative path, it is relative to the *Content.mgcb* file itself.

## Content Section
The content section is where each asset to be compiled is defined along with the configurations for importing and processing that asset. The following flags must be defined for each asset in the order shown.

```sh
/importer:<class_name>
/processor:<class_name>
/processorParam:<name>=<value>
/build:<content_filepath>;[destination_filepath]
``` 

- The `/importer` flag specifies which importer to use to when importing the file asset.
  - `<class_name>` defines class name of the importer.
- The `/processor` flag specifies which processor to use to process the content imported by the importer. 
  - `<class_name>` defines the class name of the processor.
- The `/processorParam` flag specifies a named parameter and the value to set for the parameter.
  - `<name>` is the name of the parameter
  - `<value>` is the value to set.  
  - The parameter names and values available depend on the processor being used. 
- The `/build` flag specifies the file path to the asset to import and process using the previously defined flags.
  - `<content_filepath>` is the path to the file and can be an absolute path or a relative path.  When it is a relative path, it is relative to the *Content.mgcb* file.
  - `[destination_filepath]` is optional and can be used to change the output path of the asset when built. This can be an absolute path or a relative path.  When it is a relative path, it is relative to the `/outputDir` path defined in the [Global Properties Section](#global-properties-section).


This is not a file you would typically edit by hand, [although you can if you want to learn the syntax](https://docs.monogame.net/articles/tools/mgcb.html).  Instead, you load this file in the *MonoGame Content Builder Editor* so that it can be edited visually with inside the UI application.

## See Also
- [MonoGame Content Builder](https://docs.monogame.net/articles/getting_started/tools/mgcb.html)
- [MonoGame Content Builder Editor](https://docs.monogame.net/articles/getting_started/tools/mgcb_editor.html)

## Next
- [2-3: The dotnet-tools Manifest File](./02-03-the-dotnet-tools-manifest-file.md)
