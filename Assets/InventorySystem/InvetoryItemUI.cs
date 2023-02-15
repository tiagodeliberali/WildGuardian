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
	private ItemInstance item;
	private Button itemButtom;
	private SignalBus signalBus;

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
		signalBus.Fire(ItemActionSignal.Use(item));
		Remove();
	}

	public void Associate(ItemInstance item, SignalBus signalBus)
	{
		this.item = item;
		this.signalBus = signalBus;
	}

	public void SetRemoveButtonActive(bool active)
	{
		RemoveButton.gameObject.SetActive(active);
	}

	public void EnableInteractonIfType(ItemType type)
	{
		itemButtom.interactable = type == item.Definition.type;
	}

	public void EnableInteraction(bool active)
	{
		itemButtom.interactable = active;
	}
}
