# Chapter 2-5: The Program File

This is the code file that contains the main entry point for the application. All C# application, regardless of being a MonoGame project or not, require a main entry point that specifies where code execution should start when the application runs.  This one provided by the MonoGame template very simple:

```cs
using var game = new Game1();
game.Run();
```

Here, a new instance of the `Game1` class is initialized within a `using` context, and then the `Run()` method is called to begin execution of the game. 

> [!NOTE]
> MonoGame projects use a [top-level statement](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/program-structure/top-level-statements) style for the *Program.cs* file.  Top-level statements allows you to write code without the boilerplate that is typically included to define the main entry point.  For example, traditionally with the boilerplate code, the *Program.cs* file would look like the following
> ```cs
> public class Program 
> {
>     public static void Main(string[] args)
>     {
>         using var game = new Game1();
>         game.Run();
>     }
> }
> ```
>
> Top-level statements were introduced in C# 10.

## See Also
- [Top-level statements - programs without Main methods](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/program-structure/top-level-statements)

## Next
- [Chapter 2-6: The Game1 File](./02-06-the-game1-file.md)
