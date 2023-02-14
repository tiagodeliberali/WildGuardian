using Assets.Items;
using Assets.Signals;

using UnityEngine;
using UnityEngine.UI;

using Zenject;

public class InvetoryItemUI : MonoBehaviour
{
	private ItemInstance item;
	private Button itemButtom;
	private SignalBus signalBus;

	public Button RemoveButton;

	[Inject]
	public void Contruct(SignalBus signalBus)
	{
		this.signalBus = signalBus;
	}

	private void Awake()
	{
		itemButtom = gameObject.GetComponent<Button>();
	}

	public void RemoveItem()
	{
		signalBus.Fire(ItemActionSignal.Drop(item));
		Destroy(gameObject);
	}

	public void UseItem()
	{
		signalBus.Fire(ItemActionSignal.Use(item));
		Destroy(gameObject);
	}

	public void AssociateItem(ItemInstance item)
	{
		this.item = item;
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
