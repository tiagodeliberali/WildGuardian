using Assets.Items;
using Assets.Signals;

using UnityEngine;

using Zenject;

public class ItemPickup : MonoBehaviour
{
	public Item Item;
	private ItemInstance instance;
	private SignalBus signalBus;

	[Inject]
	public void Contruct(SignalBus signalBus)
	{
		this.signalBus = signalBus;
	}

	private void Awake()
	{
		instance = new ItemInstance(Item);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		signalBus.Fire(ItemActionSignal.Pickup(instance));
		Destroy(gameObject);
	}
}
