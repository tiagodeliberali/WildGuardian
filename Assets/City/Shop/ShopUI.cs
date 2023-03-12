using System;
using System.Collections.Generic;

using Assets.Character;
using Assets.InventorySystem;
using Assets.Items;
using Assets.Signals;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

using Zenject;

public class ShopUI : MonoBehaviour, IAssociateInventory
{
    private Action closeInventoryUI;
    private InventoryUI inventoryUI;
    private CharacterData character;
    private SignalBus signalBus;
    private Item item;

    private List<Item> shopInventory;
    private float buyFactor;
    private float sellFactor;
    private ItemType itemType;
    private bool isShopBuyingItems;

    public Button ConfirmSellButton;
    public TextMeshProUGUI Value;
    public Image Icon;


    [Inject]
    public void Contruct(InventoryUI inventoryUI, SignalBus signalBus, CharacterData character)
    {
        this.inventoryUI = inventoryUI;
        this.signalBus = signalBus;
        this.character = character;
    }

    public void OpenUI(List<Item> shopInventory, float buyFactor, float sellFactor, ItemType itemType)
    {
        this.shopInventory = shopInventory;
        this.buyFactor = buyFactor;
        this.sellFactor = sellFactor;
        this.itemType = itemType;
        this.isShopBuyingItems = true;

        this.gameObject.SetActive(true);
        inventoryUI.OpenWindow(this, false);
        this.inventoryUI.ClearExternalInventory();

        this.ResetDetails();
    }

    private void ResetDetails()
    {
        ConfirmSellButton.interactable = false;
        Value.text = string.Empty;
    }

    public void CloseButtonClick() => this.closeInventoryUI();

    public void SetShopToSellItemsUI()
    {
        this.ResetDetails();
        this.isShopBuyingItems = false;
        this.inventoryUI.SetExternalInventory(this.shopInventory);
    }

    public void SetShopToBuyItemsUI()
    {
        this.ResetDetails();
        this.isShopBuyingItems = true;
        this.inventoryUI.ClearExternalInventory();
    }

    public void ConfirmTransaction()
    {
        var price = this.GetItemPrice(item);

        if (this.isShopBuyingItems)
        {
            this.signalBus.Fire(ItemActionSignal.Sell(this.item));
            this.character.AddMoney(price);
            this.shopInventory.Add(item);
        }
        else
        {
            this.signalBus.Fire(ItemActionSignal.Pickup(this.item));
            this.character.SpendMoney(price);
            this.shopInventory.Remove(item);
        }

        this.closeInventoryUI();
    }

    public void AssociateCloseCall(Action closeWindow) => this.closeInventoryUI = closeWindow;

    public void CloseWindow() => this.gameObject.SetActive(false);

    public ItemType GetItemType() => this.itemType;

    public bool SelectItem(Item item)
    {
        this.item = item;
        var price = this.GetItemPrice(item);

        ConfirmSellButton.interactable = this.isShopBuyingItems || this.character.CanSpendMoney(price);

        Value.text = $"$ {price}";

        return false;
    }

    private int GetItemPrice(Item item)
    {
        var priceAdjustment = this.isShopBuyingItems ? this.buyFactor : this.sellFactor;
        return (int) Math.Floor(item.value * priceAdjustment);
    }
}
