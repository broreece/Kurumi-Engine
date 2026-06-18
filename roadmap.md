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

## Milestone: Visualization
**Focus areas:**

- Re-implementation of the game's menu.

---

## (GM-01) Re-introduce the game menu ##
### Complexity: 4 ###
### Independent: 3 ###
### Momentum: 3 ###
### Impact: 4 ###

**Description:** We removed the game menu (Escape in game) we should reintroduce it so that the player can
see paryt member information and items.

**Steps:**
- It might be worth checking our previous menu work to create this again.

---

## Visualization milestone reached.

---

## (SR-01) Script reformating. ##
### Complexity: 3 ###
### Independent: 3 ###
### Momentum: 2 ###
### Impact: 1 ###

**Description:**
During this build the script system fought back a lot and It's gotten a bit messy yets fix this.

**Steps:**
- Go through the code checking where we can make improvements.

---

## (IR-01) Inventory reformatting. ##
### Complexity: 3 ###
### Independent: 3 ###
### Momentum: 2 ###
### Impact: 1 ###

**Description:**
Inventory currently sits as a dictionary<int, int> but this can be made into a custom class.

**Steps:**
- Create a new inventory class containing the old dictionary.
- Change the deseralizer from save file to dictionary to the new class.
- Change existing usages of inventory into new class.

---

## Sharpen milestone reached.

---

## (SAVE-01) Validate saving ##
### Complexity: 2 ###
### Independent: 1 ###
### Momentum: 1 ###
### Impact: 1 ###

**Description:** Because menu has been removed we haven't tested persistance in a while validate this works.

---

## (SAVE-03) Update map converter tool to no longer upload actors. ##
### Complexity: 2 ###
### Independent: 1 ###
### Momentum: 1 ###
### Impact: 1 ###

**Description:** Update map converter tool to no longer upload actors. We can just entirely redo the tool as it'll
    remove a lot of extra code.

**Steps:**
- Update converter tool to only do tiles no longer use objects or actors.
- We currently store the actor class this can be removed or kept just for keeping track of data.

---

## Persistance milestone reached.

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
