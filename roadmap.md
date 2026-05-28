# Roadmap

This file outlines all future tickets alongside a set of fields that determine how complex, independent, the momentum 
a ticket carries and how big the impact is on the engine.

### Complexity

| Ranking | Description |
|-------|-----------|
|**1**|Very quick task|
|**2**|Small task|
|**3**|Medium task|
|**4**|Large task|
|**5**|Major feature or redesign|

### Independence

| Ranking | Description |
|-------|-----------|
|**1**|Completely independent|
|**2**|Minor interference|
|**3**|Medium interference|
|**4**|Large interference|
|**5**|Completely prevents any other work|

### Momentum

| Ranking | Description |
|-------|-----------|
|**1**|Quick win. Easy to start and finish in one sitting|
|**2**|Not too intense, a small task that can be completed shortly|
|**3**|Average amount of work no longer then 1 day straight of work|
|**4**|Large amount of planning / various things to consider before starting, might take a week|
|**5**|Heavy thinking / architectural work, could take a month|

### Impact

| Ranking | Description |
|-------|-----------|
|**1**|No or almost no impact|
|**2**|Small impact|
|**3**|Medium impact|
|**4**|Large impact|
|**5**|Fundamental engine capability|


Tickets will also display a brief description, a set of planned steps for completion and any linked tickets.

---

## Milestone: Pursuit
**Focus areas:**

- Focused on enemy formations and battle.
- Battles can now start and end with dynamic scripts.
- Further sprite additions for actors to include a dead state for after battle enemy formations.

---

## (EFMI-01) AI and enemy formation map data implementation ##
### Complexity: 5 ###
### Independent: 5 ###
### Momentum: 4 ###
### Impact: 5 ###

**Description:** This task is focused on finishing all implementation of enemy formations on the map. This will allow 
enemy formation interaction a core part of the gameplay loop.

**Steps:**
- Implement when battle formations die they can be found dead on map and appear under the player: (Mid Priority)
    - If party is victorous in battle set the enemy formations "dead" flag to true.
    - On the map scene when loading sprites for enemy formations check if dead use unique sprite.
    - When checking if a tile is passable check if the tile is passable, if all events are passable and then if there 
        is an enemy formation on the tile they are dead.

---

## (BSE-02) Battle scene enhancements 2 ##
### Complexity: 3 ###
### Independent: 3 ###
### Momentum: 3 ###
### Impact: 3 ###

**Description:** Minor enhancements to make the battles more dynamic, allow multiple target attack, damage display and 
more dynamic enemy targetting.

**Steps:** 
- Implement additional attack options: (Low Priority)
    - Implement party wide attacks.
    - Implement enemy group wide attacks.
    - Implement random enemy hit attacks.
- Implement damage text:
    - Used to exist in forge.
    - Create custom config declaring it's font type, size and maybe a offset.
- Implement function to load the index of the first healthy party member when generating the first choices in a 
    battle scene:
    - Might have to check how this logic will work around the battle state ensure that the state and scene stay the 
    same in both.

---

## (BSE-03) Battle scene enhancements 3 ##
### Complexity: 2 ###
### Independent: 2 ###
### Momentum: 3 ###
### Impact: 3 ###

**Description:**
Allow battle scenes to end either in victory of fail.

**Steps:**
- Implement defeat bool function: (Low Priority).
    - Check if the enemy formation has a "OnLoseEvent", if so execute the event.
    - If not we'll add a TODO to create a game over scene.
- Implement victory bool function: (Low Priority)
    - If all enemies are dead return to previous scene.
    - If the event had a "OnWinEvent" execute it after returning to the scene.
    - Make sure we always check defeat before victory.

---

## Pursuit milestone reached.

---

## (LIE-01) Lookup indexes enhancement ##
### Complexity: 1 ###
### Independent: 1 ###
### Momentum: 1 ###
### Impact: 1 ###

**Description:** In loaders that also load index lookups we should store values after inital load to prevent re-loading

**Steps:**
- In formation definition loader and state short name loader create new fields to store the loaded data.
- Update the index getter functions to utilize the stored fields.

