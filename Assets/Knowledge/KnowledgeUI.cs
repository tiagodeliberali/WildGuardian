using System.Collections.Generic;

using Assets.Knowledge;
using Assets.Signals;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

using Zenject;

public class KnowledgeUI : MonoBehaviour
{
	public SignalBus signalBus;
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

	private Dictionary<string, KnowledgeItemInstance> items = new Dictionary<string, KnowledgeItemInstance>();

	// Used to instantiate items UI on the inventory
	public Transform ItemPlaceholder;
	public GameObject InventoryItem;

	[Inject]
	public void Contruct(SignalBus signalBus)
	{
		this.signalBus = signalBus;
	}

	private void Awake()
	{
		signalBus.Subscribe<ItemActionSignal>(this.OnItemActionHappened);
	}

	private void OnItemActionHappened(ItemActionSignal itemAction)
	{
		if (itemAction.Action.Equals(ItemAction.Pickup))
		{
			CollectItem(itemAction.Item.Definition);
		}
	}

	public void OpenWindow()
	{
		knowledge.SetActive(true);
		details.SetActive(false);
		
		signalBus.Fire(UISignal.Opened());
	}

	public void CloseWindow()
	{
		knowledge.SetActive(false);
		signalBus.Fire(UISignal.Closed());
	}

	public void CollectItem(Item item)
	{
		if (!items.ContainsKey(item.name))
		{
			items.Add(item.name, new KnowledgeItemInstance(item));

			GameObject obj = Instantiate(InventoryItem, ItemPlaceholder);

			var itemName = obj.transform.Find("Icon").GetComponent<Image>();
			itemName.sprite = item.icon;

			var controller = obj.GetComponent<KnowledgeItemUI>();
			controller.AssociateItem(item.name, this);
		}

		items[item.name].Add();
	}

	public void SelectItem(string name)
	{
		var instance = items[name];
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
