using System.Collections.Generic;
using System.Linq;

using Assets;
using Assets.Character;
using Assets.InventorySystem;
using Assets.Items;
using Assets.Signals;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

using Zenject;

/// <summary>
/// Loads all data from CharacterData
/// </summary>
public class InventoryUI : MonoBehaviour
{
    public List<InvetoryItemUI> InventoryItems = new List<InvetoryItemUI>();

    private SignalBus signalBus;
    private CharacterData character;
    private List<Item> externalInventory;

    private PuppyAssociateInventory puppyAssociation;

    // Used to instantiate items UI on the inventory
    public Transform ItemPlaceholder;
    public GameObject InventoryItem;

    // UI
    public GameObject Inventory;
    public Toggle EnableRemove;

    private IAssociateInventory association;

    [Inject]
    public void Contruct(SignalBus signalBus, CharacterData character, PuppyAssociateInventory puppyAssociation)
    {
        this.signalBus = signalBus;
        this.character = character;
        this.puppyAssociation = puppyAssociation;

        signalBus.Subscribe<UISignal>(this.OnUIStateChange);
    }

    private void OnUIStateChange(UISignal signal)
    {
        if (Inventory.activeSelf && signal.IsOpen)
        {
            this.CloseWindow();
        }
    }

    public void OpenWindow(IAssociateInventory association, bool loadItems = true)
    {
        association.AssociateCloseCall(this.CloseWindow);

        this.association = association;
        this.externalInventory = null;

        this.LoadWindow();

        if (loadItems)
        {
            this.LoadItems();
            this.EnableItemsOfType(association.GetItemType());
        }
    }

    public void SetExternalInventory(List<Item> externalInventory)
    {
        this.externalInventory = externalInventory;
        this.LoadItems();
        this.EnableItemsOfType(association.GetItemType());
    }

    public void ClearExternalInventory()
    {
        this.externalInventory = null;
        this.LoadItems();
        this.EnableItemsOfType(association.GetItemType());
    }

    public void OpenWindow()
    {
        this.association = puppyAssociation;
        this.externalInventory = null;
        this.LoadWindow();
        this.LoadItems();
        this.EnableItemsOfType(puppyAssociation.GetItemType());
    }

    private void LoadWindow()
    {
        if (Inventory.activeSelf)
        {
            this.CloseWindow();
            return;
        }

        signalBus.Fire(UISignal.Opened());

        Inventory.SetActive(true);
    }

    public void CloseWindow()
    {
        Inventory.SetActive(false);
        signalBus.Fire(UISignal.Closed());
        this.ClearItems();

        association?.CloseWindow();
        association = null;
    }

    private void ClearItems()
    {
        foreach (var item in this.GetValidInventoryItems())
        {
            item.Remove();
        }

        InventoryItems.Clear();
    }

    private void LoadItems()
    {
        this.ClearItems();
        foreach (var instance in this.GetInventoryToShow())
        {
            var controller = InventoryItem
                .GetComponent<IGenerateGameObject>()
                .Build(ItemPlaceholder, instance)
                .GetComponent<InvetoryItemUI>();

            controller.Associate(instance, signalBus, association);
            controller.SetRemoveButtonActive(EnableRemove.isOn);

            InventoryItems.Add(controller);
        }
    }

    private IReadOnlyList<Item> GetInventoryToShow()
    {
        return externalInventory == null
            ? character.Inventory
            : externalInventory;
    }

    public void EnableItemsRemove()
    {
        foreach (var item in this.GetValidInventoryItems())
        {
            item.SetRemoveButtonActive(EnableRemove.isOn);
        }
    }

    private void EnableItemsOfType(ItemType type)
    {
        foreach (var item in this.GetValidInventoryItems())
        {
            item.EnableInteractonIfType(type);
        }
    }

    private IEnumerable<InvetoryItemUI> GetValidInventoryItems() => InventoryItems.Where(x => x != null);
}
