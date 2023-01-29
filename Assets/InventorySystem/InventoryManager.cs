using System.Collections.Generic;

using Mono.Cecil;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> items = new List<Item>();

	public Transform ItemContent;
	public GameObject InventoryItem;

	public Toggle EnableRemove;

	public InvetoryItemRemoveController[] InventoryItems;


	private void Awake()
	{
		Instance = this;
	}

	public void Add(Item item)
	{
		items.Add(item);
	}

	public bool Remove(Item item)
	{
		return items.Remove(item);
	}

	public void ListItems()
	{
		foreach (Transform item in ItemContent)
		{
			Destroy(item.gameObject);
		}

		foreach (Item item in items)
		{
			GameObject obj = Instantiate(InventoryItem, ItemContent);
			
			var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
			itemIcon.sprite = item.icon;

			var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
			itemName.text = item.itemName;

			var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

			if (EnableRemove.isOn)
			{
				removeButton.gameObject.SetActive(true);
			}
		}

		SetInventoryItems();
	}

	public void EnableItemsRemove()
	{
		foreach (Transform item in ItemContent)
		{
			item.Find("RemoveButton").gameObject.SetActive(EnableRemove.isOn);
		}
	}

	private void SetInventoryItems()
	{
		this.InventoryItems = ItemContent.GetComponentsInChildren<InvetoryItemRemoveController>();

		for (int i = 0; i < items.Count; i++)
		{
			InventoryItems[i].AddItem(items[i]);
		}
	}
}
