using System.Collections.Generic;
using System.Text;

namespace CronExpressionParser.Core
{
	public class ModelTranslator
	{
		public static string TranslateToText(OutputModel outputModel)
		{
			var outputText = new StringBuilder();

			outputText.AppendLine(FormatLine("minute", outputModel.Minutes));
			outputText.AppendLine(FormatLine("hour", outputModel.Hours));
			outputText.AppendLine(FormatLine("day of month", outputModel.DaysOfMonth));
			outputText.AppendLine(FormatLine("month", outputModel.Months));
			outputText.AppendLine(FormatLine("day of week", outputModel.DaysOfWeek));
			outputText.AppendLine(FormatLine("command", outputModel.Command));

			return outputText.ToString();
		}

		private static string FormatLine(string title, IEnumerable<int> values)
		{
			return $"{title.PadRight(14, ' ')}{string.Join(' ', values)}";
		}

		private static string FormatLine(string title, string command)
		{
			return $"{title.PadRight(14, ' ')}{command}";
		}
	}
}