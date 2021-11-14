using System;
using System.Collections.Generic;
using System.Linq;
using CronExpressionParser.Core.Config;
using CronExpressionParser.Core.Utilities;

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
			if (minutesExpression.Equals(Constants.StarChar.ToString()))
			{
				minutesExpression = $"{minValue}-{maxValue}";
			}

			var valuesSplitByComma = minutesExpression.Split(Constants.CommaChar);

			if (valuesSplitByComma.Length > 1)
			{
				return TryParseIntegers(valuesSplitByComma).ToList();
			}

			var valuesSplitByDash = minutesExpression.Split(Constants.RangeChar);

			if (valuesSplitByDash.Length > 1)
			{
				var parsedIntegersSplitByDash = TryParseIntegers(valuesSplitByDash).ToList();
				return parsedIntegersSplitByDash[0].GetIncrementsOf(1, parsedIntegersSplitByDash[1]).ToList();
			}

			var valuesSplitBySlash = minutesExpression.Split(Constants.IncrementsChar);

			if (valuesSplitBySlash.Length == 1)
			{
				var parsedInteger = TryParseIntegers(new[] { minutesExpression });
				return parsedInteger.ToList();
			}

			valuesSplitBySlash[0] = valuesSplitBySlash[0].Replace(Constants.StarChar, minValue.ToString().First());
			var parsedIntegersSplitBySlash = TryParseIntegers(valuesSplitBySlash).ToList();

			return parsedIntegersSplitBySlash[0].GetIncrementsOf(parsedIntegersSplitBySlash[1], maxValue).ToList();
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
	}
}