using System;

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
    private SignalBus signalBus;
    private Item item;

    public Button ConfirmSellButton;
    public TextMeshProUGUI Value;
    public Image Icon;


    [Inject]
    public void Contruct(InventoryUI inventoryUI,  SignalBus signalBus)
    {
        this.inventoryUI = inventoryUI;
        this.signalBus = signalBus;
    }

    public void OpenUI()
    {
        this.gameObject.SetActive(true);
        inventoryUI.OpenWindow(this);

        ConfirmSellButton.interactable = false;
        Value.text = string.Empty;
    }

    public void CloseButtonClick() => this.closeInventoryUI();

    public void ConfirmSell()
    {
        this.signalBus.Fire(ItemActionSignal.Sell(this.item));
        this.closeInventoryUI();
    }

    public void AssociateCloseCall(Action closeWindow) => this.closeInventoryUI = closeWindow;

    public void CloseWindow() => this.gameObject.SetActive(false);

    public ItemType GetItemType() => ItemType.Egg;

    public bool SelectItem(Item item)
    {
        this.item = item;

        ConfirmSellButton.interactable = true;
        Value.text = $"$ {item.value}";

        return false;
    }
}
