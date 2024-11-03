# Chapter 05: Texture Atlas

In this chapter, we'll learn about texture atlases, an optimization technique for game rendering. Building on the source rectangle concept from [Chapter 04](04-working-with-textures.md#drawing-texture-regions), we'll create a class to represents a Texture Atlas that will help us manage sprites more efficiently and improve our game's performance.

When rendering graphics in MonoGame using `SpriteBatch`, the goal is to minimize state changes by batching similar draw calls together. While putting all your draw calls between `SpriteBatch.Begin()` and `SpriteBatch.End()` is important, another factor to consider is *texture swapping*.

Every time `SpriteBatch.Draw()` is called with a different texture than the previous call, MonoGame must perform a texture swap on the GPU - an expensive operation that can impact performance. Let's examine this using a simple Pong game example:



```cs
private Texture2D _ball;
private Texture2D _paddle;
private Vector2 _leftPaddlePosition;
private Vector2 _rightPaddlePosition;
private Vector2 _ballPosition;

protected override void Draw(GameTime gameTime)
{
    GraphicsDevice.Clear(Color.CornflowerBlue);

    _spriteBatch.Begin();

    // First draw call - initial texture bind
    _spriteBatch.Draw(_paddle, _leftPaddlePosition, Color.White);

    // Second draw call - requires texture swap to ball
    _spriteBatch.Draw(_ball, _ballPosition, Color.White);

    // Third draw call - requires texture swap back to paddle
    _spriteBatch.Draw(_paddle, _rightPaddlePosition, Color.White);

    _spriteBatch.End();
}
```

Let's break down what happens in the GPU during these draw calls:

