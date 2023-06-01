# ![Dante](https://github.com/doelfi/Unity/assets/91377218/24d3af5d-f64d-49c1-80c8-3013f73f6c69) Dante's Inferno 2D

Dante's Inferno 2D is a jump and run game following Dante through the circles of hell. Each circle bares its own obstacles varying from dangerous spikes to insidious enemies attacking Dante. 
But neither can stop Dantes curiosity for the next circle of hell. So press start and join Dante on his journey through hell. 


Starting in circle 1 Limbo, the first circle, one gets used to playing by following a simple tutorial. 
In Lust, the second circle, the first enemy occurs. 
Then, in Gluttony, precise jumping skills are required to reach the next level. 
Afterwards, the fourth circle Greed shows who has a good balance and who dares to collect another coin. 
Getting closer to the end circle no. 5 Anger provokes the player even more - it is hell after all. 
Last but not least, the name of the last circle Fraud already tells that not everything is as it seems. 

So be careful and don't die - 
to keep hell away.  

# Features 
## Levels
- Each level introduces new elements (spikes, platforms, coins, moving enemies, moving platforms, falling spikes)
- Each level has a unique background (Based on the circles of hell from Dante’s Inferno)
- The first level has tutorial instructions shown on screen 

![ExampleScene](https://github.com/doelfi/Unity/assets/91377218/c77b8ebd-54ec-4f36-b2e7-17f4a9d28ed3)

## Movement
- Jumping is only possible when on ground
- When hitting the platform from the side, jumping is not possible (has to be on top)
## Animation
- Unique player animations for idling, walking and jumping
- Coins are animated
- Enemies have animations for walking left and right
## Threats
- Spikes and moving enemies (and lower bound if the player falls down)
- Enemies walk until they hit something (edge of a platform, wall, spike, other enemy, etc.) and then turn around
- Enemy gets destroyed when the player jumps on top of them
## User Interface and Stats
- Stats are displayed on screen with a custom font and are correctly updated when something changes
- When taking damage, lives are reduced by one
- When reaching zero lives, a game over screen is presented
- On a key press the main screen is loaded so the game can be started again
- Coins can be collected
- Three collected coins turn into one additional life and coins get reset to zero
- Coins that were collected in a level get reset when taking damage (Thus restarting the level with the coin on its original position)
- When the game is successfully finished, an end screen with all times per level is shown

![MainScreen](https://github.com/doelfi/Unity/assets/91377218/55c88d1f-eff2-4a2e-94b7-2c0c1b959e47)

## Sounds
- Different sounds for different important actions
- Multiple sounds at once possible
- Pausing game for important sounds to finish (before loading)
- Looping walking sound so it sounds reasonable
- Walking sound really only plays while on the ground
## Speed Run Times
- Times are saved on each level switch in a txt-file
- Txt-file is read on start-up oft the game
- Times can be viewed in a separate scene
