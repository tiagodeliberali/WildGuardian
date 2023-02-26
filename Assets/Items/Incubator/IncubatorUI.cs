using System.Collections.Generic;

using Assets.InventorySystem;
using Assets.Items;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

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

    public bool SelectItem(Item definition)
    {
        if (items.Count == MaxNumberOfEggs)
        {
            return false;
        }

        GameObject inventoryObject = Instantiate(InventoryItem, InventoryItemPlaceholder);

        var itemIcon = inventoryObject.transform.Find("ItemIcon").GetComponent<Image>();
        itemIcon.sprite = definition.icon;

        var itemName = inventoryObject.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        itemName.text = definition.itemName;

        var inventoryItemController = inventoryObject.GetComponent<IncubatorItemUI>();
        inventoryItemController.Associate(definition, timeManager, this);
        items.Add(inventoryItemController);

        GameObject incubatorObject = Instantiate(Item, ItemPlaceholder);

        var spriteRenderer = incubatorObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = definition.icon;

        var itemController = incubatorObject.GetComponent<IncubatorItem>();
        itemController.Associate(definition, timeManager);

        var itemPickup = incubatorObject.GetComponent<ItemPickup>();
        itemPickup.Associate(definition, this.signalBus);
        itemPickup.Enabled = false;

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
}
