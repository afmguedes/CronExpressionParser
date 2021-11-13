using System;
using CronExpressionParser.Core.Exceptions;

namespace CronExpressionParser.Core
{
	public class CronExpression
	{
		private const char FieldSeparator = ' ';
		private const int NumberOfFields = 6;

		public static CronExpression Create(string inputExpression)
		{
			var fields = inputExpression.Split(FieldSeparator);

			if (fields.Length != NumberOfFields)
			{
				throw new InvalidNumberOfFieldsException(LogMessageFactory.InvalidNumberOfFieldsLogMessage);
			}

			throw new NotImplementedException();
		}

		public OutputModel Expand()
		{
			throw new NotImplementedException();
		}
	}
}