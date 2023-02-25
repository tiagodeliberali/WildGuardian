using System.Linq;

using Assets.Items;
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
    public TextMeshProUGUI value;


    public TextMeshProUGUI incubatorOrFood;
    public TextMeshProUGUI timeToNext;
    public TextMeshProUGUI next;

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
        value.text = $"$ {definition.value}";

        bool isAnimal = new ItemType[] { ItemType.Egg, ItemType.Puppy }.Contains(instance.Definition.type);

        if (isAnimal)
        {
            var animal = instance.Definition as Animal;

            incubatorOrFood.text = instance.Definition.type.Equals(ItemType.Egg)
                ? animal.GetIncubator()
                : animal.GetFood();

            timeToNext.text = $"{animal.timeToNext} dias";
            next.text = animal.next.itemName;
        }

        incubatorOrFood.transform.parent.gameObject.SetActive(isAnimal);
        timeToNext.transform.parent.gameObject.SetActive(isAnimal);
        next.transform.parent.gameObject.SetActive(isAnimal);

        count.text = $"{instance.Count}";

        details.SetActive(true);
    }
}
