using Assets;
using Assets.GameTime;
using Assets.Items;

using UnityEngine;

using Zenject;

public class IncubatorItem : MonoBehaviour, IGenerateGameObject
{
    private Animal animal;
    private TimeManager timeManager;
    private SpriteRenderer icon;

    public int TotalDays;
    public int ElapsedDays;
    private ItemPickup itemPickup;

    private void Start() => this.icon = this.GetComponent<SpriteRenderer>();

    internal void Associate(SignalBus signalBus, TimeManager timeManager, bool enablePicking, Vector3? position = null)
    {
        this.timeManager = timeManager;

        TotalDays = animal.timeToNext;

        this.timeManager.OnDayChanged += this.TimeManager_OnDayChanged;
        this.timeManager.OnHourChanged += this.TimeManager_OnDayChanged;

        this.itemPickup = this.GetComponent<ItemPickup>();
        this.itemPickup.OnItemPickup += this.ItemPickup_OnItemPickup;
        this.itemPickup.Associate(animal, signalBus);
        this.itemPickup.Enabled = enablePicking;

        if (position.HasValue)
        {
            this.transform.position = position.Value;
        }
    }

    private void ItemPickup_OnItemPickup(Item Item)
    {
        this.timeManager.OnDayChanged -= this.TimeManager_OnDayChanged;
        this.timeManager.OnHourChanged -= this.TimeManager_OnDayChanged;
    }

    private void Update()
    {
        if (this.animal != null && this.animal.type.Equals(ItemType.Puppy)) 
        {
            this.transform.position += new Vector3(0.01f, 0f, 0);
        }
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

            if (next is Animal nextAnimal)
            {
                this.animal = nextAnimal;
                this.TotalDays = nextAnimal.timeToNext;
                this.transform.localPosition += new Vector3(0, -1.5f, 0);
            }
            else
            {
                this.animal = null;
                this.timeManager.OnDayChanged -= this.TimeManager_OnDayChanged;
                this.timeManager.OnHourChanged -= this.TimeManager_OnDayChanged;
            }
        }
    }

    public GameObject Build<T>(Transform placeholder, T instance)
    {
        if (instance is Item definition)
        {
            GameObject incubatorObject = Instantiate(this.gameObject, placeholder);
            incubatorObject.GetComponent<SpriteRenderer>().sprite = definition.icon;
            incubatorObject.GetComponent<IncubatorItem>().animal = definition as Animal;

            return incubatorObject;
        }

        return null;
    }
}
