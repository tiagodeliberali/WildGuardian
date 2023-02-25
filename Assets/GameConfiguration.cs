namespace Assets
{
    public static class GameConfiguration
    {
        public static float DeltaTimeVelocity = 3f;

        public static float TimeToElapseOneHour = 3f;

        // Light intesity over the day
        public static int TimeToChangeLightInSecods = 2;

        public static float LightMorningGlobalIntensity = 0.7f;
        public static float LightNoonGlobalIntensity = 1f;
        public static float LightAfternoonGlobalIntensity = 0.6f;
        public static float LightNightGlobalIntensity = 0.2f;

        public static float LightMorningLightsIntensity = 0.2f;
        public static float LightNoonLightsIntensity = 0f;
        public static float LightAfternoonLightsIntensity = 0.3f;
        public static float LightNightLightsIntensity = 0.8f;

        // Time of day definitions
        public static int StartOfMorning = 6;
        public static int StartOfNoon = 8;
        public static int StartOfAfternoon = 18;
        public static int StartOfNight = 20;
    }
}
