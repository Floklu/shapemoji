# ShapeMoji

## How to deploy the Game

### What you need

* PC with Windows 10 and a HDMI port
* 16 GB Hard Disk Space
* 4 GB RAM
* UnityHub with Unity 2019.4.13f1 (UnityHub suggests to download the appropriate version after you defined the repository to use)
* Gitbash
* Touch-Monitor with at least 12 Touchpoints and a HDMI port
* HDMI Cable

### Procedure

#### Clone the repository
Clone the repository from https://gitlab.hochschule-stralsund.de/spo_ws2020_21/shapemoji/shapemoji to an appropriate place using Gitbash: `git clone https://gitlab.hochschule-stralsund.de/spo_ws2020_21/shapemoji/shapemoji` or every other tool that you like for cloning repositories.
#### Add the repository to UnityHub
Open up UnityHub and go to "Projects". Use the ADD button to add the "src"-folder of the cloned repository as a project to UnityHub.
#### Open the project
By clicking on the added project called "src". Make sure you have chosen 2019.4.13f1 as "Unity Version".
#### Check the opened project
Go ahead and chose "Default" as layout options in the upper right corner for the purpose of this guide. Check the lower left corner for any exceptions. If the some exception appears just click on "Assets/Reimport All". If its a different exception or if the previous handling did not work you might want to search for the bug or continue none the less if you just want to build the project.
#### Build the project
In program menu select "File" > "Build Settings". In the new window "Build Settings" select "Build". In the new window "Build Windows" select a folder, to save the new Build.
#### Setup Touch-Monitor
Connect the PC to the Touch-Monitor with the HDMI Cable. Place the Touch-Monitor in the horizontal position for the best playing experience.
#### Run the game
Open File Explorer in the Start Menu and navigate to the folder with the saved build. Select ShapeMoji Application in the build folder to run the game.

## How to play

### Game Objective

The main goal of the game is to cover the emojis with stones as good as possible. The game lasts for 3 minutes and for each emoji covered with stones a score is determined. Scores for each emoji are added up and the winner is determined in the end of the game.

### Game Modes
There are 2 game modes in the game, that can be selected in the main menu.

* 2vs2: 4 players are divided in 2 groups, each group consists of 2 players. The groups compete against each other.
* 1vs1: 2 players compete against each other.

### Gameplay
Before the game began, each player decides at which base he will play during the game. Each player should stay near his base during the game.
The game begins, after the countdown is over.
During the game the players can shoot the projectile, in order to catch stones and items. Then the projectile must be dragged back to the base, by rotating the wheel. After the projectile is back at it's initial position, the hooked stone/item can be used.
The stones are placed into the inventory.
From the inventory the stones can be placed on the emoji and in the workshop.
In the workshop the stone can be scaled and rotated. Afterwards the stone can be dragged away to emoji.
The players try to cover the emoji with stones. For stones, that are badly placed, negative points will be added.
In order to get points for the emoji coverage, the toggle button must be activated. In game mode "2vs2", both toggle buttons must be activated. After it, a new emoji is loaded.
If a player activates Fire Item, the opponent's cannon starts to burn and must be restored with a water droplet.
The game can be paused by touching the timer in the game field.
The game ends after 3 minutes, the players will be warned with a countdown before the end.

