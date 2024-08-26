> [!CAUTION]
> THIS DOCUMENT IS CURRENTLY BEING REFACTORED


# 3-3: MonoGame.Content.Builder.Tasks

When you create a new project using the supplied MonoGame templates, there will be two NuGet package references added. The first will be the platform specific MonoGame Framework package, and the second will be the *MonoGame.Content.Builder.Task* package.

```xml
<ItemGroup>
    <PackageReference Include="MonoGame.Framework.DesktopGL" Version="3.8.2.1105" />
    <PackageReference Include="MonoGame.Content.Builder.Task" Version="3.8.2.1105" />
</ItemGroup>
```

This reference defines project build time tasks that automate the building of the content project files and then copying them to the final project build output.  It's important to note again that this task is **responsible for copying the compiled assets to the project build output**.  The *Content.mgcb* file itself is your **content project**, separate from your actual C# project (**.csproj**).  When the content project is built, the resulting *.xnb* files created are built to the location defined the *Content.mgcb* file, which will be */Content/bin/Platform/* by default.  The task will then copy those compiled *.xnb* assets from the content project output directory to the final project output directory where the game executable is created.
---

<div align="right"><table border=1><tr><td>Next Up</td></tr><tr><td>

[3-4: The ContentManager Class](./03-04-the-contentmanager-class.md)

</td></tr></table></div>
