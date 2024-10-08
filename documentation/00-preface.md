# Preface

- [Who This Is For](#who-this-is-for)
- [How This Documentation Is Organized](#how-this-documentation-is-organized)
- [Conventions Used in This Documentation](#conventions-used-in-this-documentation)
  - [Italics](#italics)
  - [Inline Code Blocks](#inline-code-blocks)
  - [Code Blocks](#code-blocks)
- [Next](#next)

--- 

I have been using MonoGame for the past several years (since 2017).  It was a time in my game development journey where I was looking for something that I had more control over.  I didn't want to spend the time to write a full game engine, but I also wanted to have more control than what the current engines at the time (i.e. Unity) offered.  At that time, there was a vast amount of resources available for getting started, but none of them felt like they fit a good beginner series.  Even now, the resources available still seem this way.  They either require the reader to have a great understanding of game development and programming, or they assume the reader has none and instead focuses on teaching programming more than teaching MonoGame.  Even still, some relied too heavily on third party libraries, others were simply very bare bones asking the reader to just copy and paste code without explaining the *what* of it all.

Since then, I have written various small self contained tutorials on different topics for MonoGame to try and give back to the community for those getting started.  I also participate regularly in the community discussion channels, answering questions and offering technical advice, so I'm very familiar with the topics and pain points that users get hung up on when first starting out.

My hopes with this documentation is to take the lessons I've learned, and what I've learned from helping others, to fill the gap that I wanted filled when I first started MonoGame.  To present using MonoGame in a straight forward way, introducing concepts and building off of them as we go along in a way that makes sense and is easy to follow.  

## Who This Is For
This documentation is meant to be an introduction to game development and MonoGame.  Readers should have a foundational understanding of C# and be comfortable with concepts such as classes and objects.

> [!NOTE]
> If you are just getting started with C# for the first time, I would recommend following the official [Learn C#](https://dotnet.microsoft.com/en-us/learn/csharp) tutorials provided by Microsoft.  These are free tutorials that teach you programming concepts as well as the C# languages.  Once you feel you have a good foundation with that, come back and continue here.


## How This Documentation Is Organized
This documentation will introduce game development concepts using the MonoGame framework while walking the reader through the development of a Snake clone.  The documentation is organized such that each chapter should be read sequentially, with each introducing new concepts and building off of the previous chapters.  

The first part of this documentation will walk the reader through an introduction to MonoGame and creating your first Hello World game.

| Chapter | Summary | 
|---|---|
| [*Chapter 01: What is MonoGame?*](./01-what-is-monogame.md) | Gives a brief history of MonoGame and takes the reader through the features and advantages of using MonoGame for game development. | 
| [*Chapter 02: Getting Started*](./02-getting-started.md) | Walks the reader through setting up their development environment for MonoGame development and creating their first MonoGame game application. |


In the second part of this documentation, we'll begin to develop our 2D game and creating a reusuable library for common tasks that can be used for future projects.

- [TODO: Add chapter list when finished]

## Conventions Used in This Documentation
The following conventions are used in this documentation

### Italics
*Italics* are used for emphasis, technical terms, and paths such as file paths including filenames and extensions.

### Inline Code Blocks  
`Inline code` blocks are used for methods, functions, and variable names when they are discussed with the body a text paragraph.

### Code Blocks
```cs
// Example Code Block
public void Foo() { }
```
Code blocks are used to show code examples with syntax highlighting

## Next
[Chapter 01: What is MonoGame?](./01-what-is-monogame.md)
