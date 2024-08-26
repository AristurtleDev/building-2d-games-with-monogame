> [!CAUTION]
> THIS DOCUMENT IS CURRENTLY BEING REFACTORED

 
# 3-2: MonoGame Content Builder Editor

The *MonoGame Content Builder Editor (MGCB Editor)* is a tool that provides a graphical user interface for managing the assets to add to your game.  We briefly used this in [Chapter 03](./03_hello_world_a_crash_course_in_monogame.md) to add the images and audio content to the prototype game we created.  

![The MonoGame Content Builder Editor (MGCB Editor)](./images/chapter_06/mgcb-editor.png)  
**Figure 6-1:** *The MonoGame Content Builder Editor (MGCB Editor).*

The editor itself has three panels to make note of
1. **Project Panel**: The project panel is located in the upper-left side of the windows. Here you will see a tree node view of all the assets added to the the content project. The top node, **Content** represents the content project itself.  When selecting any item from the project panel, the properties for that item will appear in the properties panel.
2. **Properties Panel**: The properties panel displays the properties of the current item selected in the project panel.  In the Figure 6-1 above, we can see that the *Content* node is selected, and the properties panel is showing the properties of that node.  You may also notice that the properties here are the same properties that we saw above inside the *Content.mgcb* file.
3. **Build Output Panel**: The build output panel will display the results of performing a build of the current content project.  If there are any errors that occur during the content build process, you can view them here to see the exception message to further determine the cause of the failure.

Ultimately what this tool does is modify the *Content.mgcb* file for you based on the content items and properties you set inside the editor.  This provides a more manageable and visual way to define the assets to build for the game project.

> [!CAUTION]
> The MGCB Editor only applies the changes made to the *Content.mgcb* file when you tell it to save.  It does not perform any type of auto-saving and will not warn you about saving if you exit without saving first.  Ensure that after making changes you perform a save either by using the save icon in the tool bar or by selecting *File > Save* in the top menu.

---

<div align="right"><table border=1><tr><td>Next Up</td></tr><tr><td>

[3-3: MonoGame.Content.Builder.Tasks](./03-03-mongoame-content.builder.tasks.md)

</td></tr></table></div>
