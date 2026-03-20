namespace Scenes.Map.Core;

using Config.Runtime.Map;
using Engine.Assets;
using Engine.Rendering;
using Game.Maps.Elements;
using Game.Maps.Tiles;
using Scenes.Base;
using Scenes.Map.Interfaces;
using States.Map.Interfaces;
using SFML.Graphics;
using SFML.System;

/// <summary>
/// The map scene class. Controls camera and draws sprites on the map scene.
/// Inherits from the Scene abstract class.
/// </summary>
public sealed class MapScene : SceneBase, IMapSceneView {
    /// <summary>
    /// Function that initalizes map scene variables.
    /// </summary>
    /// <param name="window">The game window object.</param>
    /// <param name="assetManager">The game asset manager object, used to load textures, fonts etc.</param>
    /// <param name="animatedTileSheetConfig">The animated tile sheet config.</param>
    /// <param name="characterFieldSpriteConfig">The character field sprite config.</param>
    /// <param name="mapBackgroundSpriteConfig">The map background sprite config.</param>
    /// <param name="mapConfig">The map config.</param>
    /// <param name="tileSheetConfig">The tile sheet config.</param>
    /// <param name="mapView">The map view object the scene is displaying.</param>
    public MapScene(GameWindow window, AssetManager assetManager, AnimatedTileSheetConfig animatedTileSheetConfig, 
        CharacterFieldSpriteConfig characterFieldSpriteConfig, MapBackgroundSpriteConfig mapBackgroundSpriteConfig, 
        MapConfig mapConfig, TileSheetConfig tileSheetConfig, IMapView mapView)
         : base(window, assetManager) {
        // Load reused config variables.
        walkLength = characterFieldSpriteConfig.GetWalkAnimationSpeed();
        walkFrames = characterFieldSpriteConfig.GetWalkAnimationFrames();
        characterWidth = characterFieldSpriteConfig.GetCharacterWidth();
        characterHeight = characterFieldSpriteConfig.GetCharacterHeight();
        tileHeight = tileSheetConfig.GetTileHeight();
        tileWidth = tileSheetConfig.GetTileWidth();
        tileSheetMaxTilesWide = tileSheetConfig.GetTileSheetMaxTilesWide();
        mapMaxTilesWide = mapConfig.GetMaxTilesWide();
        mapMaxTilesHigh = mapConfig.GetMaxTilesHigh();
        actorMoveVariance = mapConfig.GetActorMoveVariance();
        tileAnimationInterval = animatedTileSheetConfig.GetAnimationInterval();
        tileAnimatedFrames = animatedTileSheetConfig.GetAnimatedFrames();
        this.mapView = mapView;

        // Start animation clocks.
        animationClock = new Clock();
        partyWalkAnimationClock = new Clock();
        scrollingCameraClock = new Clock();

        // Sets the actor clocks and intervals.
        actorWalkAnimationClocks = [];
        actorControllerClocks = [];

        // Forced movement clocks.
        forcedMovementClock = new Clock();

        // Calculate scale for full size art.
        Vector2f fullScreen = new(window.GetWidth(), window.GetHeight());
        mapBackgroundScale = new(fullScreen.X / mapBackgroundSpriteConfig.GetWidth(), 
            fullScreen.Y / mapBackgroundSpriteConfig.GetHeight());

        // Load tile sheet texture.
        string texturePath = Path.Combine(
            AppContext.BaseDirectory,
            assetManager.GetTileSheetFileName(mapView.GetTileSheetId())
        );
        string animatedTexturePath = Path.Combine(
            AppContext.BaseDirectory,
            assetManager.GetAnimatedTileSheetFileName(mapView.GetTileSheetId())
        );
        string mapBackgroundTextPath = Path.Combine(
            AppContext.BaseDirectory,
            assetManager.GetMapBackgroundFileName(mapView.GetBackgroundArtId())
        );
        tileSheetTexture = new Texture(texturePath);
        animatedTileSheetTexture = new Texture(animatedTexturePath);
        Texture mapBackgroundTexture = new(mapBackgroundTextPath);
        mapBackgroundSprite = new Sprite(mapBackgroundTexture) {
            Scale = mapBackgroundScale,
            Position = new(0, 0)
        };
        string characterFieldSheetPath = Path.Combine(
            AppContext.BaseDirectory,
            assetManager.GetCharacterFieldSheetFileName(mapView.GetLeadFieldSpriteId())
        );
        characterFieldSheetTexture = new Texture(characterFieldSheetPath);
        
        // Store list of actors.
        List<IActorView> mapActors = mapView.GetListActorViews();

        // Create actor textures array.
        actorTextures = new Texture[mapActors.Count];

        // Load all clocks and actor textures from map.
        for (int mapActorIndex = 0; mapActorIndex < mapActors.Count; mapActorIndex ++) {
            string actorFieldSheetPath = Path.Combine(
                AppContext.BaseDirectory,
                assetManager.GetActorSheetFileName(mapActors[mapActorIndex].GetFieldSpriteId() - 1)
            );
            actorTextures[mapActorIndex] = new Texture(actorFieldSheetPath);
            actorWalkAnimationClocks.Add(new Clock());
            actorControllerClocks.Add(new Clock());
        }

        // Update camera controls on map basis.
        cameraRight = mapView.GetWidth() < mapMaxTilesWide ? (float) (mapMaxTilesWide - mapView.GetHeight()) / 2 : 0;
        cameraDown = mapView.GetWidth() < mapMaxTilesHigh ? (float) (mapMaxTilesHigh - mapView.GetHeight()) / 2 : 0;
        CenterParty(mapView.GetPartyXLocation(), mapView.GetPartyYLocation());
    }

