using System;
using System.Collections.Generic;
using System.Linq;

namespace CronExpressionParser.Core.Fields
{
	public class DaysOfMonthField : IField
	{
		private const int DAYSOFMONTH_MINVALUE = 1;
		private const int DAYSOFMONTH_MAXVALUE = 31;

		public List<int> TryParse(string daysOfMonthExpression)
		{
			if (daysOfMonthExpression.Equals("*"))
			{
				daysOfMonthExpression = $"{DAYSOFMONTH_MINVALUE}-{DAYSOFMONTH_MAXVALUE}";
			}

			var valuesSplitByComma = daysOfMonthExpression.Split(',');

			if (valuesSplitByComma.Length > 1)
			{
				return TryParseIntegers(valuesSplitByComma).ToList();
			}

			var valuesSplitByDash = daysOfMonthExpression.Split('-');

			if (valuesSplitByDash.Length > 1)
			{
				var parsedIntegersSplitByDash = TryParseIntegers(valuesSplitByDash).ToList();
				return new List<int>(Enumerable.Range(parsedIntegersSplitByDash[0], parsedIntegersSplitByDash[1]));
			}

			var valuesSplitBySlash = daysOfMonthExpression.Split('/');

			if (valuesSplitBySlash.Length == 1)
			{
				var parsedInteger = TryParseIntegers(new[] { daysOfMonthExpression });
				return parsedInteger.ToList();
			}

			valuesSplitBySlash[0] = valuesSplitBySlash[0].Replace('*', DAYSOFMONTH_MINVALUE.ToString().First());
			var parsedIntegersSplitBySlash = TryParseIntegers(valuesSplitBySlash).ToList();

			return new List<int>(GetIncrementsOfStartingAt(parsedIntegersSplitBySlash[1], parsedIntegersSplitBySlash[0]));
		}

		private static IEnumerable<int> TryParseIntegers(IEnumerable<string> values)
		{
			var parsedIntegers = new List<int>();

			foreach (var value in values)
			{
				if (!int.TryParse(value, out var parsedValue) || parsedValue > DAYSOFMONTH_MAXVALUE)
				{
					var exceptionMessage =
						string.Format(LogMessageFactory.InvalidValueForExpressionFieldMessageTemplate, value,
							nameof(DaysOfMonthField));
					throw new ArgumentException(exceptionMessage);
				}

				parsedIntegers.Add(parsedValue);
			}

			return parsedIntegers;
		}

		private static IEnumerable<int> GetIncrementsOfStartingAt(int increment, int start)
		{
			var incrementedValues = new List<int>();

			for (var i = start; i <= DAYSOFMONTH_MAXVALUE; i += increment)
			{
				incrementedValues.Add(i);
			}

			return incrementedValues;
		}
	}
}