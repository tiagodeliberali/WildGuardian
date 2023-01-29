using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;

	private void OnMouseDown()
	{
		Pickup();
	}

	private void Pickup()
	{
		InventoryManager.Instance.Add(Item);
		Destroy(gameObject);
	}
}
