using System;

using Assets.GameTime;
using Assets.Signals;

using UnityEngine;

using Zenject;

using static UnityEditor.Progress;

public class ItemPickup : MonoBehaviour
{
    public Item Item;
    private SignalBus signalBus;

    public bool Enabled = true;

    public delegate void PickupEventDelegate(Item Item);

    public event PickupEventDelegate OnItemPickup;

    [Inject]
    public void Contruct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }

    public void Associate(Item item, SignalBus signalBus)
    {
        this.Item = item;
        this.signalBus = signalBus;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Enabled)
        {
            return;
        }

        OnItemPickup?.Invoke(Item);
        signalBus.Fire(ItemActionSignal.Pickup(Item));
        Destroy(this.gameObject);
    }

    public void SetItem(Item item) => this.Item = item;
}
