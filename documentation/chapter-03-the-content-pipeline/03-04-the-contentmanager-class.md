> [!CAUTION]
> THIS DOCUMENT IS CURRENTLY BEING REFACTORED


# 3-4: The ContentManager Class

The final part of the content pipeline workflow is the *ContentManager class* used in the game code to load the content at runtime. The `Game` class initializes a new instance of the *ContentManager class* [when the constructor is called](./05_the_game_class.md#the-game1-constructor) and is provided as an inherited property.

When loading content, you use the `ContentManager.Load<T>()` method.  The `T` type parameter specifies which type of content you are loading (`Texture2D`, `SpriteFont`, `SoundEffect`, etc).  The method itself takes a single parameter which is the path to the content file to load, minus the extension.  **The path is relative to the `ContentManager.RootDirectory` path which is `/Content/` by default.**  
   
```cs
Texture2D texture = Content.Load<Texture2D>("Graphics/image");
Song music = Content.Load<Song>("Music/background_music");
SoundEffect sfx = Content.Load<SoundEffect>("Audio/pickup_sfx");
```

After an asset is loaded for the first time, the *ContentManager class* will cache it internally.  By caching the asset, any subsequent calls to load that asset will serve the cached version instead of doing a full disk read again to load it directly from the *.xnb* file.  However, this also means that all content loaded is cached and using memory.  Depending on the size and number of assets loaded, over time the memory usage can become large.

To help with this, the *ContentManager class* also has the `ContentManager.Unload()` method.  Calling this method without providing a parameter will unload all cached content that has been loaded. You can also pass in the same path value you used to load the content as a parameter to unload only that specific asset.

```cs
//  Unloads all cached assets
Content.Unload();

//  Unloads a specific asset
Content.Unload("Graphics/image");
```
---

<div align="right"><table border=1><tr><td>Next Up</td></tr><tr><td>

[Chapter 4]()

</td></tr></table></div>
