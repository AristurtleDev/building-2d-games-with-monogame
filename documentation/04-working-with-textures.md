# Chapter 04: Working with Textures

---

All content you would us in your game, such as graphics and audio, can be loaded in MonoGame through the *content pipeline*.  The *content pipeline* isn't one single thing, instead it's a set of tools and utilities provided by the MonoGame framework to

1. Convert assets to an internal format that is optimized for the platform(s) your game is targeting.
2. Copy the compiled assets to your game project's build folder.
3. Load the compiled assets at runtime.

The *content pipeline* is nt a requirement to use; assets can be loaded at runtime in your game directly from their raw file format.  Doing this, however, removes the benefits you get from pre-processing them.  For instance, when an image is loaded as a texture in your game, the data for that image has to be sent to the GPU and stored in memory there.  The GPU doesn't understand formats like PNG and JPEG, so instead it has to be decompressed from those formats into raw bytes as a format the GPU understands.  Using the *content pipeline* to pre-process image files compiles them to a format that is understood by the GPU on the target platform(s).  For instance, on a desktop platform, an image can be compressed using DXT compression](https://en.wikipedia.org/wiki/S3_Texture_Compression), and the GPU understands this compressed format without having to decompress it first, reducing the overall memory footprint.



