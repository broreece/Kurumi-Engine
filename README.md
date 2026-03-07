# Kurumi Engine

Kurumi Engine is a modular RPG engine written in **C# using SFML**.  
It focuses on **clean architecture, extensibility, and data-driven gameplay systems**.

The engine is designed for building **narrative-heavy RPGs** with scripted events, AI actors, and modular UI systems.

---

## Development Status

Iron Seed  
Active Development

Recent work focused on a large **core refactor** to improve engine architecture and maintainability.

Current cleanup tasks include:
- Fixing UI script steps (dialogue / choice boxes)
- Improving forced movement actor logic (some issues where if blocked forced movements do not function as intended)
- Implementing proper game pause behavior when menus are active

Next milestone:
- Introduction of unit testing
- Expanding battle state scope
- Improving visibility and functionality of statuses

---

## Design Philosophy

Kurumi Engine is heavily inspired by the design philosophy behind games like **Fear & Hunger**.

Key ideas:

- **Script-driven gameplay**
- **Minimal engine magic**
- **Deterministic step-based event logic**
- **Clear separation between engine systems**

The engine aims to keep gameplay logic **explicit and readable**, avoiding overly abstract systems that obscure control flow.

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
- Dynamic actor control systems that do not restrict player controls.
- Threaded saving for performance.

### Engine Infrastructure
- JSON/YAML configuration support
- SQLite data storage
- Modular rendering pipeline

---

## Example Script

Kurumi Engine uses **step-based scripts** to control events and gameplay flow.

Example:

```yaml
script:
  - ShowDialogue:
      speaker: "Guard"
      text: "You shouldn't be here."

  - ChoiceBox:
      choices:
        - "Leave"
        - "Stay"

  - Conditional:
      if: choice == "Stay"
      then:
        - StartCombat:
            enemy: "Guard"

```
The above yaml is just an example, the real data is formatted in strings currently outline in the documentation.

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
| **YAML / JSON** | Configuration files and save data |
| **Git** | Version control and development workflow |

---

## Project Goals

Kurumi Engine aims to provide a clean, modular foundation for building narrative-driven RPGs.  
The project focuses on long-term maintainability, deterministic gameplay systems, and a data-driven design philosophy.

Core goals include:

- Build a **modular RPG engine architecture** with clear system boundaries
- Support **script-driven gameplay events** and deterministic step execution
- Maintain **clean and readable gameplay logic** without excessive engine abstraction
- Provide a flexible foundation for **AI actors, dialogue systems, and scripted events**
- Develop a codebase that prioritizes **maintainability, extensibility, and clarity**

---
