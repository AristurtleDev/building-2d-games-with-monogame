# Chapter 3: The Content Pipeline

- [Why Use The Content Pipeline?](#why-use-the-content-pipeline)

---

What is a game without assets; images file uses for textures, audio files for sound effects and music, fonts for rendering text, 3D models, and shaders.  Without assets, well, we would just have a cornflower blue game window and not much else.  So how do you load assets into the game so the artwork you worked so hard on creating can be used to represent your player, and the music your best friend created can be played throughout the level?  

The MonoGame framework provides a workflow called **The Content Pipeline**.  The content pipeline is composed of different components that you work with during the development phase of the game, the build phase, and the runtime phase to take those assets and load them into your game.  These components are

1. The [**MonoGame Content Builder Editor (MGCB Editor)**](./03-01-monogame-content-builder-editor.md) tool used to edit the *Content.mgcb* content project file.
2. The [**MonoGame Content Builder (MGCB)**](./03-02-monogame-content-builder.md) tool which performs the compilation of the assets defined in the *Content.mgcb* content project file
3. The [**MonoGame.Content.Builder.Task**](./03-03-mongoame-content.builder.tasks.md) NuGet package reference which contains task to automate building the content and copying the compiled content to your projects output directory
4. The [**ContentManager class**](./03-04-the-contentmanager-class.md) use to load the compiled assets in game at runtime.

Notice that above I said the MonoGame framework *provides* this workflow.  It is not a requirement to use it, and there are other methods built into the framework to load your assets directly from file.  However, this isn't always the most optimal approach, let's explore why.

## Why Use The Content Pipeline?
When using the content pipeline, your assets are compiled into an optimized format for the platform you are targeting.  For instance, when an image is loaded as a texture, the data has to be sent to the  graphical processing unit (GPU) and stored in memory there.  The GPU doesn't know what to do with data formats like PNG, so instead, it has to be decompressed into raw bytes as a format the GPU understands.  For some platforms, like Desktop, this may not seem that big of a deal, but for mobile devices and consoles, you only have so much memory that can be dedicated for the GPU. 

Instead, if you use a workflow like the content pipeline to pre-process the image file, you can compile it into a format that is understood by the GPU.  For instance, for desktop platforms, image data can be compressed using [DXT compression](https://en.wikipedia.org/wiki/S3_Texture_Compression), which is a format that GPUs understand.  Now, when you load the image that was pre-processed, it can send the DXT compressed data to the GPU instead of having to unpack it and send the raw bytes, reducing the memory footprint.

Another benefit of using the content pipeline is through the [**ContentManager class**](https://docs.monogame.net/api/Microsoft.Xna.Framework.Content.ContentManager.html) in the MonoGame framework.  This class is used to load content that was compiled by the content pipeline at runtime in the game.  The content manager itself will cache the asset the first time it's loaded from disk, so any subsequent calls to load the asset will return the already read cached data instead of having to perform another disk read, decreasing load times within the game.

In the following sections, we'll explore the different components that make up the content pipeline workflow in MonoGame and how they all work together to provide an out-of-box content management system for your game.

## See Also
- [Why Use The Content Pipeline? | MonoGame](https://docs.monogame.net/articles/getting_started/content_pipeline/why_content_pipeline.html)

## Next
- [Chapter 3-1: MonoGame Content Builder Editor](./03-01-monogame-content-builder-editor.md)
