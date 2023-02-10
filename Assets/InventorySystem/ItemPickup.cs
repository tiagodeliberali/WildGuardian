using UnityEngine;

using Zenject;

public class ItemPickup : MonoBehaviour
{
    public Item Item;
	private InventoryManager inventoryManager;

	[Inject]
	public void Contruct(InventoryManager inventoryManager)
	{
		this.inventoryManager = inventoryManager;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Pickup();
	}

	private void Pickup()
	{
		inventoryManager.Add(Item);
		Destroy(gameObject);
	}
}
