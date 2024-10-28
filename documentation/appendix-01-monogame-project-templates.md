# Appendix: MonoGame Project Templates

In [*Chapter 02: Getting Started*](./02-getting-started.md) you [installed the MonoGame project templates](./02-getting-started.md#install-monogame-project-templates).  Using the templates to create a new project will automatically generate files and a project structure as a starting point for a new MonoGame application.  

MonoGame offers several templates to create a new project based on the platform(s) you are targeting and ancillary project templates for extensions and libraries.

In VSCode and Visual Studio 2022, when you create a new .NET project, you can see the MonoGame templates available to create the project with.  You can also see a list of the MonoGame templates by opening a *Command Prompt* or *Terminal* window and executing the command

```sh
dotnet new list MonoGame
```

The following table lists the different templates available and what they are used for

| Template Name | Short Name | Description |
|---|---|---|
| *MonoGame Android Application* | `mgandroid` | A MonoGame game project template targeting the Android platform using OpenGL |
| *MonoGame Content Pipeline Extension* | `mgpipeline` | A MonoGame project template to create a content pipeline extension library. |
| *MonoGame Cross-Platform Desktop Application* | `mgdesktopgl` | A MonoGame project template targeting Windows, macOS, and Linux using OpenGl. |
| *MonoGame Game Library* | `mglib` | A MonoGame project template to create a game library that can be referenced by other projects. |
| *MonoGame iOS Application* | `mgios` | A MonoGame project template targeting iPhone and iPad. |
| *MonoGame Shared Library Project* | `mgshared` | A MonoGame project template that creates a shared C# project that can be used to share code and content with other projects. |
| *MonoGame Windows Desktop Application* | `mgwindowsdx` | A MonoGame project template targeting Windows only using DirectX. |

> [!NOTE]
> The *Short Name* of a templates is the name you would use when creating a new MonoGame project using the dotnet CLI command.  For example, if creating a new *MonoGame Cross-Platform Desktop Application* using the dotnet CLI, you would execute the following command using the `mgdesktopgl` short name:
>
> ```sh
> dotnet new mgdesktopgl
> ```
