using System.Collections.Generic;

using Assets.Signals;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

using Zenject;

public class InventoryManager : MonoBehaviour
{
	public List<InvetoryItemController> InventoryItems = new List<InvetoryItemController>();

	private SignalBus signalBus;
	private KnowledgeManager knowledgeManager;

	// Used to instantiate items UI on the inventory
	public Transform ItemPlaceholder;
	public GameObject InventoryItem;

	// UI
	public GameObject Inventory;
	public Toggle EnableRemove;

	[Inject]
	public void Contruct(SignalBus signalBus, KnowledgeManager knowledgeManager)
	{
		this.signalBus = signalBus;
		this.knowledgeManager = knowledgeManager;
	}

	public void OpenWindow()
	{
		Inventory.SetActive(true);
		signalBus.Fire(UISignal.Opened());
	}

	public void CloseWindow()
	{
		Inventory.SetActive(false);
		signalBus.Fire(UISignal.Closed());
	}

	public void Add(Item item)
	{
		GameObject obj = Instantiate(InventoryItem, ItemPlaceholder);

		var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
		itemIcon.sprite = item.icon;

		var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
		itemName.text = item.itemName;

		var controller = obj.GetComponent<InvetoryItemController>();
		controller.AssociateItem(item, this);
		controller.SetRemoveButtonActive(EnableRemove.isOn);

		InventoryItems.Add(controller);
		knowledgeManager.CollectItem(item);
	}

	public void Remove(InvetoryItemController item)
	{
		InventoryItems.Remove(item);
	}

	public void EnableItemsRemove()
	{
		foreach (var item in InventoryItems)
		{
			item.SetRemoveButtonActive(EnableRemove.isOn);
		}
	}

	public void EnableItemsOfType(ItemType type)
	{
		foreach (var item in InventoryItems)
		{
			item.EnableButtonIfType(type);
		}
	}
}