    /// <summary>
    /// Function that updates all map scene sprites.
    /// </summary>
    public override void Update() {
        // Clear list of existing sprites.
        ClearSprites();

        // Add map background sprite.
        AddSprite(mapBackgroundSprite);

        // Scale all sprites.
        float widthScale = window.GetWidthScale();
        float heightScale = window.GetHeightScale();
        Vector2f scale = new(widthScale, heightScale);

        // Only draw the neccesary tiles.
        int maxWidth = mapMaxTilesWide < mapView.GetWidth() ? mapMaxTilesWide : mapView.GetWidth();
        int maxHeight = mapMaxTilesHigh < mapView.GetHeight() ? mapMaxTilesHigh : mapView.GetHeight();

        // Calculate the current tile animation frame.
        int animationFrame = animationClock.ElapsedTime.AsMilliseconds() / tileAnimationInterval;
        if (animationFrame >= tileAnimatedFrames) {
            animationFrame = 0;
            animationClock.Restart();
        }        

        // Check parties current walk animation step, if cycle has ended change frames.
        int currentWalkFrame = mapView.GetPartyCurrentAnimationFrame();
        int currentMoveTime = partyWalkAnimationClock.ElapsedTime.AsMilliseconds();
        if (currentWalkFrame > 0) {
            if (currentMoveTime > (walkLength / (walkFrames - 1))) {
                partyWalkAnimationClock.Restart();
                currentWalkFrame = currentWalkFrame == walkFrames - 1 ? 0 : currentWalkFrame + 1;
                mapView.SetPartyCurrentAnimationFrame(currentWalkFrame);
            }
            // If the walk animation ended
            if (currentWalkFrame == 0) {
                scrollingCamera = 1;
            }
            else if (scrollingCameraClock.ElapsedTime.AsMilliseconds() > 0) {
                scrollingCamera = (float) scrollingCameraClock.ElapsedTime.AsMilliseconds() / (float) walkLength;
            }
        }
        else {
            // If party is not moving turn off scrolling camera.
            scrollingCamera = 1;
        }

        // Store parties location.
        MapElement party = mapView.GetParty();
        int partyXLocation = party.GetXLocation();
        int partyYLocation = party.GetYLocation();
        int oldPartyXLocation = party.GetOldXLocation();
        int oldPartyYLocation = party.GetOldYLocation();
        // Check if the camera was scrolled.
        bool xScrolled = false;
        bool yScrolled = false;
        bool negativeMovement = false;
        int movementAdjuster = currentWalkFrame == 0 ? 0 : 1;
        // We also need to offset the map based on the direction that is being scrolled.
        int extraTileUp = 0;
        int extraTileRight = 0;
        int extraTileDown = 0;
        int extraTileLeft = 0;

        // Calculate additional camera settings.
        int halfWidth = mapMaxTilesWide / 2;
        int halfHeight = mapMaxTilesHigh / 2;

        int mapWidth = mapView.GetWidth();
        int mapHeight = mapView.GetHeight();

        bool movingLeft  = oldPartyXLocation > partyXLocation;
        bool movingRight = oldPartyXLocation < partyXLocation;
        bool movingUp    = oldPartyYLocation > partyYLocation;
        bool movingDown  = oldPartyYLocation < partyYLocation;

        if (movingLeft) {
            xScrolled = partyXLocation >= halfWidth && partyXLocation < (mapWidth - halfWidth) - 1;
            negativeMovement = true;
            movementAdjuster = 0;
            extraTileLeft = 1;
        }
        else if (movingRight) {
            xScrolled = partyXLocation > halfWidth && partyXLocation < (mapWidth - halfWidth);
            movementAdjuster *= -1;
            extraTileRight = 1;
        }
        else if (movingUp) {
            yScrolled = partyYLocation >= halfHeight && partyYLocation < (mapHeight - halfHeight) - 1;
            negativeMovement = true;
            movementAdjuster = 0;
            extraTileUp = 1;
        }
        else if (movingDown) {
            yScrolled = partyYLocation > halfHeight && partyYLocation < (mapHeight - halfHeight);
            movementAdjuster *= -1;
            extraTileDown = 1;
        }

        if (xScrolled || yScrolled) {
            // Invert the scrolling camera value, if it's in use.
            scrollingCamera = (float)1.0 - scrollingCamera;
            // Flip to negative value when moving up or left.
            scrollingCamera = negativeMovement ? scrollingCamera * (float)-1.0 : scrollingCamera;
        }
        else {
            scrollingCamera = 0;
            extraTileUp = 0;
            extraTileRight = 0;
            extraTileDown = 0;
            extraTileLeft = 0;
        }

        // Create map sprites.
        for (int xIndex = 0 - extraTileRight; xIndex < maxWidth + extraTileLeft; xIndex ++) {
            for (int yIndex = 0 - extraTileDown; yIndex < maxHeight + extraTileUp; yIndex ++) {
                List<TileObject> tileObjects = mapView.GetTileObjectsOfTile(xIndex + (int) tilesRight,
                        yIndex + (int) tilesDown);
                for (int currentNodeId = 0; currentNodeId < tileObjects.Count; currentNodeId ++) {
                    TileObject currentNode = tileObjects[currentNodeId];
                    int currentArtId = currentNode.GetArtId();
                    
                    // Find placement of sprite on the tilesheet texture.
                    Sprite tileSprite;
                    if (currentNode.IsAnimated()) {
                        tileSprite = new(animatedTileSheetTexture,
                            new IntRect(tileWidth * animationFrame, tileHeight * currentArtId, tileWidth, tileHeight));
                    }
                    else {
                        int div = currentArtId / tileSheetMaxTilesWide;
                        int mod = currentArtId % tileSheetMaxTilesWide;
                        tileSprite = new(tileSheetTexture,
                            new IntRect(mod * tileWidth, div * tileHeight, tileWidth, tileHeight));
                    }
                    Vector2f position;
                    if (xScrolled) {
                        position = new((xIndex + cameraRight + scrollingCamera) * tileWidth * widthScale,
                            (yIndex + cameraDown) * tileHeight * heightScale);
                    }
                    else if (yScrolled) {
                        position = new((xIndex + cameraRight) * tileWidth * widthScale,
                            (yIndex + cameraDown + scrollingCamera) * tileHeight * heightScale);
                    }
                    else {
                        position = new((xIndex + cameraRight) * tileWidth * widthScale,
                            (yIndex + cameraDown) * tileHeight * heightScale);
                    }
                    tileSprite.Position = position;
                    tileSprite.Scale = scale;
                    AddSprite(tileSprite);
                }
            }
        }

        // Load all actors above and below the party.
        // TODO: Create custom interface just for below party and adder here.
        List<IActorView> mapActors = mapView.GetListActorViews();
        List<IActorView> actorsBelowParty = [];
        List<IActorView> actorsAboveParty = [];
        foreach (IActorView mapActor in mapActors) {
            if (mapActor.IsBelowParty()) {
                actorsBelowParty.Add(mapActor);
            }
            else {
                actorsAboveParty.Add(mapActor);
            }
        }

        // Draw actors below the party.
        DrawActors(actorsBelowParty, xScrolled, yScrolled, extraTileRight, extraTileLeft, extraTileUp, extraTileDown);

        // Draw party.
        Sprite characterSprite = new(characterFieldSheetTexture,
            new IntRect(currentWalkFrame * characterWidth,
            mapView.GetPartyDirection() * characterHeight, characterWidth, characterHeight));
        // Load difference between tile size and character size.
        int widthDifference = (characterWidth - tileWidth) / 2;
        int heightDifference = characterHeight - tileHeight;
        // Calculate float based on current animation frame and check if party is currently moving.
        float walkingDistance = currentWalkFrame == 0 ? 0 : (float) currentWalkFrame / (float) walkFrames;
        // Draw based on animation cycle to target each movement.
        float baseX = partyXLocation + cameraRight - tilesRight;
        float baseY = partyYLocation + cameraDown - tilesDown;

        if (oldPartyXLocation != partyXLocation) {
            // Horizontal movement
            baseX += walkingDistance + scrollingCamera + movementAdjuster;
        }
        else {
            // Vertical movement
            baseY += movementAdjuster + scrollingCamera + walkingDistance;
        }

        // Convert to pixel space
        float pixelX = (baseX * tileWidth) - widthDifference;
        float pixelY = (baseY * tileHeight) - heightDifference;

        // Apply scaling
        float finalX = pixelX * widthScale;
        float finalY = pixelY * heightScale;
        characterSprite.Scale = new Vector2f(widthScale, heightScale);
        characterSprite.Position = new Vector2f(finalX, finalY);
        AddSprite(characterSprite);

        // Draw actors above the party.
        DrawActors(actorsAboveParty, xScrolled, yScrolled, extraTileRight, extraTileLeft, extraTileUp, extraTileDown);
    }

