using Assets.Knowledge;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class KnowledgeDetailsUI : MonoBehaviour
{
	public GameObject details;

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

	public void SetActive(bool active)
	{
		details.SetActive(active);
	}

	public void SelectItem(KnowledgeItemInstance instance)
	{
		var definition = instance.Definition;

		icon.sprite = definition.icon;

		itemName.text = definition.itemName;
		description.text = definition.description;
		incubator.text = definition.incubator;

		eggValue.text = $"$ {definition.eggValue}";
		puppyValue.text = $"$ {definition.puppyValue}";
		drop.text = definition.drop;

		dropValue.text = $"$ {definition.dropValue}";
		timeToHatch.text = $"{definition.timeToHatch} dias";
		timeToGrow.text = $"{definition.timeToGrow} dias";

		food.text = definition.food;
		count.text = $"{instance.Count}";

		details.SetActive(true);
	}
}
