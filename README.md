# Preface

I have been using MonoGame for the past several years (since 2017). It was a time in my game development journey where I was looking for something that I had more control over. I didn't want to spend the time to write a full game engine, but I also wanted to have more control than what the current engines at the time (i.e. Unity) offered. At that time, there was a vast amount of resources available for getting started, but none of them felt like they fit a good beginner series. Even now, the resources available still seem this way. They either require the reader to have a great understanding of game development and programming, or they assume the reader has none and instead focuses on teaching programming more than teaching MonoGame. Even still, some relied too heavily on third party libraries, others were simply very bare bones asking the reader to just copy and paste code without explaining the _what_ of it all.

Since then, I have written various small self contained tutorials on different topics for MonoGame to try and give back to the community for those getting started. I also participate regularly in the community discussion channels, answering questions and offering technical advice, so I'm very familiar with the topics and pain points that users get hung up on when first starting out.

My hopes with this documentation is to take the lessons I've learned, and what I've learned from helping others, to fill the gap that I wanted filled when I first started MonoGame. To present using MonoGame in a straight forward way, introducing concepts and building off of them as we go along in a way that makes sense and is easy to follow.

## Who This Is For

This documentation is meant to be an introduction to game development and MonoGame. Readers should have a foundational understanding of C# and be comfortable with concepts such as classes and objects.

> NOTE
> If you are just getting started with C# for the first time, I would recommend following the official [Learn C#](https://dotnet.microsoft.com/en-us/learn/csharp) tutorials provided by Microsoft. These are free tutorials that teach you programming concepts as well as the C# languages. Once you feel you have a good foundation with that, come back and continue here.

## How This Documentation Is Organized

This documentation will introduce game development concepts using the MonoGame framework while walking the reader through the development of a Snake clone. The documentation is organized such that each chapter should be read sequentially, with each introducing new concepts and building off of the previous chapters.

> [!CAUTION]
> This is currently a work in progress and is not finished.

| Chapter                                                                            | Summary                                                                                                                                                                                                           |
| ---------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [_Chapter 01: What is MonoGame?_](./documentation/01-what-is-monogame.md)          | Gives a brief history of MonoGame and takes the reader through the features and advantages of using MonoGame for game development.                                                                                |
| [_Chapter 02: Getting Started_](./documentation/02-getting-started.md)             | Walks the reader through setting up their development environment for MonoGame development and creating their first MonoGame game application.                                                                    |
| [_Chapter 02: The Game1 File_](./documentation/03-the-game1-file.md)               | Gives an overview of the _Game1.cs_ file that is generated when creating a new MonoGame project.                                                                                                                  |
| [_Chapter 04: Working With Textures_](./documentation/04-working-with-textures.md) | Walks the read through loading textures in MonoGame both directly from file and using the content pipeline. Then go through methods of drawing the texture, including drawing subregions within a single texture. |
| _Chapter 05_                                                                       | (Coming soon ...)                                                                                                                                                                                                 |

In additional to the chapter documentation, supplemental documentation is also provided to give a more in-depth look at different topics with MonoGame. These are provided through the Appendix documentation below:

| Appendix                                                                                               | Summary                                                                                                                                                                |
| ------------------------------------------------------------------------------------------------------ | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [_Appendix 01: MonoGame Project Templates_](./documentation/appendix-01-monogame-project-templates.md) | Shows the different MonoGame project templates available and what each one is used for.                                                                                |
| [_Appendix 02: MonoGame Project Overview_](./documentation/appendix-02-monogame-project-overview.md)   | An in-depth overview of the standard MonoGame project created from a MonoGame project templates, including all files and the default contents generated for the files. |
| [_Appendix 03: The Content Pipeline_](./documentation/appendix-03-the-content-pipeline.md)             | An in-depth overview of the content pipeline workflow provided by the MonoGame framework and the individual components that make up the overall workflow.              |

## Conventions Used in This Documentation

The following conventions are used in this documentation

### Italics

_Italics_ are used for emphasis, technical terms, and paths such as file paths including filenames and extensions.

### Inline Code Blocks

`Inline code` blocks are used for methods, functions, and variable names when they are discussed with the body a text paragraph.

### Code Blocks

```cs
// Example Code Block
public void Foo() { }
```

Code blocks are used to show code examples with syntax highlighting

## MonoGame

If you ever have questions about MonoGame or would like to talk with other developers to share ideas or just hang out with us, you can find us in the various MonoGame communities below

* [Discord](https://discord.gg/monogame)
* [GitHub Discussions Forum](https://github.com/MonoGame/MonoGame/discussions)
* [Community Forums (deprecated)](https://community.monogame.net/)
* [Reddit](https://www.reddit.com/r/monogame/)
* [Facebook](https://www.facebook.com/monogamecommunity)

## Acknowledgements

## Copyright and License

All documentation is copyright © 2024 Christopher Whitley and is provided for educational purposes only. Permission is not granted to anyone for use of documentation for commercial reasons without express written consent by original copyright holder.

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
