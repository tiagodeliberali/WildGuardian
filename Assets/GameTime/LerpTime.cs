namespace Assets.GameTime
{
    using System;

    using UnityEngine;

    public class LerpTime
    {
        private float startValue;
        private float endValue;
        private long endTime;
        private long lerpDuration;

        private TimeSpan DefaultLerpDuration = TimeSpan.FromSeconds(GameConfiguration.TimeToChangeLightInSecods);

        public LerpTime(float startValue, float endValue)
        {
            this.startValue = startValue;
            this.endValue = endValue;

            this.endTime = (DateTime.Now.TimeOfDay + DefaultLerpDuration).Ticks;
            this.lerpDuration = DefaultLerpDuration.Ticks;
        }

        public float GetValue()
        {
            float ratio = Math.Clamp(1.0f * (lerpDuration - (endTime - DateTime.Now.TimeOfDay.Ticks)) / lerpDuration, 0, 1f);
            return Mathf.Lerp(startValue, endValue, ratio);
        }

        public bool IsCompleted()
        {
            return endTime < DateTime.Now.TimeOfDay.Ticks;
        }
    }
}
