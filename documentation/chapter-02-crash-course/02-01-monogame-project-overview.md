# 2-1: MonoGame Project Overview

In the [previous chapter](./03_hello_world.md) we created a new MonoGame project using one of the MonoGame project templates. Using the templates to create a new project automatically generates files and a project structure as a starting point for a new game.  MonoGame offers several templates to create a new project with based on the platform(s) you are targeting for your game and ancillary project templates for extensions and libraries.  The following table lists the different templates available and what they are used for.

**Table 2-1-1:** *The MonoGame Project Templates.*
| Template Name | Short Name | Description |
|---|---|---|
| MonoGame Android Application | mgandroid | A MonoGame game project template targeting the Android platform using OpenGL |
| MonoGame Content Pipeline Extension | mgpipeline | A MonoGame project template to create a content pipeline extension library. |
| MonoGame Cross-Platform Desktop Application | mgdesktopgl | A MonoGame project template targeting Windows, macOS, and Linux using OpenGl. |
| MonoGame Game Library | mglib | A MonoGame project template to create a game library that can be referenced by other projects. |
| MonoGame iOS Application | mgios | A MonoGame project template targeting iPhone and iPad. |
| MonoGame Shared Library Project | mgshared | A MonoGame project template that creates a shared C# project that can be used to share code and content with other projects. |
| MonoGame Windows Desktop Application | mgwindowsdx | A MonoGame project template targeting Windows only using DirectX. |

Regardless of the project template chosen, each project will generate a project structure similar to the following:

```
|   MyGame.sln
|
\---MyGame
    |   app.manifest
    |   Game1.cs
    |   Icon.bmp
    |   Icon.ico
    |   MyGame.csproj
    |   Program.cs
    |
    +---.config
    |       dotnet-tools.json
    |
    +---Content
    |       Content.mgcb
```

> [!NOTE]
> Project templates that target mobile devices such as Android and iOS may contain additional manifest files specific for those devices, which are not covered in this tutorial. 

| File Name | Description |
|---|---|
| *.sln | The solution file used to group one or more projects. |
| [*.csproj](./02-02-the-csproj-project-file.md) | The C# Project file used to define project-level configurations including the target framework and package references. |
| app.manifest | A .NET manifest file that defines application-level settings and requirements for the application when running on Windows only.  Does not specify any configurations for other operating systems. |
| [Content.mgcb](./02-03-the-content-file.md) | The MonoGame content project file that defines the assets to build by the content pipeline. |
| [dotnet-tools.json](./02-04-the-dotnet-tools-manifest-file.md) | A .NET manifest file that defines the dotnet tools used by the project. |
| [Game1.cs](./02-05-the-game1-file.md) | A C# code file that contains the code for initializing, loading content, updating, and rendering the game.  This is the heart of every MonoGame project. |
| [Icon.bmp and Icon.ico](./02-06-the-icon-files.md) | Image file used by the MonoGame framework to display an icon in the window title bar and task manager. |
| [Program.cs](./02-07-the-program-file.md) | A C# code file that defines the main entry point when the application runs. |


> [!NOTE]
> The *\*.sln* and *app.manifest* files are not discussed in detail as part of this tutorial.  For more information on these files, please refer to the links provided in the [See Also](#see-also) section below.

## See Also
- [What are solutions and projects in Visual Studio?](https://learn.microsoft.com/en-us/visualstudio/ide/solutions-and-projects-in-visual-studio?view=vs-2022#solutions)
- [Application Manifests](https://learn.microsoft.com/en-us/windows/win32/sbscs/application-manifests)
- [Dotnet Tools](https://learn.microsoft.com/en-us/dotnet/core/tools/global-tools)

## Next Steps
- [2-2: The csproj Project File](./02-02-the-csproj-project-file.md)
