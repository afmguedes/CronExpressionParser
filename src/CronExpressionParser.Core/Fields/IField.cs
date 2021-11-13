using System.Collections.Generic;

namespace CronExpressionParser.Core.Fields
{
	public interface IField
	{
		List<int> TryParse(string fieldExpression);
	}
}