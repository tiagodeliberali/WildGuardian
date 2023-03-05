using System;
using System.Collections.Generic;

using Assets;
using Assets.InventorySystem;
using Assets.Items;

using UnityEngine;

using Zenject;

public class IncubatorUI : MonoBehaviour, IAssociateInventory
{
    // Used to instantiate items UI on the inventory
    public Transform InventoryItemPlaceholder;
    public GameObject InventoryItem;

    public Transform ItemPlaceholder;
    public GameObject Item;

    public int MaxNumberOfEggs = 1;

    private InventoryUI inventoryUI;
    private TimeManager timeManager;
    private SignalBus signalBus;
    private List<IncubatorItemUI> items = new List<IncubatorItemUI>();
    private Action closeInventoryUI;

    [Inject]
    public void Contruct(InventoryUI inventoryUI, TimeManager timeManager, SignalBus signalBus)
    {
        this.inventoryUI = inventoryUI;
        this.timeManager = timeManager;
        this.signalBus = signalBus;
    }

    public void OpenUI()
    {
        this.gameObject.SetActive(true);
        inventoryUI.OpenWindow(this);
    }

    public void CloseWindow()
    {
        this.gameObject.SetActive(false);
    }

    public void CloseButtonClick()
    {
        this.closeInventoryUI();
    }

    public bool SelectItem(Item definition)
    {
        if (items.Count == MaxNumberOfEggs)
        {
            return false;
        }

        var inventoryItemController = InventoryItem
               .GetComponent<IGenerateGameObject>()
               .Build(InventoryItemPlaceholder, definition)
               .GetComponent<IncubatorItemUI>();

        inventoryItemController.Associate(timeManager, this);
        items.Add(inventoryItemController);

        var itemController = Item
            .GetComponent<IGenerateGameObject>()
            .Build(ItemPlaceholder, definition)
            .GetComponent<IncubatorItem>();

        itemController.Associate(signalBus, timeManager, false);

        return true;
    }

    public ItemType GetItemType()
    {
        return ItemType.Egg;
    }

    internal void Remove(IncubatorItemUI incubatorItemUI)
    {
        items.Remove(incubatorItemUI);
    }

    public void AssociateCloseCall(Action closeWindow)
    {
        this.closeInventoryUI = closeWindow;
    }
}
