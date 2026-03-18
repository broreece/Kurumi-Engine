# Roadmap

This file outlines all future tickets alongside a set of fields that determine how complex, independent, the momentum a ticket carries and how big the impact is on the engine.

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
|**2**|Moderate focus required|
|**3**|Heavy thinking / architectural work|

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

## Next milestone:
### Forge
**Focus areas:**

- AI and enemy formation map data implementation
- Battle scene implementation
- Advanced saving implementation
- Minor changes such as logger improvements and script enhancements

---

## (CSAF) Custom script and actor format implementation ##
### Complexity: 5 ###
### Independent: 5 ###
### Momentum: 3 ###
### Impact: 3 ###

**Description:** Currently scripts and actors are stored in string format, we should create a custom format.

**Steps:**
(CSAF-01):
- Load these fields in the database in a "LoadActors" function:
    - If the enum value for behaviour is a path check the "pathed_actors_path" to load a path.
- Change "IActorHandler" class as a result of this change:
    - Use interfaces in places we used to use actor handler.
(CSAF-02):
- Scripts should be stored in json formats.
- Each script variable stored with a string key for higher readability.
- Script steps should be indexed with the name of the type of script step so we'll maintain it's switch statement check.
- Because it's json each script step can have different parameters which is better then database.
- Store these scripts in "/Assets/Scripts" then create a registry file like the maps registry json file.
(CSAF-03):
- To link actors and scripts "actors" database should have a field for a linked script.
- If the script value isn't empty or -1 load the script from the script registry.
- We might need a file to deserealize scripts from json into our format.

---

## AI and enemy formation map data implementation ##
### Complexity: 5 ###
### Independent: 5 ###
### Momentum: 3 ###
### Impact: 5 ###

**Description:** This task is focused on finishing all implementation of enemy formations on the map. This will allow enemy formation interaction a core part of the gameplay loop.

**Steps:**
- Implement a max limit on the smart tracking events:
    - For normal smart tracking events perhaps no limit is fine, hard code a value like 0 to reference infinite range.
    - If the distance is greater then the limit throw the error to not move.
- Load enemy formations on new maps in map scene or in the map class: (High Priority)
    - When loading a map into map scene loop through enemy formations file and place enemy formations in the map by creating them.
- Create clocks for each enemy formation on a map: (High Priority)
    - Each enemy formation we create and place on the map create 2 clocks to control movement and determine when they give up chasing the party.
- Implement the AI switching when found and check against the timer when to turn off the found flag: (High Priority)
    - When we check each map event also check each enemy formation on the map in the "onFound" trigger, if found set found flag to true. 
    - In the movement function we check each enemy formation, if the found flag is set to true we use it's other AI to determine movement.
    - Each frame the party is in the on found events range we reset the chase clock, if the chase clock reaches the limit we turn found to false.
- Implement when battle formations die they can be found dead on map and appear under the player: (Mid Priority)
    - If party is victorous in battle set the enemy formations "dead" flag to true.
    - On the map scene when loading sprites for enemy formations check if dead use unique sprite.
    - When checking if a tile is passable check if the tile is passable, if all events are passable and then if there is an enemy formation on the
    tile they are dead.
    - **NOTE** Rendering under the player might involve the work on ticket: (MSAC-02)

**Related tickets:**
- (MSAC-02)

---

## (AS-01) Advanced saving implementation ##
### Complexity: 3 ###
### Independent: 3 ###
### Momentum: 3 ###
### Impact: 3 ###

**Description:** Allow saving of enemy formation and the playable characters.

**Steps:**
- Implement saving enemy formations: (Mid Priority)
    - In save manager create a new save function where we write the enemy formation data back into the yaml format.
    - Pass the enemy formation registry to update any existing enemy formations.
- Implement Saving playable characters:
    - In save manager create a new save function where we write the playable character data back into the yaml format.
    - Pass playable character registry back to update playable characters.
- Test both saves work.

---

## (BSE-03) Battle scene enhancements ##
### Complexity: 3 ###
### Independent: 3 ###
### Momentum: 2 ###
### Impact: 3 ###

**Description:** Minor enhancements to make the battles more dynamic, allow multiple target attack, font size for damage display and more dynamic enemy targetting.

**Steps:** 
- Implement additional attack options: (Low Priority)
    - Implement party wide attacks.
    - Implement enemy group wide attacks.
    - Implement random enemy hit attacks.
- Implement new config for the damage text to have a font size:
    - Currently hard coded make sure it's in battle scene config.
- Implement function to load the index of the first healthy party member when generating the first choices in a battle scene:
    - Might have to check how this logic will work around the battle state ensure that the state and scene stay the same in both.

---

## (BSE-02) Battle scene changes ##
### Complexity: 2 ###
### Independent: 2 ###
### Momentum: 2 ###
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

