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

    public void OpenWindow(IAssociateInventory association)
    {
        this.association = association;
        this.LoadWindow();
        this.EnableItemsOfType(association.GetItemType());
    }

    public void OpenWindow()
    {
        this.association = puppyAssociation;
        this.LoadWindow();
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
        this.LoadItems();
    }

    public void CloseWindow()
    {
        Inventory.SetActive(false);
        signalBus.Fire(UISignal.Closed());
        this.ClearItems();

        if (association != null)
        {
            association.CloseWindow();
        }

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
        foreach (var instance in character.Inventory)
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
