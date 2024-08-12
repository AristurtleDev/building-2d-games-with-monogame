# Chapter 4: MonoGame Project Overview

- [The *csproj* C# Project File](#the-csproj-c-project-file)
- [The *dotnet-tools.json* Tools Manifest File](#the-dotnet-toolsjson-tools-manifest-file)
- [The *Icon.ico* and *Icon.bmp* Files](#the-iconico-and-iconbmp-files)
- [The *Content.mgcb* Content Project File](#the-contentmgcb-content-project-file)
- [The *Program.cs* C# File](#the-programcs-c-file)
- [The *Game.cs* C# File](#the-gamecs-c-file)
- [Conclusion](#conclusion)

---

In the [previous chapter](./03_hello_world_a_crash_course_in_monogame.md) we did a crash course in creating a new MonoGame project and writing the initial prototype for our MonoGameSnake game.  While we touched on concepts of MonoGame in code, we did not discuss the actual files that are included by default when you create a new MonoGame project.  After creating a project using one of the MonoGame templates, the project structure will be similar to the following: 

```
MonoGameSnake
|   app.manifest
|   Game1.cs
|   Icon.bmp
|   Icon.ico
|   MonoGameSnake.csproj
|   Program.cs
|
+---.config
|       dotnet-tools.json
|
\---Content
        Content.mgcb
```

This is the project structure you would see when creating a MonoGame project targeting desktops.  Projects that target mobile devices such as Android or iOS will contain the same files along with some additional manifest files specific for those devices, which are not covered in this document. Some of these files are standard files you would find in any C# project, such as the *.csproj* C# project file, *app.manifest* application manifest file, and *dotnet-tools.json* tools manifest file.  Discussing these common files in detail is out of scope for this documentation, though we will touch on some brief concepts of them below.  If you would like to learn more about them you can find the official documentation on Microsoft Learn at the following linkes

- [Understanding The Project File](https://learn.microsoft.com/en-us/aspnet/web-forms/overview/deployment/web-deployment-in-the-enterprise/understanding-the-project-file)
- [Application Manifests](https://learn.microsoft.com/en-us/windows/win32/sbscs/application-manifests)
- [Dotnet Tools](https://learn.microsoft.com/en-us/dotnet/core/tools/global-tools)

## The *csproj* C# Project File
The first file we're going to take a look at is the projects *csproj* project file.  

```xml
<Project Sdk="Microsoft.NET.Sdk">
	<PropertyGroup>
		<OutputType>WinExe</OutputType>
		<TargetFramework>net6.0</TargetFramework>
		<RollForward>Major</RollForward>
		<PublishReadyToRun>false</PublishReadyToRun>
		<TieredCompilation>false</TieredCompilation>
	</PropertyGroup>
	<PropertyGroup>
		<ApplicationManifest>app.manifest</ApplicationManifest>
		<ApplicationIcon>Icon.ico</ApplicationIcon>
	</PropertyGroup>
	<ItemGroup>
		<None Remove="Icon.ico" />
		<None Remove="Icon.bmp" />
	</ItemGroup>
	<ItemGroup>
		<EmbeddedResource Include="Icon.ico" />
		<EmbeddedResource Include="Icon.bmp" />
	</ItemGroup>
	<ItemGroup>
		<PackageReference Include="MonoGame.Framework.DesktopGL" Version="3.8.1.303" />
		<PackageReference Include="MonoGame.Content.Builder.Task" Version="3.8.1.303" />
	</ItemGroup>
	<Target Name="RestoreDotnetTools" BeforeTargets="Restore">
		<Message Text="Restoring dotnet tools" Importance="High" />
		<Exec Command="dotnet tool restore" />
	</Target>
</Project>
```

The first and second `<PropertyGroup>` sections contain the standard information you would find in any C# project file, such as the `<TargetFrameWork>`, `<ApplicationManifest>` and `<ApplicationIcon>`.  

In the first `<ItemGroup>` section there are two `<None>` tags with a `Remove` attribute to remove the icon *.ico* and *.bmp* files.  This removes them as files that are part of the project, so they will not show up in the *Solution Explorer* only.  It does not remove the files themselves such as deleting them.

The second `<ItemGroup>` section specifies two `<EmbeddedResource>` tags with the `Include` attribute to include the icon *.ico* and *.bmp* files as embedded resources in the game application.  This means when the game project is built, those two icon files will be embedded into the assembly. Internally, the MonoGame framework will look for these within the assembly and use them as the icon files for the window title bar and the taskbar.  

The third `<ItemGroup>` section specifies two `<PackageReferences>` tags, one to include *MonoGame.Framework./** and another to include *MonoGame.Content.Builder.Task*.  This section is adding these two NuGet packages as references to the project, which is how the MonoGame framework is distributed.  The *MonoGame.Framework./** package contains the actual framework code specific for the platform you are targeting (e.g. *MonoGame.Framework.DesktopGL* when targeting cross-platform Windows, Mac, and Linux or *MonoGame.FrameWork.WindowsDX* when targeting Windows only).  The *MonoGame.Content.Builder.Task* package contains tasks that will execute when you perform a build of your game that are responsible for automatically building the assets added to your content project and copying the compiled assets to the project build directory.

Finally, is the `<Target>` tag which defines a target named *RestoreDotnetTool* that executes before the *Restore* target.  This is here to ensure that the dotnet tools defined in the *dotnet-tools.json* manifest file are downloaded and ready to use when you do a project restore.

## The *dotnet-tools.json* Tools Manifest File
The next file we're going to look at the contents of is the *dotnet-tools.json* tools manifest file. This file is located inside the *.config/* directory in the project's root directory.

```json
{
  "version": 1,
  "isRoot": true,
  "tools": {
    "dotnet-mgcb": {
      "version": "3.8.1.303",
      "commands": [
        "mgcb"
      ]
    },
    "dotnet-mgcb-editor": {
      "version": "3.8.1.303",
      "commands": [
        "mgcb-editor"
      ]
    },
    "dotnet-mgcb-editor-linux": {
      "version": "3.8.1.303",
      "commands": [
        "mgcb-editor-linux"
      ]
    },
    "dotnet-mgcb-editor-windows": {
      "version": "3.8.1.303",
      "commands": [
        "mgcb-editor-windows"
      ]
    },
    "dotnet-mgcb-editor-mac": {
      "version": "3.8.1.303",
      "commands": [
        "mgcb-editor-mac"
      ]
    }
  }
}
```

This is the manifest file that specifies which **local** dotnet tools the project uses.  The table below contains a brief overview of what each of these tools are used for

| Tool | Description |
| --- | --- |
| *dotnet-mgcb* | This contains the *MonoGame Content Builder* tool that is responsible for compiling the assets added to your content project. |
| *dotnet-mgcb-editor* | This contains the *MonoGame Content Builder Editor* tool used to open the content project file and provides a visual UI managing the assets to add to your content project. |
| *dotnet-mgcb-editor-linux* | This contains the bootstrap launcher tool to open the *MonoGame Content Builder Editor* on a Windows operating system. |
| *dotnet-mgcb-editor-windows* | This contains the bootstrap launcher tool to open the *MonoGame Content Builder Editor* on a Linux operating system. |
| *dotnet-mgcb-editor-mac* | This contains the bootstrap launcher tool to open the *MonoGame Content Builder Editor* on a macOS operating system. |

These tools are necessary if you are using the *Content Pipeline* to manage and load assets in your game.  These tools are also expected to be part of the project for the *MonoGame.Content.Builder.Task* NuGet package mentioned earlier in the C# project file.  

> [!NOTE]
> It may seem wasted to include the *MonoGame Content Builder Editor* launchers for all three operating system types, especially if you are only developing on one operating system.  However, by including all three, it makes it easier if you ever need to switch to a different operating system for your project, or when working on a team where the code is shared using a git repository and team members have different operating system.

## The *Icon.ico* and *Icon.bmp* Files
These are the icon files used when by the application for it's desktop icon, the icon displayed in the window title bar area, and the icon displayed in the task bar on windows or the docs on macOS. By default, these are icons of the MonoGame logo

![The default MonoGame logo icon included in a new MonoGame project](./images/chapter_04/icon.png)  
**Figure 4-1:** *The default MonoGame logo icon included in a new MonoGame project.*

If you want to customize the icons used for your game, you can replace these files. 

> [!CAUTION]
> When replacing the icon files with a custom icon for your game, ensure that they are named exactly the same as the default icon files.  This is because, as mentioned above, the MonoGame framework expects files with these names and extension to be embedded in the project assembly in order to load and display them.

## The *Content.mgcb* Content Project File
Next up is the *Content.mgcb* content project file located in the *Contnet/* directory in the project's root directory.  The *MonoGame Content Builder* uses the configurations defined in this file to know what assets to build and how to build them so they can be loaded in your game.  

```
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

This is not a file you would typically edit by hand, [although you can if you want to learn the syntax](https://docs.monogame.net/articles/tools/mgcb.html).  Instead, you load this file in the *MonoGame Content Builder Editor* so that it can be edited visually with inside the UI application.

## The *Program.cs* C# File
This is the code file that contains the main entry point for the application. All C# application, regardless of being a MonoGame project or not, require a main entry point that specifies where code execution should start when the application runs.  This one provided by the MonoGame template very simple:

```cs
using var game = new Game1();
game.Run();
```

Here, a new instance of the `Game1` class is initialized within a `using` context, and then the `Run()` method is called to begin execution of the game. 

> [!NOTE]
> MonoGame projects use a [top-level statement](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/program-structure/top-level-statements) style for hte *Program.cs* file.  Top-level statements allows you to write code without the boilerplate that is typically included to define the main entry point.  For example, traditionally with the boilerplate code, the *Program.cs* file would look like the following
> ```cs
> public class Program 
> {
>     public static void Main(string[] args)
>     {
>         using var game = new Game1();
>         game.Run();
>     }
> }
> ```
>
> Top-level statements were introduced in C# 10.



## The *Game.cs* C# File
Finally we have the *Game1.cs* C# file.  This file contains the definition of the `Game1` class, which is the heart of our game.

```cs
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
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

The `Game1` class derives from the `Game` class and is responsible for setting up graphic services, initializing the game, loading content, and running the *game loop*.  Understanding this file is important, which is why we're going to dedicate the entire next chapter to discussing this file.

## Conclusion
In this chapter we discussed the standard files generated when creating a new MonoGame project, and touched on the important concepts of the content contained within each.  Next, we'll take a look at the *Game1.cs* file in more detail and discuss the order of execution of methods for the `Game1` class.

[Go to Chapter 5: The Game Class](./05_the_game_class.md)
