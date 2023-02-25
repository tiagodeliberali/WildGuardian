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

    private void Start()
    {
        icon = GetComponent<SpriteRenderer>();
    }

    internal void Associate(Item definition, TimeManager timeManager)
    {
        this.animal = definition as Animal;
        this.timeManager = timeManager;

        TotalDays = animal.timeToNext;
        
        timeManager.OnDayChanged += TimeManager_OnDayChanged;
        timeManager.OnHourChanged += TimeManager_OnDayChanged;
    }

    private void TimeManager_OnDayChanged(TimeData time)
    {
        ElapsedDays++;

        if (ElapsedDays == TotalDays)
        {
            ElapsedDays = 0;

            var next = animal.next;

            icon.sprite = next.icon;
            transform.localScale = new Vector3(0.4f, 0.4f, 1);
            
            if (next is Animal nextAnimal)
            {
                animal = nextAnimal;
                TotalDays = nextAnimal.timeToNext;
                transform.localPosition += new Vector3(0, -1.5f, 0);
            }
            else
            {
                timeManager.OnDayChanged -= TimeManager_OnDayChanged;
                timeManager.OnHourChanged -= TimeManager_OnDayChanged;
            }
        }
    }
}
