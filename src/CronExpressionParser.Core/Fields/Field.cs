using System;
using System.Collections.Generic;
using System.Linq;

namespace CronExpressionParser.Core.Fields
{
	public abstract class Field
	{
		private readonly int MIN_VALUE; 
		private readonly int MAX_VALUE;

		protected Field(int minValue, int maxValue)
		{
			MIN_VALUE = minValue;
			MAX_VALUE = maxValue;
		}

		public List<int> TryParse(string minutesExpression)
		{
			if (minutesExpression.Equals("*"))
			{
				minutesExpression = $"{MIN_VALUE}-{MAX_VALUE}";
			}

			var valuesSplitByComma = minutesExpression.Split(',');

			if (valuesSplitByComma.Length > 1)
			{
				return TryParseIntegers(valuesSplitByComma).ToList();
			}

			var valuesSplitByDash = minutesExpression.Split('-');

			if (valuesSplitByDash.Length > 1)
			{
				var parsedIntegersSplitByDash = TryParseIntegers(valuesSplitByDash).ToList();
				return new List<int>(Enumerable.Range(parsedIntegersSplitByDash[0], parsedIntegersSplitByDash[1]));
			}

			var valuesSplitBySlash = minutesExpression.Split('/');

			if (valuesSplitBySlash.Length == 1)
			{
				var parsedInteger = TryParseIntegers(new[] { minutesExpression });
				return parsedInteger.ToList();
			}

			valuesSplitBySlash[0] = valuesSplitBySlash[0].Replace('*', MIN_VALUE.ToString().First());
			var parsedIntegersSplitBySlash = TryParseIntegers(valuesSplitBySlash).ToList();

			return new List<int>(GetIncrementsOfStartingAt(parsedIntegersSplitBySlash[1], parsedIntegersSplitBySlash[0]));
		}

		private IEnumerable<int> TryParseIntegers(IEnumerable<string> values)
		{
			var parsedIntegers = new List<int>();

			foreach (var value in values)
			{
				if (!int.TryParse(value, out var parsedValue) || parsedValue > MAX_VALUE)
				{
					var exceptionMessage =
						string.Format(LogMessageFactory.InvalidValueForExpressionFieldMessageTemplate, value,
							nameof(MinutesField));
					throw new ArgumentException(exceptionMessage);
				}

				parsedIntegers.Add(parsedValue);
			}

			return parsedIntegers;
		}

		private IEnumerable<int> GetIncrementsOfStartingAt(int increment, int start)
		{
			var incrementedValues = new List<int>();

			for (var i = start; i <= MAX_VALUE; i += increment)
			{
				incrementedValues.Add(i);
			}

			return incrementedValues;
		}
	}
}