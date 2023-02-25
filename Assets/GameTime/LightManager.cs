using System.Collections.Generic;
using System.Linq;

using Assets;
using Assets.GameTime;

using UnityEngine;
using UnityEngine.Rendering.Universal;

using Zenject;

public class LightManager : MonoBehaviour
{
    public Light2D GlobalLight;

    private List<Light2D> lights;
    private TimeManager timeManager;

    private LerpTime globalLightIntensity;
    private LerpTime lightsIntensity;

    [Inject]
    public void Contruct(TimeManager timeManager)
    {
        this.timeManager = timeManager;
        timeManager.OnLightChanged += TimeManager_OnLightChanged;
    }

    private void Awake()
    {
        lights = GameObject.FindGameObjectsWithTag("Light").Select(x => x.GetComponent<Light2D>()).ToList();
    }

    private void Start()
    {
        SetLight(timeManager.TimeData);
    }

    private void Update()
    {
        if (globalLightIntensity != null && lightsIntensity != null)
        {

            if (!globalLightIntensity.IsCompleted())
            {
                GlobalLight.intensity = globalLightIntensity.GetValue();
                lights.ForEach(x => x.intensity = lightsIntensity.GetValue());
            }
            else
            {
                globalLightIntensity = null;
                lightsIntensity = null;
            }
        }
    }

    private void TimeManager_OnLightChanged(TimeData time)
    {
        SetLight(time);
    }

    private void SetLight(TimeData time)
    {
        float nextGlobalIntensity = 0;
        float nextLightsIntensity = 0;

        switch (time.TimeOfDay)
        {
            case TimeOfDay.Morning:
                nextGlobalIntensity = GameConfiguration.LightMorningGlobalIntensity;
                nextLightsIntensity = GameConfiguration.LightMorningLightsIntensity;
                break;
            case TimeOfDay.Noon:
                nextGlobalIntensity = GameConfiguration.LightNoonGlobalIntensity;
                nextLightsIntensity = GameConfiguration.LightNoonLightsIntensity;
                break;
            case TimeOfDay.Afternoon:
                nextGlobalIntensity = GameConfiguration.LightAfternoonGlobalIntensity;
                nextLightsIntensity = GameConfiguration.LightAfternoonLightsIntensity;
                break;
            case TimeOfDay.Night:
                nextGlobalIntensity = GameConfiguration.LightNightGlobalIntensity;
                nextLightsIntensity = GameConfiguration.LightNightLightsIntensity;
                break;
        }

        globalLightIntensity = new LerpTime(GlobalLight.intensity, nextGlobalIntensity);
        lightsIntensity = new LerpTime(lights.First().intensity, nextLightsIntensity);
    }
}
