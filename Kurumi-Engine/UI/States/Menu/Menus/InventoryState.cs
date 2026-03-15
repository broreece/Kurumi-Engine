namespace UI.States.Menu.Menus;

using Config.Runtime.Menus;
using Config.Runtime.Windows;
using Engine.Input.Scenes;
using UI.Component.Components;
using UI.Core;
using UI.Input;
using UI.Interfaces;
using Utils.Strings;

/// <summary>
/// Inventory state class, Opens and displays the parties inventory.
/// </summary>
public class InventoryState : UIState, IInventoryInputController {
    /// <summary>
    /// Constructor for the inventory state.
    /// </summary>
    /// <param name="gameUIContext">The context required for menu game UI.</param>
    /// <param name="inventoryConfig">The inventory config object.</param>
    /// <param name="windowConfig">The window config object.</param>
    /// <param name="gameWindowScalesAccessor">The game window scales accessor.</param>
    /// <param name="windowAssetManager">The window asset manager object.</param>
    /// <param name="inventory">The list of inventory items that make up the inventory.</param>
    public InventoryState(IGameUIContext gameUIContext, InventoryConfig inventoryConfig, WindowConfig windowConfig, 
        IGameWindowScaleAccessor gameWindowScalesAccessor, IWindowAssetAccessor windowAssetManager, List<IInventoryItemAccessor> inventory) {
        this.gameUIContext = gameUIContext;
        currentChoiceX = 0;
        currentChoiceY = 0;

        // Assign input map.
        inputMap = new InventoryInputMap(this);

        // Create inventory lists.
        leftSideInventory = [];
        rightSideInventory = [];
        int index = 0;
        foreach (IInventoryItemAccessor item in inventory) {
            if (index % 2 == 0) {
                leftSideInventory.Add(item);
            }
            else {
                rightSideInventory.Add(item);
            }
            index ++;
        }

        // Store config variables.
        maxLinesPerPage = windowConfig.GetMaxLinesPerWindow();
        int windowArtId = inventoryConfig.GetWindowId();
        int choiceBoxArtId = inventoryConfig.GetChoiceBoxId();
        int fontId = inventoryConfig.GetFontId();
        fontSize = inventoryConfig.GetFontSize();
        int itemsWindowWidth = inventoryConfig.GetInventoryWindowWidth();
        int itemsWindowHeight = inventoryConfig.GetInventoryWindowHeight();
        int itemsWindowX = inventoryConfig.GetInventoryWindowX();
        int itemsWindowY = inventoryConfig.GetInventoryWindowY();
        int descriptionWindowWidth = inventoryConfig.GetDescriptionWindowWidth();
        int descriptionWindowHeight = inventoryConfig.GetDescriptionWindowHeight();
        int descriptionWindowX = inventoryConfig.GetDescriptionWindowX();
        int descriptionWindowY = inventoryConfig.GetDescriptionWindowY();

        // Store window art and font file name.
        string windowArtFileName = windowAssetManager.GetWindowArtFileName(windowArtId);
        string choiceBoxArtFileName = windowAssetManager.GetChoiceSelectionFileName(choiceBoxArtId);
        fontArtFileName = windowAssetManager.GetFontFileName(fontId);

        // Item description box.
        itemDescriptionWindow = new WindowComponent(descriptionWindowX, descriptionWindowY, descriptionWindowWidth, descriptionWindowHeight,
            windowArtFileName, windowConfig, gameWindowScalesAccessor);
        if (leftSideInventory.Count > 0) {
            itemDescriptionText = new PageTextComponent(descriptionWindowX, descriptionWindowY, fontSize, fontArtFileName, 
                PageGenerator.TurnTextIntoPages(leftSideInventory[0].GetItemDescription(), maxLinesPerPage));
        }
        else {
            itemDescriptionText = new PageTextComponent(descriptionWindowX, descriptionWindowY, fontSize, fontArtFileName, 
                new string[1, 1] {{ "" }});
        }
        components.Push(itemDescriptionWindow);
        components.Push(itemDescriptionText);

        // Inventory items window.
        itemsWindow = new WindowComponent(itemsWindowX, itemsWindowY, itemsWindowWidth, itemsWindowHeight, windowArtFileName,
            windowConfig, gameWindowScalesAccessor);
        int halfItemsWindowWidth = (int) (((float) gameWindowScalesAccessor.GetWidth() * (float) ((float) itemsWindowWidth 
            / (float) 100)) / (float) 2);
        itemText = [];
        AssignItemText(leftSideInventory, itemsWindowX, itemsWindowY);
        AssignItemText(rightSideInventory, itemsWindowX + halfItemsWindowWidth, itemsWindowY);
        components.Push(itemsWindow);
        foreach (ListTextComponent listItemText in itemText) {
            components.Push(listItemText);
        }

        // Choice boxes.
        // TODO: (MIC-01) Get rid of magic number here add choice box height into config. Use offset too.
        int choiceBoxHeight = (itemsWindowHeight / fontSize) * 2;
        leftChoiceBox = new ChoiceBoxComponent(itemsWindowX, itemsWindowY, itemsWindowWidth / 2, choiceBoxHeight,  fontSize, 
            choiceBoxArtFileName, leftSideInventory.Count, windowConfig, gameWindowScalesAccessor);
        rightChoiceBox = new ChoiceBoxComponent(itemsWindowX + halfItemsWindowWidth, itemsWindowY, itemsWindowWidth / 2, 
            choiceBoxHeight, fontSize, choiceBoxArtFileName, leftSideInventory.Count, windowConfig, gameWindowScalesAccessor);
        components.Push(leftChoiceBox);
    }

