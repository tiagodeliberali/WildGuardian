using Assets.GameTime;
using Assets.Items;

using UnityEngine;

public class IncubatorItem : MonoBehaviour
{
    private Animal animal;
    private TimeManager timeManager;
    private SpriteRenderer icon;

    public int TotalDays;
    public int ElapsedDays;

    private void Start() => this.icon = this.GetComponent<SpriteRenderer>();

    internal void Associate(Item definition, TimeManager timeManager)
    {
        this.animal = definition as Animal;
        this.timeManager = timeManager;

        TotalDays = animal.timeToNext;

        timeManager.OnDayChanged += this.TimeManager_OnDayChanged;
        timeManager.OnHourChanged += this.TimeManager_OnDayChanged;
    }

    private void TimeManager_OnDayChanged(TimeData time)
    {
        ElapsedDays++;

        if (ElapsedDays == TotalDays)
        {
            ElapsedDays = 0;

            var next = animal.next;

            icon.sprite = next.icon;
            this.transform.localScale = new Vector3(0.4f, 0.4f, 1);

            if (next is Animal nextAnimal)
            {
                animal = nextAnimal;
                TotalDays = nextAnimal.timeToNext;
                this.transform.localPosition += new Vector3(0, -1.5f, 0);
            }
            else
            {
                timeManager.OnDayChanged -= this.TimeManager_OnDayChanged;
                timeManager.OnHourChanged -= this.TimeManager_OnDayChanged;
            }
        }
    }
}
