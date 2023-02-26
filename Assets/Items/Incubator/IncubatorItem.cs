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
    private ItemPickup itemPickup;

    private void Start() => this.icon = this.GetComponent<SpriteRenderer>();

    internal void Associate(Item definition, TimeManager timeManager)
    {
        this.animal = definition as Animal;
        this.timeManager = timeManager;

        TotalDays = animal.timeToNext;

        this.timeManager.OnDayChanged += this.TimeManager_OnDayChanged;
        this.timeManager.OnHourChanged += this.TimeManager_OnDayChanged;

        this.itemPickup = this.GetComponent<ItemPickup>();
        this.itemPickup.OnItemPickup += this.ItemPickup_OnItemPickup;
    }

    private void ItemPickup_OnItemPickup(Item Item)
    {
        this.timeManager.OnDayChanged -= this.TimeManager_OnDayChanged;
        this.timeManager.OnHourChanged -= this.TimeManager_OnDayChanged;
    }

    private void TimeManager_OnDayChanged(TimeData time)
    {
        ElapsedDays++;

        if (ElapsedDays == TotalDays)
        {
            ElapsedDays = 0;

            var next = this.animal.next;
            this.itemPickup.SetItem(next);
            this.itemPickup.Enabled = true;

            icon.sprite = next.icon;
            this.transform.localScale = new Vector3(2.5f, 2.5f, 1);

            if (next is Animal nextAnimal)
            {
                this.animal = nextAnimal;
                this.TotalDays = nextAnimal.timeToNext;
                this.transform.localPosition += new Vector3(0, -1.5f, 0);
            }
            else
            {
                this.timeManager.OnDayChanged -= this.TimeManager_OnDayChanged;
                this.timeManager.OnHourChanged -= this.TimeManager_OnDayChanged;
            }
        }
    }
}