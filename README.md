> [!CAUTION]
> This is currently a work in progress and is not finished.  Please come back later once finished.


# Building 2D Games with MonoGame
We are currently in an era of game development where the number of tools available to the average person is astounding.  The number of new game releases every week, especially in the indie to small studio section, continues to climb as the tools become easier to use and add more features.  Being in this age of game development, it's easy to look at successful games like Celeste and think "I can just use the same tools", then jump in without fully understanding the whole scope.  Game development, including the actual code, is an art form.  Much like any art form, having tools isn't enough.  You wouldn't expect to be handed a canvas, some paint, and a brush and then paint the Mona Lisa.  You would need to understand the tools your using, how the brush and canvas interact, the different techniques for brush strokes and paint mixing, and lots of practice.  Then when you've reached a level of comfort with the tools, you could attempt a Mona Lisa.

The documentation in this repository is intended to cover the concepts of game development using the MonoGame framework as our tool.  We'll take all the foundational aspects of game development and create a Snake game. Each chapter is broken into individual sections that introduce new concepts that we can build off of.  While we'll have created a Snake game by the end of this tutorial, that isn't the actual goal. The game is just a vehicle used to introduce the concepts.  The goal in the end is to have a solid foundation of 2D game development using MonoGame and to have created reusable modules that can be expanded and used in any future projects as a jump start.

Since we'll be using the MonoGame framework as our tool, this means we'll be using C# as the programming language.  Readers should have a foundational understanding of C# and be comfortable with concepts such as classes and objects. If you are entirely new to C# or programming in general, I recommend following the official [Learn C#](https://dotnet.microsoft.com/en-us/learn/csharp) tutorials provided by Microsoft. These free tutorials teach you programming concepts as well as the C# language.  Once you have a good foundation with C#, come back and continue here.

# Chapters
The following is a list of each chapter and its sections for this tutorial.  The chapters are meant to be read in the order below as each chapter will build off of concepts discussed in the previous one.

- [Chapter 1: Getting Started](./documentation/chapter-01-getting-started/01-00-getting-started.md)
  - [1-1: What Is MonoGame](./documentation/chapter-01-getting-started/01-01-what-is-monogame.md)
  - [1-2: Setting Up Your Development Environment](./documentation/chapter-01-getting-started/01-02-setting-up-your-development-environment.md)
  - [1-3: Hello World](./documentation/chapter-01-getting-started/01-03-hello-world.md)
- [Chapter 2: Crash Course](./documentation/chapter-02-crash-course/02-00-crash-course.md)
  - [2-1: MonoGame Project Overview](./documentation/chapter-02-behind-the-scenes/02-01-monogame-project-overview.md)
  - [2-2: The Game Class](./documentation/chapter-02-behind-the-scenes/02-02-the-game-class.md)
- [Chapter 3: The Content Pipeline](./documentation/chapter-03-the-content-pipeline/03-00-the-content-pipeline.md)
  - [3-1: MonoGame Content Builder](./documentation/chapter-03-the-content-pipeline/03-01-monogame-content-builder.md)
  - [3-2: MonoGame Content Builder Editor](./documentation/chapter-03-the-content-pipeline/03-02-monogame-content-builder-editor.md)
  - [3-4: MonoGame.Content.Builder.Tasks](./documentation/chapter-03-the-content-pipeline/03-03-mongoame-content.builder.tasks.md)
  - [3-4: The ContentManager Class](./documentation/chapter-03-the-content-pipeline/03-04-the-contentmanager-class.md)

# Acknowledgements

# Copyright and License
All documentation is copyright © 2024 Christopher Whitley and is provided for educational purposes only.  Permission is not granted to anyone for use of documentation for commercial reasons without express written consent by original copyright holder.

All source code in this repository that accompanies the documentation, but not including source code within the documentation is licensed under the Unlicense as follows

```
This is free and unencumbered software released into the public domain.

Anyone is free to copy, modify, publish, use, compile, sell, or distribute this 
software, either in source code form or as a compiled binary, for any purpose, 
commercial or non-commercial, and by any means.

In jurisdictions that recognize copyright laws, the author or authors of this 
software dedicate any and all copyright interest in the software to the public 
domain. We make this dedication for the benefit of the public at large and to 
the detriment of our heirs and successors. We intend this dedication to be an 
overt act of relinquishment in perpetuity of all present and future rights to 
this software under copyright law.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS BE 
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

For more information, please refer to <http://unlicense.org/>
```
