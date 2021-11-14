using System;
using System.Collections.Generic;
using System.Linq;
using CronExpressionParser.Core.Fields;
using FluentAssertions;
using NUnit.Framework;

namespace CronExpressionParser.UnitTests
{
	[TestFixture]
	public class DaysOfMonthFieldShould
	{
		private static readonly TestCaseData[] HappyPathTestCases =
		{
			new TestCaseData("1", new List<int> { 1 }).SetName("SingleValueExpression"),
			new TestCaseData("*", new List<int>(Enumerable.Range(1, 31))).SetName("SingleStarExpression"),
			new TestCaseData("1, 2, 3", new List<int> { 1, 2, 3 }).SetName("AdditionalValuesExpression"),
			new TestCaseData("1-5", new List<int> { 1, 2, 3, 4, 5 }).SetName("RangeExpression"),
			new TestCaseData("*/10", new List<int> { 1, 11, 21, 31 }).SetName("IncrementsFromStarExpression"),
			new TestCaseData("2/5", new List<int> { 2, 7, 12, 17, 22, 27 }).SetName("IncrementsFromValueExpression")
		};

		[TestCaseSource(nameof(HappyPathTestCases))]
		public void ReturnExpectedDaysOfMonth_WhenPassed(string daysOfMonthExpression, List<int> expectedDaysOfMonth)
		{
			var actualDaysOfMonth = new DaysOfMonthField().TryParse(daysOfMonthExpression);

			actualDaysOfMonth.Should().BeEquivalentTo(expectedDaysOfMonth);
		}

		private static readonly TestCaseData[] UnhappyPathTestCases =
		{
			new TestCaseData("$", "'$' is not a valid value for DaysOfMonthField").SetName("InvalidSingleCharacterExpression"),
			new TestCaseData("1,", "'' is not a valid value for DaysOfMonthField").SetName("InvalidAdditionalValuesExpression"),
			new TestCaseData("2,3,=", "'=' is not a valid value for DaysOfMonthField").SetName("InvalidCharacterAdditionalValuesExpression"),
			new TestCaseData("10-", "'' is not a valid value for DaysOfMonthField").SetName("InvalidRangeValuesExpression"),
			new TestCaseData("0-+", "'+' is not a valid value for DaysOfMonthField").SetName("InvalidCharacterRangeValuesExpression"),
			new TestCaseData("0/&", "'&' is not a valid value for DaysOfMonthField").SetName("InvalidCharacterIncrementValuesExpression"),
			new TestCaseData("*/*", "'*' is not a valid value for DaysOfMonthField").SetName("InvalidCharactersIncrementValuesExpression"),
			new TestCaseData("32", "'32' is not a valid value for DaysOfMonthField").SetName("OutOfRangeValueExpression")
		};

		[TestCaseSource(nameof(UnhappyPathTestCases))]
		public void ThrowArgumentException_WhenPassed(string daysOfMonthExpression, string expectedMessage)
		{
			Action action = () => new DaysOfMonthField().TryParse(daysOfMonthExpression);

			action.Should().Throw<ArgumentException>().WithMessage(expectedMessage);
		}
	}
}