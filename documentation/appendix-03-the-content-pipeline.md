# Appendix 03: The Content Pipeline

- [Why Use The Content Pipeline?](#why-use-the-content-pipeline)
- [MonoGame Content Builder Editor (MGCB Editor)](#monogame-content-builder-editor-mgcb-editor)
  - [Using the MGCB Editor](#using-the-mgcb-editor)
    - [Toolbar](#toolbar)
    - [Project Panel](#project-panel)
    - [Properties Panel](#properties-panel)
    - [Build Output Panel](#build-output-panel)
    - [Adding New Content Items](#adding-new-content-items)
    - [Adding Existing Content Items](#adding-existing-content-items)
    - [Excluding Content Items](#excluding-content-items)
    - [Organizing Content](#organizing-content)
    - [Saving Changes](#saving-changes)
    - [Building Content](#building-content)
- [MonoGame Content Builder Tool (MGCB Tool)](#monogame-content-builder-tool-mgcb-tool)
- [MonoGame.Content.Builder.Tasks](#monogamecontentbuildertasks)
  - [1. CollectContentReferences Task](#1-collectcontentreferences-task)
  - [2. PrepareContentBuilder Task](#2-preparecontentbuilder-task)
  - [3. RunContentBuilder Task](#3-runcontentbuilder-task)
  - [4. IncludeContent Task](#4-includecontent-task)
- [ContentManager Class](#contentmanager-class)
  - [Loading Assets](#loading-assets)
  - [ContentManager Cache](#contentmanager-cache)
  - [Unload Assets](#unload-assets)
- [Conclusion](#conclusion)
- [Test Your Knowledge](#test-your-knowledge)

---

The MonoGame framework provides an out-of-box workflow for managing game assets, preprocessing them, and loading them in game.  This workflow is commonly referred to as the *content pipeline*.  The content pipeline is not a single thing, instead it is composed of a set of tools and utilities that create the workflow.  This workflow provides the following:

1. Compiling your source file assets to an internal format that is optimized for the platform(s) your game is targeting.
2. Copying the compiled assets to the game project's build directory when you perform a project build.
3. Loading the compiled assets at runtime.

The tools and utilities that make up the content pipeline are:

1. The *MonoGame Content Builder Editor (MGCB Editor)* - A tool used to provide a graphical user interface to edit the *Content.mgcb* content project file in your game project.
2. The *MonoGame Content Builder Tool (MGCB Tool)* - A tool that performs the compilation of the source assets defined in the *Content.mgcb* content project file.
3. The *MonoGame.Content.Builder.Tasks* - A NuGet package reference which contains tasks to automate building the content project using the MGCB Tool and then copying the compiled content to your game projects build directory.
4. The *ContentManager* class - A class provided in code by the MonoGame framework used to load the compiled assets in your game at runtime.

## Why Use The Content Pipeline?
The content pipeline isn't a requirement when developing games with MonoGame.  Developers can choose to instead load the source asset files directly using the various `FromFile` methods.  When using hte content pipeline, however, the source asset files are compiled into an optimized format for the platform(s) targeted by the game.  For instance, when an image file is loaded as a texture in your game, the data for that image file has to be sent to the graphics processing unit (GPU) and stored in memory there.  The GPU doesn't compressed formats like PNG and JPEG, instead it has to be decompressed from those formats into raw bytes as a format the GPU understands.  By pre-processing the image files using the content pipeline, they can be compressed using a format that the GPUs of the target platform(s) understand.  For instance, on desktop platforms, the image file can be compressed using [DXT compression](https://en.wikipedia.org/wiki/S3_Texture_Compression), which the GPU understands without having to decompress first reducing the overall memory footprint of the game.

Another benefit of using the content pipeline is the *ContentManager* class provided by the MonoGame framework.  This class is used to load the compiled assets in your game at runtime.  The content manager itself will also cache the asset the first time it's loaded from disk, meaning any subsequent calls to load the asset will return the already read cached data instead of having to perform another disc read.  This can decrease loading times for commonly used assets.

## MonoGame Content Builder Editor (MGCB Editor)
The *MonoGame Content Builder Editor (MGCB Editor)* is a tool provided by the MonoGame framework with a graphical user interface (GUI) for managing the assets to add to your game.

![Figure A3-1: The MonoGame Content Builder Editor (MGCB Editor).](./images/appendix-03-the-content-pipeline/mgcb-editor.png)  
**Figure A3-1:** *The MonoGame Content Builder Editor (MGCB Editor).*

WHen changes are made in the MGCB Editor and then saved, those changes are written to the *Content.mgcb* content project file that is part of your MonoGame project.  

The tool itself is distributed via NuGet as a [dotnet tool](https://learn.microsoft.com/en-us/dotnet/core/tools/global-tools).  You can see the tool listed the *dotnet-tools.json* dotnet tools manifest file that is part of every MonoGame project as shown in [Appendix 02](./appendix-02-monogame-project-overview.md#the-dotnet-toolsjson-tools-manifest-file).  

### Using the MGCB Editor
To use the MGCB Editor, you first need to launch the tool application.  There are a few ways to do this depending on your environment setup.

1.  When using Visual Studio Code (VSCode), there is a third-party extension created by MonoGame community member r88 canned [*MonoGame for VSCode*](https://marketplace.visualstudio.com/items?itemName=r88.monogame).  While not an official extension from the MonoGame foundation, the developers regularly use it and support its recommendation.  Using this extension will add an icon at the top of code files in a MonoGame project that can be clicked to open the *Content.mgcb* file of the MonoGame project in the MGCB Editor. It also provided *Command Palette* options to launch the MGCB Editor.
2.  When using Visual Studio 2022, MonoGame offers an official extension that can be installed.  The extension is named [*MonoGame Framework C# project templates*](https://marketplace.visualstudio.com/items?itemName=MonoGame.MonoGame-Templates-VSExtension).  Along with installing the MonoGame C# project templates, it also allows developers to open the *Content.mgcb* file from their project in the MGCB Editor by simply double-clicking the file in the *Solution Explorer* panel.
3.  Regardless of editors and/or operating system, the tool can also be opened using the dotnet command line interface (CLI).  To do this, open a command prompt or terminal window in the same directory as the MonoGame project's *.csproj* file and enter the following command
   
    ```sh
    dotnet mgcb-editor ./Content/Content.mgcb
    ```
#### Toolbar
The toolbar is located at the top of the editor window and contains shortcut icons that can be used to perform different tasks.


![Figure A3-2: The MonoGame Content Builder Editor (MGCB Editor) Toolbar.](./images/appendix-03-the-content-pipeline/mgcb-editor-toolbar.png)  
**Figure A3-2:** *The MonoGame Content Builder Editor (MGCB Editor) Toolbar.*

From left-to-right, the icons on the tool bar are:

| Icon | Name | Description |
| --- | --- | --- |
| ![Create New Project Icon](./images/appendix-03-the-content-pipeline/new-icon.png) | *Create New Project* | Creates a new *Content.mgcb* content project |
| ![Open Existing Project Icon](./images/appendix-03-the-content-pipeline/open-existing-icon.png) | *Open Existing Project* | Opens an existing *Content.mgcb* content project file. |
| ![Save Current Project Icon](./images/appendix-03-the-content-pipeline/save-icon.png) | *Save Current Project* | Saves the current project, writing ot the *Content.mgcb* content project file that is open in the editor. |
| ![Undo Last Action Icon](./images/appendix-03-the-content-pipeline/undo-icon.png) | *Undo Last Action* | Performs an undo on the last action performed. |
| ![Redo Action Icon](./images/appendix-03-the-content-pipeline/redo-icon.png) | *Redo Action* | Performs a redo of the last undo action performed. |
| ![Add New Item Icon](./images/appendix-03-the-content-pipeline/add-new-item-icon.png) | *Add New Item* | Open the *Add New Item* dialog to create and add a new item to the current selected node in the **Project Panel**. |
| ![Add Existing Item Icon](./images/appendix-03-the-content-pipeline/add-existing-item-icon.png) | *Add Existing Item* | Adds an existing item to the current selected node in the *Project Panel*. |
| ![Add New Folder Icon](./images/appendix-03-the-content-pipeline/add-new-folder-icon.png) | *Add New Folder* | Adds a new folder to the current selected node in the *Project Panel*. |
| ![Add Existing Folder Icon](./images/appendix-03-the-content-pipeline/add-existing-folder-icon.png) | *Add Existing Folder* | Adds an existing folder, and all files within that folder, to the current selected node in the **Project Panel**. |
| ![Build Content Project Icon](./images/appendix-03-the-content-pipeline/build-icon.png) | *Build Content Project* | Performs a build of the current content project.  Will only build content that hasn't already been built if a build was previously performed. |
| ![Rebuild Content Project Icon](./images/appendix-03-the-content-pipeline/rebuild-icon.png) | *Rebuild Content Project* | Performs a rebuild of the current content project which first cleans all previous built content then performs a full build of all content. |
| ![Clean Content Project Icon](./images/appendix-03-the-content-pipeline/clean-icon.png) | *Clean Content Project* | Cleans all previously built content. |
| ![Cancel Build Icon](./images/appendix-03-the-content-pipeline/cancel-build-icon.png) | *Cancel Build* | Stops the current build in progress. Only available if a build is currently being performed. |

#### Project Panel
Below the toolbar on the left side of the MGCB Editor window is the *Project Panel*.

![Figure A3-3The MonoGame Content Builder Editor (MGCB Editor) Project Panel](./images/appendix-03-the-content-pipeline/project-panel.png)  
**Figure A3-3:** *The MonoGame Content Builder Editor (MGCB Editor) Project Panel.*

The *Project Panel* provides a tree node view of all assets added to the content project.  The top node, *Content*, represents the content project itself.  Right-clicking on a node will open a context menu that is specific to that node type; content project, folder, or file.  When selecting any node from the Project Panel, the properties available for that node node item will appear in the *Properties Panel* below it.

#### Properties Panel
Below the *Project Panel* on the left side of the MGCB Editor window is the *Properties Panel*.

![Figure A3-4: The MonoGame Content Builder Editor (MGCB Editor) Properties Panel](./images/appendix-03-the-content-pipeline/properties-panel.png)  
**Figure A3-4:** *The MonoGame Content Builder Editor (MGCB Editor) Properties Panel.*

THe *Properties Panel* contains configurable properties for the current selected node in the *Project Panel*.  The available properties will differ depending on the node item type selected and which *Processor* is selected for that node item.  In Figure A3-2 above, the *image.png* file node is selected, so we see the properties available for an image file.  

To view all available properties based on the node item type and processor selected, refer to the [Built-in Content Importers and Processors](https://docs.monogame.net/articles/getting_started/content_pipeline/using_mgcb_editor.html#built-in-content-importers-and-processors) section of the official MonoGame documentation.

#### Build Output Panel
The *Build Output Panel* is located on the right side of the MGCB Editor window.

![Figure A3-5: The MonoGame Content Builder Editor (MGCB Editor) Build Output Panel](./images/appendix-03-the-content-pipeline/build-panel.png)  
**Figure A3-5:** *The MonoGame Content Builder Editor (MGCB Editor) Build Output Panel.*

The *Build Output Panel* displays the results of building the assets currently added to the content project.  If there are any issues building assets, you can view the error message here to determine the cause and how to resolve it.  For example, in Figure A3-5 above, there was an exception thrown when attempting to build the *image.png* asset file.

#### Adding New Content Items
To add new content items to the content project, select a node in the *Project Panel* and click the *Add New Item* icon from the toolbar.  Alternatively, you can also right-click the node and choose *Add > New Item...* from the context menu.  Doing these will open the *New File* dialog box.

![Figure A3-6: The MonoGame Content Builder Editor (MGCB Editor) Build New File Dialog](./images/appendix-03-the-content-pipeline/new-file-dialog.png)  
**Figure A3-6:** *The MonoGame Content Builder Editor (MGCB Editor) New File Dialog.*

As shown in Figure A3-6 above, this will display the built-in content item types that can be added.  Select the item type you wish to add, give it a name, then click the *Create* button to create the content item and add it to the content project.

#### Adding Existing Content Items
To add existing content items to the content project, select a node in the *Project Panel* and click the *Add Existing Item* icon from the toolbar.  Alternatively, you can also right-click the node and choose *Add > Existing Item...* from the context menu.  Doing these will open a file dialog chooser window where you can navigate to the existing item you want to add and select it.

> [!TIP]
> You can select multiple files to add in one go.

Once you have selected the item, you will be presented with the *Add File Dialog* containing the following options:

![Figure A3-7: The MonoGame Content Builder Editor (MGCB Editor) Build Add File Dialog](./images/appendix-03-the-content-pipeline/add-file-dialog.png)  
**Figure A3-7:** *The MonoGame Content Builder Editor (MGCB Editor) Add File Dialog.*

- *Copy the file to the directory*: This will create a copy of the file you selected and place that copy inside the content directory.  Changes to the original file will not be reflected in the copy made.
- *Add a link to the file*: This will add a reference to the file you selected instead of creating a copy.  Changes to the original file will be reflected in content builds.  The reference is added as a relative path, relative to the *Content.mgcb* content project file.  This means if the source file or the *Content.mgcb* file are moved, the reference can break and will need to be re-added.
- *Skip adding the file*: This will skip adding the current file shown.  This is useful when selecting multiple files to add.

Choose the option that best fits your development environment and click the *Add* button to finish adding the existing item to the content project.

#### Excluding Content Items
If there is a content item that has been added to the content project that no longer needs to be there, you can exclude it.  To do this, right-click the item node in the *Project Panel* and choose *Exclude From Project* in the context menu.

> [!NOTE]
> When excluding an item from the content project, it removes it from the content project itself.  It does not delete the actual file on your hard drive.

#### Organizing Content
Content items added to the content project can be organized by creating folders to place related content items in.  For instance, you can create an *images* folder to place your images in and an *audio* folder to place music and sound effects in.

To add a new folder to the content project, select either the *Content* node or an existing folder node and click the *Add New Folder* icon from the toolbar.  Alternatively, you can right-click the *Content node* or an existing folder node and choose *Add > New Folder...* from the context menu.

When you add a new folder to the content project, a folder will be created in teh the *Content* directory of your MonoGame project.  Folders, just like other content items, can be excluded from the content project.  Excluding a folder will exclude the folder and all content items contained within it.

One gotcha to organizing content in the MGCB Editor is that you cannot drag and drop content item nodes to reorganize them into different folders.  Instead, if you want ot move an existing content item to a different folder, you will need to:

1. Exclude the content item.
2. If hte item was copied ot the *Content* directory when added, you will need to physically move the item into teh folder using the file explorer.
3. Add the item to the desired folder node in the MGCB Editor.

#### Saving Changes
While working inside the MGCB Editor, changes performed are only saved when you click teh *Save* icon on the tool bar or choose *File > Save* from the top menu.  The MGCB Editor does not auto-save for you.

If you attempt to close the editor when there are changes that have not been saved, you will be prompted if you would like to save those changes first or discard them.

#### Building Content
To perform a build of the content project, you can click the *Build* icon on the toolbar or select *Build > Build* from the top menu.  Performing this action will perform a build of all content items added ot the content project.

If instead you want to build a single content item, maybe due to checking for errors on that specific item, you can do so by right-clicking the content item node in the *Project Panel* and choose *Rebuild* from the context menu.

## MonoGame Content Builder Tool (MGCB Tool)
The *MonoGame Content Builder Tool (MGCB Tool)* is a tool provided by the MonOGame framework that compiles the game assets added to your *Content.mgcb* content project into *.xnb* binary encoded files.  These files can then be loaded at runtime in your game using the *ContentManager* class.  

The tool itself is distributed via NuGet as a [dotnet tool](https://learn.microsoft.com/en-us/dotnet/core/tools/global-tools).  You can see the tool listed the *dotnet-tools.json* dotnet tools manifest file that is part of every MonoGame project as shown in [Appendix 02](./appendix-02-monogame-project-overview.md#the-dotnet-toolsjson-tools-manifest-file).  

To execute the tool manually, you can do so by entering the following dotnet CLI command in a command prompt or terminal opened in the same directory as the MonoGame project's *.csproj* file and enter the following command

```sh
dotnet mgcb [file_path]
```

The `[file_path]` is a required argument and is the path to the *Content.mgcb* file that the MGCB Tool should process.

This is not something you would need to normally execute manually, as well see in the next section, the content pipeline workflow provides a utility that will automatically build the content project for you.

## MonoGame.Content.Builder.Tasks
The *MonoGame.Content.Builder.Tasks* is a NuGet package that is referenced in all new MonoGame projects that are created using the MonoGame project templates.  You can see it listed as a reference in the *.csproj* file as shown in [Appendix 02](./appendix-02-monogame-project-overview.md#the-csproj-project-file).  

This NuGet contains [MSBuild tasks](https://learn.microsoft.com/en-us/visualstudio/msbuild/msbuild-tasks?view=vs-2022) that will execute when you perform a build of your game project.  These tasks, in execution order are

1. *CollectContentReferences*: Identifies and organizes content project (*.mgcb*) files.
2. *PrepareContentBuilder*: Configures the environment and creates output directories.
3. *RunContentBuilder*: Executes the MGCB Tool to perform a build of the content projects collected by step 1.
4. *IncludeContent*: Includes the compiles .xnb files from step 3 in the MonoGame game project's build directory by copying them from the content project build directory.

The actual code defined within the tasks is not something you would ever need to edit yourself.  However, for full transparency, each task is broken down and shown in the following sections.

### 1. CollectContentReferences Task
The first task that is executed is the *CollectContentReferences* task.

```xml
<Target Name="CollectContentReferences">

<ItemGroup Condition="'$(EnableMGCBItems)' == 'true'">
    <MonoGameContentReference Include="**/*.mgcb" />
</ItemGroup>

<ItemGroup>

    <!-- Start with existing metadata. -->
    <ContentReference Include="@(MonoGameContentReference)">
    <Link>%(MonoGameContentReference.Link)</Link>
    <FullDir>%(MonoGameContentReference.RootDir)%(MonoGameContentReference.Directory)</FullDir>
    <ContentFolder>%(MonoGameContentReference.ContentFolder)</ContentFolder>
    <OutputFolder>%(MonoGameContentReference.OutputFolder)</OutputFolder>
    <OutputFolder Condition="'%(MonoGameContentReference.OutputFolder)' == '' ">%(MonoGameContentReference.Filename)</OutputFolder>
    </ContentReference>

    <!--
    Process intermediate metadata.
    Switch all back-slashes to forward-slashes so the MGCB command doesn't think it's trying to escape characters or quotes.
    ContentFolder will be the name of the containing folder (using the Link if it exists) so the directory structure of the included content mimics that of the source content.
    -->
    <ContentReference>
    <FullDir>$([System.String]::Copy("%(FullDir)").Replace('\','/'))</FullDir>
    <ContentFolder Condition="'%(ContentFolder)' == '' AND '%(Link)' != ''">$([System.IO.Path]::GetDirectoryName(%(Link)))</ContentFolder>
    <ContentFolder Condition="'%(ContentFolder)' == '' AND '%(Link)' == '' AND '%(RelativeDir)' != ''">$([System.IO.Path]::GetFileName($([System.IO.Path]::GetDirectoryName(%(RelativeDir)))))</ContentFolder>
    </ContentReference>

    <!-- Assemble final metadata. -->
    <ContentReference>
    <ContentDir>%(ContentFolder)/</ContentDir>
    <ContentOutputDir>%(FullDir)bin/$(MonoGamePlatform)/%(OutputFolder)</ContentOutputDir>
    <ContentIntermediateOutputDir>%(FullDir)obj/$(MonoGamePlatform)/$(TargetFramework)/%(OutputFolder)</ContentIntermediateOutputDir>
    </ContentReference>

</ItemGroup>

</Target>
```

This task will:

1. Search for all *.mgcb* content project files in the MonoGame game project and store them as *MonoGameContentReference* elements.
2. Populate metadata such as directories and output paths for each reference.
3. Ensure paths are formatted correctly for cross-platform builds

### 2. PrepareContentBuilder Task
After identifying the content to build, the next task to run is the *PrepareContentBuilder* task.

```xml
<Target Name="PrepareContentBuilder" DependsOnTargets="CollectContentReferences">

<PropertyGroup>
    <PlatformResourcePrefix Condition="'$(MonoGamePlatform)' == 'MacOSX'">$(MonoMacResourcePrefix)</PlatformResourcePrefix>
    <PlatformResourcePrefix Condition="'$(MonoGamePlatform)' == 'iOS'">$(IPhoneResourcePrefix)</PlatformResourcePrefix>
    <PlatformResourcePrefix Condition="'$(MonoGamePlatform)' == 'Android'">$(MonoAndroidAssetsPrefix)</PlatformResourcePrefix>
    <PlatformResourcePrefix Condition="'$(PlatformResourcePrefix)' != '' And !HasTrailingSlash('$(PlatformResourcePrefix)')">$(PlatformResourcePrefix)\</PlatformResourcePrefix>
    <PlatformResourcePrefix Condition="'$(PlatformResourcePrefix)' == ''"></PlatformResourcePrefix>
    <MonoGameMGCBAdditionalArguments Condition="'$(MonoGameMGCBAdditionalArguments)' == ''">/quiet</MonoGameMGCBAdditionalArguments>
</PropertyGroup>

<Error
    Text="The MonoGamePlatform property was not defined in the project!"
    Condition="'$(MonoGamePlatform)' == ''" />

<Warning
    Text="No Content References Found. Please make sure your .mgcb file has a build action of MonoGameContentReference"
    Condition="'%(ContentReference.FullPath)' == ''" />

<Warning
    Text="Content Reference output directory contains '..' which may cause content to be placed outside of the output directory. Please set ContentFolder on your MonoGameContentReference '%(ContentReference.Filename)' to enforce the correct content output location."
    Condition="$([System.String]::Copy('%(ContentReference.ContentDir)').Contains('..'))" />

<MakeDir Directories="%(ContentReference.ContentOutputDir)"/>
<MakeDir Directories="%(ContentReference.ContentIntermediateOutputDir)"/>

</Target>
```

This task will:

1. Configure platform-specific properties such as resource prefixes.
2. Ensure that the required platform, *MonoGamePlatform*, value is defined.
3. Create the output directories where the compiled content assets will be stored.

### 3. RunContentBuilder Task
Next is the *RunContentBuilder* Task

```xml
<Target Name="RunContentBuilder" DependsOnTargets="PrepareContentBuilder">

<!-- Remove this line if they make dotnet tool restore part of dotnet restore build -->
<!-- https://github.com/dotnet/sdk/issues/4241 -->
<Exec Command="&quot;$(DotnetCommand)&quot; tool restore" />

<!-- Execute MGCB from the project directory so we use the correct manifest. -->
<Exec
    Condition="'%(ContentReference.FullPath)' != ''"
    Command="&quot;$(DotnetCommand)&quot; &quot;$(MGCBCommand)&quot; $(MonoGameMGCBAdditionalArguments) /@:&quot;%(ContentReference.FullPath)&quot; /platform:$(MonoGamePlatform) /outputDir:&quot;%(ContentReference.ContentOutputDir)&quot; /intermediateDir:&quot;%(ContentReference.ContentIntermediateOutputDir)&quot; /workingDir:&quot;%(ContentReference.FullDir)&quot;"
    WorkingDirectory="$(MSBuildProjectDirectory)" />

<ItemGroup>
    <ExtraContent
    Condition="'%(ContentReference.ContentOutputDir)' != ''"
    Include="%(ContentReference.ContentOutputDir)\**\*.*">
    <ContentDir>%(ContentReference.ContentDir)</ContentDir>
    </ExtraContent>
</ItemGroup>

</Target>
```

This task will:
1. Perform a *dotnet restore* to ensure that all dotnet tools needed, such as the MGCB Tool, are available.
2. Execute the MGCB Tool for each *.mgcb* file found during the *CollectContentReferences* task.  This will compile the content defined in the *.mgcb* files into *.xnb* files and output them in the content project output directory.

### 4. IncludeContent Task
The final task to run is the *IncludeContent* task.

```xml
<Target
Name="IncludeContent"
DependsOnTargets="RunContentBuilder"
Condition="'$(EnableMGCBItems)' == 'true' OR '@(MonoGameContentReference)' != ''"
Outputs="%(ExtraContent.RecursiveDir)%(ExtraContent.Filename)%(ExtraContent.Extension)"
BeforeTargets="BeforeBuild">

<!-- Call CreateItem on each piece of ExtraContent so it's easy to switch the item type by platform. -->
<CreateItem
    Include="%(ExtraContent.FullPath)"
    AdditionalMetadata="Link=$(PlatformResourcePrefix)%(ExtraContent.ContentDir)%(ExtraContent.RecursiveDir)%(ExtraContent.Filename)%(ExtraContent.Extension);CopyToOutputDirectory=PreserveNewest"
    Condition="'%(ExtraContent.Filename)' != ''">
    <Output TaskParameter="Include" ItemName="Content" Condition="'$(MonoGamePlatform)' != 'Android' And '$(MonoGamePlatform)' != 'iOS' And '$(MonoGamePlatform)' != 'MacOSX'" />
    <Output TaskParameter="Include" ItemName="BundleResource" Condition="'$(MonoGamePlatform)' == 'MacOSX' Or '$(MonoGamePlatform)' == 'iOS'" />
    <Output TaskParameter="Include" ItemName="AndroidAsset" Condition="'$(MonoGamePlatform)' == 'Android'" />
</CreateItem>

</Target>
```

This task will:

1. Add includes for all build *.xnb* content files so that they are copied from the content project's build directory to the MonoGame game project's build directory.


## ContentManager Class
The final part of the content pipeline workflow is the *ContentManager* class.  This class is used at runtime in your game to load the content that was compiled and copied during the build process by the tasks defined in the *MonoGame.COntent.Builder.Tasks* NuGet.

The `Game` class that is inherited by the `Game1` class initializes a new instance of the *ContentManager* class during its constructor and makes it available as one of the inherited properties as shown in [Appendix 02](./appendix-02-monogame-project-overview.md#additional-properties).  

### Loading Assets
To load content using hte *ContentManager* class, execute the `ContentManager.Load<T>` method.  The `T` type parameter specifies which content type you are loading (e.g. `Texture2D`, `SpriteFont`, `SoundEffect`, etc).  The method itself has a single parameter which is the *asset name* of the content item to load.

The *asset name* is the path to the content item to load, minus any extensions, relative to the `ContentManager.RootDirectory` path.  In the `Game1` constructor, as shown in [Appendix 02](./appendix-02-monogame-project-overview.md#the-game1-constructor), the `ContentManager.RootDirectory` path is set to `"Content"` by default.  So if you had a content item to load that was in the directory */Content/images/ball.png*, the *asset name* you would provide would be `"images/ball".

Below are some examples of loading content using the *ContentManager* class:

```cs
Texture2D texture = Content.Load<Texture2D>("images/image");
Song music = Content.Load<Song>("audio/music/background_music");
SoundEffect sfx = Content.Load<SoundEffect>("audio/sfx/pickup_sfx");
```

### ContentManager Cache
When you load an asset for the first time using the *ContentManager* class, it will cache the asset internally.  By caching the asset, any subsequent calls to load that same asset later will return the cached version instead of having to perform a full disck read and deserialization of the content.

### Unload Assets
There may be times when you want to unload assets that were previously loaded by the *ContentManager* class.  For instance, a common scenario is to have a *ContentManager* instance that is used to load global asset and a separate instance used in a scene that only loads assets used by that scene.  When the scene ends, you may want to unload the assets that were loaded since they will no longer be used, reducing your memory footprint.

To unload assets, the *ContentManager* class provides three methods.  All three methods are demonstrated below:

```cs
//  Unloads a single asset with the asset name provided
Content.UnloadAsset(assetName);

//  Unloads multiple assets using the collection of asset names provided
Content.UnloadAssets(new List<string>() { asset1, asset2, asset3 });

//  Unloads all assets
Content.Unload();
```

## Conclusion
Here, we have discussed
- The *content pipeline* and the advantages of using it
- The individual components of the content pipeline and how they work together to automate pre-processing and compiling game assets to load in your game.

## Test Your Knowledge
1. When choosing to exclude an asset in the MGCB Editor, is the file deleted from your computer?

    <details>

    <summary>Question 1 Answer</summary>

    > No, the file is not deleted from your computer.  It is only removed as an asset from the content project.

    </details><br />

2. Does the MGCB Editor auto save as you add and/or remove assets in the interface

    <details>

    <summary>Question 2 Answer</summary>

    > No, the MGCB Editor does not auto save, so users should ensure that they save after making changes by either choosing *File > Save* from the top menu, or clicking the *Save* icon on the toolbar.
    
    </details><br />

3. What is the purpose of the MGCB tool?

    <details>

    <summary>Question 3 Answer</summary>

    > The MGCB tool compiles the assets added to the content project into the *.xnb* files that can be loaded at runtime in your game.
    
    </details><br />

4. Do you need to copy the compiled assets from the content project output directory to your game project output directory?
   
    <details>

    <summary>Question 4 Answer</summary>

    > Normally, no.  A MonoGame project created using one of the MonoGame project templates will have a reference to the *MonoGame.Content.Builder.Tasks* NuGet.  One of the tasks performed by this NuGet reference is automatically copying the compiled assets from the content project output directory to your project output directory when performing a project build.

    </details><br />

5. In my Content folder, I have a directory named *images*.  Inside this directory, I have an image file of a ball named *ball.png*.  I have added this file to my content project in the MGCB Editor and now want to load it in the game using the *ContentManager* class.  When loading it, what value would I need to give for the *asset name* parameter to load the *ball.png* image.

    <details>

    <summary>Question 5 Answer</summary>

    > The *asset name* parameter should be the path to the asset to load, relative to the `ContentManager.RootDirectory`, minus the extension.  Since the `ContentManager.RootDirectory` is set to `Content` by default, and the image file was placed at the path `Content/images/ball.png`, the *asset name* would be `images/ball`.
    
    </details><br />

6. In this scenario, I need to unload two assets that were previously loaded using the *ContentManager* class.  The *asset name* for these assets are `images/ball` and `audio/sfx/bounce`.  Given this information, how can I unload these assets?
   
    <details>

    <summary>Question 6 Answer</summary>

    > These assets can be unloaded in one of two ways.
    >
    > - The first way would be to unload them individually using the `ContentManager.UnloadAsset` method
    >
    >     ```cs
    >     ContentManager.UnloadAsset("images/ball");
    >     ContentManager.UnloadAsset("audio/sfx/bounce");
    >     ```
    >
    > - The second way would be to use the `ContentManager.UnloadAssets` method and supply a collection containing the *asset name* of both assets
    >
    >     ```cs
    >     ContentManager.UnloadAssets(new List<string>( "images/ball", "audio/sfx/bounce"));
    >     ```
    
    </details><br />
