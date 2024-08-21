# Chapter 6: The Content Pipeline

---
In [Chapter 3](./03_hello_world_a_crash_course_in_monogame.md) we got a quick look at using the *content pipeline* to add both images and audio assets to our game.  In this chapter, we'll explore the *content pipeline* in more detail, discuss the advantages of using it, and how the entire workflow executes from start to finish.

## What Is The Content Pipeline?
The *content pipeline* is an out-of-the-box workflow provided by the MonoGame framework for managing the various types of assets that go into your game.  These assets can include images uses for textures, audio files used for sound effects and/or songs, fonts, 3D models, and shaders.  However it's not limited to just these asset types.  The MonoGame framework provides projects that developers can use to create extensions for the pipeline for custom asset types not handled by default.  Since the *content pipeline* itself is a workflow, it's made up of different components used at design time, build time, and run time in your game project.  These components include

1. The *MonoGame Content Builder (MGCB)* tool which performs the compilation of the game assets added to the content project
2. The *MonoGame Content Builder Editor (MGCB Editor)* tool used to edit the content project to add the assets to compile and be included with the game build
3. The *MonoGame.Content.Builder.Tasks* package reference which is used to automate the building of content when you perform a project build and copying the compiled content to the project output directory
4. The *ContentManager class* use to load the compiled assets in game at runtime to use.

### MonoGame Content Builder
The *MonoGame Content Builder* is a tool that compiles the game assets added to your content project into *.xnb* binary encoded files that can then be loaded at runtime in game using the *ContentManager class*.  For it to know which assets to compile, it reads the contents of the *Content.mgcb* file from the game project.  
