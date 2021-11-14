using System;
using System.Collections.Generic;

namespace CronExpressionParser.Core.Utilities
{
	public static class MyExtensions
	{
		public static IEnumerable<int> GetIncrementsOf(this int start, int increment, int maxValue)
		{
			if (increment <= 0)
			{
				throw new ArgumentOutOfRangeException(null, LogMessageFactory.InvalidIncrementsValueLogMessage);
			}

			var incrementedValues = new List<int>();

			for (var i = start; i <= maxValue; i += increment)
			{
				incrementedValues.Add(i);
			}

			return incrementedValues;
		}
	}
}