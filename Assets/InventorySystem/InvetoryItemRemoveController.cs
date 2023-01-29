using UnityEngine;
using UnityEngine.UI;

public class InvetoryItemRemoveController : MonoBehaviour
{
    Item item;

    public Button RemoveButtom;

    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);

        Destroy(gameObject);
    }

	public void UseItem()
	{
        Debug.Log("Item used!");

        RemoveItem();
	}

	public void AddItem(Item item)
    {
        this.item = item;
    }
}
