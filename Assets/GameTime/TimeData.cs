namespace Assets.GameTime
{
	public class TimeData
	{
		private const int StartOfDay = 6;
		private const int EndOfDay = 18;

		public readonly int Day;
		public readonly int Hour;

		public bool IsDayLight => Hour >= StartOfDay && Hour <= EndOfDay;

		public TimeData()
		{
			Day = 1;
			Hour = 6;
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
			return Hour == StartOfDay
				|| Hour == EndOfDay;
		}

		public bool IsNewDay()
		{
			return Hour == 0;
		}
	}
}
