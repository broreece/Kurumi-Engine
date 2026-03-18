# Kurumi Engine

<p align="center">
  <img src="Kurumi-Engine/Assets/Icons/readme_icon.png" width="300">
</p>

Kurumi Engine is a modular RPG engine written in **C# using SFML**, alongside data stored in **SQL, YAML and JSON**.
It focuses on **clean architecture, extensibility, and data-driven gameplay systems**.

The engine is designed for building **RPGs** with scripted events, AI actors, and modular UI systems.

---

## Development Status

Forge 
Active Development

First build worked focused on a large **core refactor** to improve engine architecture and maintainability, previous build 'crucible' focused on cleaning up up post refractor and creating tools to help development (Such as logging, unit tests).

Current tasks include:
- Custom data formats for scripts and actors
- AI and enemy formation map data implementation
- Battle scene implementation
- Advanced saving implementation
- Minor changes such as logger improvements and script enhancements

---

## Design Philosophy

Kurumi Engine is heavily inspired by the design philosophy behind games like **Fear & Hunger** and other engines such as **RPG Maker**.

Key ideas:

- **Minimal engine magic**
- **Deterministic step-based script logic in map and battle**
- **Engine follows SOLID principles**

The engine aims to keep any gameplay logic outside of scripts **explicit and readable**. If any licensed user wanted to edit the engine for their own purpose it should be straight-forward.

---

## Features

Current engine capabilities include:

### Core Systems
- Scene and state-based game architecture
- Script-driven event system
- Actor AI controllers
- Tile-based map navigation
- Modular UI state system that allows for blocking and non blocking UI elements

### Gameplay Systems
- Custom scripting and stored variables/flags allow dynamic gameplay
- Dynamic actor control systems that do not restrict player controls
- The same script-driven event system applies in battles for unique battle experiences
- Enemy formations can persist across saves meaning hostile NPCs affect the game world

---

## Example Script

Kurumi Engine uses **step-based scripts** to control events and gameplay flow.

Example:

```
"ChoiceBox,Yes-No,2,BasicTextWindow,You said no,StartBattle,0,0,0,"

```
Where if you reply 'yes' a battle will start, and if you reply 'no' no battle will start.

Plans to change from a string format to a custom format exist.

---

## Project Structure

The engine is organized into modular systems to keep gameplay logic, config, rendering, and scripting cleanly separated.

```
Kurumi-Engine
│
├── Assets
│
|── Bootstrap
| |── Contains the program that opens the game.
|
├── Config
| |── Contains config yaml files, managers and runtime objects.
|
├── Database
| |── Contains the SQL database and database loader object.
│
|── Docs
| |── Contains any documentation of the project.
|
├── Engine
| |── Contains core engine features such as the logger, exception handler, game context, render window object etc.
│
|── Game
| |── Contains game elements.
|
|── Registry
| |── Registry contains and stores the game elements.
|
├── Save
| |── Contains the save manager and save files as well as the seralization data formats.
|
|── Scenes
| |── Contains the SFML displaying scenes.
|
|── Scripts
| |── Contains all logic surronding scripts and the script steps that make up a script.
|
|── States
| |── Contains the backend logic of the scenes.
|
|── UI
| |── Contains all logic surronding the UI system and the possible UI states and components.
|
|── Utils
| |── Any utility classes required.
```
---

## Tech Stack

| Technology | Purpose |
|-----------|--------|
| **C# (.NET 8)** | Core engine development |
| **SFML.NET** | Rendering, window management, and input handling |
| **SQLite** | Structured game data storage |
| **YAML** | Configuration files |
| **JSON** | Dynamic save data and scripts |
| **Git** | Version control and development workflow |
| **Xunit** | Unit tests |

---

## Project Goals

- Build a **modular RPG engine architecture** with clear system boundaries and clean and readable code.
- Implement actors, scripts and enemy formations such that they act in tandem to create dynamic gameplay with no adjustments to code.
- Build an engine that is both simple to use and allows for complex situations.

---
