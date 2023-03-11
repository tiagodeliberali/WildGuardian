using Assets.GameTime;

using UnityEngine;

using Zenject;

public class PulbicLightSprite : MonoBehaviour
{
    public Sprite TurnedOnLight;
    public Sprite TurnedOffLight;

    private SpriteRenderer spriteRenderer;
    
    [Inject]
    public void Contruct(TimeManager timeManager)
    {
        timeManager.OnLightChanged += this.TimeManager_OnLightChanged;
    }

    private void TimeManager_OnLightChanged(TimeData time)
    {
        switch (time.TimeOfDay)
        {
            case TimeOfDay.Noon:
                this.spriteRenderer.sprite = TurnedOffLight;
                break;
            case TimeOfDay.Morning:
            case TimeOfDay.Afternoon:
            case TimeOfDay.Night:
                this.spriteRenderer.sprite = TurnedOnLight;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    }
}
