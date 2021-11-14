namespace CronExpressionParser.Core.Fields
{
	public class MinutesField : Field, IField
	{
		private const int MIN_VALUE = 0;
		private const int MAX_VALUE = 59;

		public MinutesField() : base(MIN_VALUE, MAX_VALUE)
		{
		}
	}
}