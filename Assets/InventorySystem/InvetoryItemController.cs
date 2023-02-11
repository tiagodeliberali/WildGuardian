using UnityEngine;
using UnityEngine.UI;

public class InvetoryItemController : MonoBehaviour
{
	private Item item;
	private InventoryManager inventoryManager;
	private Button button;
	public Button RemoveButton;

	public void RemoveItem()
	{
		inventoryManager.Remove(this);
		Destroy(gameObject);
	}

	public void UseItem()
	{
		Debug.Log($"Item used! {item.name}");
		RemoveItem();
	}

	public void AssociateItem(Item item, InventoryManager inventoryManager)
	{
		this.item = item;
		this.inventoryManager = inventoryManager;

		button = gameObject.GetComponent<Button>();
	}

	public void SetRemoveButtonActive(bool active)
	{
		RemoveButton.gameObject.SetActive(active);
	}

	public void EnableButtonIfType(ItemType type)
	{
		button.interactable = type == item.type;
	}
}
