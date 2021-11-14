using System;
using System.Collections.Generic;
using System.Linq;
using CronExpressionParser.Core.Fields;
using FluentAssertions;
using NUnit.Framework;

namespace CronExpressionParser.UnitTests
{
	[TestFixture]
	public class MonthsFieldShould
	{
		private static readonly TestCaseData[] HappyPathTestCases =
		{
			new TestCaseData("1", new List<int> { 1 }).SetName("SingleValueExpression"),
			new TestCaseData("*", new List<int>(Enumerable.Range(1, 12))).SetName("SingleStarExpression"),
			new TestCaseData("1, 2, 3", new List<int> { 1, 2, 3 }).SetName("AdditionalValuesExpression"),
			new TestCaseData("1-5", new List<int> { 1, 2, 3, 4, 5 }).SetName("RangeExpression"),
			new TestCaseData("*/10", new List<int> { 1, 11 }).SetName("IncrementsFromStarExpression"),
			new TestCaseData("2/5", new List<int> { 2, 7, 12 }).SetName("IncrementsFromValueExpression")
		};

		[TestCaseSource(nameof(HappyPathTestCases))]
		public void ReturnExpectedMonths_WhenPassed(string monthsExpression, List<int> expectedMonths)
		{
			var actualDaysOfMonth = new MonthsField().TryParse(monthsExpression);

			actualDaysOfMonth.Should().BeEquivalentTo(expectedMonths);
		}

		private static readonly TestCaseData[] UnhappyPathTestCases =
		{
			new TestCaseData("$", "'$' is not a valid value for MonthsField").SetName("InvalidSingleCharacterExpression"),
			new TestCaseData("1,", "'' is not a valid value for MonthsField").SetName("InvalidAdditionalValuesExpression"),
			new TestCaseData("2,3,=", "'=' is not a valid value for MonthsField").SetName("InvalidCharacterAdditionalValuesExpression"),
			new TestCaseData("10-", "'' is not a valid value for MonthsField").SetName("InvalidRangeValuesExpression"),
			new TestCaseData("0-+", "'+' is not a valid value for MonthsField").SetName("InvalidCharacterRangeValuesExpression"),
			new TestCaseData("0/&", "'&' is not a valid value for MonthsField").SetName("InvalidCharacterIncrementValuesExpression"),
			new TestCaseData("*/*", "'*' is not a valid value for MonthsField").SetName("InvalidCharactersIncrementValuesExpression"),
			new TestCaseData("13", "'13' is not a valid value for MonthsField").SetName("OutOfRangeValueExpression")
		};

		[TestCaseSource(nameof(UnhappyPathTestCases))]
		public void ThrowArgumentException_WhenPassed(string monthsExpression, string expectedMessage)
		{
			Action action = () => new MonthsField().TryParse(monthsExpression);

			action.Should().Throw<ArgumentException>().WithMessage(expectedMessage);
		}
	}
}