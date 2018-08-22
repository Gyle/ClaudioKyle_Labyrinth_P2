# 18.2 COMP313 Assignment 2
## Student: Kyle Claudio, Lucas Gosling

## Burndown Chart
http://radekstepan.com/burnchart/#!/

To show: 
1) Log into GitHub and put in the path to this Repo.
2) Select add project on top left
3) enter path. EG: Gyle/COMP313_Unity_Game

## Debugging
Currently Right click is Debug Mode. In this mode, left click opens and closes all doors. The purpose of the mode is to quick test enemy interation and enemy nav mesh. In addition to testing for other features without needing to traverse the whole scene properly as that takes time.

## Architecture of the Game
### My code
Other than modifcations to library scripts, I have created scripts for handling enemy actions, dying, and pit fall event.

#### Enemy Actions
The enemy is attached to the EnemyController.js script. The role of this script is to patrol the area using a set of waypoint
that I provide to the enemy controller. It utilises  Nav Mesh Agent for its path finding logic. It is also aware if the 
trap door has opened, meaning it will not walk off into the pit or try to path find to the other side. In addition, it 
has a trigger on enter function for its agro range. It will de-agro once the path to the player partial. It knows when to 
animate itself for the right actions of walk, idle, and attack.

#### Player Actions




### Library Used
#### IvyMoon's Door Script
This script initially was made for first person perspective games to open doors. It would display GUI text regarding which button to
push to open or close. There was only a one to one relationship, meaning one door to one switch. Modifying this script was my first time coding in Unity3D and C#. Here are my modifications with corresponding `commit id`:

* Replaced option for single trigger switch to an array of switches `ed98e514cb23233a415501bb153bc46109b16847`
* Created for loops to go through every switch to see if the player pressed the open or close button near the switches that opens the door `ed98e514cb23233a415501bb153bc46109b16847`
* Added detection for clicking on the switch instead of using a button to open the switch `f037c5a87583d2d97e2885ec6832af9e9c58b54a`
* Added logic for only opening door if player clicked on switch they're standing next to `b91c23fee751bef2c50c6f5658825f1f7d5eeb0c`
* Animate door when opening or closing `f94b3162c2da3383cae36f795dec85810bf6eef1`

#### Unity Standard Third Person Controller Script
I utilised Unity's script to make our player move. I downloaded a random humanoid asset from the asset store. I then
proceeded to add the third person controller script into the player to make it move. I also modified this code to 
stop the player from moving when they have died `6e8b70b8f0ab5eb54df54fd1727fb9d5d7201fe9`

#### Unity Nav Mesh
I utilised Unity's script to make our enemy move. It intelligently path finds and knows when doors is closed or if the 
trap door is open, indicating it cannoy cross the gap. All done with use of Nav Mesh Obstacle.