---

## (LFBU-01) Seperate large functions into sub functions ##
### Complexity: 1 ###
### Independent: 1 ###
### Momentum: 3 ###
### Impact: 1 ###

**Description:** To better apply to CRUD principles we should seperate out the large functions in the code into sub
functions.

**Steps:**
- Search code for largest functions and break them up (E.G: Map Factories "Create" function).
- Also whilst refractoring ensure that any using statements are correctly spaced based on type and then subtype;
    if all main types match.

---

## (PF-01) Run a profiler on the code ##
### Complexity: 2 ###
### Independent: 1 ###
### Momentum: 2 ###
### Impact: 1 ###

**Description:** We should start using a profiler to check where our code is heaviest.

**Steps:**
- Utilize 'Visual Studio Performance Profiler'.

---

## (ASE-01) Actor scripts stored in enemy formations should execute on find. ##
### Complexity: 2 ###
### Independent: 1 ###
### Momentum: 2 ###
### Impact: 1 ###

**Description:** Actor scripts stored in formations are never executed. We want the on find script to execute if the
enemy formation finds the party.

**Steps:**
- Edit the map state function that triggers when an enemy formation finds the party to activate it's stored script.

---

## (TNCB-01) Add text window with namebox (and choice box) ##
### Complexity: 1 ###
### Independent: 1 ###
### Momentum: 2 ###
### Impact: 1 ###

**Description:** Create the ability to add a new text window with namebox and choice box.

**Steps:**
- Add the new UI view using our existing windows as examples.
- Create a new script that can open this UI view (Ensure freezing takes place the same as choice box).

---

## (EFI-01) Pass enemy formation from map state to battle state. ##
### Complexity: 2 ###
### Independent: 3 ###
### Momentum: 2 ###
### Impact: 1 ###

**Description:** We recreate the enemy formations when a battle starts, this means we pass a nullable navigation grid.
    We can drastically improve this logic.

**Steps:**
- In the battle start request we should pass the enemy formation.
- This means in the battle state we don't need to recreate the enemy formation, it also removes the formation factory
    dependency in battle state.

---

## (MSC-01) Party movement speed config. ##
### Complexity: 2 ###
### Independent: 2 ###
### Momentum: 2 ###
### Impact: 1 ###

**Description:** We don't have a hard coded movement speed, we need to fix this and store it in the party class.

**Steps:**
- Create new config for the movement speed.
- Update the movement controls so holding buttons will continue movement.

---

## Refinement milestone reached.

---

## (ASE-01) Additional script enhancements ##
### Complexity: 2 ###
### Independent: 1 ###
### Momentum: 1 ###
### Impact: 1 ###

**Description:** Allow dynamic speed in the force move party script step.

**Steps:**
- The force move party currently uses the parties base speed, we can change this to a custom one:
    - Add a new parameter in "ForceMoveParty" in the map script context and pass it into the map state function.

---

## (LI-01) Log improvements and throw exceptions when failing to load config ##
### Complexity: 2 ###
### Independent: 1 ###
### Momentum: 2 ###
### Impact: 1 ###

**Description:** Improve logger by making it not static and store config.

**Steps:**
- Make new config object/runtime and yaml file for logger:
    - This should contain directory name and file name.
- Make the logger non-static.
- Pass config into constructor.
- Build logger in bootstrap before any other config object.
- In each config object we should find a way to try and catch if the config yaml is missing.
- Surrond the load config objects witha try and catch in bootstrap, if an exception is thrown we can log and handle it.

---

## (EMI-01) Improvements to error messages ##
### Complexity: 2 ###
### Independent: 1 ###
### Momentum: 3 ###
### Impact: 1 ###

**Description:** Make the error message look unique.

**Steps:**
- Make a icon for the error windows.
- Make a button that closes the error windows.
- Import a default font for the error windows.
- Add another new icon for the error windows a big X.

---

## Create demonstration video ##
### Complexity: 2 ###
### Independent: 1 ###
### Momentum: 4.5 ###
### Impact: 3 ###

**Description:**
Create a small demonstration scenario showcasing key engine capabilities.

