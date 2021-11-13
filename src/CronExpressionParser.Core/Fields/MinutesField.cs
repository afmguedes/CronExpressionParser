using System.Collections.Generic;

namespace CronExpressionParser.Core.Fields
{
	public class MinutesField : IField
	{

		public List<int> TryParse(string minutesExpression)
		{
			return new List<int> { int.Parse(minutesExpression) };
		}
	}
}