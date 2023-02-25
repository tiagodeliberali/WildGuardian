using Assets.Items;

using TMPro;

using UnityEngine;

public class IncubatorItemUI : MonoBehaviour
{
    private IncubatorUI incubatorUI;
    public int TotalDays;
    public int ElapsedDays;

    public TextMeshProUGUI itemTime;

    internal void Associate(Item definition, TimeManager timeManager, IncubatorUI incubatorUI)
    {
        this.incubatorUI = incubatorUI;

        TotalDays = (definition as Animal).timeToNext;
        itemTime.text = $"({ElapsedDays}/{TotalDays})";

        timeManager.OnDayChanged += TimeManager_OnDayChanged;
        timeManager.OnHourChanged += TimeManager_OnDayChanged;
    }

    private void TimeManager_OnDayChanged(Assets.GameTime.TimeData time)
    {
        ElapsedDays++;

        itemTime.text = $"({ElapsedDays}/{TotalDays})";

        if (ElapsedDays == TotalDays)
        {
            incubatorUI.Remove(this);
            Destroy(gameObject);
        }
    }
}