**Steps:**
- The first video should show two actors having forced movements, the player can still move during this time, then one 
actor continues with a speech, this should go directly into a cutscene.
- The second video should demonstrate enemy formations, they chase the player for the player to escape. The game is then 
saved and when loaded the enemy formation persists on the map.

---

## Add custom stun status and status changes ##
### Complexity: 2 ###
### Independent: 3 ###
### Momentum: 4 ###
### Impact: 2 ###

**Description:** Create the ability for statuses to block movement in battle, and testing creation of new statuses in 
the database.

**Steps:**
- Add a new enum for statuses "Behaviour":
    - The existing "Priority" can represent ordering of statuses,
    - The new "Behaviour" can represent if a status erases other statuses when added or can be stacked.
    - Create new fields in Database for status definitions.
    - Create new field in the status definition class. 
    - Correctly adjust the laoder to pass the new field.
    - Edit the status resolver system to finalize this.
- Add a stun status: (Extremely Low Priority)
    - Create status in database.
    - Ensure that when checking if enemy/party commits action they are able to move by checking all their statuses and 
    confirming they can move.
    - Add battle scene event step to inflict statuses to a specified enemy only (Used for body parts).
- Add a new "Hidden" boolean to statuses: (Extremely Low Priority)
    - Add new field in table of database.
    - Add new field into class of status.cs.
    - When loading statuses in database.cs add new field.

---

## Item pool implementation ##
### Complexity: 3 ###
### Independent: 2 ###
### Momentum: 3 ###
### Impact: 3 ###

**Description:** Allow item pool containers, granting dynamic rogue like features in the engine.

**Steps:**
- Load item pools in database: (Mid Priority)
    - We already have a database table representing item pools, We need to create an item pool 2D list of ints.
    - The 1st index is the item pool id, the second index is the item ids in the list.
    - Create a map scene event step that adds a random item from an item pool to the inventory, display a text window 
    displaying the name of the item as well.

---

## Global world elements ##
### Complexity: 4 ###
### Independent: 4 ###
### Momentum: 5 ###
### Impact: 4 ###

**Description:** Expand the engine to feature a global timer and mechanics that force the player to plan on the moment.

**Steps:**
- Implement a global timer: (High Priority)
    - We need a global timer that is stored in core and starts running only on map scenes.
    - This global timer needs to be accesible by map scene events.
    - This global timer should be saved in the info.json.
- Implement a hunger and sleep meter: (High Priority)
    - These meters should start at 100 then slowly reduce.
    - Add some food items in the item database, have the effect be "ReduceHunger".
    - Create a reduce hunger map scene event step.
    - Create a "Sleep" Map scene event step that moves time forward and reduces the parties sleep meter by the time 
    slept.

---

## Status/health and MP art implementations ##
### Complexity: 3 ###
### Independent: 2 ###
### Momentum: 4 ###
### Impact: 2 ###

**Description:** Give an easy to read representation for HP/MP and status values.

**Steps:**
- Create art representing a health bar and mp bar to draw in battle and menu: (Low Priority)
    - The art should have both a filled and empty portions, like a green/red half of health bar.
- Add a new check in status art to allow hidden statuses (Missing limbs): (Low Priority)
    - Update status database for new field.
    - Update status class for new field.
    - Render this status art in battle scene and the main menu scene.
    - When drawing status art check art if it is a hidden status or not.

---

## Battle sprite character additions ## 
### Complexity: 3 ###
### Independent: 2 ###
### Momentum: 4 ###
### Impact: 2 ###

**Description:** Adds dynamic display to battle scenes.

**Steps:**
- Edit the existing character battle sprite to add additional frames: (Low Priority)
    - Make battle sprites animated for party members.
    - Make another battle sprite for party members who are ready to attack.
    - Make a sprite for party members who are guarding.
    - Make variations of each depending on what limbs the characters have.
    - Make a sprite for each dead party member.

---

## (TS-01) Title screen implementation ## 
### Complexity: 2 ###
### Independent: 1 ###
### Momentum: 4 ###
### Impact: 3 ###

