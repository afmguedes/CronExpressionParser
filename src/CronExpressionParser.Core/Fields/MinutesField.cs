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
			var values = minutesExpression.Split(',');

			if (values.Length > 1)
			{
				return values.Select(int.Parse).ToList();
			}

			return minutesExpression.Equals("*") ? new List<int>(Enumerable.Range(MINUTES_MINVALUE, MINUTES_MAXVALUE)) : new List<int> { int.Parse(minutesExpression) };
		}
	}
}