## Add text window with namebox and choice box ##
### Complexity: 1 ###
### Independent: 1 ###
### Momentum: 1 ###
### Impact: 1 ###

**Description:** Create the ability to add a new text window with namebox and choice box.

**Steps:**
- Add a new UI State inherit from dialogue with choice state and then add a namebox component.
- Create a new script that performs the same function as the previous one but also loads the name box defaults.

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
### Momentum: 1 ###
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
### Momentum: 2 ###
### Impact: 1 ###

**Description:** Make the error message look more like a windows error message.

**Steps:**
- Make a icon for the error windows.
- Make a button that closes the error windows.
- Import a default font for the error windows.
- Add another new icon for the error windows a big X.

---

## (FD-01) Forge Demonstration Build ##
### Complexity: 2 ###
### Independent: 1 ###
### Momentum: 3 ###
### Impact: 1 ###

**Description:**
Create a small demonstration scenario showcasing key engine capabilities implemented during the Forge build.

**Steps:**
- The first video should show two actors having forced movements, the player can still move during this time, then one actor
continues with a speech, this should go directly into a cutscene.
- The second video should demonstrate enemy formations, they chase the player for the player to escape. The game is then saved
and when loaded the enemy formation persists on the map.

---

Forge build reached!

---

## Add custom stun status and status changes ##
### Complexity: 2 ###
### Independent: 3 ###
### Momentum: 3 ###
### Impact: 2 ###

**Description:** Create the ability for statuses to block movement in battle, and testing creation of new statuses in the database.

**Steps:**
- Add a stun status: (Extremely Low Priority)
    - Create status in database.
    - Ensure that when checking if enemy/party commits action they are able to move by checking all their statuses and confirming they can move.
    - Add battle scene event step to inflict statuses to a specified enemy only (Used for body parts).
- Add a new "Hidden" boolean to statuses: (Extremely Low Priority)
    - Add new field in table of database.
    - Add new field into class of status.cs.
    - When loading statuses in database.cs add new field.

---

## Item pool implementation ##
### Complexity: 3 ###
### Independent: 2 ###
### Momentum: 2 ###
### Impact: 3 ###

**Description:** Allow item pool containers, granting dynamic rogue like features in the engine.

**Steps:**
- Load item pools in database: (Mid Priority)
    - We already have a database table representing item pools, We need to create an item pool 2D list of ints.
    - The 1st index is the item pool id, the second index is the item ids in the list.
    - Create a map scene event step that adds a random item from an item pool to the inventory, display a text window displaying the name
    of the item as well.

---

## Global world elements ##
### Complexity: 4 ###
### Independent: 4 ###
### Momentum: 3 ###
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
    - Create a "Sleep" Map scene event step that moves time forward and reduces the parties sleep meter by the time slept.

---

## Status/health and MP art implementations ##
### Complexity: 3 ###
### Independent: 2 ###
### Momentum: 3 ###
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
### Momentum: 3 ###
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
### Momentum: 3 ###
### Impact: 3 ###

**Description:** Create a title screen.

**Steps:**
- Use a choice box for the following: New Game, Load, Options, Quit.
- If choice is new game simply load a new game via core.
- If choice is load game open the file selector menu.
- If choice is quit force close window.

---

## Actor vision enhancements ## 
### Complexity: 3 ###
### Independent: 3 ###
### Momentum: 3 ###
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
### Momentum: 2 ###
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

# Unplaned tickets:

---

## Make a tiled application modification to output in our format ##
### Complexity: 3 ###
### Independent: 1 ###
### Momentum: 3 ###
### Impact: 3 ###

**Description:** Currently we have a messy way to convert files using a python script. Yets just modify the tiled application to do the work for us.

**Steps:** N/A

---

## (MIC-01) Menu implementation completion ##
### Complexity: 3 ###
### Independent: 3 ###
### Momentum: 3 ###
### Impact: 3 ###

**Description:** Finish implementation of the various menus such as inventory, save and load etc.

**Steps:** N/A

---

## (UICA-01) UI closing animation implementation ##
### Complexity: 3 ###
### Independent: 3 ###
### Momentum: 3 ###
### Impact: 1 ###

**Description:** Make a plan for closing of UI components, each component should have a way to close.

**Steps:** N/A

---

## (MI) Music implementation: ##
### Complexity: 2 ###
### Independent: 2 ###
### Momentum: 3 ###
### Impact: 4 ###

**Description:** Implement both sound effects and music. Make a plan and write notes here.

**Steps:** N/A

---

## Potential Threading actor AIs ##
### Complexity: 5 ###
### Independent: 5 ###
### Momentum: 3 ###
### Impact: 2 ###

**Description** Just a plan ticket, if we do thread actor AIs it might improve performance however we will need to carefully account for the current system and how we will implement threading here.

**Steps:** N/A