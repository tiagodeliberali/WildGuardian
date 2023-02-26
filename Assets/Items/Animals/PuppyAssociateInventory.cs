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
        GameObject obj = Instantiate(ItemPrefab, ItemPlaceholder);
        obj.transform.position = PlayerReference.transform.position + new Vector3(0, -1f, 0);
        obj.transform.localScale = new Vector3(2.5f, 2.5f, 1);

        var spriteRenderer = obj.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.icon;

        var itemController = obj.GetComponent<IncubatorItem>();
        itemController.Associate(item, timeManager);

        var itemPickup = obj.GetComponent<ItemPickup>();
        itemPickup.Associate(item, signalBus);
        itemPickup.Enabled = false;

        return true;
    }
}
