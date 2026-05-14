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

## Milestone: Quench
**Focus areas:**

- Focused primarily on minor clean up
- Allows non-passable tiles to be see-through
- Update windows and UI to have locations work regardless of window size

---

## (DKE-01) Remove full paths in asset registry json. ##
### Complexity: 1 ###
### Independent: 1 ###
### Momentum: 1 ###
### Impact: 2 ###

**Description:** Currently we put the full path in the asset registry, this is risky because if the art folders
change it'll result in us having to change a lot of lines. Removing the full path also reduces a lot of bloat.

**Steps:**
- Create a function to generate the paths based on type.
- Check if our current design for animated tile sheets and static tile sheets using the same key is correct.

---

## (VE-01) Allow non-passable tiles to be see through. ##
### Complexity: 2 ###
### Independent: 1 ###
### Momentum: 1 ###
### Impact: 2 ###

**Description:** Currently in our vision resolver if a tile is not passable actors can not see through them.
We should change this so that we have options for visible tiles that are not passable.

**Steps:**
- Edit the database to have the new field for tiles.
- Edit the tile class to include new field.
- Edit logic in vision resolver:
    - Remove navigation grid pass dictionary of booleans where the coordinate is the key.
    - The values set to if the tiles are visible or not.

---

## (DKE-01) Create exceptions for if a dictionary key doesen't exist. ##
### Complexity: 1 ###
### Independent: 1 ###
### Momentum: 1 ###
### Impact: 3 ###

**Description:** During our work on the forge refractor we introduced dictionaries for storing scripts, maps and assets.
We need to introduce checkers for if keys exist.

**Steps:**
- Create exceptions for if a key is not found anywhere we check for keys (Scripts, Assets etc.)
- Additional math exceptions in damage calculator.
- Additional exception in the capability container for script contexts.

---

## Change the hard coded indexes in the force move actor to use actor keys. ##
### Complexity: 2 ###
### Independent: 2 ###
### Momentum: 1 ###
### Impact: 1 ###

**Description:** Currently when force moving actors we use "ActorIndex" yets change the format to a string key in
dictionary.

**Steps:**
- Edit the map's json format to include keys for actors.
- Edit the index variables to be strings called "ActorKey".
- Edit the way the maps store actors from a list to a string dictionary initally:
    - We will need to change how we convert the list to a dictionary of int coordinates.

---

## Cache factories currently being made in game state. ##
### Complexity: 2 ###
### Independent: 2 ###
### Momentum: 1 ###
### Impact: 1 ###

**Description:** Currently in the battle state and map state we are creating factories. This should all be done in
bootstrap and stored in game context to improve performance.

**Steps:**
- Go through map state and battle state removing factory initalization and store them in game context instead.
- Also store the UIRenderSystem reused in the state manager and battle state in state context or somewhere else. It's
currently being created twice.

---

## Clean up the roadmap, make release plan for all tickets. ##
### Complexity: 1 ###
### Independent: 1 ###
### Momentum: 1 ###
### Impact: 1 ###

**Description:** Current roadmap is a bit messy and needs to be cleaned up.

**Steps:**
- Go through the roadmap and clean it all up.
- Ensure all tickets are planned and grouped in releases.

---

## (SBS-01) Determine scaling of sprites on battle state. ##
### Complexity: 2 ###
### Independent: 1 ###
### Momentum: 1 ###
### Impact: 1 ###

**Description:** The sprites on the battle state are not scaled, we should determine how we scale.

**Steps:**
- Update scaling on party battle renderer and enemy renderer.
- We should also wrap around the selection based on the number of options instead of max number of options.

---

## Windows and UI elements should appear based on the screen's width and height. ##
### Complexity: 4 ###
### Independent: 3 ###
### Momentum: 3 ###
### Impact: 3 ###

**Description:** Currently I believe UI elements will render in the same place regardless of window size,
this should be fixed to be based on percents of the window size.

**Steps:**
- We can divide the intended window screen sizes by the real size to determine the new position or perhaps use a
SFML camera object to adjust placement and zoom.

---

Quench Reached

---

## AI and enemy formation map data implementation ##
### Complexity: 5 ###
### Independent: 5 ###
### Momentum: 4 ###
### Impact: 5 ###

**Description:** This task is focused on finishing all implementation of enemy formations on the map. This will allow 
enemy formation interaction a core part of the gameplay loop.

**Steps:**
- Implement a max limit on the smart tracking events:
    - For normal smart tracking events perhaps no limit is fine, hard code a value like 0 to reference infinite range.
    - If the distance is greater then the limit throw the error to not move.
- Load enemy formations on new maps in map scene or in the map class: (High Priority)
    - When loading a map into map scene loop through enemy formations file and place enemy formations in the map by 
    creating them.
- Create clocks for each enemy formation on a map: (High Priority)
    - Each enemy formation we create and place on the map create 2 clocks to control movement and determine when they 
    give up chasing the party.
- Implement the AI switching when found and check against the timer when to turn off the found flag: (High Priority)
    - When we check each map event also check each enemy formation on the map in the "onFound" trigger, if found set 
    found flag to true. 
    - In the movement function we check each enemy formation, if the found flag is set to true we use it's other AI to 
    determine movement.
    - Each frame the party is in the on found events range we reset the chase clock, if the chase clock reaches the 
    limit we turn found to false.
- Implement when battle formations die they can be found dead on map and appear under the player: (Mid Priority)
    - If party is victorous in battle set the enemy formations "dead" flag to true.
    - On the map scene when loading sprites for enemy formations check if dead use unique sprite.
    - When checking if a tile is passable check if the tile is passable, if all events are passable and then if there 
    is an enemy formation on the
    tile they are dead.
    - **NOTE** Rendering under the player might involve the work on ticket: (MSAC-02)

**Related tickets:**
- (MSAC-02)

---

## (BSE-03) Battle scene enhancements ##
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

## (BSE-02) Battle scene changes ##
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

## Add text window with namebox and choice box ##
### Complexity: 1 ###
### Independent: 1 ###
### Momentum: 2 ###
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

## Make a tiled application modification to output in our format ##
### Complexity: 3 ###
### Independent: 1 ###
### Momentum: 4 ###
### Impact: 3 ###

**Description:** Currently we have a messy way to convert files using a python script. Yets just modify the tiled 
application to do the work for us.

**Steps:** N/A

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

## (MLE-01) Loading maps throw a specific error for if the format was wrong or if the format was right. ##
### Complexity: 1 ###
### Independent: 1 ###
### Momentum: 1 ###
### Impact: 1 ###

**Description** In the map loader we can create a custom exception to specify the issue. Also exists in asset registry
and script registry.

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
