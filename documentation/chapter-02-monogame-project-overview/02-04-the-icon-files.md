# Chapter 2-4: The Icon Files

In the project root directory are the *Icon.bmp* and `Icon.ico* icon files.  These are the icon files used to display the icon for the executable on the PC, the window title bar area, and the icon displayed in the task bar on Windows or docs on macOs.  

When a new MonoGame project is created, the icons by default are of the MonoGame logo.

![The default MonoGame logo icon included in a new MonoGame project](./images/02-01/icon.png)  
**Figure 2-4-1:** *The default MonoGame logo icon included in a new MonoGame project.*

If you want to customize the icons used for you game, you only need to replace these files in the project directory.  However, when replacing the files, ensure that your custom icon filenames are named exactly the same.  If you recall from [2-1: The csproj Project File](./02-02-the-csproj-project-file.md), it embeds the icons into the assembly using the following

```xml
<ItemGroup>
    <EmbeddedResource Include="Icon.ico" />
    <EmbeddedResource Include="Icon.bmp" />
</ItemGroup>
```

The MonoGame framework expects the icon files to be named exactly this when they are embedded into the assembly in order to load and display them.

## Next
- [2-5 The Program File](./02-05-the-program-file.md)
