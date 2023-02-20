using System;

using Assets.GameTime;

using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public delegate void TimeEventDelegate(TimeData time);

    public event TimeEventDelegate OnHourChanged;
	public event TimeEventDelegate OnDayChanged;
    public event TimeEventDelegate OnLightChanged;

    public TimeData time { get; private set; }

	private TimeSpan currentTime;

	void Start()
    {
        // load from previous state...
        time = new TimeData();
		currentTime = DateTime.Now.TimeOfDay;
	}

	private void FixedUpdate()
	{
		if ((DateTime.Now.TimeOfDay - currentTime).TotalSeconds > 5)
        {
            time = time.TickHour();

            if (time.IsNewDay())
            {
                OnDayChanged?.Invoke(time);
            }

            OnHourChanged?.Invoke(time);

            if (time.IsTimeToChangeDayLight())
            {
                OnLightChanged?.Invoke(time);
            }

            currentTime = DateTime.Now.TimeOfDay;
		}
	}
}
