using System;

using Assets;
using Assets.GameTime;
using Assets.Signals;

using Unity.VisualScripting.Antlr3.Runtime;

using UnityEngine;

using Zenject;

public class TimeManager : MonoBehaviour
{
    public delegate void TimeEventDelegate(TimeData time);

    public event TimeEventDelegate OnHourChanged;
	public event TimeEventDelegate OnDayChanged;
    public event TimeEventDelegate OnLightChanged;

    public TimeData TimeData { get; private set; } = new TimeData();

	private TimeSpan currentTime;
	private bool paused;

	[Inject]
	public void Contruct(SignalBus signalBus)
	{
		signalBus.Subscribe<UISignal>(this.OnUIStateChange);
	}

	private void OnUIStateChange(UISignal signal)
	{
		paused = signal.IsOpen;
	}

	void Start()
    {
        currentTime = DateTime.Now.TimeOfDay;
	}

	private void FixedUpdate()
	{
		if (paused)
		{
			currentTime = DateTime.Now.TimeOfDay;
			return;
		}

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
