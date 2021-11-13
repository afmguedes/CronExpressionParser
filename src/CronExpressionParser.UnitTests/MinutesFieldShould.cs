using System.Collections.Generic;
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
	}
}