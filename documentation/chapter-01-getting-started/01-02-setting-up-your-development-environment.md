# 1-2: Setting Up Your Development Environment

- [Install the .NET SDK](#install-the-net-sdk)
  - [Windows](#windows)
  - [macOS](#macos)
  - [Linux](#linux)
- [Install Additional Workloads (Optional)](#install-additional-workloads-optional)
- [Install the MonoGame C# Templates](#install-the-monogame-c-templates)
- [Install Visual Studio Code](#install-visual-studio-code)
  - [Windows](#windows-1)
  - [macOS](#macos-1)
  - [Linux](#linux-1)
- [Install the C# Dev Kit Extension](#install-the-c-dev-kit-extension)
- [Setup WINE for Effect Compilation (macOS and Linux Only)](#setup-wine-for-effect-compilation-macos-and-linux-only)
  - [macOS](#macos-2)
  - [Linux](#linux-2)
- [Conclusion](#conclusion)
- [See Also](#see-also)
- [Next Steps](#next-steps)

--- 

Unlike game engines such as Godot, Unity, and Unreal, MonoGame takes a different approach to game development.  It does not come as a standalone program with a graphical user interface that you download, install, and use to create games.  Instead, MonoGame integrates into the standard C# development workflow, offering a code-first approach to game development.  This approach offers several advantages:

- **Flexibility**: Developers are not locked into using a specific editor or interface, allowing them to use the their preferred C# development tools.
- **Integration**: As a .NET library itself, MonoGame can easily integrate with other .NET libraries and tools.
- **Cross-platform development**: Since C# is cross-platform, and MonoGame is cross-platform, developers can develop MonoGame projects on Windows, macOS, or Linux, with only slight differences in the setup process for each operating system.
- **Version control friendly**: The code-first approach makes it easier to use version control systems like Git for your game projects.

While the environment setup process is similar to standard C# development, there are some MonoGame-specific steps.  These can vary slight depending on your operating system and integrated development environment (IDE).

Visual Studio Code is a light weight editor that can be used to develop many different types of applications.  Depending on the programming language you are using, it's just a matter of installing the correct extension to support that language. Visual Studio Code is also cross-platform, meaning you can install and use it for development on Windows, macOS, and Linux.  Due to it's free and cross-platform nature, we'll be using Visual Studio Code throughout this tutorial.

> [!NOTE]
> If you would prefer to use Visual Studio 2022 (Windows Only) or JetBrains Rider, you can view official documentation provided by MonoGame on setting these up
> - [Visual Studio 2022 (Windows Only)](https://docs.monogame.net/articles/getting_started/2_choosing_your_ide_visual_studio.html)
> - [JetBrains Rider](https://docs.monogame.net/articles/getting_started/2_choosing_your_ide_rider.html)
>
> The only differences when using this compared to Visual Studio Code is interactions within the editor interface itself.

## Install the .NET SDK
Before installing Visual Studio Code, the .NET SDK will need to be downloaded and installed.  To do this, following the instructions for your operating system below

### Windows
To install the .NET SDK for Windows:

1. Open a web browser and navigate to https://dotnet.microsoft.com/en-us/download.
2. Click the *Download .NET SDK x64* button to download the .NET SDK installer.
3. Once the download finishes, run the installer.

> [!WARNING]
>
> If you plan to target Windows only using the DirectX backend project type, you will also need to download and install [the DirectX June 2010 runtime](https://www.microsoft.com/en-us/download/details.aspx?id=8109) for audio and game pads to work properly.

### macOS
To install the .NET SDK for macOS:

1. Open a web browser and navigate to https://dotnet.microsoft.com/en-us/download
2. Click the *Download .NET SDK x64 (Intel)** button to download the .NET SDK installer.
    > [!NOTE]
    > For the time being, MonoGame requires that you install the *.NET SDK x64 (Intel)* version even if you are using an Apple Silicon (M1/M2/m#) Mac.  For Apple Silicon Macs, it also requires that [Rosetta](https://support.apple.com/en-us/HT211861) is enabled.

3. Once the download finishes, run the installer.

### Linux
To install the .NET SDK for Linux:
1. Open a new terminal window
2. Enter the following command to download the dotnet install script:
    ```sh
    wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
    ```
3. Grant the script permission to execute:
    ```sh
    chmod + x ./dotnet-install.sh
    ```
4. Execute the script to install the .NET SDK:
    ```sh
    ./dotnet-install.sh
    ```
5. Update `PATH` variables so the `dotnet` command is available to use in a terminal:
    ```sh
    echo 'export DOTNET_ROOT=$HOME/.dotnet' >> ~/.bashrc
    echo 'export PATH=$PATH:$DOTNET_ROOT:$DOTNET_ROOT/tools' >> ~/.bashrc
    ```
6. Re-source the terminal session so that the `PATH` variable is updated:
    ```sh
    source ~./bashrc
    ```

## Install Additional Workloads (Optional)
After installing the .NET SDK, if you intend to target mobile devices such as Android or iOS, you will also need to install the corresponding mobile workloads.  To do this, open a command prompt or terminal and enter the following commands

```sh
dotnet workload install ios
dotnet workload install android
```

## Install the MonoGame C# Templates
The .NET SDK installation provides the default C# project templates.  MonoGame provides additional templates that can be installed to create new projects configured to create games with MonoGame.  To install the MonoGame templates, open a command prompt or terminal and enter the following command

```sh
dotnet new install MonoGame.Templates.CSharp
```

## Install Visual Studio Code
To install VSCode, follow the instructions for your operating system below:

### Windows
To install Visual Studio Code on Windows, perform the following
1. Open a browser and navigate to https://code.visualstudio.com/.
2. Click the *Download for Windows* button to download the installer.
3. Once the download finishes, run the installer.

> [!TIP]
> When installing Visual Studio Code for windows, you will have the option to choose if you would like to add *Open with Visual Studio Code* to context menus for files and/or directories.  Checking these boxes are useful since it adds the option when right-clicking a file or directory to quickly open it in Visual Studio Code.

### macOS
To install Visual Studio Code on macOS, perform the following:

1. Open a web browser and navigate to https://code.visualstudio.com/.
2. Click the *Download for macOS* button to download the *.zip* archive.
3. Once the download finishes, double click the *.zip* archive to extract the *Visual Studio Code.app* application package.
4. Drag-and-drop the *Visual Studio Code.app* application package into your *Application directory to make it available in the macOS LaunchPad.

> [!TIP]
> After installing Visual Studio Code on macOS, open the application, then open the *Command Palette* by pressing `CMD+SHIFT+P`.  In the command palette type `shell command` and choose the *Shell Command: Install 'code' command in PATH* option.  
>
> Doing this will allow you to open Visual Studio Code from a terminal by using the `code` cli command.

### Linux
To install Visual Studio Code on Linux, perform the following:

1. Open a web browser and navigate to https://code.visualstudio.com/.
2. Click the *.deb* download button to download the package for Debian based Linux distributions, or the *.rpm* download button for Red Hat based Linux distributions.
3. Once the download finishes, open the package downloaded to install.

> [!TIP]
> After installing Visual Studio Code on Linux, open the application, then open the *Command Palette* by pressing `CTRL+SHIFT+P`.  In the command palette type `shell command` and choose the *Shell Command: Install 'code' command in PATH* option.  
>
> Doing this will allow you to open Visual Studio Code from a terminal by using the `code` cli command.

## Install the C# Dev Kit Extension
For C# development using Visual Studio Code, it's recommended to use the official *C# Dev Kit* extension provided by Microsoft.  Installing this extension will add additional features to Visual Studio Code such as a project system and solution explorer for C# projects, code editing features including syntax highlighting, code completion, code navigation, and refactoring, NuGet package management, and debugging tools.

To install the C# Dev Kit extension, perform the following:

1. Launch the Visual Studio Code application.
2. Open the *Extensions Panel* by clicking the icon in the *Activity Bar* on the left or choosing *View > Extensions* from the top menu.
3. Enter `C#` in the *Search Box*
4. Click install for the *C# Dev Kit* extension.

> [!NOTE]
> When you search `C#` in the *Extension Panel* you may notice there is the C# Dev Kit extension and a base standard C# extension.  When installing the C# Dev Kit extension, the base extension will also be installed as a requirement.

## Setup WINE for Effect Compilation (macOS and Linux Only)
Effect (shader) compilation requires access to DirectX. This means it will not work natively on macOS and Linux systems, but it can be used through [WINE](https://www.winehq.org/).  MonoGame provides a setup script that can be executed to setup the WINE environment.  Below you can find the steps based on your operating system. To do this, follow the instructions for your operating system below:

### macOS
To setup the WINE environment on macOS for effect compilation, perform the following:

1. Open a new terminal.
2. Execute the following command to install prerequisites:
    ```sh
    brew install p7zip curl
    brew install --cask wine-stable
    ```
3. Execute the following command to download the MonoGame WINE setup script and run it:
    ```sh
    wget -qO- https://monogame.net/downloads/net8_mgfxc_wine_setup.sh | bash
    ```

### Linux
To setup the WINE environment on Linux for effect compilation, perform the following:

1. Open a new terminal.
2. Execute the following command to install prerequisites:
    ```sh
    sudo apt install curl p7zip-full wine64
    ```
3. Execute the following command to download the MonoGame WINE setup script and run it:
    ```sh
    wget -qO- https://monogame.net/downloads/net8_mgfxc_wine_setup.sh | bash    
    ```

After performing these setups, regardless of macOS or Linux, a new directory called `.winemonogame` will be created in your home directory.  If you ever wish to undo the setup this script performed, you can just simply delete this directory.

## Conclusion
In this section, we installed the .NET SDK, MonoGame project templates, Visual Studio Code, and the C# Dev Kit extension for Visual Studio Code.  We're now ready to create a new MonoGame project.  In the next chapter, we'll go through the steps of creating your first MonoGame project.

## See Also
- [Setting Up Visual Studio on Windows | MonoGame](https://docs.monogame.net/articles/getting_started/2_choosing_your_ide_visual_studio.html)
- [Setting Up Your Development Environment for VSCode | MonoGame](https://docs.monogame.net/articles/getting_started/2_choosing_your_ide_vscode.html?tabs=windows)
- [Setting Up Your Development Environment for Rider | MonoGame](https://docs.monogame.net/articles/getting_started/2_choosing_your_ide_rider.html)

## Next Steps
- [1-3: Hello World](./01-03-hello-world.md)
