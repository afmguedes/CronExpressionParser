namespace CronExpressionParser.Core
{
	public class LogMessageFactory
	{
		public const string InvalidNumberOfFieldsLogMessage = "Please provide all 6 fields in the input expression";
		public const string InvalidValueForExpressionFieldMessageTemplate = "'{0}' is not a valid value for {1}";
		public const string InvalidIncrementsValueLogMessage = "Please provide a value greater than zero for the increment";
	}
}