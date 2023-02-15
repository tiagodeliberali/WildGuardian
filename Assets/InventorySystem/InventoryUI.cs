using System.Collections.Generic;

using Assets.Character;
using Assets.Items;
using Assets.Signals;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

using Zenject;

public class InventoryUI : MonoBehaviour
{
	public List<InvetoryItemUI> InventoryItems = new List<InvetoryItemUI>();

	private SignalBus signalBus;
	private CharacterData character;

	// Used to instantiate items UI on the inventory
	public Transform ItemPlaceholder;
	public GameObject InventoryItem;

	// UI
	public GameObject Inventory;
	public Toggle EnableRemove;

	[Inject]
	public void Contruct(SignalBus signalBus, CharacterData character)
	{
		this.signalBus = signalBus;
		this.character = character;
	}

	public void OpenWindow()
	{
		Inventory.SetActive(true);
		signalBus.Fire(UISignal.Opened());
		LoadItems();
	}

	public void CloseWindow()
	{
		Inventory.SetActive(false);
		signalBus.Fire(UISignal.Closed());
		ClearItems();
	}

	private void ClearItems()
	{
		foreach (var item in InventoryItems)
		{
			item.RemoveItem();
		}

		InventoryItems.Clear();
	}

	private void LoadItems()
	{
		foreach (var instance in character.inventory)
		{
			GameObject obj = Instantiate(InventoryItem, ItemPlaceholder);

			var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
			itemIcon.sprite = instance.Definition.icon;

			var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
			itemName.text = instance.Definition.itemName;

			var controller = obj.GetComponent<InvetoryItemUI>();
			controller.AssociateItem(instance);
			controller.SetRemoveButtonActive(EnableRemove.isOn);

			InventoryItems.Add(controller);
		}	
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
			item.EnableInteractonIfType(type);
		}
	}
}
