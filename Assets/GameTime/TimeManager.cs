using System;

using Assets;
using Assets.GameTime;

using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public delegate void TimeEventDelegate(TimeData time);

    public event TimeEventDelegate OnHourChanged;
	public event TimeEventDelegate OnDayChanged;
    public event TimeEventDelegate OnLightChanged;

    public TimeData TimeData { get; private set; } = new TimeData();

	private TimeSpan currentTime;

	void Start()
    {
        currentTime = DateTime.Now.TimeOfDay;
	}

	private void FixedUpdate()
	{
		if ((DateTime.Now.TimeOfDay - currentTime).TotalSeconds > GameConfiguration.TimeToElapseOneHour)
        {
            TimeData = TimeData.TickHour();

            if (TimeData.IsNewDay())
            {
                OnDayChanged?.Invoke(TimeData);
            }

            OnHourChanged?.Invoke(TimeData);

            if (TimeData.IsTimeToChangeDayLight())
            {
                OnLightChanged?.Invoke(TimeData);
            }

            currentTime = DateTime.Now.TimeOfDay;
		}
	}
}
