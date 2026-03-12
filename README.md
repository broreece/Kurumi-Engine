# Kurumi Engine

<p align="center">
  <img src="Assets/Icons/readme_icon.png" width="300">
</p>

Kurumi Engine is a modular RPG engine written in **C# using SFML**.  
It focuses on **clean architecture, extensibility, and data-driven gameplay systems**.

The engine is designed for building **narrative-heavy RPGs** with scripted events, AI actors, and modular UI systems.

---

## Development Status

Crucible 
Active Development

Recent work focused on a large **core refactor** to improve engine architecture and maintainability.

Current cleanup tasks include:
- Fixing UI script steps (dialogue / choice boxes)
- Pausing script execution during forced movement of actors
- Exceptions are all unique and handled in code

Next milestone:
- Introduction of unit testing
- Expanding battle state scope (No more hard coded choices)
- Fixing issue involving strange percentage of height for choice boxes

---

## Design Philosophy

Kurumi Engine is heavily inspired by the design philosophy behind games like **Fear & Hunger**.

Key ideas:

- **Script-driven gameplay**
- **Minimal engine magic**
- **Deterministic step-based event logic**
- **Clear separation between engine systems**

The engine aims to keep gameplay logic **explicit and readable**.

---

## Features

Current engine capabilities include:

### Core Systems
- Scene and state-based game architecture
- Script-driven event system
- Actor AI controllers
- Tile-based map navigation
- Modular UI state system
- Input mapping system

### Gameplay Systems
- Custom scripting and stored variables allows dynamic gameplay
- Dynamic actor control systems that do not restrict player controls
- Same script-driven event system applies in battles for unique battle experiences

### Engine Infrastructure
- YAML used for config
- JSON used for dynamic data
- SQLite used for static data
- Modular rendering pipeline

---

## Example Script

Kurumi Engine uses **step-based scripts** to control events and gameplay flow.

Example:

```
"ChoiceBox,Yes-No,2,BasicTextWindow,You said no,StartBattle,0,0,0,"

```
Where if you reply yes a battle will start and if you reply no no battle will start.

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
|
├── Config
│
├── Database
│
|── Docs
|
├── Engine
│ ├── Input
│ ├── Rendering
│ ├── Runtime
│ ├── Systems
│
|── Game
|
|── Registry
|
├── Save
|
|── Scenes
|
|── Scripts
|
|── States
|
|── UI
|
|── Utils
```
---

## Tech Stack

| Technology | Purpose |
|-----------|--------|
| **C# (.NET 8)** | Core engine development |
| **SFML.NET** | Rendering, window management, and input handling |
| **SQLite** | Structured game data storage |
| **YAML / JSON** | Configuration files and dynamic save data |
| **Git** | Version control and development workflow |

---

## Project Goals

Kurumi Engine aims to provide a clean, modular foundation for building narrative-driven RPGs.  
The project focuses on long-term maintainability, deterministic gameplay systems, and a data-driven design philosophy.

Core goals include:

- Build a **modular RPG engine architecture** with clear system boundaries
- Support **script-driven gameplay events** and deterministic step execution
- Maintain **clean and readable gameplay logic**
- Provide a flexible foundation for **AI actors, dialogue systems, and scripted events**
- Develop a codebase that prioritizes **maintainability, extensibility, and clarity**

---
