using UnityEngine;
using UnityEngine.UI;

public class InvetoryItemController : MonoBehaviour
{
    private Item item;
    private Button button;
    public Button RemoveButton;

	public void RemoveItem()
    {
        InventoryManager.Instance.Remove(this);
		Destroy(gameObject);
    }

	public void UseItem()
	{
        Debug.Log($"Item used! {item.name}");
        RemoveItem();
	}

	public void AssociateItem(Item item)
    {
        this.item = item;
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
