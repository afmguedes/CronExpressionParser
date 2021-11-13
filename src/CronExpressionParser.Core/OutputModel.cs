using System.Collections.Generic;

namespace CronExpressionParser.Core
{
	public class OutputModel
	{
		public IEnumerable<int> Minutes { get; set; }
		public IEnumerable<int> Hours { get; set; }
		public IEnumerable<int> DaysOfMonth { get; set; }
		public IEnumerable<int> Months { get; set; }
		public IEnumerable<int> DaysOfWeek { get; set; }
		public string Command { get; set; }

		public OutputModel(IEnumerable<int> minutes, IEnumerable<int> hours, IEnumerable<int> daysOfMonth, IEnumerable<int> months, IEnumerable<int> daysOfWeek, string command)
		{
			Minutes = minutes;
			Hours = hours;
			DaysOfMonth = daysOfMonth;
			Months = months;
			DaysOfWeek = daysOfWeek;
			Command = command;
		}
	}
}