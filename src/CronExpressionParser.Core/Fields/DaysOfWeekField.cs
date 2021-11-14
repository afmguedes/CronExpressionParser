using System;
using System.Collections.Generic;
using System.Linq;

namespace CronExpressionParser.Core.Fields
{
	public class DaysOfWeekField : IField
	{
		private const int DAYSOFWEEK_MINVALUE = 1;
		private const int DAYSOFWEEK_MAXVALUE = 7;

		public List<int> TryParse(string daysOfWeekExpression)
		{
			if (daysOfWeekExpression.Equals("*"))
			{
				daysOfWeekExpression = $"{DAYSOFWEEK_MINVALUE}-{DAYSOFWEEK_MAXVALUE}";
			}

			var valuesSplitByComma = daysOfWeekExpression.Split(',');

			if (valuesSplitByComma.Length > 1)
			{
				return TryParseIntegers(valuesSplitByComma).ToList();
			}

			var valuesSplitByDash = daysOfWeekExpression.Split('-');

			if (valuesSplitByDash.Length > 1)
			{
				var parsedIntegersSplitByDash = TryParseIntegers(valuesSplitByDash).ToList();
				return new List<int>(Enumerable.Range(parsedIntegersSplitByDash[0], parsedIntegersSplitByDash[1]));
			}

			var valuesSplitBySlash = daysOfWeekExpression.Split('/');

			if (valuesSplitBySlash.Length == 1)
			{
				var parsedInteger = TryParseIntegers(new[] { daysOfWeekExpression });
				return parsedInteger.ToList();
			}

			valuesSplitBySlash[0] = valuesSplitBySlash[0].Replace('*', DAYSOFWEEK_MINVALUE.ToString().First());
			var parsedIntegersSplitBySlash = TryParseIntegers(valuesSplitBySlash).ToList();

			return new List<int>(GetIncrementsOfStartingAt(parsedIntegersSplitBySlash[1], parsedIntegersSplitBySlash[0]));
		}

		private static IEnumerable<int> TryParseIntegers(IEnumerable<string> values)
		{
			var parsedIntegers = new List<int>();

			foreach (var value in values)
			{
				if (!int.TryParse(value, out var parsedValue) || parsedValue > DAYSOFWEEK_MAXVALUE)
				{
					throw new ArgumentException($"'{value}' is not a valid value for {nameof(DaysOfWeekField)}");
				}

				parsedIntegers.Add(parsedValue);
			}

			return parsedIntegers;
		}

		private static IEnumerable<int> GetIncrementsOfStartingAt(int increment, int start)
		{
			var incrementedValues = new List<int>();

			for (var i = start; i <= DAYSOFWEEK_MAXVALUE; i += increment)
			{
				incrementedValues.Add(i);
			}

			return incrementedValues;
		}
	}
}