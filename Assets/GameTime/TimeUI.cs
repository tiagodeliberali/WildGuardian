using Assets.GameTime;

using TMPro;

using UnityEngine;

using Zenject;

public class TimeUI : MonoBehaviour
{
    public TextMeshProUGUI Day;
	public TextMeshProUGUI Hour;

	private TimeManager timeManager;

	[Inject]
	public void Contruct(TimeManager timeManager)
	{
		this.timeManager = timeManager;
		timeManager.OnDayChanged += TimeManager_OnDayChanged;
		timeManager.OnHourChanged += TimeManager_OnHourChanged;
	}

	private void Start()
	{
		Day.text = $"Dia {timeManager.time.Day}";
		Hour.text = $"{timeManager.time.Hour}:00";
	}

	private void TimeManager_OnHourChanged(TimeData time)
	{
		Hour.text = $"{time.Hour}:00";
		
	}

	private void TimeManager_OnDayChanged(TimeData time)
	{
		Day.text = $"Dia {time.Day}";
	}
}
