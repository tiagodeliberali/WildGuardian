using UnityEngine;
using UnityEngine.UI;

public class InvetoryItemController : MonoBehaviour
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

	public void AssociateItem(Item item)
    {
        this.item = item;
    }
}
