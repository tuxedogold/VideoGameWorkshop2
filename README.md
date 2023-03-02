# VideoGameWorkshop

### Set up your development environment
##### If at any point these instructions do not make sense, ***STOP*** and ask!

1) Go to ```https://github.com/tuxedogold/VideoGameWorkshop2```

2) Click the GREEN ```<> Code``` button

3) From the drop down list select ```download zip```

4) From the downloads folder, extract the zip into *C:\VideoGameWorkshop2*

5) Open an administrative command prompt
a) Press Windows key
b) Type ```cmd``` in the search field
c) Select *run as administrator*
d) In the dark window type ```cd C:\VideoGameWorkshop2``` and press enter to submit the command
f) It should now say: ```C:\VideoGameWorkshop2>```. 
Type ```powershell –ExecutionPolicy Bypass ./install_environment.ps1```
so that it looks like ```C:\VideoGameWorkshop2>powershell -ExecutionPolicy Bypass ./install_environment.ps1```. Press Enter.
###### DO NOT TOUCH ANYTHING 

If you see red text, let Mr. Gold know. Otherwise, you successfully installed your development environment at the end, when you see this at the bottom: 

> ---Completed Installation : ) ---
> C:\VideoGameWorkshop2>


### Run the starter pack
##### Inside C:\VideoGameWorkshop2 is a starter pack for building our game. 

1) Press the Windows key
2) Type ```VSCode``` and click *Open*
3) From the top menu, click on *File*
4) Click *Open Folder*
5) Navigate to C:\VideoGameWorkshop2 then click *Select Folder*
6) From the top menu, click *Terminal*
7) Click *New Terminal*
8) Test opening the sprite editor by typing ```.\sprite``` in the terminal and press enter. 
a) If a new window opens that says *MGCB Editor* in the top left corner, the sprite editor works. Ignore step b. 
b) If it says "Run dotnet tool restore" to make the "mgcb-editor" command available in red text, type
```dotnet tool restore``` then try ```.\sprite``` again.

### Try test running the game

Go back to VSCode where you typed ```.\sprite``` in step 8, but instead type ```.\play``` in the terminal and press enter. If you see two fish and can move with WASD it works! Press ```ESC``` to close. 

## Challenge 1: Change the red fish out for a new sprite
1) Press the Windows key
2) Type ```VSCode``` and click *Open*
3) From the top menu, click on *File*
4) Click *Open Folder*
5) Navigate to C:\VideoGameWorkshop2 then click *Select Folder*
6) From the top menu, click *Terminal*
7) Click *New Terminal*
8) Type ```./sprite``` to open the MGCB editor
9) In the MGCB editor top menu, click *File*
10) Click *Open...*
10) Navigate to C:\VideoGameWorkshop2\Content\Content
11) Click *Open*
12) Right click on the *img* folder in the Project box
13) Click *Add* then *Existing item...*
14) Select *TheNextRedFish.png* and click *Open*
15) From the top menu, click *Build* then *Rebuild*
16) In VSCode, under the *Explorer* *VideoGameWorkshop2* drop down, click *MyFirstGame.cs*
17) On line 44, change 

```        _otherFish = new Fish(Content.Load<Texture2D>("img/RedFish"),```

into 

```        _otherFish = new Fish(Content.Load<Texture2D>("img/TheNextRedFish"),```

You must type this exactly or it will not work. Notice how the *.png* file extension is assumed.

18) Press Crtl+S to save the file

19) Test run the game by typing ```.\play``` in the terminal and pressing enter. You should see a new fish in the game. Let's talk about the background.

## Challenge 2: Create more sprites of your choosing 
1) Change the sprite declaration (line 44) to a List of sprites 
```
List<Sprite> sprites = new List<Sprite>()
                    {
                        // ... add your sprites here seperated by commas!                
                    };

```
2) Change the update method to update all sprites in a loop
```
foreach( var sprite in sprites)
{
    sprite.Update();
}

```
3) Change the draw method to call draw on all sprites in a loop
```
foreach(var sprite in sprites)
{
    sprite.Draw(_spriteBatch);
}
```
4) Add all sprites to your list and remove the old references to the individual sprites

## Challenge 3: Win/Lose (Instructions at the level of Junior Developer)
Change the code to create win conditions and lose conditions.

In the update method of MyFirstGame, write an If statement that determines if you win and/or lose the game. Based on that result, set a boolean Win/Lose variable.

In the draw method of MyFirstGame, detect if Win/Lose and tell the player what happened by displaying a sprite that you made.

To make a sprite invisible, just move its location to far off the screen. When you want to show it to the player, move it into the screen.




