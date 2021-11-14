using System.Collections.Generic;
using CronExpressionParser.Core.Config;
using CronExpressionParser.Core.Exceptions;
using CronExpressionParser.Core.Fields;

namespace CronExpressionParser.Core
{
	public class CronExpression
	{
		private readonly IList<int> minutes;
		private readonly IList<int> hours;
		private readonly IList<int> daysOfMonth;
		private readonly IList<int> months;
		private readonly IList<int> daysOfWeek;
		private readonly string command;

		private CronExpression(IList<int> minutes, IList<int> hours, IList<int> daysOfMonth, IList<int> months, IList<int> daysOfWeek, string command)
		{
			this.minutes = minutes;
			this.hours = hours;
			this.daysOfMonth = daysOfMonth;
			this.months = months;
			this.daysOfWeek = daysOfWeek;
			this.command = command;
		}

		public static CronExpression Create(string inputExpression)
		{
			var fields = inputExpression.Split(Constants.SpaceChar);

			if (fields.Length != Constants.NumberOfFields)
			{
				throw new InvalidNumberOfFieldsException(LogMessageFactory.InvalidNumberOfFieldsLogMessage);
			}

			var numericFields = ParseNumericFields(fields);

			return new CronExpression(numericFields[Constants.MinutesIndex], numericFields[Constants.HoursIndex],
				numericFields[Constants.DaysOfMonthIndex], numericFields[Constants.MonthsIndex], numericFields[Constants.DaysOfWeekIndex],
				fields[Constants.CommandIndex]);
		}

		public OutputModel Expand()
		{
			return new OutputModel(minutes, hours, daysOfMonth, months, daysOfWeek, command);
		}

		private static IList<List<int>> ParseNumericFields(IReadOnlyList<string> fields)
		{
			var minutes = new MinutesField().TryParse(fields[Constants.MinutesIndex]);
			var hours = new HoursField().TryParse(fields[Constants.HoursIndex]);
			var daysOfMonth = new DaysOfMonthField().TryParse(fields[Constants.DaysOfMonthIndex]);
			var months = new MonthsField().TryParse(fields[Constants.MonthsIndex]);
			var daysOfWeek = new DaysOfWeekField().TryParse(fields[Constants.DaysOfWeekIndex]);

			return new List<List<int>> { minutes, hours, daysOfMonth, months, daysOfWeek };
		}
	}
}