using System;

namespace LocalizationUtilities.Exceptions;

public class InvalidLocalizationKeyException : Exception
{
	public InvalidLocalizationKeyException() : base() { }
	public InvalidLocalizationKeyException(string message) : base(message) { }
}