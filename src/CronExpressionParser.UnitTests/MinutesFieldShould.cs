using System;
using System.Collections.Generic;
using System.Linq;
using CronExpressionParser.Core.Fields;
using FluentAssertions;
using NUnit.Framework;

namespace CronExpressionParser.UnitTests
{
	[TestFixture]
	public class MinutesFieldShould
	{
		private static readonly TestCaseData[] HappyPathTestCases =
		{
			new TestCaseData("1", new List<int> { 1 }).SetName("SingleValueExpression"),
			new TestCaseData("*", new List<int>(Enumerable.Range(0, 60))).SetName("SingleStarExpression"),
			new TestCaseData("1, 2, 3", new List<int> { 1, 2, 3 }).SetName("AdditionalValuesExpression"),
			new TestCaseData("1-5", new List<int> { 1, 2, 3, 4, 5 }).SetName("RangeExpression"),
			new TestCaseData("*/10", new List<int> { 0, 10, 20, 30, 40, 50 }).SetName("IncrementsFromStarExpression"),
			new TestCaseData("5/20", new List<int> { 5, 25, 45 }).SetName("IncrementsFromValueExpression")
		};

		[TestCaseSource(nameof(HappyPathTestCases))]
		public void ReturnExpectedMinutes_WhenPassed(string minutesExpression, List<int> expectedMinutes)
		{
			var actualMinutes = new MinutesField().TryParse(minutesExpression);
			
			actualMinutes.Should().BeEquivalentTo(expectedMinutes);
		}

		private static readonly TestCaseData[] UnhappyPathTestCases =
		{
			new TestCaseData("$", "'$' is not a valid value for MinutesField").SetName("InvalidSingleCharacterExpression"),
			new TestCaseData("1,", "'' is not a valid value for MinutesField").SetName("InvalidAdditionalValuesExpression"),
			new TestCaseData("2,3,=", "'=' is not a valid value for MinutesField").SetName("InvalidCharacterAdditionalValuesExpression"),
			new TestCaseData("10-", "'' is not a valid value for MinutesField").SetName("InvalidRangeValuesExpression"),
			new TestCaseData("0-+", "'+' is not a valid value for MinutesField").SetName("InvalidCharacterRangeValuesExpression"),
			new TestCaseData("0/&", "'&' is not a valid value for MinutesField").SetName("InvalidCharacterIncrementValuesExpression"),
			new TestCaseData("*/*", "'*' is not a valid value for MinutesField").SetName("InvalidCharactersIncrementValuesExpression"),
			new TestCaseData("60", "'60' is not a valid value for MinutesField").SetName("OutOfRangeValueExpression")
		};

		[TestCaseSource(nameof(UnhappyPathTestCases))]
		public void ThrowArgumentException_WhenPassed(string minutesExpression, string expectedMessage)
		{
			Action action = () => new MinutesField().TryParse(minutesExpression);

			action.Should().Throw<ArgumentException>().WithMessage(expectedMessage);
		}
	}
}