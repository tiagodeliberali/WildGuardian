using System;
using System.Collections.Generic;

using Assets.Items;
using Assets.Signals;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

using Zenject;

public class InventoryUI : MonoBehaviour
{
	public Dictionary<Guid, InvetoryItemUI> InventoryItems = new Dictionary<Guid, InvetoryItemUI>();

	private SignalBus signalBus;

	// Used to instantiate items UI on the inventory
	public Transform ItemPlaceholder;
	public GameObject InventoryItem;

	// UI
	public GameObject Inventory;
	public Toggle EnableRemove;

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
		switch (itemAction.Action)
		{
			case ItemAction.Pickup:
				Add(itemAction.Item);
				break;
			case ItemAction.Sell:
			case ItemAction.Drop:
			case ItemAction.Use:
				InventoryItems.Remove(itemAction.Item.Id);
				break;
		}
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

	private void Add(ItemInstance instance)
	{
		GameObject obj = Instantiate(InventoryItem, ItemPlaceholder);

		var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
		itemIcon.sprite = instance.Definition.icon;

		var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
		itemName.text = instance.Definition.itemName;

		var controller = obj.GetComponent<InvetoryItemUI>();
		controller.AssociateItem(instance);
		controller.SetRemoveButtonActive(EnableRemove.isOn);

		InventoryItems.Add(instance.Id, controller);
	}

	public void EnableItemsRemove()
	{
		foreach (var item in InventoryItems)
		{
			item.Value.SetRemoveButtonActive(EnableRemove.isOn);
		}
	}

	public void EnableItemsOfType(ItemType type)
	{
		foreach (var item in InventoryItems)
		{
			item.Value.EnableInteractonIfType(type);
		}
	}
}
