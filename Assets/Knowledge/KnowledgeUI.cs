using System.Collections.Generic;

using Assets.Character;
using Assets.Knowledge;
using Assets.Signals;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

using Zenject;

public class KnowledgeUI : MonoBehaviour
{
	public GameObject knowledge;
	public GameObject details;

	public Image icon;

	public TextMeshProUGUI itemName;
	public TextMeshProUGUI description;
	public TextMeshProUGUI incubator;

	public TextMeshProUGUI eggValue;
	public TextMeshProUGUI puppyValue;
	public TextMeshProUGUI drop;

	public TextMeshProUGUI dropValue;
	public TextMeshProUGUI timeToHatch;
	public TextMeshProUGUI timeToGrow;

	public TextMeshProUGUI food;
	public TextMeshProUGUI count;

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
	}

	public void OpenWindow()
	{
		knowledge.SetActive(true);
		details.SetActive(false);
		
		signalBus.Fire(UISignal.Opened());
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
		foreach (var item in character.knowledge.Values)
		{
			GameObject obj = Instantiate(InventoryItem, ItemPlaceholder);

			var itemName = obj.transform.Find("Icon").GetComponent<Image>();
			itemName.sprite = item.Definition.icon;

			var controller = obj.GetComponent<KnowledgeItemUI>();
			controller.AssociateItem(item, this);

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

	public void SelectItem(KnowledgeItemInstance instance)
	{
		var definition = instance.Definition;

		icon.sprite = definition.icon;

		itemName.text = definition.itemName;
		description.text = definition.description;
		incubator.text = definition.incubator;

		eggValue.text = $"$ {definition.eggValue}";
		puppyValue.text = $"$ {definition.puppyValue}";
		drop.text = definition.drop;

		dropValue.text = $"$ {definition.dropValue}";
		timeToHatch.text = $"{definition.timeToHatch} dias";
		timeToGrow.text = $"{definition.timeToGrow} dias";

		food.text = definition.food;
		count.text = $"{instance.Count}";

		details.SetActive(true);
	}
}
