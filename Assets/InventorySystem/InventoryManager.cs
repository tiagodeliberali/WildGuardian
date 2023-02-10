using System.Collections.Generic;

using Assets.MessageSystem;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

using Zenject;

public class InventoryManager : MonoBehaviour
{
	public List<InvetoryItemController> InventoryItems = new List<InvetoryItemController>();

	public MessageManager messageManager;

	// Used to instantiate items UI on the inventory
	public Transform ItemPlaceholder;
	public GameObject InventoryItem;

	// UI
	public GameObject Inventory;
	public Toggle EnableRemove;

	private InventoryManager inventoryManager;

	[Inject]
	public void Contruct(InventoryManager inventoryManager)
	{
		this.inventoryManager = inventoryManager;
	}

	public void OpenWindow()
	{
		Inventory.SetActive(true);
		messageManager.AlertSubscribers(new Message(MessageType.UIWindowOpened));
	}

	public void CloseWindow()
	{
		Inventory.SetActive(false);
		messageManager.AlertSubscribers(new Message(MessageType.UIWindowClosed));
	}

	public void Add(Item item)
	{
		GameObject obj = Instantiate(InventoryItem, ItemPlaceholder);

		var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
		itemIcon.sprite = item.icon;

		var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
		itemName.text = item.itemName;

		var controller = obj.GetComponent<InvetoryItemController>();
		controller.AssociateItem(item, inventoryManager);
		controller.SetRemoveButtonActive(EnableRemove.isOn);

		InventoryItems.Add(controller);
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
