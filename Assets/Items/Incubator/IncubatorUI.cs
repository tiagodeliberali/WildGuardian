using System;
using System.Collections.Generic;

using Assets.InventorySystem;
using Assets.Items;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

using Zenject;

public class IncubatorUI : MonoBehaviour, IAssociateInventory
{
	// Used to instantiate items UI on the inventory
	public Transform ItemPlaceholder;
	public GameObject InventoryItem;

	public int MaxNumberOfEggs = 2;

	private InventoryUI inventoryUI;
	private TimeManager timeManager;
	private List<IncubatorItemUI> items = new List<IncubatorItemUI>();

	[Inject]
	public void Contruct(InventoryUI inventoryUI, TimeManager timeManager)
	{
		this.inventoryUI = inventoryUI;
		this.timeManager = timeManager;
	}

	public void OpenUI()
	{
		gameObject.SetActive(true);
		inventoryUI.OpenWindow(this);
	}

	public void CloseWindow()
	{
		gameObject.SetActive(false);
	}

	public bool SelectItem(ItemInstance instance)
	{
		if (items.Count == MaxNumberOfEggs)
		{
			return false;
		}

		GameObject obj = Instantiate(InventoryItem, ItemPlaceholder);

		var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
		itemIcon.sprite = instance.Definition.icon;

		var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
		itemName.text = instance.Definition.itemName;

		var controller = obj.GetComponent<IncubatorItemUI>();
		controller.Associate(instance.Definition, timeManager, this);
		items.Add(controller);

		return true;
	}

	public ItemType GetItemType()
	{
		return ItemType.Egg;
	}

	internal void Remove(IncubatorItemUI incubatorItemUI)
	{
		items.Remove(incubatorItemUI);
	}
}
