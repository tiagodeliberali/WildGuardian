using Assets.Signals;

using UnityEngine;

using Zenject;

public class ItemPickup : MonoBehaviour
{
    public Item Item;
    private SignalBus signalBus;

    [Inject]
    public void Contruct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        signalBus.Fire(ItemActionSignal.Pickup(Item));
        Destroy(this.gameObject);
    }
}
