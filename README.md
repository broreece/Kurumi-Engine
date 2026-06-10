# Kurumi Engine

<p align="center">
  <img src="Kurumi-Engine/Assets/Icons/readme_icon.png" width="300">
</p>

Kurumi Engine is a modular RPG engine written in C# using SFML, designed around a clear separation between engine 
systems, game logic, and external data. It uses SQLite for structured definitions and YAML/JSON for configuration 
enabling a data-driven workflow.

The engine emphasizes system-driven architecture, where states orchestrate behavior, systems execute logic, and 
contexts handle input, allowing complex features—such as scripted events, AI actors, and dynamic UI to remain decoupled 
and scalable.

---

## Development Status

Visualization build is currently in progress, it is focused on:
- Re-implementation of the game's menu.
- Addition of items and an item display in the menu.
- Creation of a demo video for the capabilities of the engine.

---

## Design Philosophy

Kurumi Engine is heavily inspired by the design philosophy behind games like **Fear & Hunger** and other engines such 
as **RPG Maker**.

Key ideas:

- **Minimal engine magic, should be easy to use without large changes**
- **Engine follows SOLID principles and .NET coding standards**

The engine aims to keep any gameplay logic outside of scripts **explicit and readable**. If any user wanted to edit the 
engine for their own purpose it should be straight-forward.

---

## Features

Current engine capabilities include:

### Core Systems
- Script-driven event system applying on both overworld and battles
- Actor AI controllers allowing dynamic changing of AI movement patterns
- Modular UI state system that allows for blocking and non blocking UI elements
- Enemy formations persist across saves and affect the game world

---

## Example Script

Kurumi Engine uses **step-based scripts** to control events and gameplay flow.

Example:

```
{
    "Name": "Change map script",
    "FirstStep": "step_1",
    "Steps": {
        "step_1": {
            "Type": "ChangeMap",
            "Parameters": {
                "MapID": 1,
                "XLocation": 0,
                "YLocation": 0
            }
        }
    }
}

```
This is a basic script that changes the current map to the map with ID 1 at coordinate (0,0).

---

## Project Structure

The engine is organized into modular systems to keep gameplay logic, config, rendering, and scripting cleanly separated.

```
Kurumi-Engine/
│
├── Assets/
│   Contains raw game assets and the asset registry used to resolve file paths.
│
├── Bootstrap/
│   Entry point of the application.
│
├── Config/
│   Defines runtime configuration for the engine and game.
│
├── Data/
│   ├── Definitions/
│   ├── State/
│   └── Runtime/
│   Represents structured game data at different lifecycle stages.
│
├── Engine/
│   ├── Assets/
│   ├── Context/
│   ├── Input/
│   ├── State/
│   ├── Systems/
│   └── UI/
│   Core engine architecture and reuseable systems.
│
├── Game/
│   ├── Maps/
│   ├── Scripts/
│   └── UI/
│   Game-specific logic and behavior.
│
├── Infrastructure/
│   ├── Database/
│   ├── Persistence/
│   ├── Rendering/
│   └── Logging & Handler/
│   Handles external systems (file IO, database, rendering backend).
│
├── Utils/
│   General-purpose helper utilities.
```
---

## Tech Stack

| Technology | Purpose |
|-----------|--------|
| **C# (.NET 8)** | Core engine development |
| **Python 3.11** | Additional scripts handled in the tooling repo |
| **SFML.NET** | Rendering, window management, and input handling |
| **SQLite** | Static game definitions (items, enemies, etc.) |
| **YAML** | Configuration files |
| **JSON** | Dynamic save data, custom map and script formats |
| **Git** | Version control and development workflow |
| **Xunit** | Unit tests |

---

## Tooling

Official tooling and asset pipeline utilities are maintained separately in:
- https://github.com/broreece/Kurumi-Tools

This includes:
- Tiled map conversion

---

## Project Goals

- Build a **modular RPG engine architecture** with clear system boundaries and clean and readable code.
- The engine should be highly scalable, simple to use but allows growth smoothly.

---
