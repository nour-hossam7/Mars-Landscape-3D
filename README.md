# Mars Landscape 3D

A 3D Mars exploration game developed using Unity.  
The project features player movement, character animations, collisions, a sci-fi environment, collectible energy cells, and a dynamic score system.

## Features

- Third-person player movement
- Walking, running, jumping, and idle animations
- Camera follow system
- Mars-themed 3D environment
- Sci-fi skybox
- Character Controller and collision system
- Energy Cell collectibles
- Dynamic Energy counter
- Dynamic Score counter
- Rotating and floating collectibles
- Unity UI Canvas using TextMeshPro

## Controls

| Action | Key |
|---|---|
| Move Forward | W |
| Move Backward | S |
| Move Left | A |
| Move Right | D |
| Run | Left Shift |
| Jump | Space |

## Gameplay

The player explores the Mars environment and collects Energy Cells placed around the map.

Each collected Energy Cell:

- Increases Energy by 1
- Increases Score by 10
- Disappears after collection

## Project Structure

```text
Assets/
Packages/
ProjectSettings/

Unity-generated folders such as Library, Temp, Logs, and UserSettings are excluded using .gitignore.

Technologies
Unity 6
C#
TextMeshPro
Universal Render Pipeline
Git and GitHub
How to Run
Clone the repository:
git clone https://github.com/YOUR_USERNAME/Mars-Landscape-3D.git
Open Unity Hub.
Click Add Project from Disk.
Select the cloned project folder.
Open the main scene.
Press the Play button.
Main Scripts
PlayerMovement

Handles:

Camera-relative movement
Walking and running
Player rotation
Jumping and gravity
Animator parameters
CameraFollow

Makes the main camera follow the player smoothly.

EnergyCollectable

Handles:

Collectible rotation
Floating movement
Player detection
Energy and score rewards
Destroying the collectible after collection
GameManager

Handles:

Energy count
Score count
Updating the UI
Task Requirements Completed
Created a UI Canvas containing collectible and score counts
Linked UI elements with C# scripts
Added collectible Energy Cells
Added player interaction with collectibles
Uploaded the Unity project to GitHub using a Unity .gitignore
Author

Nour Hossam

GitHub: nour-hossam7
LinkedIn: Nour Hossam
