using System;

namespace CronExpressionParser.Core.Exceptions
{
	public class InvalidNumberOfFieldsException : Exception
	{
		public InvalidNumberOfFieldsException(string message)
			: base(message)
		{
		}
	}
}