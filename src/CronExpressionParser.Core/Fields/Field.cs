using System;
using System.Collections.Generic;
using System.Linq;

namespace CronExpressionParser.Core.Fields
{
	public abstract class Field : IField
	{
		private readonly string fieldName;
		private readonly int minValue; 
		private readonly int maxValue;

		protected Field(string fieldName, int minValue, int maxValue)
		{
			this.fieldName = fieldName;
			this.minValue = minValue;
			this.maxValue = maxValue;
		}

		public List<int> TryParse(string minutesExpression)
		{
			if (minutesExpression.Equals("*"))
			{
				minutesExpression = $"{minValue}-{maxValue}";
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

			valuesSplitBySlash[0] = valuesSplitBySlash[0].Replace('*', minValue.ToString().First());
			var parsedIntegersSplitBySlash = TryParseIntegers(valuesSplitBySlash).ToList();

			return new List<int>(GetIncrementsOfStartingAt(parsedIntegersSplitBySlash[1], parsedIntegersSplitBySlash[0]));
		}

		private IEnumerable<int> TryParseIntegers(IEnumerable<string> values)
		{
			var parsedIntegers = new List<int>();

			foreach (var value in values)
			{
				if (!int.TryParse(value, out var parsedValue) || parsedValue > maxValue)
				{
					var exceptionMessage =
						string.Format(LogMessageFactory.InvalidValueForExpressionFieldMessageTemplate, value, fieldName);
					throw new ArgumentException(exceptionMessage);
				}

				parsedIntegers.Add(parsedValue);
			}

			return parsedIntegers;
		}

		private IEnumerable<int> GetIncrementsOfStartingAt(int increment, int start)
		{
			var incrementedValues = new List<int>();

			for (var i = start; i <= maxValue; i += increment)
			{
				incrementedValues.Add(i);
			}

			return incrementedValues;
		}
	}
}