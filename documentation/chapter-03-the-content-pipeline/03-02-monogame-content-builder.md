> [!CAUTION]
> THIS DOCUMENT IS CURRENTLY BEING REFACTORED


# 3-1: MonoGame Content Builder

The *MonoGame Content Builder* is a tool that compiles the game assets added to your content project into *.xnb* binary encoded files that can then be loaded at runtime in game using the *ContentManager class*.  For it to know which assets to compile, it reads the contents of the *Content.mgcb* file located by default in the */Content/* directory in the game project. The *Content.mgcb* file itself looks similar to the following

```text

#----------------------------- Global Properties ----------------------------#

/outputDir:bin/$(Platform)
/intermediateDir:obj/$(Platform)
/platform:DesktopGL
/config:
/profile:Reach
/compress:False

#-------------------------------- References --------------------------------#


#---------------------------------- Content ---------------------------------#

#begin image.png
/importer:TextureImporter
/processor:TextureProcessor
/processorParam:ColorKeyColor=255,0,255,255
/processorParam:ColorKeyEnabled=True
/processorParam:GenerateMipmaps=False
/processorParam:PremultiplyAlpha=True
/processorParam:ResizeToPowerOfTwo=False
/processorParam:MakeSquare=False
/processorParam:TextureFormat=Color
/build:image.png
```

In the above example, we can see that the *Content.mgcb* file is broken down into three sections, *Global Properties*, *References*, and *Content*.  The *Global Properties* section defines the flags used when calling the MGCB that will affect all content built such as the directory to output the compiled assets to and which platform is being targeted.  The *References* section would contain the path to an third party content pipeline extension dll references used to allow the MGCB to build custom content.  Finally, the *Content* section defines the content files to be built, and the flags to use when building it.  Each content item will have an */importer* and a */processor* to define what the MGCB should use for that item to import and process it.  The */processorParam* flags depend on which processor is being used.  Above, we are telling it to import *image.png* using the *TextureImporter* and *TextureProcessor*, so the processor params defined are those for textures.

> [!TIP]
> When the *Content.mgcb* file is read, each of the lines that begin with a `/` are passed as arguments to the MGCB process.  This is essentially what the schema of this file defines, the arguments to use.  For instance, without using the *Content.mgcb* file, if you wanted to call the MGCB process directly using the same configuration as above it would look like the following.
> 
> ```sh
> dotnet mgcb /outputDir:bin/$(Platform) /intermediateDir:obj/$(Platform) /platform:DesktopGL /config: /profile:Reach /compress:False /importer:TextureImporter /processor:TextureProcessor /processorParam:ColorKeyColor=255,0,255,255 /processorParam:ColorKeyEnabled=True /processorParam:GenerateMipmaps=False /processorParam:PremultiplyAlpha=True /processorParam:ResizeToPowerOfTwo=False/processorParam:MakeSquare=False /processorParam:TextureFormat=Color /build:image.png
> ```
> Thankfully, we instead have the *Content.mgcb* file to define this and make it more readable than a long cli command.

However, editing this file manually for every asset you want to add to the game through the content pipeline would overwhelming.  Instead, MonoGame provides the *MonoGame Content Builder Editor (MGCB)* editor to assist in editing this file.

---

<div align="right"><table border=1><tr><td>Next Up</td></tr><tr><td>

[3-2: MonoGame Content Builder Editor](./03-02-monogame-content-builder-editor.md)

</td></tr></table></div>
