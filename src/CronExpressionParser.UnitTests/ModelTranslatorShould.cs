using CronExpressionParser.Core;
using FluentAssertions;
using NUnit.Framework;

namespace CronExpressionParser.UnitTests
{
	[TestFixture]
	public class ModelTranslatorShould
	{
		[Test]
		public void ReturnExpectedResult_WhenValidOutputModelPassed()
		{
			const string expectedOutputText = 
				"minute        0 15 30 45\r\n" +
			    "hour          0\r\n" +
				"day of month  1 15\r\n" +
				"month         1 2 3 4 5 6 7 8 9 10 11 12\r\n" +
				"day of week   1 2 3 4 5\r\n" +
				"command       /usr/bin/find\r\n";
			
			var minutes = new[] { 0, 15, 30, 45 };
			var hours = new[] { 0 };
			var daysOfMonth = new[] { 1, 15 };
			var months = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
			var daysOfWeek = new[] { 1, 2, 3, 4, 5 };
			const string command = "/usr/bin/find";
			var outputModel = new OutputModel(minutes, hours, daysOfMonth, months, daysOfWeek, command);
			
			var actualOutputText = ModelTranslator.TranslateToText(outputModel);
			
			actualOutputText.Should().Be(expectedOutputText);
		}
	}
}