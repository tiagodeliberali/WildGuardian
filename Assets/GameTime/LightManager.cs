using System;
using System.Collections.Generic;
using System.Linq;

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
				nextGlobalIntensity = 0.7f;
				nextLightsIntensity = 0.2f;
				break;
			case TimeOfDay.Noon:
				nextGlobalIntensity = 1;
				nextLightsIntensity = 0;
				break;
			case TimeOfDay.Afternoon:
				nextGlobalIntensity = 0.6f;
				nextLightsIntensity = 0.3f;
				break;
			case TimeOfDay.Night:
				nextGlobalIntensity = 0.2f;
				nextLightsIntensity = 0.8f;
				break;
		}

		globalLightIntensity = new LerpTime(GlobalLight.intensity, nextGlobalIntensity);
		lightsIntensity = new LerpTime(lights.First().intensity, nextLightsIntensity);
	}
}
