# Chapter 3-5: Conclusion

In Chapter 3, we discussed the **The Content Pipeline** and the advantages of using it.  We also discussed the individual components that make up the workflow for the pipeline, including the [MonoGame Content Builder Editor (MGCB Editor)](./03-01-monogame-content-builder-editor.md), the [MonoGame Content Builder (MGCB)](./03-02-monogame-content-builder.md), the [MonoGame.Content.Builder.Tasks](./03-03-mongoame-content.builder.tasks.md), and the [*ContentManager class](./03-04-the-contentmanager-class.md).

All of these component provide an out-of-box workflow for adding and managing the assets used in your game.

In the next chapter, we'll use the foundation we've built through Chapter 1, 2, and 3 to start building our example game.

## Test your Knowledge
1. When choosing to exclude an asset in the MGCB Editor, is the file deleted from your computer?

    <details>

    <summary>Question 1 Answer</summary>

    > No, the file is not deleted from your computer.  It is only removed as an asset from the content project.

    </details><br />

2. Does the MGCB Editor auto save as you add and/or remove assets in the interface

    <details>

    <summary>Question 2 Answer</summary>

    > No, the MGCB Editor does not auto save, so users should ensure that they save after making changes by either choosing *File > Save* from the top menu, or clicking the Save icon in the toolbar.
    
    </details><br />

3. What is the purpose of the MGCB tool?

    <details>

    <summary>Question 3 Answer</summary>

    > The MGCB tool compiles the assets added to the content project into the *.xnb* files that can be loaded at runtime in your game.
    
    </details><br />

4. Do you need to copy the compiled assets from the content project output directory to your game project output directory?
   
    <details>

    <summary>Question 4 Answer</summary>

    > Normally, no.  A MonoGame project created using one of the MonoGame project templates will have a reference to the `MonoGame.Content.Builder.Tasks` NuGet.  One of the tasks performed by this NuGet reference is automatically copying the compiled assets from the content project output directory to your project output directory when performing a project build.

    </details><br />

5. In my Content folder, I have a directory named `Images`.  Inside this directory, I have an image file of a ball named `ball.png`.  I have added this file to my content project in the MGCB Editor and now want to load it in the game using the `ContentManager`.  When loading it, what value would I need to give for the **asset name** parameter to load the `ball.png` image.

    <details>

    <summary>Question 5 Answer</summary>

    > The **asset name** parameter should be the path to the asset to load, relative to the `ContentManager.RootDirectory`, minus the extension.  Since the `ContentManager.RootDirectory` is set to `Content` by default, and the image file was placed at the path `Content/Images/ball.png`, the **asset name** would be `Images/ball`.
    
    </details><br />

6. In this scenario, I need to unload two assets that were previously loaded using the `ContentManager`.  The **asset name** for these assets are `Images/ball` and `SoundEffects/bounce`.  Given this information, how can I unload these assets?
   
    <details>

    <summary>Question 6 Answer</summary>

    > These assets can be unloaded in one of two ways.
    >
    > - The first way would be to unload them individually using the `ContentManager.UnloadAsset` method
    >
    >     ```cs
    >     ContentManager.UnloadAsset("Images/ball");
    >     ContentManager.UnloadAsset("SoundEffects/bounce");
    >     ```
    >
    > - The second way would be to use the `ContentManager.UnloadAssets` method and supply a collection containing the **asset name** of both assets
    >
    >     ```cs
    >     ContentManager.UnloadAssets(new List<string>( "Images/ball", "SoundEffects/bounce"));
    >     ```
    
    </details><br />


# Next
- [Chapter 4: Designing Our Game](#)
