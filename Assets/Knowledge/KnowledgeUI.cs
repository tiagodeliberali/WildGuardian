using System.Collections.Generic;

using Assets;
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
    public GameObject KnowledgeItem;

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
            this.CloseWindow();
        }
    }

    public void OpenWindow()
    {
        if (knowledge.activeSelf)
        {
            this.CloseWindow();
            return;
        }

        signalBus.Fire(UISignal.Opened());

        knowledge.SetActive(true);
        knowledgeDetails.SetActive(false);

        this.LoadItems();
    }

    public void CloseWindow()
    {
        knowledge.SetActive(false);
        signalBus.Fire(UISignal.Closed());
        this.ClearItems();
    }

    public void LoadItems()
    {
        foreach (var item in character.Knowledge.Values)
        {
            var controller = KnowledgeItem
                .GetComponent<IGenerateGameObject>()
                .Build(ItemPlaceholder, item)
                .GetComponent<KnowledgeItemUI>();

            controller.AssociateItem(knowledgeDetails);

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
