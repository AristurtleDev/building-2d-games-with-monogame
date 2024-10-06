# Chapter 4: Designing the Game

- [Game Summary](#game-summary)
  - [Pitch](#pitch)
  - [Inspiration](#inspiration)
  - [Game Play Overview](#game-play-overview)
  - [Controls](#controls)
- [Technical Specifications](#technical-specifications)
  - [Target Systems](#target-systems)
  - [Resolution](#resolution)
  - [Images](#images)
- [User Interface Flow](#user-interface-flow)
  - [1. Splash Screen](#1-splash-screen)
  - [2. Title Screen](#2-title-screen)
  - [3. Options Screen](#3-options-screen)
  - [4. Gameplay Screen](#4-gameplay-screen)
  - [5. Pause Screen](#5-pause-screen)
  - [6. Game Over Screen](#6-game-over-screen)
- [See Also](#see-also)
- [Next](#next)

---  

Before jumping directly into coding a game, it's always best to start with designing an initial concept first.  These are sometimes referred to as a [Game Design Document (GDD)](https://en.wikipedia.org/wiki/Game_design_document).  A GDD is a **living document** is used to organize and describe the overall design of the game, from the systems and mechanics to the artwork direction and visual design.  Even for small games, having a GDD is helpful because it gives an overall view of the finished product and gives the developer a reference while developing the game.  As mentioned, it is a **living document** which means the information in it can change after the initial concept based on things we learn while developing.  

Below, we're going to create a simplified design document.  We'll then use this design document as a reference while developing the game in the subsequent chapters.


## Game Summary
### Pitch
MonoGameSnake is a recreation of the classic snake game.  Players control the snake by choosing its movement direction (up, down, left, or right), trying to collect the food items that increase the snake's length and player's score.  Colliding with the body of the snake ends the game.

### Inspiration
The inspiration for MonoGameSnake is the more known variant of snake that was originally developed for the Nokia 6110 cell phones


![Figure 4-0-1: Screenshot of Snake for the Nokia 6110.](./images/04-01/Snake-nokia-phone.jpg)  
**Figure 4-0-1:** *Screenshot of Snake for the Nokia 6110.*

In that version, the snake moves on a grid based system, one grid cell at a time, in the current chosen direction by the player.  However, in that version, the game ends not only when colliding with the body, but also when colliding with the walls.  The wall limitation will be removed in our version and the snake will wrap around the play area instead.

### Game Play Overview
- The snake itself will move continuously on a fixed timer in the direction input by the player. 
- Each movement, the snake will move in that direction one grid space at time.  
- The players goal is to navigate the snake towards the food item to eat it.
- Each time a food item is eaten, the score will increase, the length of the snake's body will increase, and a new food item will be spawn in a random location in the play area.
- If the snake reaches the edge of the play area, it should wrap around to appear on the opposite side instead of hte traditional wall death
- If the snake collides with its own body, then the game is over.

### Controls
The game will be controllable with both keyboard and gamepad. 

> [!NOTE]
> The gamepad mappings below are based on an Xbox controller scheme.


| Action       | Keyboard Key   | Gamepad Button        |
| ------------ | -------------- | --------------------- |
| **GamePlay** |                |
| Up           | W, Up-Arrow    | D-pad Up, Y Button    |
| Down         | S, Down-Arrow  | D-pad Down, A Button  |
| Left         | A, Left-Arrow  | D-pad Left, X Button  |
| Right        | D, Right-Arrow | D-pad Right, B Button |
| Pause        | Esc            | Start Button          |
| **Menus**    |                |
| Up           | Up-Arrow       | D-pad Up              |
| Down         | Down-Arrow     | D-pad Down            |
| Left         | Left-Arrow     | D-pad Left            |
| Right        | Right-Arrow    | D-pad Right           |
| Confirm      | Enter          | A Button              |

## Technical Specifications
### Target Systems
The game will be developed to target
- Windows
- MacOS
- Linux

### Resolution
The game will target a 1920x1080 resolution.  This was determined by looking at the [Steam Hardware Survey](https://store.steampowered.com/hwsurvey/Steam-Hardware-Software-Survey), as of September 2024, with 1920x1080 being the the most common display resolution.

### Images
Images will be designed at 64x64 pixels.


## User Interface Flow
The following defines the flow of the game and user interaction starting from the point of the initial launch of the game application.

![Figure 4-0-2: Game Flow.](./images/04-01/game-flow.png)  
**Figure 4-0-2:** *Game Flow.*

### 1. Splash Screen
When the game first launches, a splash screen is shown to players to show any branding or logos necessary. Players should be able to press an input to skip past splash screen.

### 2. Title Screen

The title screen will feature the game logo at the top center with three buttons below it. The three buttons should be
1. **Play Game**: When clicked, the screen should transition to the game play screen.
2. **Options**: When clicked, the options screen should be shown.
3. **Quit Game**: When clicked the game application should exit.

![Figure 4-0-3: Title Screen Sketch.](./images/04-01/title-screen.png)  
**Figure 4-0-3:** *Title Screen Sketch.*


### 3. Options Screen

The options screen will look similar to the title screen in that it features the game logo at the top center. Below the logo are the options to adjust volume, make the game full screen, and a button to return to the title screen.

![Figure 4-0-4: Options Screen Sketch.](./images/04-01/options-screen.png)  
**Figure 4-0-4:** *Options Screen Sketch.*


### 4. Gameplay Screen

The gameplay screen will contain the players current score in the top-left with a list of high scores below that.  The remaining screen content area on the right will be the game play board where the snake moves around to collect the item that increases score and length.

Since the height of the target resolution does not divide cleanly into the height of our image size of 64 pixels, the game area has been adjusted to 1536x1024 pixel, ensuring a clean grid of 24x16 cells.

![Figure 4-0-5: Gameplay Screen Sketch.](./images/04-01/gameplay-screen.png)  
**Figure 4-0-5:** *Gameplay Screen Sketch.*


### 5. Pause Screen

When the player presses the input to pause the game, the pause screen will appear.  This should hide the current gameplay area and replace it with a pause menu with the options to resume gameplay or quit to return to the title screen.

![Figure 4-0-6: Pause Screen Sketch.](./images/04-01/pause-screen.png)  
**Figure 4-0-6:** *Pause Screen Sketch.*


### 6. Game Over Screen

When the player meets the conditions for a game over, then the game over screen is shown. When this is shown, similar to the pause screen, the gameplay area is hidden and the menu is shown instead, with two buttons; one to retry and another to quit

![Figure 4-0-7: Game Over Screen Sketch.](./images/04-01/gameover-screen.png)  
**Figure 4-0-7:** *Game Over Screen Sketch.*


## See Also
- [Game Design Document | Wikipedia](https://en.wikipedia.org/wiki/Game_design_document)
  
## Next
Chapter 5 coming soon.