    /// <summary>
    /// Function used to reset a scenes clocks if the game is paused.
    /// </summary>
    public override void ResetClocks() {
        foreach (Clock actorControllerClock in actorControllerClocks) {
            actorControllerClock.Restart();
        }
    }

    /// <summary>
    /// Function used to center the camera and rest any timers associated with movement.
    /// </summary>
    /// <param name="x">The x location being moved to.</param>
    /// <param name="y">The y location being moved to.</param>
    public void OnPartyMoved(int x, int y) {
        partyWalkAnimationClock.Restart();
        scrollingCameraClock.Restart();
        scrollingCamera = 0;
        CenterParty(x, y);
    }

    /// <summary>
    /// Restarts a provided actor walk animation clock.
    /// </summary>
    /// <param name="actorIndex">The index of the clock to restart.</param>
    public void ResetActorWalkAnimationClock(int actorIndex) {
        actorWalkAnimationClocks[actorIndex].Restart();
    }

    /// <summary>
    /// Function used to reset the forced movement clock.
    /// </summary>
    public void ResetForcedMovementClock() {
        forcedMovementClock.Restart();
    }

    /// <summary>
    /// Function used to tick the forced movement clock.
    /// </summary>
    /// <returns>The time passed since last tick.</returns>
    public int TickForcedMovementClock() {
        int forcedMovementElapsedTime = forcedMovementClock.ElapsedTime.AsMilliseconds();
        forcedMovementClock.Restart();
        return forcedMovementElapsedTime;
    }

