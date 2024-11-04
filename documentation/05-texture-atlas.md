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

## Creating the `Sprite` Class
So far, the `TextureAtlas` class we've created just has a base foundation allowing us to add `Rectangle` values as regions that represents source rectangles.  Now we need something for the `TextureAtlas` to return when we ask it to give us one of those regions.  In a 2D game, you can think of every `SpriteBatch.Draw` method call executed as drawing a different *sprite*.  Even though they may all use the same source `Texture2D`, each draw call will have some differences in parameters like the source rectangle to define the region of the texture to draw, and other parameters such as *rotation*, *scale*, and *origin*.  These parameters are all part of the `SpriteBatch.Draw` method call, and we can use these to define a `Sprite` class to represent the individual sprites to be rendered.

Add a new class file to your project named *Sprite.cs*, then replace the contents of the file with the following code:

```cs
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameSnake;

public class Sprite
{
    //  The reference to the source texture used when rendering this sprite.
    private readonly Texture2D _texture;

    //  The source rectangle that represents the region in the source texture to use when rendering this sprite.
    private readonly Rectangle _sourceRectangle;

    //  The color tint to apply when rendering this sprite.
    //  Default value is Color.White.
    public Color Color { get; set; } = Color.White;

    //  The amount of rotation, in radians, to apply when rendering this sprite.
    //  Default value is no rotation, or 0.0f.
    public float Rotation { get; set; } = 0.0f;

    //  The scale factor to apply to the x- and y-axes when rendering this sprite.
    //  Default value is a scale of 1 on the x- and y-axes, or Vector2.Zero.
    public Vector2 Scale { get; set; } = Vector2.One;

    //  The xy-coordinate origin point to use when rendering this sprite.
    //  Affects the offset of the texture when rendered as well as being the origin in which the sprite is
    //  rotated and scaled from.
    //  Default value is top-left origin, or Vector2.Zero.
    public Vector2 Origin { get; set; } = Vector2.Zero;

    //  The SpriteEffect value that specifies if the sprite should be flipped horizontally, vertically, or both
    //  when rendered.
    //  Default value is no horizontal or vertical flipping, or SpriteEffects.None.
    public SpriteEffects Effects { get; set; } = SpriteEffects.None;

    //  The depth at which the sprite is rendered.
    //  Default value is 0.0f.
    public float LayerDepth { get; set; } = 0.0f;

    //  The width of this sprite, derived from the width of the source rectangle multiplied by the scale factor
    //  of the x-axis.
    public float Width => _sourceRectangle.Width * Scale.X;

    //  The height of this sprite, derived from the height of the source rectangle multiplied by the scale factor
    //  of the y-axis.
    public float Height => _sourceRectangle.Height * Scale.Y;

    //  Creates a new Sprite instance using the source texture and source rectangle provided.
    //  The source texture is checked to ensure it is not null and that it was not previously disposed.
    public Sprite(Texture2D texture, Rectangle sourceRectangle)
    {
        Debug.Assert(texture is not null);
        Debug.Assert(!texture.IsDisposed);

        _texture = texture;
        _sourceRectangle = sourceRectangle;
    }

    //  Draws this sprite using the SpriteBatch given at the position specified.
    //  Other parameters for the SpriteBatch.Draw method call are supplied via tha properties of this Sprite.
    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        spriteBatch.Draw(_texture, position, _sourceRectangle, Color, Rotation, Origin, Scale, Effects, LayerDepth);
    }
}
```

At the top of the `Sprite` class, there are two instance members defines; `_texture` and `_sourceRectangle`.  Just like with the `TextureAtlas` class, the `_texture` member holds a reference to the `Texture2D` that represents the texture atlas.  The `_sourceRectangle` member is the `Rectangle` value that defines the region within the `_texture` to use when the sprite is rendered.

Below the instance members are the properties of the `Sprite` class.  These properties are `Color`, `Rotation`, `Scale`, `Origin`, `Effects`, and `LayerDepth`, all of which mirror the parameter values that can be supplied to the `SpriteBatch.Draw` method when rendering the sprite.   Each of the properties are given a default value which mirrors the default value of the `SpriteBatch.Draw` parameter they are used for.

Two additional properties for the `Width` and `Height` of the `Sprite` also exist.  These dimensions are derived from the width and height of the source rectangle used by the `Sprite`, and multiplied by the `Scale` factor to give the accurate width and height of the `Sprite` when it's rendered.

Following the properties is the `Sprite` constructor.  Similar to the `TextureAtlas` here we specify that it requires a `Texture2D` parameter to be given and the texture given is checked to ensure that it is not null and was not previously disposed of using the `Debug.Assert` method calls.  The second parameter is the `Rectangle` value that defines the source rectangle within the texture to render.  Both of these are stored in their respective field members of the `Sprite` class.  

Finally, we have the `Draw` method.  This method is responsible for rendering the sprite.  It requires a `SpriteBatch` as parameter which will be the `SpriteBatch` instance used to render the sprite, and a `Vector2` value that represents the position to render the sprite at. Then the `SpriteBatch.Draw` method is executed using the `SpriteBatch` given, using the fields and properties of the `Sprite` as the parameters, to render it at the position specified.

## Creating a `Sprite` From the `TextureAtlas`
We now have a `TextureAtlas` class that represents a source texture with defined regions and a `Sprite` class that represents an image that uses one of the regions and the properties to use when rendering it.  All that's left is to combine the two so that we can tell the `TextureAtlas` a region and have it return back a `Sprite` that represents that region. Open up the *TextureAtlas.cs* class file and add the follow method to it at the bottom of the class:

```cs
//  Creates a new Sprite instance from this texture atlas using the region specified.
//  A check is made to ensure the region has been defined in this texture atlas.
public Sprite CreateSprite(string regionName)
{
    Debug.Assert(_regionLookup.ContainsKey(regionName));

    Rectangle region = _regionLookup[regionName];
    return new Sprite(_texture, region);
}
```

For this new `CreateSprite` method we only need to supply it with the name of the region to create the sprite from.  First, a check is made using `Debug.Assert` to ensure that the region has been defined so we can catch any slip ups in development.  Then we get the `Rectangle` value that represents that region from the `_regionLookup` dictionary and use the source `_texture` of the `TextureAtlas` and the region to create and return a new `Sprite` instance.
