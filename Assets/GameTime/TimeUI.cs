using Assets.GameTime;

using TMPro;

using UnityEngine;

using Zenject;

public class TimeUI : MonoBehaviour
{
    private TextMeshProUGUI dayText;
    private TextMeshProUGUI hourText;

    [Inject]
    public void Contruct(TimeManager timeManager)
    {
        timeManager.OnDayChanged += this.TimeManager_OnDayChanged;
        timeManager.OnHourChanged += this.TimeManager_OnHourChanged;

        dayText = this.transform.Find("DayText").GetComponent<TextMeshProUGUI>();
        hourText = this.transform.Find("HourText").GetComponent<TextMeshProUGUI>();

        dayText.text = $"Dia {timeManager.TimeData.Day}";
        hourText.text = $"{timeManager.TimeData.Hour}:00";
    }

    private void TimeManager_OnHourChanged(TimeData time)
    {
        hourText.text = $"{time.Hour}:00";
    }

    private void TimeManager_OnDayChanged(TimeData time)
    {
        dayText.text = $"Dia {time.Day}";
    }
}
