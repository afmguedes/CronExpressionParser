namespace CronExpressionParser.Core.Fields
{
	public class MinutesField : Field, IField
	{
		private const int MinValue = 0;
		private const int MaxValue = 59;

		public MinutesField() : base(nameof(MinutesField), MinValue, MaxValue)
		{
		}
	}
}