namespace Assets.GameTime
{
	public class TimeData
	{
		public readonly int Day;
		public readonly int Hour;

		public TimeOfDay TimeOfDay
		{
			get
			{
				if (Hour < GameConfiguration.StartOfMorning) 
					return TimeOfDay.Night;

				if (Hour < GameConfiguration.StartOfNoon)
					return TimeOfDay.Morning;

				if (Hour < GameConfiguration.StartOfAfternoon)
					return TimeOfDay.Noon;

				if (Hour < GameConfiguration.StartOfNight)
					return TimeOfDay.Afternoon;

				return TimeOfDay.Night;
			}
		}

		public TimeData()
		{
			Day = 1;
			Hour = 4;
		}

		public TimeData(int day, int hour)
		{
			Day = day;
			Hour = hour;
		}

		public TimeData TickHour()
		{
			int newHour =  Hour + 1;
			int newDay = Day;

			if (newHour == 24)
			{
				newHour = 0;
				newDay++;
			}

			return new TimeData(newDay, newHour);
		}

		public bool IsTimeToChangeDayLight()
		{
			return Hour == GameConfiguration.StartOfMorning
				|| Hour == GameConfiguration.StartOfNoon
				|| Hour == GameConfiguration.StartOfAfternoon
				|| Hour == GameConfiguration.StartOfNight;
		}

		public bool IsNewDay()
		{
			return Hour == 0;
		}
	}
}
