namespace CronExpressionParser.Core.Fields
{
	public class MonthsField : Field
	{
		private const int MinValue = 1;
		private const int MaxValue = 12;

		public MonthsField() : base(nameof(MonthsField), MinValue, MaxValue)
		{
		}
	}
}