namespace CronExpressionParser.Core.Fields
{
	public class DaysOfWeekField : Field
	{
		private const int MinValue = 1;
		private const int MaxValue = 7;

		public DaysOfWeekField() : base(nameof(DaysOfWeekField), MinValue, MaxValue)
		{
		}
	}
}