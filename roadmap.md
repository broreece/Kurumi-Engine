# Roadmap

This file outlines all future tickets alongside a set of fields that determine how complex, independent and how big the impact is on the engine.

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
### Crucible
**Focus areas:**

- Clean up post refractor.
- Update roadmap to clearly seperate tickets and new fields.

---

## (RGCA-01) Restrict "GameContext" access in script context ##
### Complexity: 1 ###
### Independent: 1 ###
### Impact: 1 ###

**Description:** Currently we have a getter for the game context, this shouldn't be the case instead use functions on script context:

**Steps:**
- Remove any instances of "GetGameContext().SomeFunction()" in script steps and just call functions in script context.
- Remove accessto "GetGameContext()" entirely.

---

## (TWNUI-01) Implement a WindowWithTextAndNamebox UI State ##
### Complexity: 3 ###
### Independent: 2 ###
### Impact: 2 ###

**Description:** Create a new UI State Modal for the window with text and namebox script.

**Steps:**
- This is almost identical to Dialogue state but includes an additional window, perhaps we can inherit from Dialgoue state.
- Create custom defaults class that contains both a name box, we then pass both the textwindowdefaults and the new defaults to construct the objects.
- Use inheritance to reduce duplicated code.

---

## (FMA-01) Forced move actors should pause script execution too ##
### Complexity: 3 ###
### Independent: 3 ###
### Impact: 2 ###

**Desciption:** When using the force move actors step pause the script step and look into continuing the script after the force move controller is popped.

**Steps:**
- When executing the force move script pass the script context and the script step.
- After the force move ends do the same as UI script, resume and set the current script step.
- If multiple force move perhaps we should store the script contexts and script steps in some type of dictionary or lists with indexes.

---

## (HE-01) Handle/Throw all exceptions ##
### Complexity: 2 ###
### Independent: 1 ###
### Impact: 1 ###

**Description:** Any instances of exceptions being thrown we should catch them and use our engine logic to handle.

**Steps:**
- In the "PopUIStack()" function in game context throw an error if the empty stack is tried to pop.
- In "GetPartiesSprites" function in save manager throw an error if the save file isn't found.
- In the battle scene's "Update()" function throw an error if the battle targetting view is empty.
- In the battle scene's "UpdateChoiceBoxChoices" function called when loading a characters options if a null character index is passed throw a custom exception.
- In battle script context and map script context throw a custom exception if the script has been finished before pausing.

---

## (CBPI-01) Choice box parameter investigation ##
### Complexity: 3 ###
### Independent: 1 ###
### Impact: 2 ###

**Description:** When the choice box height is 14 it's way too small for the default font size, when it's 15 it's way too big

**Steps:**
- We need to figure out why it's drastically changing what should be 1% of the screen size.
- Fix this and recursion test against the other window UI components.
- Introduce custom spacing in the choice box config.


The global message displays when another UI state is up but the second the game is unpaused the message disapears:
Priority: High

We need to fix tiles animating and the global message timer not resetting when the game is paused like other timers.
    - When paused loop over the UI stack updating with the paused value, ensure that in the global message we reset if paused.
    - Add animated tiles timer to the reset clock in the map scene.

---

## Introduce unit testing ##
### Complexity: 3 ###
### Independent: 1 ###
### Impact: 1 ###

**Description:** Create unit testing to ensure no reversion bugs ever occur.

**Steps:**
- Research how to create C# unit testing.
- Create new folder for unit tests.
- Make test cases for the following:
    - Test applying statuses:
        - Add a status.
        - Apply a status that removes other statuses.
        - Apply multiple statuses.
    - Test save/load:
        - Ensure status can be loaded onto playable characters.
        - Ensure any stat changes made to playable characters save.
        - Ensure equipment is saved.
        - Ensure inventory is saved.
        - Ensure information remains the same.
    - Damage formula:
        - Use some hard coded values to ensure that damage is being calculated correctly.

---

## (BSE-01) Hard coded battle scene changes ##
### Complexity: 2 ###
### Independent: 2 ###
### Impact: 2 ###

**Description:** Currently some choices in the battle scene are hard coded "Run Away"/"Items" perhaps these should be moved into a yaml to check if they are enabled, disabled, what the text says.

**Steps:**
- Introduce a new config file to determine the textand if items/run away are enabled.

---

## Plan next roadmap tasks to implement for next build.
### Complexity: 1 ###
### Independent: 1 ###
### Impact: 1 ###

**Description:** Move the tasks for the next build into a group, make a name for the build as well.

**Steps:** N/A 

---

Crucible build release point. At this point any clean up tasks post our refractor have finished and we can move on to new features.

---

## Add custom stun status and status changes ##
### Complexity: 2 ###
### Independent: 3 ###
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

