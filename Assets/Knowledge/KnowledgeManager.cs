using System.Collections.Generic;

using Assets.Signals;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

using Zenject;

public class KnowledgeManager : MonoBehaviour
{
	public SignalBus signalBus;
	public GameObject knowledge;
	public GameObject details;

	public Transform listOfItems;

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

	private Dictionary<string, Item> items = new Dictionary<string, Item>();

	// Used to instantiate items UI on the inventory
	public Transform ItemPlaceholder;
	public GameObject InventoryItem;

	[Inject]
	public void Contruct(InventoryManager inventoryManager, SignalBus signalBus)
	{
		this.signalBus = signalBus;
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
			items.Add(item.name, item);

			GameObject obj = Instantiate(InventoryItem, ItemPlaceholder);
			var controller = obj.GetComponent<KnowledgeItemController>();
			controller.AssociateItem(item, this);
		}

		items[item.name].count++;
	}

	public void AssociateItem(Item item)
	{
		var knowItem = items[item.name];

		icon.sprite = knowItem.icon;

		itemName.text = knowItem.itemName;
		description.text = knowItem.description;
		incubator.text = knowItem.incubator;

		eggValue.text = $"$ {knowItem.eggValue}";
		puppyValue.text = $"$ {knowItem.puppyValue}";
		drop.text = knowItem.drop;

		dropValue.text = $"$ {knowItem.dropValue}";
		timeToHatch.text = $"{knowItem.timeToHatch} dias";
		timeToGrow.text = $"{knowItem.timeToGrow} dias";

		food.text = knowItem.food;
		count.text = $"{knowItem.count}";

		details.SetActive(true);
	}
}
