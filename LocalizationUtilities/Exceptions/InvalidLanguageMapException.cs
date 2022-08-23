using System;

namespace LocalizationUtilities.Exceptions;

public class InvalidLanguageMapException : Exception
{
	public InvalidLanguageMapException() : base() { }
	public InvalidLanguageMapException(string message) : base(message) { }
}