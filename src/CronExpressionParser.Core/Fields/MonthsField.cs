using System;
using System.Collections.Generic;
using System.Linq;

namespace CronExpressionParser.Core.Fields
{
	public class MonthsField : IField
	{
		private const int MONTHS_MINVALUE = 1;
		private const int MONTHS_MAXVALUE = 12;

		public List<int> TryParse(string monthsExpression)
		{
			if (monthsExpression.Equals("*"))
			{
				monthsExpression = $"{MONTHS_MINVALUE}-{MONTHS_MAXVALUE}";
			}

			var valuesSplitByComma = monthsExpression.Split(',');

			if (valuesSplitByComma.Length > 1)
			{
				return TryParseIntegers(valuesSplitByComma).ToList();
			}

			var valuesSplitByDash = monthsExpression.Split('-');

			if (valuesSplitByDash.Length > 1)
			{
				var parsedIntegersSplitByDash = TryParseIntegers(valuesSplitByDash).ToList();
				return new List<int>(Enumerable.Range(parsedIntegersSplitByDash[0], parsedIntegersSplitByDash[1]));
			}

			var valuesSplitBySlash = monthsExpression.Split('/');

			if (valuesSplitBySlash.Length == 1)
			{
				var parsedInteger = TryParseIntegers(new[] { monthsExpression });
				return parsedInteger.ToList();
			}

			valuesSplitBySlash[0] = valuesSplitBySlash[0].Replace('*', MONTHS_MINVALUE.ToString().First());
			var parsedIntegersSplitBySlash = TryParseIntegers(valuesSplitBySlash).ToList();

			return new List<int>(GetIncrementsOfStartingAt(parsedIntegersSplitBySlash[1], parsedIntegersSplitBySlash[0]));
		}

		private static IEnumerable<int> TryParseIntegers(IEnumerable<string> values)
		{
			var parsedIntegers = new List<int>();

			foreach (var value in values)
			{
				if (!int.TryParse(value, out var parsedValue) || parsedValue > MONTHS_MAXVALUE)
				{
					throw new ArgumentException($"'{value}' is not a valid value for {nameof(MonthsField)}");
				}

				parsedIntegers.Add(parsedValue);
			}

			return parsedIntegers;
		}

		private static IEnumerable<int> GetIncrementsOfStartingAt(int increment, int start)
		{
			var incrementedValues = new List<int>();

			for (var i = start; i <= MONTHS_MAXVALUE; i += increment)
			{
				incrementedValues.Add(i);
			}

			return incrementedValues;
		}
	}
}