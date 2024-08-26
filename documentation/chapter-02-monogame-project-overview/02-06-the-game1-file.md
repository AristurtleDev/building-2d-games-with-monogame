# Chapter 2-6: The Game1 File

- [Namespace Imports](#namespace-imports)
- [Class Declaration](#class-declaration)
- [Instance Members](#instance-members)
- [The Game1 Constructor](#the-game1-constructor)
- [The Initialize Method](#the-initialize-method)
- [The LoadContent Method](#the-loadcontent-method)
- [The Update Method](#the-update-method)
- [The Draw Method](#the-draw-method)
- [Additional Methods](#additional-methods)
- [Additional Properties](#additional-properties)
- [Order of Execution](#order-of-execution)

---

At the heart of every MonoGame project is an implementation of the [`Game`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html) class.  The [`Game`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html) class is responsible for initializing the graphics services, initializing the game, loading content, and finally updating and rendering our game.

When creating a new MonoGame project, this is provided by the *Game1.cs* code file that defines the class `Game1` which derives from the MonoGame framework [`Game`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html) class.  When first generated, it contains the following code:

```cs
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameSnake
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
```

> [!TIP]
> By default, the MonoGame project templates will name this class `Game1`.  This is not a hard requirement and you can change the name of this class to anything else that may make more sense for your project. Regardless, it will be referred to as `Game1` throughout the documentation in this tutorial.

The base MonoGame [`Game`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html) class provides [virtual methods](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual) that can be overridden in our `Game1` implementation to provide the logic for our game.

> [!CAUTION]
> When overriding one of the virtual methods from the base [`Game`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html) class, it is important that you keep the `base` method call.  Many of the base [`Game`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html) class methods have logic for initializations, updating, and rendering that still need to be called even though we are overwriting the implementation.

Let's break this file down into individual sections to better understand it.

## Namespace Imports
Starting at the top of the file are the namespace imports

```cs
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
```

These import the most common used namespaces in a MonoGame project, including the base framework, graphics, and input. 

> [!NOTE] 
> You may be wondering why the types within MonoGame exist with `Microsoft.Xna.Framework.*` namespaces.  If you recall from the [Introduction to MonoGame](01_introduction_to_monogame.md), MonoGame is an open source reimplementation of Microsoft's XNA Framework.  To ensure compatibility with XNA projects, MonoGame implements the same namespaces that XNA did.

## Class Declaration
Following the namespace imports is the class declaration:

```cs
public class Game1 : Game
```

Here it is defining our class called `Game1` which is [inheriting](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/inheritance) from the base [`Game`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html) class provided by the MonoGame framework. This inheritance is what gives us access to the virtual methods we can override and additional properties, which we'll discuss in more detail below.

## Instance Members
Inside the class declaration, the first things we see are teh following two instance members:

```cs
private GraphicsDeviceManager _graphics;
private SpriteBatch _spriteBatch;
```

- The [`GraphicsDeviceManager`](https://docs.monogame.net/api/Microsoft.Xna.Framework.GraphicsDeviceManager.html) is responsible for initializing and providing access to the graphics device and other graphic presentation configurations.  It contains a property named [`GraphicsDevice`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.GraphicsDevice.html) that represents the actual graphics device on the device the game is running on. The [`GraphicsDevice`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.GraphicsDevice.html) is the interface between your game and the graphics processing unit (GPU) for everything on screen.  It also contains the properties [`PreferredBackBufferWidth`](https://docs.monogame.net/api/Microsoft.Xna.Framework.GraphicsDeviceManager.html#Microsoft_Xna_Framework_GraphicsDeviceManager_PreferredBackBufferWidth) and [`PreferredBackBufferHeight`](https://docs.monogame.net/api/Microsoft.Xna.Framework.GraphicsDeviceManager.html#Microsoft_Xna_Framework_GraphicsDeviceManager_PreferredBackBufferHeight) which can be used to set the width and height, in pixels, of the game screen's back buffer.

- The [`SpriteBatch`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.SpriteBatch.html) is used to perform 2D graphics rendering of the textures for the game.  A game will consist of multiple textures that are rendered to represent the game visually, and the [`SpriteBatch`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.SpriteBatch.html) provides an optimized method of rendering multiple textures in a single batch call to the GPU instead of doing one render at a time.

We'll cover the [`GraphicsDeviceManager`](https://docs.monogame.net/api/Microsoft.Xna.Framework.GraphicsDeviceManager.html), the [`GraphicsDevice`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.GraphicsDevice.html), and the [`SpriteBatch`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.SpriteBatch.html) in more detail in later chapters.

## The Game1 Constructor
Next is the `Game1` constructor.  This responsible for creating a new instance of the `Game1` class when `new Game1()` is called in [the Program.cs](./02-05-the-program-file.md) file.

```cs
public Game1()
{
    _graphics = new GraphicsDeviceManager(this);
    Content.RootDirectory = "Content";
    IsMouseVisible = true;
}
```

When this constructor is called
1. First the base [`Game`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html) constructor that the `Game1` class inherits from is called.  Here instances of internal components needed are created and platform specific initializations occur.  Once the base constructor finishes, the logic `Game1` constructor is then executed
2. The [`GraphicsDeviceManager`](https://docs.monogame.net/api/Microsoft.Xna.Framework.GraphicsDeviceManager.html) is created and stored in the `_graphics` member variable.
3. The [`RootDirectory`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Content.ContentManager.html#Microsoft_Xna_Framework_Content_ContentManager_RootDirectory) property of the [`Content`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Content) object is set to the *Content* directory.  The [`Content`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Content) object is of the type [`ContentManager`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Content.ContentManager.html) and is provided to us through the [`Game`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html) class inheritance.  Setting the root directory tells the content manager to use that directory as the root directory when resolving relative paths during content loading.
4. [`IsMouseVisible`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_IsMouseVisible) is set to `true` so that the mouse cursor is visible when moved over the game window.  This is another property that is provided through the inheritance from the [`Game`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html) class.

## The Initialize Method
Below the constructor is the override of the [`Initialize()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Initialize) method.
This method is where we can do any initializations for our game.

```cs
protected override void Initialize()
{
    base.Initialize();
}
```

> [!NOTE]
> This method is called only once by the MonoGame framework and is called immediately after the constructor is called.

You might be wondering why we have an [`Initialize()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Initialize) method instead of performing all of our initializations within the constructor.  It's [advised to not call overridable methods from within a constructor](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca2214) as this can lead to unexpected states in object construction when called.  Additionally, the constructor itself is initially called in [the Program.cs file](./02-05-the-program-file.md) when a new instance of the class is created.  As mentioned above in, when the constructor is called, the base constructor is executed first which instantiates properties and services that maybe needed later for our game initializations.  

> [!CAUTION]
> When `base.Initialize()` is called, the last thing it does before returning back is making a call to the [`LoadContent()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_LoadContent) method.  This means that if anything you are initializing requires assets loaded from the [`LoadContent()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_LoadContent) method, it should be done **after** the `base.Initialize()` call, not **before** it.


## The LoadContent Method
The [`LoadContent()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_LoadContent) method is used to load any assets that are used by our game.  Content loading and managing assets will be discussed in later chapters, for now, it's only important to know that his is where you can load your game assets at.

```cs
protected override void LoadContent()
{
    _spriteBatch = new SpriteBatch(GraphicsDevice);
}
```

> [!NOTE]
> This method will only be called once by the MonoGame framework and it is called **during** the execution of the `base.Initialize()` call within the [`Initialize()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Initialize) method.

The default implementation provided in the template instantiates a new instance of the [`SpriteBatch`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.SpriteBatch.html) and stores it in the `_spriteBatch` instance member.  When creating a new [`SpriteBatch`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.SpriteBatch.html) instance, it requires that an instance of the [`GraphicsDevice`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.GraphicsDevice.html) object type be given to it.  Here we pass in the one that is provided as a property from the [`Game`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html) class inheritance. 

> [!NOTE]
> The [`GraphicsDevice`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.GraphicsDevice.html) object provided as a property from the inheritance of the [`Game`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html) class?  Yep! The [`Game`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html) class provides a property for accessing the [`GraphicsDevice`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.GraphicsDevice.html) object without having to go through the [`GraphicsDeviceManager`](https://docs.monogame.net/api/Microsoft.Xna.Framework.GraphicsDeviceManager.html) to get it.  Not that this is not a `static` property and is only available at the class instance scope of the [`Game`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html) class.


## The Update Method
The [`Update()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Update_Microsoft_Xna_Framework_GameTime_) method is where we perform all of the game logic; input handling, physics, collisions, etc.  The method takes in a single [`GameTime`](https://docs.monogame.net/api/Microsoft.Xna.Framework.GameTime.html) parameter that provides a snapshot of the game's current timing values, sometimes known as delta time.

```cs
protected override void Update(GameTime gameTime)
{
    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

    base.Update(gameTime);
}
```

The default implementation performs the following: 

1. Input is polled to determine if the **Back** button on the player one game pad is pressed or if the **Esc** key on the keyboard is pressed.  If either are `true`, then the [`Exit()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Exit) method is called to exit the game.
2. The `base.Update()` method is called.  When this is called, the base update method will call the [`Update()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Update_Microsoft_Xna_Framework_GameTime_) method on an [`IUpdateable`](https://docs.monogame.net/api/Microsoft.Xna.Framework.IUpdateable.html) objects that have been added to the [`GameComponentCollection`](https://docs.monogame.net/api/Microsoft.Xna.Framework.GameComponentCollection.html) of the [`Game`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html) class. 

## The Draw Method
The [`Draw()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Draw_Microsoft_Xna_Framework_GameTime_) method is where all of the game logic for rendering occurs.  Just like with the [`Update()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Update_Microsoft_Xna_Framework_GameTime_) method, it takes a single [`GameTime`](https://docs.monogame.net/api/Microsoft.Xna.Framework.GameTime.html) object as a parameter that provides a snapshot of the game's current timing values.

```cs
protected override void Draw(GameTime gameTime)
{
    GraphicsDevice.Clear(Color.CornflowerBlue);

    base.Draw(gameTime);
}
```

The default implementation performs the following:

1. [`GraphicsDevice.Clear(Color.CornflowerBlue)`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.GraphicsDevice.html#Microsoft_Xna_Framework_Graphics_GraphicsDevice_Clear_Microsoft_Xna_Framework_Color_) is called, which clears the back buffer using the color Cornflower Blue to prepare it for rendering.
2. The `base.Draw()` method is called.  When this is called, the base draw method will call the [`Draw()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Draw_Microsoft_Xna_Framework_GameTime_) method on any [`IDrawable`](https://docs.monogame.net/api/Microsoft.Xna.Framework.IDrawable.html) objects that have been added to the [`GameComponentCollection`](https://docs.monogame.net/api/Microsoft.Xna.Framework.GameComponentCollection.html) collection of the [`Game`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html) class.

## Additional Methods
In additional to the methods mentioned above, the [`Game`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html) class offer other virtual methods that can be overridden, though they are used less often than the ones provided by default. The following table shows the additional virtual methods that can be overridden:

**Table 5-1:** *Virtual methods of the [`Game`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html) class that can be overridden in the `Game1` class.*  
| Virtual Method                                                                                                                                                                  | Description                                                                                                                                                                                                                                                                                                                                                                                                                   |
| ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [`BeginDraw()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_BeginDraw)                                                         | Called automatically by the framework, immediately before [`Draw()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Draw_Microsoft_Xna_Framework_GameTime_) is called.  If this method returns `false` then [`Draw()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Draw_Microsoft_Xna_Framework_GameTime_) will not be called. |
| [`BeginRun()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_BeginRun)                                                           | Called automatically by the framework, immediately after [`Initialize()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Initialize) but before the first call to [`Update()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Update_Microsoft_Xna_Framework_GameTime_).                                                          |
| [`Dispose()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Dispose)                                                             | Called when the game instance is disposed of, performing any clean up of unmanaged resources used by the application.                                                                                                                                                                                                                                                                                                         |
| [`EndDraw()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_EndDraw)                                                             | Called automatically by the framework, immediately after [`Draw()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Draw_Microsoft_Xna_Framework_GameTime_) is called and performs the logic to present the rendered frame to the game window.  **If this method is overridden, ensure that you call `base.EndDraw()` so that the internal render presentation is called.**      |
| [`EndRun()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_EndRun)                                                               | Called automatically by the framework, immediately after the game loop as been terminated before the application exits.                                                                                                                                                                                                                                                                                                       |
| [`OnActivated()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_OnActivated_System_Object_System_EventArgs_)                     | Called automatically by the framework whenever the game window gains focus.  The base method raises the [`Activated`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Activated) event.                                                                                                                                                                                          |
| [`OnDeactivated()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_OnDeactivated_System_Object_System_EventArgs_)                 | Called automatically by the framework whenever the game window loses focus.  The base method raises the [`Deactivated`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Deactivated) event.                                                                                                                                                                                      |
| [`OnExiting()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_OnExiting_System_Object_Microsoft_Xna_Framework_ExitingEventArgs_) | Called automatically by the framework when it is detected that the game should be exited. The base method raises the [`Exiting`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Exiting) event for the application.                                                                                                                                                             |
| [`UnloadContent()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_UnloadContent)                                                 | Use this method to unload graphical resources loaded by the content manager. This method is called automatically when the game is exiting.                                                                                                                                                                                                                                                                                    |



## Additional Properties
Along with the above methods, by inheriting from the [`Game`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html) class, the `Game1` class has access to the following properties:

**Table 5-2:** *Properties of the [`Game`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html) class that are accessible in the `Game1` class through inheritance.*  

| Property Name                                                                                                                         | Type                                                                                                            | Description                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               |
| ------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [`Components`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Components)               | [`GameComponentCollection`](https://docs.monogame.net/api/Microsoft.Xna.Framework.GameComponentCollection.html) | A collection of game component objects that are automatically updated and/or rendered during the base calls to [`Update()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Update_Microsoft_Xna_Framework_GameTime_) and [`Draw()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Draw_Microsoft_Xna_Framework_GameTime_) respectively. To add components to this collection, they should derive from either [`IUpdateable`](https://docs.monogame.net/api/Microsoft.Xna.Framework.IUpdateable.html) or [`IDrawable`](https://docs.monogame.net/api/Microsoft.Xna.Framework.IDrawable.html). |
| [`Content`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Content)                     | [`ContentManager`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Content.ContentManager.html)           | The content manager used to load and manage the lifetime of assets for the game.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          |
| [`GraphicsDevice`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_GraphicsDevice)       | [`GraphicsDevice`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.GraphicsDevice.html)          | Gets the graphics device used for rendering by this game.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 |
| [`InactiveSleepTime`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_InactiveSleepTime) | `TimeSpan`                                                                                                      | Gets or sets the time to sleep between frames when the game is not active. Must be a positive value. When the game window loses focus, the game loop will sleep for this duration between frames to reduce CPU usage.                                                                                                                                                                                                                                                                                                                                                                                                                                                                     |
| [`IsActive`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_IsActive)                   | `bool`                                                                                                          | Indicates if the game window currently has focus and is active.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           |
| [`IsFixedTimeStep`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_IsFixedTimeStep)     | `bool`                                                                                                          | Determines whether the game uses a fixed or variable time step for updates. When true, the game attempts to update at a consistent rate specified by `TargetElapsedTime`. This can provide more predictable game play and physics simulations, especially on varying hardware. When false, the game updates as frequently as possible, which can provide smoother animation on high-performance systems but may lead to inconsistent behavior across different hardware.                                                                                                                                                                                                                  |
| [`IsMouseVisible`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_IsMouseVisible)       | `bool`                                                                                                          | Gets or sets whether the mouse cursor is visible when it's over the game window.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          |
| [`LaunchParameters`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_LaunchParameters)   | [`LaunchParameters`](https://docs.monogame.net/api/Microsoft.Xna.Framework.LaunchParameters.html)               | Gets the startup parameters for this game instance.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       |
| [`MaxElapsedTime`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_MaxElapsedTime)       | [`TimeSpan`](https://learn.microsoft.com/en-us/dotnet/api/system.timespan?view=net-8.0)                         | The maximum amount of time allowed to pass between updates. If the time since the last update exceeds this value, it will be clamped to this value. This prevents the game from trying to "catch up" with too many updates after a large gap in time (like a freeze or breakpoint). Must be positive and greater than or equal to `TargetElapsedTime`.                                                                                                                                                                                                                                                                                                                                    |
| [`Services`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Services)                   | [`GameServiceContainer`](https://docs.monogame.net/api/Microsoft.Xna.Framework.GameServiceContainer.html)       | Gets a container holding service providers attached to this game instance.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |
| [`TargetElapsedTime`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_TargetElapsedTime) | [`TimeSpan`](https://learn.microsoft.com/en-us/dotnet/api/system.timespan?view=net-8.0)                         | Specifies the desired time between frames when `IsFixedTimeStep` is true. This property sets the target update rate for the game. For example, setting it to 1/60th of a second targets 60 FPS. The game loop will attempt to maintain this update rate, potentially inserting small sleep periods if updates are completed faster than the target time. If updates take longer than this time, the game may run slower than intended. This property is crucial for creating consistent game play experiences across different hardware capabilities. Must be positive, non-zero, and less than or equal to `MaxElapsedTime`.                                                             |
| [`Window`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Window)                       | [`GameWindow`](https://docs.monogame.net/api/Microsoft.Xna.Framework.GameWindow.html)                           | Gets the system window that this game is displayed on. Provides access to window-specific properties and methods.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         |

## Order of Execution
Knowing the methods available isn't enough.  We can know that the [`Initialize()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Initialize) method initializes the game for us, and that [`Update()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Update_Microsoft_Xna_Framework_GameTime_) will update the game.  However, the methods are called by the MonoGame framework in a specific order, and knowing the order of execution is important to know when to expect things to happen.  For instance, how often is [`Update()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Update_Microsoft_Xna_Framework_GameTime_) called?  Is [`Draw()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Draw_Microsoft_Xna_Framework_GameTime_) always called immediately after an [`Update()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Update_Microsoft_Xna_Framework_GameTime_) call?  

Figure 2-6-1 below provides a high-level view of the execution order of events for a MonoGame application.  This follows the events from the initial construction of the `Game1` class, to the initializations, the game loop, and finally the game exiting events.

![A high-level view of the execution order of events for a MonoGame application](./images/02-02/execution-order.png)  
**Figure 5-1:** *A high-level view of the execution order of events for a MonoGame application.*

1. Initial execution begins at the [constructor](#constructor) which is called in [the Program.cs file](../chapter-02-monogame-project-overview/02-05-the-program-file.md).
   1. The base [`Game`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html) constructor is executed where the [`GameServicesContainer`](https://docs.monogame.net/api/Microsoft.Xna.Framework.GameServiceContainer.html), [`GameComponentCollection`](https://docs.monogame.net/api/Microsoft.Xna.Framework.GameComponentCollection.html), adn the [`ContentManager`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Content.ContentManager.html) are initialized, followed by any platform specific initializations
   2. Nex the logic of the `Game1` constructor is executed
2. After the constructor finishes, [`Game.Run()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Run) is called in [the Program.cs file](../chapter-02-monogame-project-overview/02-05-the-program-file.md)
   1. During the execution of the [`Game.Run()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Run) method, the [`Initialize()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Initialize) method is called first.  During the call to [`Initialize()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Initialize) is when [`LoadContent()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_LoadContent) is called
   2. After initialize is finished, a call is made to [`BeginRun()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_BeginRun).  Internally, this performs no logic in the base [`Game`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html) class, but can be overridden in the `Game1` class to provide any specific logic needed here.
   3. Finally, a single call to [`Update()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Update_Microsoft_Xna_Framework_GameTime_) is made.  single update call is made here for XNA compatibility reasons.
3. After [`Game.Run()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Run) is finished, the application enters what is called the *game loop*.  In the game loop, the [`Update()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Update_Microsoft_Xna_Framework_GameTime_) and [`Draw()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Draw_Microsoft_Xna_Framework_GameTime_) methods are called one after the other in each iteration of the loop until the game is told to exit.
   1. At the start of each game loop iteration, a check is made to see if the game is running in fixed time step mode.  If fixed time step is being used, then [`Update()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Update_Microsoft_Xna_Framework_GameTime_) will be call over and over until the amount of time that has elapsed is greater than or equal to the target elapsed time; otherwise, [`Update()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Update_Microsoft_Xna_Framework_GameTime_) is only called once.
   2. One the update(s) are completed, the rendering steps begin
      1. First a call is made to [`BeginDraw()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_BeginDraw).  This method can be overridden in the `Game1` class to execute any logic needed before the actual rendering occurs.
      2. Next, the [`Draw()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Draw_Microsoft_Xna_Framework_GameTime_) method is called.  This is where all rendering of the game occurs.
      3. Finally, [`EndDraw()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_EndDraw) is called.  This method can be overridden in the `Game1` class to execute any logic or cleanup needed after the main rendering is finished.
4. Once an exit condition is met during the game loop, the exit block is executed
   1. First, a call to [`EndRun()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_EndRun) is made.  This method can be overridden in the `Game1` class to perform any actions needed at this point.
   2. Next, [`OnExiting()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_OnExiting_System_Object_Microsoft_Xna_Framework_ExitingEventArgs_) is called, which will raise the [`Exiting`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Exiting) event.  Any subscribers to this event will now execute here.
   3. Next, the [`UnloadContent()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_UnloadContent) method is called.  This method can be overridden in the `Game1` class to perform any actions needed to unload graphic resources and other assets.
   4. Finally, the [`Dispose()`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html#Microsoft_Xna_Framework_Game_Dispose) method is called because the [`Game`](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html) class implements the [`IDisposable`](https://learn.microsoft.com/en-us/dotnet/api/system.idisposable?view=net-8.0) interface.


## See Also
- [Inheritance - derive types to create more specialized behavior](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/inheritance)
- [virtual (C# Reference)](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual)
- [override (C# Reference)](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/override)
- [MonoGame `Game` Class](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html)


## Next
- [Chapter 2-7: Conclusion](./02-07-conclusion.md)
