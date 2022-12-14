namespace LocalizationUtilities.Exceptions;

public sealed class InvalidEntryListException : Exception
{
	public InvalidEntryListException() : base() { }
	public InvalidEntryListException(string message) : base(message) { }
}
