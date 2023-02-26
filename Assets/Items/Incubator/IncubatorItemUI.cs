using Assets;
using Assets.Items;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class IncubatorItemUI : MonoBehaviour, IGenerateGameObject
{
    private Item definition;
    private IncubatorUI incubatorUI;
    public int TotalDays;
    public int ElapsedDays;

    public TextMeshProUGUI itemTime;

    internal void Associate(TimeManager timeManager, IncubatorUI incubatorUI)
    {
        this.incubatorUI = incubatorUI;

        TotalDays = (this.definition as Animal).timeToNext;
        itemTime.text = $"({ElapsedDays}/{TotalDays})";

        timeManager.OnDayChanged += this.TimeManager_OnDayChanged;
        timeManager.OnHourChanged += this.TimeManager_OnDayChanged;
    }

    private void TimeManager_OnDayChanged(Assets.GameTime.TimeData time)
    {
        ElapsedDays++;

        itemTime.text = $"({ElapsedDays}/{TotalDays})";

        if (ElapsedDays == TotalDays)
        {
            incubatorUI.Remove(this);
            Destroy(this.gameObject);
        }
    }

    public GameObject Build<T>(Transform placeholder, T instance)
    {
        if (instance is Item definition)
        {
            GameObject inventoryObject = Instantiate(this.gameObject, placeholder);

            var itemIcon = inventoryObject.transform.Find("ItemIcon").GetComponent<Image>();
            itemIcon.sprite = definition.icon;

            var itemName = inventoryObject.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            itemName.text = definition.itemName;

            inventoryObject.GetComponent<IncubatorItemUI>().definition = definition;

            return inventoryObject;
        }

        return null;
    }
}
