using System;

using Assets;
using Assets.GameTime;
using Assets.Signals;

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
            this.TimeData = this.TimeData.TickHour();

            if (this.TimeData.IsNewDay())
            {
                OnDayChanged?.Invoke(this.TimeData);
            }

            OnHourChanged?.Invoke(this.TimeData);

            if (this.TimeData.IsTimeToChangeDayLight())
            {
                OnLightChanged?.Invoke(this.TimeData);
            }

            currentTime = DateTime.Now.TimeOfDay;
        }
    }
}
