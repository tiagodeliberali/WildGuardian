using System;

using Assets;
using Assets.InventorySystem;
using Assets.Items;

using UnityEngine;

using Zenject;

public class PuppyAssociateInventory : MonoBehaviour, IAssociateInventory
{
    public Transform ItemPlaceholder;
    public GameObject ItemPrefab;
    public GameObject PlayerReference;

    private TimeManager timeManager;
    private SignalBus signalBus;

    [Inject]
    public void Contruct(SignalBus signalBus, TimeManager timeManager)
    {
        this.signalBus = signalBus;
        this.timeManager = timeManager;
    }

    public void Associate(Transform itemPlaceholder, GameObject itemPrefab)
    {
        this.ItemPlaceholder = itemPlaceholder;
        this.ItemPrefab = itemPrefab;
    }

    public void CloseWindow()
    {
        // do nothing
    }

    public ItemType GetItemType() => ItemType.Puppy;

    public bool SelectItem(Item item)
    {
        var itemController = this.ItemPrefab
            .GetComponent<IGenerateGameObject>()
            .Build(ItemPlaceholder, item)
            .GetComponent<IncubatorItem>();

        itemController.Associate(signalBus, timeManager, true, PlayerReference.transform.position + new Vector3(0, -1f, 0));

        return true;
    }

    public void AssociateCloseCall(Action closeWindow)
    {
        // do nothing
    }
}
