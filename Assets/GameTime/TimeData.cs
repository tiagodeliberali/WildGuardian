namespace Assets.GameTime
{
	public class TimeData
	{
		private const int StartOfMorning = 6;
		private const int StartOfNoon = 8;
		private const int StartOfAfternoon = 18;
		private const int StartOfNight = 20;

		public readonly int Day;
		public readonly int Hour;

		public TimeOfDay TimeOfDay
		{
			get
			{
				if (Hour < StartOfMorning) 
					return TimeOfDay.Night;

				if (Hour < StartOfNoon)
					return TimeOfDay.Morning;

				if (Hour < StartOfAfternoon)
					return TimeOfDay.Noon;

				if (Hour < StartOfNight)
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
			return Hour == StartOfMorning
				|| Hour == StartOfNoon
				|| Hour == StartOfAfternoon
				|| Hour == StartOfNight;
		}

		public bool IsNewDay()
		{
			return Hour == 0;
		}
	}
}