    /// <summary>
    /// Select function.
    /// </summary>
    public void Select() {
        // TODO: (MIC-01) Implement select.
    }

    /// <summary>
    /// Function used to move the inventory selector up.
    /// </summary>
    public void MoveUp() {
        int oldChoiceY = currentChoiceY;
        currentChoiceY = currentChoiceY > 0 ? currentChoiceY - 1
            : leftSideInventory.Count > rightSideInventory.Count && currentChoiceX == 1 ?
            rightSideInventory.Count : leftSideInventory.Count - 1;
        if (oldChoiceY != currentChoiceY) {
            UpdateDescription();
            // TODO: (MIC-01) Scroll up, update the item names.
        }
    }

    /// <summary>
    /// Function used to move the inventory selector down.
    /// </summary>
    public void MoveDown() {
        int oldChoiceY = currentChoiceY;
        currentChoiceY = (currentChoiceX == 1 && currentChoiceY < rightSideInventory.Count - 1)
            || (currentChoiceX == 0 && currentChoiceY < leftSideInventory.Count - 1)
            ? currentChoiceY + 1 : 0;
        if (oldChoiceY != currentChoiceY) {
            UpdateDescription();
            // TODO: (MIC-01) Scroll down, update the item names.
        }
    }

    /// <summary>
    /// Function used to move the inventory selector left.
    /// </summary>
    public void MoveLeft() {
        HorizontalMove();
    }

    /// <summary>
    /// Function used to move the inventory selector right.
    /// </summary>
    public void MoveRight() {
        HorizontalMove();
    }

    /// <summary>
    /// The cancel function.
    /// </summary>
    public void Cancel() {
        gameUIContext.OpenMainMenu();
        Close();
    }

    public void Escape() {
        Close();
    }

    /// <summary>
    /// Function used to close the inventory state.
    /// </summary>
    protected override void Close() {
        // TODO: (UICA-01) Implement closing animation.
        closed = true;
    }

    /// <summary>
    /// Helper function used to move the selection horizontally.
    /// </summary>
    private void HorizontalMove() {
        if (currentChoiceX == 1) {
            UpdateDescription();
            currentChoiceX = 0;
            components.Pop();
            components.Push(leftChoiceBox);
        }
        else if (rightSideInventory.Count >= currentChoiceY) {
            UpdateDescription();
            currentChoiceX = 1;
            components.Pop();
            components.Push(rightChoiceBox);
        }
    }

    /// <summary>
    /// Function used to update the current item description text.
    /// </summary>
    private void UpdateDescription() {
        string description = currentChoiceX % 2 == 0 ? leftSideInventory[currentChoiceY].GetItemDescription() 
            : rightSideInventory[currentChoiceY].GetItemDescription();
        itemDescriptionText.SetText(PageGenerator.TurnTextIntoPages(description, maxLinesPerPage));
    }

    /// <summary>
    /// Helper function used to add item names to the item text list.
    /// </summary>
    private void AssignItemText(List<IInventoryItemAccessor> inventory, int xLocation, int yLocation) {
        // TODO: (MIC-01) When we implement scrolling make sure this doesen't go over the scroll limit...
        int itemIndex = 0;
        foreach (IInventoryItemAccessor inventoryItemAccessor in inventory) {
            itemText.Add(new(xLocation, yLocation + (fontSize * itemIndex), fontSize, fontArtFileName, inventoryItemAccessor.GetItemName()));
            itemIndex ++;
        }
    }

    // Stored game variables.
    private readonly IGameUIContext gameUIContext;

    // Stored config.
    private readonly int maxLinesPerPage, fontSize;
    private int currentChoiceX, currentChoiceY;
    private readonly string fontArtFileName;

    // Components.
    private readonly WindowComponent itemDescriptionWindow, itemsWindow;
    private readonly PageTextComponent itemDescriptionText;
    private readonly ChoiceBoxComponent leftChoiceBox, rightChoiceBox;
    private readonly List<ListTextComponent> itemText;
    private readonly List<IInventoryItemAccessor> leftSideInventory, rightSideInventory;
}