    /// <summary>
    /// Ticks a specified actor controller clock by resetting the clock and returning it's stored time.
    /// </summary>
    /// <param name="index">The actor controllers's index.</param>
    /// <returns>The time passed since last tick.</returns>
    public int TickActorControllerClock(int index) {
        int timePassed = actorControllerClocks[index].ElapsedTime.AsMilliseconds();
        actorControllerClocks[index].Restart();
        return timePassed;
    }

    /// <summary>
    /// Function that generates a random interval based on a random move actor's speed.
    /// </summary>
    /// <param name="GetMovementSpeed">The base speed of the random moving actor.</param>
    /// <returns>A randomised interval for the actor to use.</returns>
    public int GenerateRandomInterval(int movementSpeed) {
        int randomRange = (int) ((actorMoveVariance / 100) * (float) movementSpeed);
        Random rand = new();
        return rand.Next(movementSpeed - randomRange, movementSpeed + randomRange + 1);
    }

    /// <summary>
    /// Updates the tile camera controls to ensure party is centered when possible.
    /// </summary>
    /// <param name="xLocation">The x location to be centered on.</param>
    /// <param name="yLocation">The y location to be centered on.</param>
    private void CenterParty(int xLocation, int yLocation) {
        int halfWidth = (int) Math.Floor(mapMaxTilesWide / (decimal) 2.0);
        if (mapView.GetWidth() > mapMaxTilesWide && xLocation > halfWidth) {
            tilesRight = xLocation - halfWidth;
            int difference = xLocation - (mapView.GetWidth() - halfWidth);
            if (difference >= 0) {
                tilesRight -= difference + 1;
            }
        }
        else {
            tilesRight = 0;
        }
        int halfHeight = (int) Math.Floor(mapMaxTilesHigh / (decimal) 2.0);
        if (mapView.GetHeight() > mapMaxTilesHigh && yLocation > halfHeight) {
            tilesDown = yLocation - halfHeight;
            int difference = yLocation - (mapView.GetHeight() - halfHeight);
            if (difference >= 0) {
                tilesDown -= difference + 1;
            }
        }
        else {
            tilesDown = 0;
        }
    }

