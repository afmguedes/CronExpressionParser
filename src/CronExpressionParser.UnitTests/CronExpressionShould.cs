using System;
using CronExpressionParser.Core;
using CronExpressionParser.Core.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace CronExpressionParser.UnitTests
{
	public class CronExpressionShould
	{
		[Test]
		public void ReturnExpectedOutputModel_WhenExpand()
		{
			const string input = "*/15 0 1,15 * 1-5 /usr/bin/find";

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

		[TestCase("", TestName = "WhenTooFewFieldsInInput")]
		[TestCase("0 0 1 1 1 command 0", TestName = "WhenTooManyFieldsInInput")]
		public void ThrowInvalidNumberOfFieldsException(string input)
		{
			Action action = () => CronExpression.Create(input).Expand();

			action.Should().Throw<InvalidNumberOfFieldsException>().WithMessage(LogMessageFactory.InvalidNumberOfFieldsLogMessage);
		}
	}
}