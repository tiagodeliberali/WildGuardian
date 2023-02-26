using Assets;
using Assets.InventorySystem;
using Assets.Items;
using Assets.Signals;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

using Zenject;

/// <summary>
/// Changes to items are communicated through signal
/// </summary>
public class InvetoryItemUI : MonoBehaviour, IGenerateGameObject
{
    private Item item;
    private Button itemButtom;
    private SignalBus signalBus;
    private IAssociateInventory association;
    public Button RemoveButton;

    private void Awake()
    {
        itemButtom = this.gameObject.GetComponent<Button>();
    }

    public void Remove()
    {
        Destroy(this.gameObject);
    }

    public void DropItem()
    {
        signalBus.Fire(ItemActionSignal.Drop(item));
        this.Remove();
    }

    public void UseItem()
    {
        if (association?.SelectItem(item) ?? true)
        {
            signalBus.Fire(ItemActionSignal.Use(item));
            this.Remove();
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

    public GameObject Build<T>(Transform placeholder, T instance)
    {
        if (instance is Item item)
        {
            var obj = Instantiate(this.gameObject, placeholder);

            obj.transform.Find("ItemIcon")
                    .GetComponent<Image>()
                    .sprite = item.icon;

            obj.transform.Find("ItemName")
                .GetComponent<TextMeshProUGUI>()
                .text = item.itemName;

            return obj;
        }

        return null;
    }
}
