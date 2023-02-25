using System.Collections.Generic;

using Assets.Character;
using Assets.Signals;

using UnityEngine;
using UnityEngine.UI;

using Zenject;

/// <summary>
/// Load data from CharacterData
/// </summary>
public class KnowledgeUI : MonoBehaviour
{
    public GameObject knowledge;
    public KnowledgeDetailsUI knowledgeDetails;

    // Used to instantiate items UI on the inventory
    public Transform ItemPlaceholder;
    public GameObject InventoryItem;

    private SignalBus signalBus;
    private CharacterData character;
    private List<KnowledgeItemUI> knowledgeItems = new List<KnowledgeItemUI>();

    [Inject]
    public void Contruct(SignalBus signalBus, CharacterData character)
    {
        this.signalBus = signalBus;
        this.character = character;

        signalBus.Subscribe<UISignal>(this.OnUIStateChange);
    }

    private void OnUIStateChange(UISignal signal)
    {
        if (knowledge.activeSelf && signal.IsOpen)
        {
            CloseWindow();
        }
    }

    public void OpenWindow()
    {
        if (knowledge.activeSelf)
        {
            CloseWindow();
            return;
        }

        signalBus.Fire(UISignal.Opened());

        knowledge.SetActive(true);
        knowledgeDetails.SetActive(false);

        LoadItems();
    }

    public void CloseWindow()
    {
        knowledge.SetActive(false);
        signalBus.Fire(UISignal.Closed());
        ClearItems();
    }

    public void LoadItems()
    {
        foreach (var item in character.Knowledge.Values)
        {
            GameObject obj = Instantiate(InventoryItem, ItemPlaceholder);

            var itemName = obj.transform.Find("Icon").GetComponent<Image>();
            itemName.sprite = item.Definition.icon;

            var controller = obj.GetComponent<KnowledgeItemUI>();
            controller.AssociateItem(item, knowledgeDetails);

            knowledgeItems.Add(controller);
        }
    }

    private void ClearItems()
    {
        foreach (var item in knowledgeItems)
        {
            item.Remove();
        }

        knowledgeItems.Clear();
    }
}
