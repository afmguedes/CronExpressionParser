using System.Collections.Generic;
using System.Linq;
using CronExpressionParser.Core.Fields;
using FluentAssertions;
using NUnit.Framework;

namespace CronExpressionParser.UnitTests
{
	public class MinutesFieldShould
	{
		private static readonly TestCaseData[] TestCases =
		{
			new TestCaseData("1", new List<int> { 1 }).SetName("SingleValueExpression"),
			new TestCaseData("*", new List<int>(Enumerable.Range(0, 59))).SetName("SingleStarExpression"),
			new TestCaseData("1, 2, 3", new List<int> { 1, 2, 3 }).SetName("AdditionalValuesExpression"),
			new TestCaseData("1-5", new List<int> { 1, 2, 3, 4, 5 }).SetName("RangeExpression"),
			new TestCaseData("*/10", new List<int> { 0, 10, 20, 30, 40, 50 }).SetName("IncrementsFromStarExpression"),
			new TestCaseData("5/20", new List<int> { 5, 25, 45 }).SetName("IncrementsFromValueExpression")
		};

		[TestCaseSource(nameof(TestCases))]
		public void ReturnExpectedMinutes_WhenPassed(string minutesExpression, List<int> expectedMinutes)
		{
			var actualMinutes = new MinutesField().TryParse(minutesExpression);
			
			actualMinutes.Should().BeEquivalentTo(expectedMinutes);
		}
	}
}