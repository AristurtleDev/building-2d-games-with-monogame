# 3-4: The ContentManager Class

- [Loading Assets](#loading-assets)
  - [ContentManager Cache](#contentmanager-cache)
- [Unload Assets](#unload-assets)
- [See Also](#see-also)
- [Next](#next)

---

The final part of the content pipeline is the [*ContentManager class*](https://docs.monogame.net/api/Microsoft.Xna.Framework.Content.ContentManager.html).  This class is used at runtime in your game to load the content that was compiled during the build process.  

The `Game` class initializes a new instance of the *ContentManager class* during its constructor and makes it available as one of the [inherited properties](../chapter-02-monogame-project-overview/02-06-the-game1-file.md#additional-properties).  

## Loading Assets
To load content using the *Content Manager class*, you use the [`ContentManager.Load<T>()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Content.ContentManager.html#Microsoft_Xna_Framework_Content_ContentManager_Load__1_System_String_).  The `T` type parameter specifies which type of content you are loading (e.g. `Texture2D`, `SpiteFont`, `SoundEffect`, etc).  Additionally, the method has a single parameter which is the **asset name** of the content to load.  

The **asset name** is the path to the content, minus the extension, relative to the **ContentManager.RootDirectory** path.  In the [`Game1` Constructor](../chapter-02-monogame-project-overview/02-06-the-game1-file.md#the-game1-constructor), you can see that the `ContentManager.RootDirectory` path is set to `"Content"` by default.  So if you had an asset to load that was in the directory */Content/Images/ball.png*, the **asset name** you would provide when loading it would be `Images/ball`.

Below are some examples of loading content using the *ContentManager class*:

```cs
Texture2D texture = Content.Load<Texture2D>("Graphics/image");
Song music = Content.Load<Song>("Music/background_music");
SoundEffect sfx = Content.Load<SoundEffect>("Audio/pickup_sfx");
```

### ContentManager Cache

When you load an asset for the first time using the `ContentManager`, it will cache the asset internally.  By caching the asset, any subsequent calls to load that same asset later will return the cached version instead of perform a full disk read and deserialization of the content.  

## Unload Assets

There may be times when you want to unload assets that were previously loaded by the `ContentManager`.  For instance, a common scenario is to have a `ContentManager` instance that is used to load global assets, and a separate instance used in a scene in your game that only loads assets used by that scene.  When that scene ends, you may want to unload the assets that were loaded during it since they will no longer be used, reducing your memory footprint.

To unload assets, the `ContentManager` provides three methods

```cs
//  Unloads a single asset with the asset name provided
Content.UnloadAsset(assetName);

//  Unloads multiple assets using the collection of asset names provided
Content.UnloadAssets(new List<string>() { asset1, asset2, asset3 });

//  Unloads all assets
Content.Unload();
```

## See Also
- [Adding the content in your game | MonoGame](https://docs.monogame.net/articles/getting_started/4_adding_content.html#adding-the-content-in-your-game)

## Next
- [Chapter 3-5: Conclusion](./03-04-the-contentmanager-class.md)
