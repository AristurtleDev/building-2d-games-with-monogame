# 3-3: MonoGame.Content.Builder.Tasks

- [1. Collecting MonoGame Content References](#1-collecting-monogame-content-references)
- [2. Preparing the Content Builder](#2-preparing-the-content-builder)
- [3. Running the MonoGame Content Builder (MGCB) Tool](#3-running-the-monogame-content-builder-mgcb-tool)
- [4. Including Content in the Build](#4-including-content-in-the-build)
- [See Also](#see-also)
- [Next](#next)

---

**MonoGame.Content.Builder.Tasks** is a NuGet package that is part of all new MonoGame projects that are created using the MonoGame project templates.  We saw it listed in the [csproj file references](../chapter-02-monogame-project-overview/02-01-the-csproj-project-file.md) in Chapter 2.

This NuGet contains [MSBuild tasks](https://learn.microsoft.com/en-us/visualstudio/msbuild/msbuild-tasks?view=vs-2022) that will execute when you do a build of your game project.  These tasks, in execution order, are

1. `CollectContentReference`: Identifies and organizes content files (*.mgcb*).
2. `PrepareContentBuilder`: Configures the environment and creates output directories.
3. `RunContentBuilder`: Executes the [MonoGame Content Builder (MGCB)](./03-02-monogame-content-builder.md) tool.
4. `IncludeContent`: Includes the built content *.xnb* files in the project output.

The actual code defined within the tasks is not something you would ever need to edit yourself.  However, for full transparency, each task is broken down and shown in the sections below.

## 1. Collecting MonoGame Content References
The first task that is executed is the `CollectContentReferences` task.  This task will identify and organize all the *.mgcb* files in your project

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

### What it does
- Searches for all *.mgcb* files and stores them as `MonoGameContentReference` elements.
- Populates metadata (like directories and output paths) for each reference.
- Ensures paths are formatted correctly for cross-platform builds

## 2. Preparing the Content Builder

After identifying the content, the next task to run is `PrepareContentBuilder`.  This task will prepare the build environment and perform validation checks for moving on to build the content.

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

### What it Does:
- Configures platform-specific properties such as resource prefixes.
- Ensures that the required platform, `MonoGamePlatform`, is defined.
- Creates the output directories where the compiled content *.xnb*'s will be stored.

## 3. Running the MonoGame Content Builder (MGCB) Tool

Once the environment has been prepared, the next task to execute is the `RunContentBuilder` task.  This task will invoke the [MonoGame Content Builder (MGCB)](./03-02-monogame-content-builder.md) tool to process the content of each *.mgcb* file.

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

### What it Does:
- Performs a `dotnet restore` to ensure that all dotnet tools needed, such as the MGCB tool, are available.
- Executes the MGCB dotnet tool for each *.mgcb* file, which will compile the content into *.xnb* files and output them in the content project output directory.

## 4. Including Content in the Build

Once the content has been built, the final task to run is the `IncludeContent` task.  

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

### What it Does:
- Includes all built *.xnb* assets into the final project output, ensuring they are copied or bundled based on platform.

## See Also
- [MonoGame.Content.Builder.Task.targets](https://github.com/MonoGame/MonoGame/blob/develop/Tools/MonoGame.Content.Builder.Task/MonoGame.Content.Builder.Task.targets)

## Next
- [Chapter 3-4: The ContentManager Class](./03-04-the-contentmanager-class.md)
