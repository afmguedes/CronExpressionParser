using System.Collections.Generic;
using System.Linq;
using CronExpressionParser.Core.Fields;
using FluentAssertions;
using NUnit.Framework;

namespace CronExpressionParser.UnitTests
{
	public class MinutesFieldShould
	{
		[Test]
		public void ReturnSingleValue_WhenSingleValueExpression()
		{
			const string minutesExpression = "1";
			var expectedMinutes = new List<int> { 1 };

			var actualMinutes = new MinutesField().TryParse(minutesExpression);
			
			actualMinutes.Should().BeEquivalentTo(expectedMinutes);
		}

		[Test]
		public void ReturnRangeOfValues_WhenSingleStarExpression()
		{
			const string minutesExpression = "*";
			var expectedMinutes = new List<int>(Enumerable.Range(0, 59));

			var actualMinutes = new MinutesField().TryParse(minutesExpression);

			actualMinutes.Should().BeEquivalentTo(expectedMinutes);
		}

		[Test]
		public void ReturnListOfValues_WhenAdditionalValuesExpression()
		{
			const string minutesExpression = "1, 2, 3";
			var expectedMinutes = new List<int> { 1, 2, 3 };

			var actualMinutes = new MinutesField().TryParse(minutesExpression);

			actualMinutes.Should().BeEquivalentTo(expectedMinutes);
		}
	}
}