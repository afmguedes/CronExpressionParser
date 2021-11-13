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
			new TestCaseData("1, 2, 3", new List<int> { 1, 2, 3 }).SetName("AdditionalValuesExpression")
		};

		[TestCaseSource(nameof(TestCases))]
		public void ReturnExpectedMinutes_WhenPassed(string minutesExpression, List<int> expectedMinutes)
		{
			var actualMinutes = new MinutesField().TryParse(minutesExpression);
			
			actualMinutes.Should().BeEquivalentTo(expectedMinutes);
		}
	}
}