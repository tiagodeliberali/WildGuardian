using System;
using System.Linq;

using Assets.Character;
using Assets.InventorySystem;
using Assets.Items;
using Assets.Signals;

using UnityEngine;

using Zenject;

/// <summary>
/// Load data from CharacterData
/// </summary>
public class KnowledgeUI : MonoBehaviour, IAssociateInventory
{
    public GameObject knowledge;
    public KnowledgeDetailsUI knowledgeDetails;

    private InventoryUI inventoryUI;
    private SignalBus signalBus;
    private CharacterData character;
    private Action closeInventoryUI;

    [Inject]
    public void Contruct(InventoryUI inventoryUI, SignalBus signalBus, CharacterData character)
    {
        this.inventoryUI = inventoryUI;
        this.signalBus = signalBus;
        this.character = character;

        signalBus.Subscribe<UISignal>(this.OnUIStateChange);
    }

    private void OnUIStateChange(UISignal signal)
    {
        if (knowledge.activeSelf && signal.IsOpen)
        {
            this.CloseWindow();
        }
    }

    public void OpenWindow()
    {
        this.gameObject.SetActive(true);
        this.inventoryUI.OpenWindow(this, false);
        this.inventoryUI.SetExternalInventory(
            character.Knowledge
                .Select(x => x.Value.Definition)
                .ToList());
    }

    public void CloseWindow() => knowledge.SetActive(false);

    public void CloseButtonClick() => this.closeInventoryUI();

    public bool SelectItem(Item item)
    {
        var knowledgeItem = character.Knowledge[item.itemName];
        knowledgeDetails.SelectItem(knowledgeItem);
        return false;
    }

    public ItemType GetItemType() => ItemType.All;


    public void AssociateCloseCall(Action closeWindow) => this.closeInventoryUI = closeWindow;
}
