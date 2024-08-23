# Chapter 3: Hello World - A Crash Course in MonoGame

- [Creating a New Project](#creating-a-new-project)
  - [Visual Studio 2022](#visual-studio-2022)
  - [Visual Studio Code](#visual-studio-code)
  - [Running The Game For The First Time](#running-the-game-for-the-first-time)
- [Conclusion](#conclusion)


---

When learning a new programming language, tutorials will often start with some form of the "Hello World" program.  For example, a classic Hello World program in C# would look something like this

```cs
using System;

namespace HelloWorldApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World");
        }
    }
}
```

The entire point of the Hello World program is to introduce some usage of the language in as minimal an example as possible.  For this tutorial, our Hello World will be creating a new MonoGame project and running it.  

## Creating a New Project
To get started, we first need to create a new project. Follow the instructions below depending on if you ar using [Visual Studio 2022](#visual-studio-2022) or [Visual Studio Code](#visual-studio-code).

### Visual Studio 2022
To create a new MonoGame project in Visual Studio 2022:

1. Launch the Visual Studio 2022 application.
2. In the launch window, click the *Create New Project* button.
3. In the *Project Type* drop-down, choose *MonoGame* to filter the templates to only show MonoGame project templates.
4. Choose the *MonoGame Cross-Platform Desktop Application* project template, then click the *Next* button.
5. Enter a name for the project and choose a location to save it
6. Click the *Create* button.

After clicking *Create*, a new C# project will be created based on the MonoGame template we picked and opened automatically in Visual Studio 2022.

![New Project Created Visual Studio 2022](./images/chapter_03/vs.png)
**Figure 3-1:** *A new MonoGame project after being created in Visual Studio 2022.*

### Visual Studio Code
To create a new MonoGame project in Visual Studio Code:

1. Launch the Visual Studio Code application.
2. Expand the *Explorer* panel if not already expanded with `CTRL/CMD+SHIFT+E`.
3. CLick the *Create .Net Project* button.  (This button is not here by default, it's available because of the C# Dev Kit extension).
4. In the prompt that opens, type `MonoGame` to filter the project templates.
5. Choose the *MonoGame Cross-Platform Desktop Application* project template.
6. In the dialog box that appears, choose the location to save the project.
7. Enter a name for the project
8. Select *Create Project*.

After selecting *Create Project*, a new C# project will be created based on the MonoGame template chosen and opened automatically in Visual Studio Code.

![Figure 3-2: A new MonoGame project after being created in Visual Studio Code.](./images/chapter_03/vscode.png)
**Figure 3-2:** *A new MonoGame project after being created in Visual Studio Code.*

### Running The Game For The First Time
Congratulations, you've just created your very first MonoGame project above. We'll cover what each of these files generated are in the following chapters, for now let's run the project as is and see the results.  You can press the `F5` key on your keyboard to run the application in debug mode regardless of you are using Visual Studio 2022 or Visual Studio Code.

> [!TIP]
> - In Visual Studio 2022, you can also run the project by clicking the run button in the tool bar at the top, or by selecting *Debug > Start Debugging* from the top menu
> - In Visual Studio Code, you can also run the project by opening the *Solution Explorer* panel on the left, then right-clicking the project and selecting *Debug > Start New Instance*, or alternatively selecting *Run > Start Debugging* from the top menu.

> [!NOTE]
> The first time you build a MonoGame project, a dotnet restore will be executed, which will download the NuGet packages used by the MonoGame project.  If they've never been restored before, then they will be downloaded from the official NuGet feed.  One of the tool packages, The *dotnet-mgcb* package is ~400mb, so depending on your internet connection speed, it may take a moment for the first build to finish.  Once the packages have been downloaded for the first time, they are cached in your global NuGet directory, so subsequent builds and projects will not have to download them.

Be amazed, the default MonoGame cornflower blue game window

![Figure 3-3: The default MonoGame cornflower blue game window.](./images/chapter_03/cornflower_blue.png)  
**Figure 3-3:** *The default MonoGame cornflower blue game window.*


While there isn't much happening here visually, there is a lot going on behind the scenes that the MonoGame framework is handling for you.  When you ran the application, the game initialization started which initializes the graphics device and creates the game window.  It's also running the main game loop over and over to render the cornflower blue background at 60 frames per second.  Additionally, it is also managing polling input from the keyboard, mouse, and any connected gamepads for the game window.  You can test this yourself by pressed the `Esc` key on your keyboard and the game will exit.

If you were to set all of this up manually, it could take hours of research and thousands of lines of code written just to get the window rendering and input polling.  Instead, we can take advantage of this base starting point that MonoGame offers us to get started creating our game without having to worry about the lower level implementations like this.

> [!NOTE]
> Above we mentioned the main *game loop* running to render the game at 60fps.  Game applications work differently than a traditional desktop application like your web browser.  Desktop applications are event based, meaning once loaded, they do not do much at all while waiting for input from the user, and then it responds to that input event.  In games, things are always happening such as objects moving around like the player or particles.  To handle this, games implement a loop structure that runs continuously, first calling the Update method and then the Draw method, until it has been told to exit.


## Conclusion
In this chapter, we created a new MonoGame game project and ran it.  In the following chapters, we'll discuss the files that are generated when a new MonoGame project is created, dive into the *Game1.cs* file, and discuss the content pipeline.

[Go to Chapter 4: MonoGame Project Overview](./04_monogame_project_overview.md)
