namespace LocalizationUtilities.Exceptions;

public sealed class InvalidLocalizationKeyException : Exception
{
	public InvalidLocalizationKeyException() : base() { }
	public InvalidLocalizationKeyException(string message) : base(message) { }
}