1. First paddle draw: GPU binds the paddle texture
2. Ball draw: GPU must unbind paddle texture and bind ball texture (Texture Swap #1)
3. Second paddle draw: GPU must unbind ball texture and rebind paddle texture (Texture Swap #2)

These texture swaps, while negligible in our simple Pong example, can become a performance issue in a full game where you might be drawing hundreds or thousands of sprites per frame.

You might think we could optimize this by reordering our draw calls to minimize texture swaps:

```cs
// Optimized draw order - only one texture swap needed
_spriteBatch.Draw(_paddle, _leftPaddlePosition, Color.White);
_spriteBatch.Draw(_paddle, _rightPaddlePosition, Color.White);
_spriteBatch.Draw(_ball, _ballPosition, Color.White);
```

While this reduces texture swaps from two to one in our example, it's not a scalable solution. In a real game with dozens of different textures and complex draw orders (think layered sprites, UI elements, particles, etc.), managing draw order by texture becomes impractical and can conflict with desired visual layering.


A better solution to this is to use a *texture atlas*.

## What is a Texture Atlas

A texture atlas (also known as a sprite sheet) is a large image file that contains multiple smaller images packed together. Instead of loading separate textures for each game element, you load the single texture file with all the images combined like a scrapbook where all your photos are arranged on the same page.

In the Pong example above, imagine taking the paddle and ball image and combining them into a single image file like in Figure 5-1 below:

<figure><img src="../images/05-texture-atlas/pong-atlas-diagram.png" alt="Figure 5-1: Pong Texture Atlas Example."><figcaption><p><strong>Figure 5-1: Pong Texture Atlas Example.</strong></p></figcaption></figure>

Now when we draw these sprites, we would be using the same texture and just specify the source rectangles for the paddle or ball when needed, completely eliminating texture swaps.

```cs
private Texture2D _textureAtlas;
private Rectangle _paddleSourceRect;
private Rectangle _ballSourceRect;

protected override void LoadContent()
{
    _textureAtlas = Content.Load<Texture2D>("pong-atlas");
    _paddleSourceRect = new Rectangle(0, 0, 32, 32);
    _ballSourceRect = new Rectangle(32, 0, 32, 32);
}

protected override void Draw(GameTime gameTime)
{
    GraphicsDevice.Clear(Color.CornflowerBlue);

    _spriteBatch.Begin();

    //  All draw calls use the same texture, so there is no texture swapping!
    _spriteBatch.Draw(_textureAtlas, _leftPaddlePosition, _paddleSourceRect, Color.White);
    _spriteBatch.Draw(_textureAtlas, _rightPaddlePosition, _paddleSourceRect, Color.White);
    _spriteBatch.Draw(_textureAtlas, _ballPosition, _ballSourceRect, Color.White);
    _spriteBatch.End();
}

```

Now when we draw these sprites, were using the same texture and just specifying the source rectangle of the texture to draw for the paddle or ball, completely eliminating texture swaps.


## Creating the `TextureAtlas` Class
Now that we know what a Texture Atlas is, let's create a class to represent one.  We could instead just have a `Texture2D` of the texture that represents the texture atlas and then create a bunch of `Rectangle` fields for each region.  This, however, would start to add a lot of code debt every project you create.  Instead, we can create a class to represent the idea of a texture atlas and use it to store the reference to the source `Texture2D` and manage a collection of the `Rectangle` values for each region.

Add a new class file to your project named *TextureAtlas.cs*, the replace the contents of the file with the following code:

```cs
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameSnake;

public class TextureAtlas
{
    //  The reference to the source texture represented by this TextureAtlas.
    private Texture2D _texture;

    //  A dictionary that provides region lookup by name for regions that have been added to this TextureAtlas.
    private Dictionary<string, Rectangle> _regionLookup;

    //  Creates a new TextureAtlas instance with the given Texture2D as the source texture.
    //  The source texture is checked to ensure it is not null and that it was not previously disposed before being
    //  provided to create this TextureAtlas.
    public TextureAtlas(Texture2D texture)
    {
        Debug.Assert(texture is not null);
        Debug.Assert(!texture.IsDisposed);

        _texture = texture;
        _regionLookup = new Dictionary<string, Rectangle>();
    }

    //  Adds a new region to the texture atlas with the specified name at the source rectangle provided.
    //  The source rectangle is a subregion of the texture atlas, so the bounds of the source rectangle must
    //  be contained within the bounds of the texture, otherwise, its invalid.
    public void AddRegion(string name, Rectangle source)
    {
        Debug.Assert(_texture.Bounds.Contains(source));

        _regionLookup.Add(name, source);
    }

    //  Removes the region with the specified name from the texture atlas.
    public void RemoveRegion(string name) => _regionLookup.Remove(name);

    //  Removes all regions from the texture atlas.
    public void RemoveAllRegions() => _regionLookup.Clear();
}
```

At the top of the class, there are two instance members defined; `_texture` and `_regionLookup`.  `_texture` holds a reference to the `Texture2D` that is our texture atlas.  This is the `Texture2D` that contains all of the smaller images inside of it.  The `_regionLookup` member is a `Dictionary<string, Rectangle>` which will hold a reference to each of the regions created and provide a way to lookup a specified region by name in a moment.  For instance, in our Pong example above, the texture atlas image contained the paddle and ball textures, so the `_regionLookup` would contain two keys, `paddle` and `ball` and each key would have the `Rectangle` value that represented it's region within the `_texture`.

After the instance members is the `TextureAtlas` constructor.  Here we specify that it requires a `Texture2D` parameter be given in order to create an instance of the `Texture2D` class.  This ensures that the `TextureAtlas` has a source texture to reference.  Before storing that reference though, checks are made to ensure that the `Texture2D` given isn't null and that it also hasn't been previously disposed of.  You might think adding these checks are pointless, because when would you ever create a `TextureAtlas` with a null parameter value or by giving it a `Texture2D` that was previously disposed of.  Of course you wouldn't right? Well we're all human and sometimes we make mistakes, so it's always best to check yourself to be sure before you publish your game with bugs you could have avoided.

> [!TIP]
> Instead of throwing an exception in the constructor if the parameter is null or has been disposed of, we instead are using `Debug.Assert` here.  This has a similar result as throwing an exception, except that the line of code is only ever executed when you run the code in a Debug build.  It asserts that the statement provided is true.  If the statement is false, then code execution will be paused at that line of code similar to if you add a breakpoint to debug.  This allows you to catch any issues while developing your game and running in a Debug build without needing to throw exceptions.  
>
> The `Debug.Assert` lines of code are also removed completely when you compile the project in a Release build, so you don't have to worry about debug specific code making its way into your final release.

Following the constructor are utility methods for adding and removing regions in the texture atlas.  The `AddRegion` method requires a name for the region and a `Rectangle` value that defines the bounds of the region within the texture atlas.  A check is made to ensure that the `Rectangle` value given as the region is actually within the bounds of the source texture.  The `RemoveRegion` method provides a way of removing a region from the texture atlas by name and `RemoveAllRegions` will remove all regions that have been added.
