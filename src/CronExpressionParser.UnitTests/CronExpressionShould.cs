using CronExpressionParser.Core;
using FluentAssertions;
using NUnit.Framework;

namespace CronExpressionParser.UnitTests
{
	public class CronExpressionShould
	{
		[Test]
		public void ReturnExpectedOutputModel_WhenExpand()
		{
			string input = "*/15 0 1,15 * 1-5 /usr/bin/find";
			
			var expectedMinutes = new[] { 0, 15, 30, 45 };
			var expectedHours = new[] { 0 };
			var expectedDaysOfMonth = new[] { 1, 15 };
			var expectedMonths = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
			var expectedDaysOfWeek = new[] { 1, 2, 3, 4, 5 };
			const string expectedCommand = "/usr/bin/find";
			var expectedOutputModel = new OutputModel(expectedMinutes, expectedHours, expectedDaysOfMonth, expectedMonths, expectedDaysOfWeek, expectedCommand);

			var actualOutputModel = CronExpression.Create(input).Expand();

			actualOutputModel.Should().BeEquivalentTo(expectedOutputModel);
		}
	}
}