**Description:** Create a title screen.

**Steps:**
- Use a choice box for the following: New Game, Load, Options, Quit.
- If choice is new game simply load a new game via core.
- If choice is load game open the file selector menu.
- If choice is quit force close window.

---

## (VE-02) Actor vision enhancements ## 
### Complexity: 3 ###
### Independent: 3 ###
### Momentum: 4 ###
### Impact: 2 ###

**Description:** Consider planning out actor visions.

**Steps:**
- We can change the logic of events range of vision, make some events have a circular range, some view straight etc.
- If we implement this we'll need to change any existing actors in our maps to accomodate a new vision type field:
    - Create an enum for vision type similar to behaviour.
    - Add this enum value to all actors.
    - In the map scene "InRangeActor" function check the actors vision type and change code accordingly.

---

## Introduce save file unit testing ##
### Complexity: 3 ###
### Independent: 1 ###
### Momentum: 4 ###
### Impact: 1 ###

**Description:** Create unit testing cases for the save and load system.

**Steps:**
- Make test cases for the following:
    - Test save/load:
        - Ensure status can be loaded onto playable characters.
        - Ensure any stat changes made to playable characters save.
        - Ensure equipment is saved.
        - Ensure inventory is saved.
        - Ensure information remains the same.

---

## Expand entity modifier creation in the equipment/status loader / database to include status resistances. ##
### Complexity: 3 ###
### Independent: 2 ###
### Momentum: 3 ###
### Impact: 2 ###

**Description** Currently status resistance modifier exists but no database exists with this data.

**Steps:**
- Edit the database so statuses and equipment can alter the status resistances.
- In the factories for statuses/equipment create the modifier.

---

## (GWTC-01) Game window title in config. ##
### Complexity: 1 ###
### Independent: 1 ###
### Momentum: 1 ###
### Impact: 2 ###

**Description** We can add game window title to game window config, load it in the game window class and assign there.

**Steps:**:
- Add new title into game window config.
- Correctly load it.
- In game window.cs update the title to use this value.

---

# Unplaned tickets:

---

## (MIC-01) Menu implementation completion ##
### Complexity: 3 ###
### Independent: 3 ###
### Momentum: 4 ###
### Impact: 3 ###

**Description:** Finish implementation of the various menus such as inventory, save and load etc.

**Steps:** N/A

---

## (UICA-01) UI closing animation implementation ##
### Complexity: 3 ###
### Independent: 3 ###
### Momentum: 4 ###
### Impact: 1 ###

**Description:** Make a plan for closing of UI components, each component should have a way to close.

**Steps:** N/A

---

## (MI) Music implementation: ##
### Complexity: 2 ###
### Independent: 2 ###
### Momentum: 4 ###
### Impact: 4 ###

**Description:** Implement both sound effects and music. Make a plan and write notes here.

**Steps:** N/A

---

## Potential Threading actor AIs ##
### Complexity: 5 ###
### Independent: 5 ###
### Momentum: 4 ###
### Impact: 2 ###

**Description** Just a plan ticket, if we do thread actor AIs it might improve performance however we will need to 
carefully account for the current system and how we will implement threading here.

**Steps:** N/A

---

## Change config groupings to use composition in YAML data ##
### Complexity: 2 ###
### Independent: 2 ###
### Momentum: 3 ###
### Impact: 1 ###

**Description** We can implement composistion for commonly occuring repeated variables such as font ID and font size.

**Steps:** N/A

---

## Expand turn effect modifier ##
### Complexity: 3 ###
### Independent: 2 ###
### Momentum: 3 ###
### Impact: 2 ###

**Description** Currently we're just assuming turn effect activates every turn. This is fine but we could also make
a modifier that activates an effect when they attack, when they get attacked etc.

**Steps:** N/A

---

## Introduce app data saves. ##
### Complexity: 2 ###
### Independent: 2 ###
### Momentum: 2 ###
### Impact: 2 ###

**Description** We can change the save file from the root repo into appdata as per industry standards. If we do this
we can remove saves from the .gitignore

**Steps:** N/A
