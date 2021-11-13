using System;

namespace CronExpressionParser.Core
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Console.WriteLine(TranslateCronIntoText(args));
			}
			catch (Exception e)
			{
				Console.WriteLine($"Exception: {e.Message}");
			}
		}

		private static string TranslateCronIntoText(string[] args)
		{
			var outputModel = CronExpression.Create(args[0]).Expand();

			return ModelTranslator.TranslateToText(outputModel);
		}
	}
}