    /// <summary>
    /// Function that draws all actors from a provided list of actor handlers.
    /// </summary>
    /// <param name="actors">The list of actors to draw.</param>
    /// <param name="xScrolled">If the camera scrolled horizontally.</param>
    /// <param name="yScrolled">If the camera scrolled vertically.</param>
    /// <param name="extraTileRight">The extra tile right camera variable.</param>
    /// <param name="extraTileLeft">The extra tile left camera variable.</param>
    /// <param name="extraTileDown">The extra tile down camera variable.</param>
    /// <param name="extraTileUp">The extra tile up camera variable.</param>
    private void DrawActors(List<IActorView> actors, bool xScrolled, bool yScrolled, int extraTileRight, 
        int extraTileLeft, int extraTileDown, int extraTileUp) {
        for (int mapActorIndex = 0; mapActorIndex < actors.Count; mapActorIndex++) {
            IActorView actor = actors[mapActorIndex];

            int xLocation = actor.GetXLocation();
            int yLocation = actor.GetYLocation();

            // If entire map fits on screen, draw everything
            if (mapView.GetWidth() <= mapMaxTilesWide && mapView.GetHeight() <= mapMaxTilesHigh) {
                AddSprite(LoadActorSprite(actor, mapActorIndex, scrollingCamera, xScrolled, yScrolled));
            }
            else {
                // Check visibility bounds
                bool insideX = mapView.GetWidth() <= mapMaxTilesWide;
                bool insideY = mapView.GetHeight() <= mapMaxTilesHigh;

                insideX = insideX || (
                    xLocation >= tilesRight - 1 - extraTileRight &&
                    xLocation <= tilesRight + mapMaxTilesWide + extraTileLeft
                );

                insideY = insideY || (
                    yLocation >= tilesDown - 1 - extraTileDown &&
                    yLocation <= tilesDown + mapMaxTilesHigh + extraTileUp
                );

                if (insideX && insideY) {
                    AddSprite(LoadActorSprite(actor, mapActorIndex, scrollingCamera, xScrolled, yScrolled));
                }
            }
        }
    }

