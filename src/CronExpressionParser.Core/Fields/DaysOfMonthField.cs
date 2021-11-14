﻿namespace CronExpressionParser.Core.Fields
{
	public class DaysOfMonthField : Field, IField
	{
		private const int MinValue = 1;
		private const int MaxValue = 31;

		public DaysOfMonthField() : base(nameof(DaysOfMonthField), MinValue, MaxValue)
		{
		}
	}
}