## (BSE-02) Battle scene changes ##
### Complexity: 2 ###
### Independent: 2 ###
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

## (AS-01) Advanced saving implementation ##
### Complexity: 3 ###
### Independent: 3 ###
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

## (MSAC) Map scene actor changes ##
### Complexity: 4 ###
### Independent: 4 ###
### Impact: 4 ###

**Description:** Allow dynamically sized actor sprites, allow sprites to be rendered under the party or above and allows larger then tile sized party field sprites.

**Steps:**
- (MSAC-01) Allow varying sizes of actor sprites: (High Priority)
    - Create a table "events" which contain art id, width and height.
    - Load these as a class in core, "EventInfo".
    - Create a folder for these actor art, move existing actor art into this folder.
    - Then we will need to edit all instances where we call "EventArtId" to reference these "EventInfo" in core.
    - When we create actor sprites, create it from actor info, width and height.
-(MSAC-02) Implement below and above player actor sprites seperately to draw in different orders: (High Priority)
    - We already have a variable in events to indicate if they are under the player.
    - When we are looping to draw events we should instead create two lists of actor sprites, ones under the player and ones above the player.
    - In the render loop we should draw tiles, then events ubove the player, then the party, then the events above the player.
- (MSAC-03) Allow character map sprites to be larger then tiles and display above current tile: (High Priority)
    - Edit the current height/width in the art .yaml file.
    - When rendering current sprites width and height we should subtract it by the difference between the character height/width from the tile height/width.
    - This change will also apply to actor sprites.

---

## AI and enemy formation map data implementation ##
### Complexity: 5 ###
### Independent: 5 ###
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

## (ASE-01) Additional script enhancements ##
### Complexity: 2 ###
### Independent: 1 ###
### Impact: 1 ###

**Description:** Allow dynamic speed in the force move party script step.

**Steps:**
- The force move party currently uses the parties base speed, we can change this to a custom one:
    - Add a new parameter in "ForceMoveParty" in the map script context and pass it into the map state function.

---

## Item pool implementation ##
### Complexity: 3 ###
### Independent: 2 ###
### Impact: 3 ###

**Description:** Allow item pool containers, granting dynamic rogue like features in the engine.

**Steps:**
- Load item pools in core: (Mid Priority)
    - We already have a database table representing item pools, We need to create an item pool 2d list of ints.
    - The 1st index is the item pool id, the second index is the item ids in the list.
    - Create a map scene event step that adds a random item from an item pool to the inventory, display a text window displaying the name
    of the item as well.

---

## Global world elements ##
### Complexity: 4 ###
### Independent: 4 ###
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
### Impact: 3 ###

**Description:** Create a title screen.

**Steps:**
- Use a choice box for the following: New Game, Load, Options, Quit.
- If choice is new game simply load a new game via core.
- If choice is load game open the file selector menu.
- If choice is quit force close window.

---

## (BSE-03) Battle scene enhancements ##
### Complexity: 3 ###
### Independent: 3 ###
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

## Actor vision enhancements ## 
### Complexity: 3 ###
### Independent: 3 ###
### Impact: 2 ###

**Description:** Consider planning out actor visions.

**Steps:**
- We can change the logic of events range of vision, make some events have a circular range, some view straight etc.
- If we implement this we'll need to change any existing actors in our maps to accomodate a new vision type field:
    - Create an enum for vision type similar to behaviour.
    - Add this enum value to all actors.
    - In the map scene "InRangeActor" function check the actors vision type and change code accordingly.

---

# Unplaned tickets:

## (MIC-01) Menu implementation completion ##
### Complexity: 3 ###
### Independent: 3 ###
### Impact: 3 ###

**Description:** Finish implementation of the various menus such as inventory, save and load etc.

**Steps:** N/A

---

## (UICA-01) UI closing animation implementation ##
### Complexity: 3 ###
### Independent: 3 ###
### Impact: 1 ###

**Description:** Make a plan for closing of UI components, each component should have a way to close.

**Steps:** N/A

---

## (BSI) Battle scene implementation ##
### Complexity: 4 ###
### Independent: 3 ###
### Impact: 4 ###

**Description:** Battle scene is currently incomplete. Make a plan and write notes here.

**Steps:** N/A

---

## (MI) Music implementation: ##
### Complexity: 2 ###
### Independent: 2 ###
### Impact: 4 ###

**Description:** Implement both sound effects and music. Make a plan and write notes here.

**Steps:** N/A

---

## Potential Threading actor AIs ##
### Complexity: 5 ###
### Independent: 5 ###
### Impact: 2 ###

**Description** Just a plan ticket, if we do thread actor AIs it might improve performance however we will need to carefully account for the current system and how we will implement threading here.

**Steps:** N/A