    /// <summary>
    /// Private function that loads an actor sprite and it's position based on variables passed.
    /// </summary>
    /// <param name="currentActor">The actor that has it's values loaded.</param>
    /// <param name="actorIndex">The index of the current actor.</param>
    /// <param name="scrollingCamera">The scrolling camera set in the draw function.</param>
    /// <param name="xScrolled">If the camera was scrolling on the x location.</param>
    /// <param name="yScrolled">If the camera was scrolling on the y location.</param>
    /// <returns></returns>
    private Sprite LoadActorSprite(IActorView currentActor, int actorIndex, float scrollingCamera,
        bool xScrolled, bool yScrolled) {
        // Load variables used for drawing actors.
        float widthScale = window.GetWidthScale();
        float heightScale = window.GetHeightScale();
        Vector2f characterScale = new(widthScale, heightScale);
        int xLocation = currentActor.GetXLocation();
        int yLocation = currentActor.GetYLocation();
        // Check if actor is currently being forced to move.
        if (mapView.ActorAtIsForcedMoved(xLocation, yLocation)) {
            actorWalkAnimationClocks[actorIndex].Restart();
            mapView.SetActorAtIsForcedMoved(xLocation, yLocation, value: false);
        }
        // Actors require unique scrolling camera controls not based on the actor movement but party movement.
        float actorXScroll = xScrolled ? scrollingCamera : 0;
        float actorYScroll = yScrolled ? scrollingCamera : 0;

        // Check actor's current walk animation step, if cycle has ended change frames.
        int currentWalkFrame = currentActor.GetCurrentAnimationFrame();
        if (currentWalkFrame > 0 && actorWalkAnimationClocks[actorIndex].ElapsedTime.AsMilliseconds() 
            > (walkLength / (walkFrames - 1))) {
            actorWalkAnimationClocks[actorIndex].Restart();
            int newFrame = currentWalkFrame == walkFrames - 1 ? 0 : currentWalkFrame + 1;
            currentActor.SetCurrentAnimationFrame(newFrame);
        }
        // Get the actor width and height and calculate the placement on the tile basedon size difference.
        int width = currentActor.GetWidth();
        int height = currentActor.GetHeight();
        int widthDifference = (width - tileWidth) / 2;
        int heightDifference = height - tileHeight;
        Sprite actorSprite = new(actorTextures[actorIndex],
            new IntRect(currentActor.GetCurrentAnimationFrame() * width, currentActor.GetDirection() * height, width, 
                height));
        // Calculate float based on current animation frame and check if party is currently moving.
        float walkingDistance = currentWalkFrame == 0 ? 0 : (float) currentWalkFrame / (float) walkFrames;
        int movementAdjuster = currentWalkFrame == 0 ? 0 : 1;
        Vector2f actorPosition;
        // Draw based on walk animation and all variables.
        float baseX = xLocation + cameraRight - tilesRight + actorXScroll;
        float baseY = yLocation + cameraDown - tilesDown + actorYScroll;

        // Adjust for movement direction
        if (currentActor.GetOldXLocation() > xLocation) {
            baseX += walkingDistance;
        }
        else if (currentActor.GetOldXLocation() < xLocation) {
            baseX -= walkingDistance;
        }
        else if (currentActor.GetOldYLocation() > yLocation) {
            baseY += movementAdjuster - walkingDistance;
        }
        else {
            baseY -= movementAdjuster - walkingDistance;
        }

        // Convert to pixel space
        float pixelX = (baseX * tileWidth) - widthDifference;
        float pixelY = (baseY * tileHeight) - heightDifference;

        // Apply scaling
        float finalX = pixelX * widthScale;
        float finalY = pixelY * heightScale;

        actorPosition = new(finalX, finalY);
        actorSprite.Position = actorPosition;
        actorSprite.Scale = characterScale;
        return actorSprite;
    }

    // Map associated with the scene.
    private readonly IMapView mapView;

    // Reused config variables.
    private readonly int walkLength, walkFrames, characterWidth, characterHeight, tileHeight, tileWidth, 
        tileSheetMaxTilesWide, mapMaxTilesWide, mapMaxTilesHigh, tileAnimationInterval, tileAnimatedFrames;
    private readonly float actorMoveVariance;

    // Map scene actor controller clocks.
    private readonly List<Clock> actorControllerClocks;

    // Camera elements.
    // Camera variables are used if map takes up less space then the max amount of tiles in the map.
    // Tiles variables are used when the map takes up more space then the max amount of tiles to center the party.
    private readonly float cameraRight, cameraDown; 
    private float scrollingCamera;
    private int tilesRight, tilesDown;

    // Current loaded textures and sprites.
    private readonly Texture tileSheetTexture, animatedTileSheetTexture, characterFieldSheetTexture;

    // Animation clock.
    private readonly Clock animationClock, partyWalkAnimationClock, scrollingCameraClock;

    // The map background sprite loaded once when a map is loaded.
    private Vector2f mapBackgroundScale;
    private readonly Sprite mapBackgroundSprite;

    // The actor walk animation clock.
    private readonly List<Clock> actorWalkAnimationClocks;

    // Actor textures.
    private readonly Texture[] actorTextures;

    // Forced movement control lock clock.
    private readonly Clock forcedMovementClock;
}
