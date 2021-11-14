using System;
using System.Collections.Generic;
using CronExpressionParser.Core;
using CronExpressionParser.Core.Utilities;
using FluentAssertions;
using NUnit.Framework;

namespace CronExpressionParser.UnitTests
{
	[TestFixture]
	public class MyExtensionsShould
	{
		[Test]
		public void ReturnExpectedList_WhenGetIncrementsOf()
		{
			const int start = 5;
			const int increment = 10;
			const int maxValue = 59;
			var expectedIncrements = new List<int> { 5, 15, 25, 35, 45, 55 };

			var actualIncrements = start.GetIncrementsOf(increment, maxValue);
			
			actualIncrements.Should().BeEquivalentTo(expectedIncrements);
		}

		[Test]
		public void ThrowArgumentOutOfRangeException_WhenIncrementsIsEqualOrLessThanZero()
		{
			const int start = 3;
			const int increment = 0;
			const int maxValue = 23;

			Action action = () => start.GetIncrementsOf(increment, maxValue);

			action.Should().Throw<ArgumentOutOfRangeException>().WithMessage(LogMessageFactory.InvalidIncrementsValueLogMessage);
		}
	}
}