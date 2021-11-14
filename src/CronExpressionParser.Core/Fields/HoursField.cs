namespace CronExpressionParser.Core.Fields
{
	public class HoursField : Field
	{
		private const int MinValue = 0;
		private const int MaxValue = 23;

		public HoursField() : base(nameof(HoursField), MinValue, MaxValue)
		{
		}
	}
}