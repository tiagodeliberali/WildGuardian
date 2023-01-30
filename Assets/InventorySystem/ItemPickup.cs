using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("Trigger happened!");
		Pickup();
	}

	private void Pickup()
	{
		InventoryManager.Instance.Add(Item);
		Destroy(gameObject);
	}
}
