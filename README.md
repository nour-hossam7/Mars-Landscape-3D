<div align="center">

# 🌌 COSMIC ECHO

### **Rise. Fight. Restore Mars.**

A cinematic 3D sci-fi action game built with Unity, featuring energy-based survival, enemy AI, boss battles, multiple camera perspectives, mission progression, and a complete victory and game-over flow.

<br>

![Unity](https://img.shields.io/badge/Unity-6.3_LTS-000000?style=for-the-badge&logo=unity&logoColor=white)
![C Sharp](https://img.shields.io/badge/C%23-Game_Development-512BD4?style=for-the-badge&logo=csharp&logoColor=white)
![Platform](https://img.shields.io/badge/Platform-Windows-0078D6?style=for-the-badge&logo=windows&logoColor=white)
![Status](https://img.shields.io/badge/Status-Playable-success?style=for-the-badge)
![Genre](https://img.shields.io/badge/Genre-Sci--Fi_Action-orange?style=for-the-badge)

</div>

---

## 🎮 About the Game

**Cosmic Echo** is a cinematic 3D sci-fi action game set on Mars in the year **2147**.

A mysterious cosmic signal has corrupted the planet's ancient Spider Guardians and destabilized the Energy Core that powers the remaining Martian colony.

The player controls **Nova**, the last surviving alien guardian capable of restoring the planet's energy and defeating the corrupted forces.

To complete the mission, the player must explore the Martian environment, collect Energy Cells, unlock the main Guardian battle, defeat the boss, eliminate the remaining Spider enemies, and restore the Energy Core before Mars falls into darkness.

---

## 📖 Story

### **Year 2147**

For centuries, the alien civilization of Mars lived under the protection of ancient Spider Guardians.

Everything changed when a mysterious signal arrived from deep space.

The signal corrupted the guardians, transforming the planet's protectors into dangerous enemies. As the central Energy Core began to collapse, the Martian colony moved closer to complete destruction.

You are **Nova**, the last surviving alien capable of restoring the Energy Core.

Your mission is clear:

> Collect the remaining Energy Cells, unlock the Guardian arena, defeat the corrupted Spider Guardian, eliminate the remaining threats, and save Mars.

The fate of the planet begins with your first battle.

---

## 🕹️ Gameplay Loop

```text
Explore Mars
     ↓
Collect 5 Energy Cells
     ↓
Unlock the Spider Guardian Battle
     ↓
Defeat the Main Boss
     ↓
Activate the Remaining Spider Enemies
     ↓
Defeat All Enemies
     ↓
Restore the Energy Core
     ↓
Mission Complete
```

The game combines exploration, survival, combat, resource management, and mission-based progression.

---

## ⚡ Energy System

Energy is the most important resource in **Cosmic Echo**.

It works as both:

- A collectible resource
- The player's health

### Energy Rules

- The player starts with `0` Energy.
- Energy Cells increase the player's current Energy.
- The main boss battle unlocks after collecting `5` Energy Cells.
- The maximum Energy capacity is `10`.
- Every successful enemy attack reduces the player's Energy.
- The player can collect additional Energy Cells during combat.
- Before the main mission starts, having `0` Energy does not cause Game Over.
- After the boss mission begins, reaching `0` Energy causes an immediate Game Over.

```text
Energy = Player Health
```

This system creates a risk-and-reward experience where the player must balance combat, exploration, and survival.

---

## 🕷️ Main Spider Guardian

The Spider Guardian is the game's primary boss encounter.

At the beginning of the mission, the Guardian remains inactive. The player must collect the required Energy Cells before the battle can begin.

### Boss Features

- Player detection system
- Chase behavior
- NavMesh navigation
- Rotation toward the player
- Web projectile attacks
- Attack cooldown system
- Attack animation timing
- Health system
- Boss health bar
- Death animation
- Collider disabling after death
- AI disabling after death
- Delayed body destruction
- Mission gate unlocking

The main Spider Guardian has more health and stronger combat settings than the regular Spider enemies.

---

## 🕸️ Regular Spider Enemies

After the main Spider Guardian is defeated, additional Spider enemies become active across the map.

They remain disabled before the boss battle is completed.

Once activated, each enemy can:

- Detect the player within a configurable range
- Chase the player using NavMesh
- Stop at the correct attack distance
- Rotate toward the player
- Launch web projectiles
- Damage the player's Energy
- Receive weapon damage
- Play a death animation
- Disable its AI after death
- Disable its colliders after death
- Be destroyed after a configurable delay

Regular enemies have lower health than the main boss, allowing the final combat phase to remain challenging without becoming unfair.

---

## 🔫 Combat System

The combat system includes:

- First-person weapon handling
- Projectile-based shooting
- Enemy hit detection
- Damage processing
- Boss and enemy health systems
- Web projectile attacks
- Configurable enemy attack ranges
- Configurable attack cooldowns
- Attack and death animations
- Player Energy damage
- Enemy AI shutdown after death

The player must manage their remaining Energy carefully while fighting the corrupted guardians.

---

## 🏆 Victory System

The mission is completed only after the player defeats:

1. The main Spider Guardian
2. Every required regular Spider enemy

The game uses enemy death events to track mission progress.

When all required enemies have been defeated:

- The player's final score is saved
- The Victory scene is loaded
- The **Mission Complete** screen appears
- The restored Energy Core is displayed
- The player can replay, return to the Main Menu, or exit the game

```text
Main Boss Defeated
        +
All Regular Enemies Defeated
        =
Mission Complete
```

---

## ☠️ Game Over System

The player loses when their Energy reaches `0` after the boss mission has started.

The Game Over system:

- Detects when player Energy reaches zero
- Prevents Game Over before the first mission unlock
- Saves the required game state
- Loads the Game Over scene
- Allows the player to restart the mission
- Allows returning to the Main Menu
- Allows exiting the game

Every new gameplay session resets:

- Energy
- Score
- Mission progression
- Boss activation state
- Game Over state
- Runtime UI references

This ensures that every replay begins as a clean new mission.

---

## 🎥 Camera System

**Cosmic Echo** supports two different camera perspectives.

### First-Person Camera

The first-person camera provides:

- Immersive combat
- Weapon visibility
- Accurate aiming
- Close interaction with the environment

### Third-Person Camera

The third-person camera provides:

- A wider view of the environment
- Better visibility during exploration
- A full view of the player character
- Improved awareness during enemy encounters

The player can switch between both camera modes during gameplay.

```text
Camera Switch Key: V
```

---

## 🗺️ Mini Map System

The game includes a functional mini map that follows the player.

The mini map uses:

- A dedicated top-down camera
- Orthographic projection
- Render Texture
- Raw Image UI
- Player-following movement

It provides a live view of the surrounding Martian environment and helps the player navigate the level.

---

## 🎵 Audio System

The project uses one centralized Audio Manager that persists between scenes.

### Audio Manager Features

- Background music
- UI button click sounds
- Energy Cell collection sounds
- Music volume control
- Sound effects control
- Music mute support
- Sound effects mute support
- Persistent audio between scenes
- Prevention of duplicate Audio Managers

### UI Button Audio

Every interactive UI button uses a reusable `UIButtonSound` component.

Button sounds are included in:

- Main Menu
- Settings
- Story
- Credits
- Pause Menu
- Game Over
- Mission Complete

### Music Settings

The Settings screen includes a functional music volume slider with values between:

```text
0 = Muted
1 = Maximum Volume
```

---

## 🖥️ User Interface

The game includes a complete cinematic sci-fi user interface.

### Available Screens

- Main Menu
- Mission Briefing
- Story
- Settings
- Credits
- Gameplay HUD
- Alien Energy UI
- Boss Health UI
- Mini Map
- Pause Menu
- Game Over
- Mission Complete

### Gameplay HUD

The gameplay interface includes:

- Current Energy value
- Energy fill bar
- Boss health bar
- Mini map
- Pause menu controls

The visual design follows a consistent futuristic Mars-inspired theme.

---

## ⏸️ Pause System

The gameplay scene includes a functional Pause Menu.

The player can:

- Pause the game
- Resume the mission
- Restart the mission
- Return to the Main Menu
- Control the game sound

The Pause Menu temporarily stops gameplay while keeping the UI responsive.

---

## ✨ Main Features

- Cinematic 3D Martian environment
- Complete playable game loop
- First-person and third-person gameplay
- Runtime camera switching
- Energy-based player health
- Collectable Energy Cells
- Mission-based progression
- Main boss battle
- Regular enemy wave activation
- NavMesh enemy navigation
- Player detection and chase AI
- Web projectile attacks
- Weapon combat system
- Boss and enemy health systems
- Attack and death animations
- Game Over logic
- Victory detection
- Final score system
- Main Menu
- Settings screen
- Mission briefing
- Credits screen
- Pause Menu
- Mini map
- Persistent Audio Manager
- Background music
- Button click sounds
- Energy collection sounds
- Music volume slider
- Replay system
- Clean session reset

---

## 🎯 Mission Objectives

```text
Collect Energy.
Restore your power.
Unlock the Guardian arena.
Defeat the Spider Guardian.
Eliminate the remaining enemies.
Restore the Energy Core.
Save Mars.
```

---

## 🎮 Controls

| Action | Control |
|---|---|
| Move Forward | `W` |
| Move Backward | `S` |
| Move Left | `A` |
| Move Right | `D` |
| Look Around | `Mouse` |
| Shoot | `Left Mouse Button` |
| Switch Camera | `V` |
| Pause Game | `Esc` |
| Use Menus | `Mouse` |

---

## 🛠️ Technologies Used

| Technology | Purpose |
|---|---|
| Unity 6.3 LTS | Game engine |
| C# | Gameplay programming |
| Unity UI | Menus and HUD |
| TextMeshPro | High-quality UI text |
| Unity NavMesh | Enemy navigation |
| Cinemachine | Camera management |
| New Input System | Player controls |
| Animator Controller | Character animations |
| Render Texture | Mini map rendering |
| Git | Version control |
| GitHub | Source code hosting |

---

## 📂 Project Structure

```text
Assets/
│
├── Art/
│   ├── Animations/
│   ├── Characters/
│   │   └── Bosses/
│   │       └── SpiderGuardian/
│   ├── Environment/
│   ├── Materials/
│   ├── Models/
│   ├── Textures/
│   └── UI/
│
├── Audio/
│   ├── Music/
│   └── SFX/
│
├── Prefabs/
│
├── Scenes/
│   ├── MainMenu
│   ├── Gameplay
│   ├── GameOver
│   └── Victory
│
├── Scripts/
│   ├── Audio/
│   ├── Bosses/
│   ├── Collectables/
│   ├── Combat/
│   ├── Core/
│   ├── Effects/
│   ├── Enemies/
│   ├── Interfaces/
│   └── Managers/
│
├── Settings/
├── TextMesh Pro/
└── ThirdParty/
```

---

## 🧩 Main Scripts

### Core Management

```text
GameManager.cs
```

Responsible for:

- Energy
- Score
- Mission progression
- Boss activation
- Game Over
- Gameplay reset
- Scene transitions
- UI updates

### Audio

```text
AudioManager.cs
UIButtonSound.cs
```

Responsible for:

- Background music
- Sound effects
- Button sounds
- Volume settings
- Persistent audio

### Enemies

```text
SpiderGuardianAI.cs
BossHealth.cs
EnemyWaveUnlocker.cs
```

Responsible for:

- Enemy detection
- Navigation
- Attacking
- Damage
- Death
- Enemy wave activation

### Victory

```text
VictoryManager.cs
VictoryUI.cs
```

Responsible for:

- Tracking defeated enemies
- Detecting mission completion
- Saving the final score
- Loading the Victory scene
- Victory screen controls

---

## 🚀 How to Run the Project

### Requirements

Before opening the project, install:

- Unity Hub
- Unity `6.3 LTS` or a compatible Unity 6 version
- Git

### Clone the Repository

```bash
git clone https://github.com/nour-hossam7/Cosmic-Echo.git
```

Move into the project directory:

```bash
cd Cosmic-Echo
```

### Open the Project

1. Open **Unity Hub**.
2. Select **Add**.
3. Choose **Add project from disk**.
4. Select the cloned project folder.
5. Open the project using Unity 6.
6. Wait for Unity to import the project assets.

### Start the Game

Open:

```text
Assets/Scenes/MainMenu
```

Then press:

```text
Play
```

---

## 🏗️ Build the Game

To create a playable build:

1. Open Unity.
2. Select `File`.
3. Open `Build Profiles`.
4. Select the Windows platform.
5. Ensure the required scenes are included.
6. Click `Build`.
7. Choose an empty output folder.

### Required Scene Order

```text
MainMenu
Gameplay
GameOver
Victory
```

---

## 🧪 Current Development Status

### Completed

- [x] Main Menu
- [x] Mission Briefing
- [x] Story screen
- [x] Credits screen
- [x] Settings screen
- [x] Music volume control
- [x] Persistent Audio Manager
- [x] UI button sounds
- [x] Energy collection sounds
- [x] Player movement
- [x] First-person camera
- [x] Third-person camera
- [x] Camera switching
- [x] Weapon combat
- [x] Energy-based health
- [x] Energy Cells
- [x] Spider Guardian AI
- [x] Boss health system
- [x] Regular Spider enemies
- [x] Enemy wave activation
- [x] NavMesh navigation
- [x] Web attacks
- [x] Death animations
- [x] Game Over system
- [x] Replay reset
- [x] Mini map
- [x] Victory detection
- [x] Mission Complete screen
- [x] Final score display

### Current Phase

```text
Final Testing and Polishing
```

---

## 🔮 Future Improvements

The project can be expanded with:

- Additional levels
- More Spider Guardians
- New enemy types
- Multiple weapons
- Weapon upgrades
- Player abilities
- Energy shields
- Difficulty selection
- Save and load system
- Checkpoints
- Advanced patrol behavior
- Improved enemy animations
- Cutscenes
- Dialogue system
- Mission objectives UI
- More mini map markers
- Damage visual effects
- Boss introduction sequence
- Additional sound effects
- More detailed Martian environments
- Performance optimization
- Full controller support

---

## 🧠 Design Decisions

### Why Energy Is Also Health

Using Energy as the player's health connects the story directly to the gameplay.

The player is not simply collecting items for points. Every Energy Cell increases their ability to survive.

This creates meaningful decisions:

- Fight immediately
- Search for more Energy
- Risk exploring dangerous areas
- Save Energy Cells for later combat

### Why the Boss Unlocks After 5 Cells

The first five Energy Cells act as the opening mission objective.

They allow the player to:

- Learn movement
- Explore the map
- Understand the Energy system
- Prepare for combat

The boss fight begins only after the player understands the basic mechanics.

### Why Regular Enemies Activate After the Boss

The main Spider Guardian represents the first major corrupted protector.

Defeating it begins the final mission phase and activates the remaining threats across the map.

This creates clear gameplay escalation:

```text
Exploration
    ↓
Boss Battle
    ↓
Enemy Hunt
    ↓
Mission Complete
```

---

## 🐞 Known Notes

- The project requires a baked NavMesh for enemy navigation.
- Scene names must exactly match the names used by the scripts.
- The primary boss GameObject should remain named `SpiderGuardian`.
- UI object names used by the Game Manager should not be changed without updating the scripts.
- Audio files and third-party assets should remain in their expected folders.
- The correct Gameplay scene must be included in the Build Profile.

---

# 🎬 Watch the Gameplay

<div align="center">

## ▶ **Gameplay Demo**

### https://drive.google.com/file/d/1IMEw5R4txxGnLSlAzwBmtiF4tDVj_gxt/view?usp=sharing

Experience the complete **Cosmic Echo** gameplay, featuring exploration, combat, boss battles, enemy AI, mission progression, camera switching, and the final victory sequence.

</div>

---

## 👩‍💻 Developer

<div align="center">

### **Nour Hossam**

AI Student • Software Developer • Game Developer

[![GitHub](https://img.shields.io/badge/GitHub-nour--hossam7-181717?style=for-the-badge&logo=github)](https://github.com/nour-hossam7)

[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nour_Hossam-0A66C2?style=for-the-badge&logo=linkedin)](https://www.linkedin.com/in/nour-hossam7/)

</div>

---

## 📜 License and Asset Credits

This project was developed for educational and graduation project purposes.

All original source code written for the project belongs to the project developer.

Third-party assets, character models, animations, fonts, sounds, textures, packages, and environment resources remain the property of their original creators and are used according to their respective licenses.

Before publishing or commercially distributing the project, review the license of every third-party asset included in the repository.

---

## ⭐ Support

If you find this project interesting:

- Star the repository
- Explore the source code
- Share your feedback
- Follow the project's future development

---

<div align="center">

# 🌌 COSMIC ECHO

### **The Energy Core has been restored. Mars is safe once again.**

<br>

**Built with Unity and C#**

<br>

⭐ **Rise. Fight. Restore the Galaxy.** ⭐

</div>
