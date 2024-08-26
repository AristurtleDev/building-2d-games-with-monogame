# Chapter 2-7: Conclusion

In Chapter 2, we discussed an overview of a typical MonoGame project created using one of the provided MonoGame project templates.  We touched on each file generated as part of a new project and the contents of each file.  Finally, we took a look at the heart of every MonoGame application, the *Game1.cs* file and discussed the order of execution for the methods when a MonoGame application runs.

In the next chapter, we'll dive into the *content pipeline* and the advantages to using it when managing the assets for your MonoGame project.

## Test your Knowledge
1. What is the purposed of the `<PackageReference>` tags found in the *\*.csproj* project file and which two packages are typically included for a **MonoGame Cross-Platform Desktop Application** project?

    <details>

    <summary>Question 1 Answer</summary>

    > The `<PackageReference>` tags are used to add NuGet package references to the project.  For a typical MonoGame project, the following packages are included
    >
    > 1. **MonoGame.Framework.\***: This package contains the MonoGame framework code specific for the platform being targeted. For OpenGL projects it will be the **MonoGame.Framework.DesktopGL** package, and for DirectX projects it will be the **MonoGame.Framework.WindowsDX** package.
    > 2. **MonoGame.Content.Builder.Tasks**: This package includes tasks that are executed during the build process of the project.  It is responsible for automating the building of assets added to the content project and copying the compiled assets to the project build directory.

    </details><br />

2. What are the three sections of the *Content.mgcb* file

    <details>

    <summary>Question 2 Answer</summary>

    > The three sections of the *Content.mgcb* file are:
    > 1. Global Properties Section
    > 2. References Section
    > 3. Content Section
    
    </details><br />

3. What version should be used for the tools listed in the *dotent-tools.json* manifest file?

    <details>

    <summary>Question 3 Answer</summary>

    > The version of each tool should match the version of MonoGame being used by the project.
    
    </details><br />

4. When replacing the icon files, can the name of the files be changed?
   
    <details>

    <summary>Question 4 Answer</summary>

    > No, the names of the icon files must match the original names of *Icon.bmp* and *Icon.ico*.  This is because they are embedded into the assembly when the project is built and the MonoGame framework expects the embedded resources to be named exactly like these.
    
    </details><br />

5. What is the purpose of the *Program.cs* file?

    <details>

    <summary>Question 5 Answer</summary>

    > The *Program.cs* file contains the main entry point for the MonoGame application, specifying where code execution should start when the application runs.
    
    </details><br />

6. If I need to create an object for my game during the `Initialize()` method, but the creation of this object depends on properties of an asset loaded during `LoadContent()`, at which point in the `Initialize()` method should I create the object?
   
    <details>

    <summary>Question 6 Answer</summary>

    > The object should be created **after** the call to `base.Initialize()` is made.  This is because the `LoadContent()` method is called during the `base.Initialize()` call.  If an object depends on properties of content that will be loaded, and the object is created before `base.Initialize()`, then the content item won't be loaded yet, which can lead to an exception being thrown.
    
    </details><br />


# Next
- [Chapter 3: The Content Pipeline](../chapter-03-the-content-pipeline/03-00-the-content-pipeline.md)
