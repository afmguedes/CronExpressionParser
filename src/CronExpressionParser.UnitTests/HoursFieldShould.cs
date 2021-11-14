using System;
using System.Collections.Generic;
using System.Linq;
using CronExpressionParser.Core.Fields;
using FluentAssertions;
using NUnit.Framework;

namespace CronExpressionParser.UnitTests
{
	[TestFixture]
	public class HoursFieldShould
	{
		private static readonly TestCaseData[] HappyPathTestCases =
		{
			new TestCaseData("1", new List<int> { 1 }).SetName("SingleValueExpression"),
			new TestCaseData("*", new List<int>(Enumerable.Range(0, 23))).SetName("SingleStarExpression"),
			new TestCaseData("1, 2, 3", new List<int> { 1, 2, 3 }).SetName("AdditionalValuesExpression"),
			new TestCaseData("1-5", new List<int> { 1, 2, 3, 4, 5 }).SetName("RangeExpression"),
			new TestCaseData("*/10", new List<int> { 0, 10, 20 }).SetName("IncrementsFromStarExpression"),
			new TestCaseData("2/5", new List<int> { 2, 7, 12, 17, 22 }).SetName("IncrementsFromValueExpression")
		};

		[TestCaseSource(nameof(HappyPathTestCases))]
		public void ReturnExpectedHours_WhenPassed(string hoursExpression, List<int> expectedHours)
		{
			var actualHours = new HoursField().TryParse(hoursExpression);

			actualHours.Should().BeEquivalentTo(expectedHours);
		}

		private static readonly TestCaseData[] UnhappyPathTestCases =
		{
			new TestCaseData("$", "'$' is not a valid value for HoursField").SetName("InvalidSingleCharacterExpression"),
			new TestCaseData("1,", "'' is not a valid value for HoursField").SetName("InvalidAdditionalValuesExpression"),
			new TestCaseData("2,3,=", "'=' is not a valid value for HoursField").SetName("InvalidCharacterAdditionalValuesExpression"),
			new TestCaseData("10-", "'' is not a valid value for HoursField").SetName("InvalidRangeValuesExpression"),
			new TestCaseData("0-+", "'+' is not a valid value for HoursField").SetName("InvalidCharacterRangeValuesExpression"),
			new TestCaseData("0/&", "'&' is not a valid value for HoursField").SetName("InvalidCharacterIncrementValuesExpression"),
			new TestCaseData("*/*", "'*' is not a valid value for HoursField").SetName("InvalidCharactersIncrementValuesExpression"),
			new TestCaseData("24", "'24' is not a valid value for HoursField").SetName("OutOfRangeValueExpression")
		};

		[TestCaseSource(nameof(UnhappyPathTestCases))]
		public void ThrowArgumentException_WhenPassed(string hoursExpression, string expectedMessage)
		{
			Action action = () => new HoursField().TryParse(hoursExpression);

			action.Should().Throw<ArgumentException>().WithMessage(expectedMessage);
		}
	}
}