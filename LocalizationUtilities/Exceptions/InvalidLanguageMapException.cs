using System;

namespace LocalizationUtilities.Exceptions;

public sealed class InvalidLanguageMapException : Exception
{
	public InvalidLanguageMapException() : base() { }
	public InvalidLanguageMapException(string message) : base(message) { }
}