using System.Collections.Generic;
using System.Linq;

namespace CronExpressionParser.Core.Fields
{
	public class MinutesField : IField
	{
		private const int MINUTES_MINVALUE = 0;
		private const int MINUTES_MAXVALUE = 59;

		public List<int> TryParse(string minutesExpression)
		{
			var valuesSplitByComma = minutesExpression.Split(',');

			if (valuesSplitByComma.Length > 1)
			{
				return valuesSplitByComma.Select(int.Parse).ToList();
			}

			var valuesSplitByDash = minutesExpression.Split('-');

			if (valuesSplitByDash.Length > 1)
			{
				return new List<int>(Enumerable.Range(int.Parse(valuesSplitByDash[0]),
					int.Parse(valuesSplitByDash[1])));
			}

			var valuesSplitBySlash = minutesExpression.Split('/');

			if (valuesSplitBySlash.Length > 1)
			{
				return new List<int>(GetIncrementsOfStartingAt(int.Parse(valuesSplitBySlash[1]), 0));
			}

			return minutesExpression.Equals("*") ? new List<int>(Enumerable.Range(MINUTES_MINVALUE, MINUTES_MAXVALUE)) : new List<int> { int.Parse(valuesSplitByComma.First()) };
		}

		private static IEnumerable<int> GetIncrementsOfStartingAt(int increment, int start)
		{
			var incrementedValues = new List<int>();

			for (var i = start; i <= MINUTES_MAXVALUE; i += increment)
			{
				incrementedValues.Add(i);
			}

			return incrementedValues;
		}
	}
}