using Assets.InventorySystem;
using Assets.Items;
using Assets.Signals;

using UnityEngine;
using UnityEngine.UI;

using Zenject;

/// <summary>
/// Changes to items are communicated through signal
/// </summary>
public class InvetoryItemUI : MonoBehaviour
{
    private Item item;
    private Button itemButtom;
    private SignalBus signalBus;
    private IAssociateInventory association;
    public Button RemoveButton;

    private void Awake()
    {
        itemButtom = gameObject.GetComponent<Button>();
    }

    public void Remove()
    {
        Destroy(gameObject);
    }

    public void DropItem()
    {
        signalBus.Fire(ItemActionSignal.Drop(item));
        Remove();
    }

    public void UseItem()
    {
        if (association?.SelectItem(item) ?? true)
        {
            signalBus.Fire(ItemActionSignal.Use(item));
            Remove();
        }
    }

    public void Associate(Item item, SignalBus signalBus, IAssociateInventory association)
    {
        this.item = item;
        this.signalBus = signalBus;
        this.association = association;
    }

    public void SetRemoveButtonActive(bool active)
    {
        RemoveButton.gameObject.SetActive(active);
    }

    public void EnableInteractonIfType(ItemType type)
    {
        itemButtom.interactable = type == item.type;
    }

    public void EnableInteraction(bool active)
    {
        itemButtom.interactable = active;
    }
}
