using Assets.Signals;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

using Zenject;

public class KnowledgeManager : MonoBehaviour
{
	public SignalBus signalBus;
	public GameObject Knowledge;
	public GameObject Details;

	public Image icon;

	public TextMeshProUGUI itemName;
	public TextMeshProUGUI description;
	public TextMeshProUGUI incubator;

	public TextMeshProUGUI eggValue;
	public TextMeshProUGUI puppyValue;
	public TextMeshProUGUI drop;

	public TextMeshProUGUI dropValue;
	public TextMeshProUGUI timeToHatch;
	public TextMeshProUGUI timeToGrow;

	public TextMeshProUGUI food;
	public TextMeshProUGUI count;

	[Inject]
	public void Contruct(InventoryManager inventoryManager, SignalBus signalBus)
	{
		this.signalBus = signalBus;
	}

	public void OpenWindow()
	{
		Knowledge.SetActive(true);
		Details.SetActive(false);
		
		signalBus.Fire(UISignal.Opened());
	}

	public void CloseWindow()
	{
		Knowledge.SetActive(false);
		signalBus.Fire(UISignal.Closed());
	}

	public void AssociateItem(Item item)
	{
		icon.sprite = item.icon;

		itemName.text = item.itemName;
		description.text = item.description;
		incubator.text = item.incubator;

		eggValue.text = $"$ {item.eggValue}";
		puppyValue.text = $"$ {item.puppyValue}";
		drop.text = item.drop;

		dropValue.text = $"$ {item.dropValue}";
		timeToHatch.text = $"{item.timeToHatch} dias";
		timeToGrow.text = $"{item.timeToGrow} dias";

		food.text = item.food;
		count.text = $"{item.count}";

		Details.SetActive(true);
	}
}
