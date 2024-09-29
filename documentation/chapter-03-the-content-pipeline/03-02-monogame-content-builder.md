# 3-2: MonoGame Content Builder

The **MonoGame Content Builder (MGCB)** is a tool provided by the MonoGame Framework that that compiles the game assets added to your content project into *.xnb* binary encoded files that can be loaded at runtime in your game using the [*ContentManager class*](https://docs.monogame.net/api/Microsoft.Xna.Framework.Content.ContentManager.html).  

The tool itself is distributed via NuGet as a [dotnet tool](https://learn.microsoft.com/en-us/dotnet/core/tools/global-tools).   We previously saw it listed in [the dotnet tools manifest file](../chapter-02-monogame-project-overview/02-03-the-dotnet-tools-manifest-file.md) in Chapter 2.  To execute the tool manually, you can enter the following command in a terminal from your game project root directory

```sh
dotnet mgcb [file_path]
```

The `[file_path]` is the path to the *Content.mgcb* file to give the MGCB to process. 

As we'll see in the next chapter, the MonoGame Framework provides a task for you that will execute the command automatically ensuring that your content is built when running or building your game.  It's rare that you would need to call this command manually yourself, but if you find yourself in a situation where you do, you can refer to the official [MonoGame Content Builder (MGCB)](https://docs.monogame.net/articles/getting_started/tools/mgcb.html) documentation.


## See Also
- [MonoGame Content Builder (MGCB) | MonoGame](https://docs.monogame.net/articles/getting_started/tools/mgcb.html)
- [MonoGame Content Builder Editor (MGCB Editor) | MonoGame](https://docs.monogame.net/articles/getting_started/tools/mgcb_editor.html)

## Next
- [Chapter 3-3: MonoGame Content Builder Task](./03-03-mongoame-content.builder.tasks.md)
