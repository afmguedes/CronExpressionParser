using System.Collections.Generic;
using System.Text;
using CronExpressionParser.Core.Config;

namespace CronExpressionParser.Core
{
	public class ModelTranslator
	{
		public static string TranslateToText(ViewModel viewModel)
		{
			var outputText = new StringBuilder();

			outputText.AppendLine(FormatLine("minute", viewModel.Minutes));
			outputText.AppendLine(FormatLine("hour", viewModel.Hours));
			outputText.AppendLine(FormatLine("day of month", viewModel.DaysOfMonth));
			outputText.AppendLine(FormatLine("month", viewModel.Months));
			outputText.AppendLine(FormatLine("day of week", viewModel.DaysOfWeek));
			outputText.AppendLine(FormatLine("command", viewModel.Command));

			return outputText.ToString();
		}

		private static string FormatLine(string title, IEnumerable<int> values)
		{
			return $"{title.PadRight(14, Constants.SpaceChar)}{string.Join(Constants.SpaceChar, values)}";
		}

		private static string FormatLine(string title, string command)
		{
			return $"{title.PadRight(14, Constants.SpaceChar)}{command}";
		}
	}
}