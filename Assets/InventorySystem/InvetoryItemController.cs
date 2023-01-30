using UnityEngine;
using UnityEngine.UI;

public class InvetoryItemController : MonoBehaviour
{
    private Item item;
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
    }

    public void SetRemoveButtonActive(bool active)
    {
		RemoveButton.gameObject.SetActive(active);
	}
}
