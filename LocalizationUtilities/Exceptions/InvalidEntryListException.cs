using System;

namespace LocalizationUtilities.Exceptions;

public class InvalidEntryListException : Exception
{
	public InvalidEntryListException() : base() { }
	public InvalidEntryListException(string message) : base(message) { }
}
