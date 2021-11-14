using System;
using System.Collections.Generic;
using System.Linq;
using CronExpressionParser.Core.Fields;
using FluentAssertions;
using NUnit.Framework;

namespace CronExpressionParser.UnitTests
{
	[TestFixture]
	public class DaysOfWeekFieldShould
	{
		private static readonly TestCaseData[] HappyPathTestCases =
		{
			new TestCaseData("1", new List<int> { 1 }).SetName("SingleValueExpression"),
			new TestCaseData("*", new List<int>(Enumerable.Range(1, 7))).SetName("SingleStarExpression"),
			new TestCaseData("1, 2, 3", new List<int> { 1, 2, 3 }).SetName("AdditionalValuesExpression"),
			new TestCaseData("1-5", new List<int> { 1, 2, 3, 4, 5 }).SetName("RangeExpression"),
			new TestCaseData("*/2", new List<int> { 1, 3, 5, 7 }).SetName("IncrementsFromStarExpression"),
			new TestCaseData("2/2", new List<int> { 2, 4, 6 }).SetName("IncrementsFromValueExpression")
		};

		[TestCaseSource(nameof(HappyPathTestCases))]
		public void ReturnExpectedDaysOfWeek_WhenPassed(string daysOfWeekExpression, List<int> expectedDaysOfWeek)
		{
			var actualDaysOfWeek = new DaysOfWeekField().TryParse(daysOfWeekExpression);

			actualDaysOfWeek.Should().BeEquivalentTo(expectedDaysOfWeek);
		}

		private static readonly TestCaseData[] UnhappyPathTestCases =
		{
			new TestCaseData("$", "'$' is not a valid value for DaysOfWeekField").SetName("InvalidSingleCharacterExpression"),
			new TestCaseData("1,", "'' is not a valid value for DaysOfWeekField").SetName("InvalidAdditionalValuesExpression"),
			new TestCaseData("2,3,=", "'=' is not a valid value for DaysOfWeekField").SetName("InvalidCharacterAdditionalValuesExpression"),
			new TestCaseData("5-", "'' is not a valid value for DaysOfWeekField").SetName("InvalidRangeValuesExpression"),
			new TestCaseData("0-+", "'+' is not a valid value for DaysOfWeekField").SetName("InvalidCharacterRangeValuesExpression"),
			new TestCaseData("0/&", "'&' is not a valid value for DaysOfWeekField").SetName("InvalidCharacterIncrementValuesExpression"),
			new TestCaseData("*/*", "'*' is not a valid value for DaysOfWeekField").SetName("InvalidCharactersIncrementValuesExpression"),
			new TestCaseData("13", "'13' is not a valid value for DaysOfWeekField").SetName("OutOfRangeValueExpression")
		};

		[TestCaseSource(nameof(UnhappyPathTestCases))]
		public void ThrowArgumentException_WhenPassed(string daysOfWeekExpression, string expectedMessage)
		{
			Action action = () => new DaysOfWeekField().TryParse(daysOfWeekExpression);

			action.Should().Throw<ArgumentException>().WithMessage(expectedMessage);
		}
	}
}