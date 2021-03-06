namespace CronExpressionParser.Core.Config
{
	public class Constants
	{
		public const char SpaceChar = ' ';
		public const char CommaChar = ',';
		public const char StarChar = '*';
		public const char RangeChar = '-';
		public const char IncrementsChar = '/';

		public const int NumberOfFields = 6;
		public const int MinutesIndex = 0;
		public const int HoursIndex = 1;
		public const int DaysOfMonthIndex = 2;
		public const int MonthsIndex = 3;
		public const int DaysOfWeekIndex = 4;
		public const int CommandIndex = 5;
	}
}