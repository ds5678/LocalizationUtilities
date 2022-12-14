using LocalizationUtilities.Exceptions;

namespace LocalizationUtilities;

/// <summary>
/// 
/// </summary>
/// <param name="LocalizationID">The id for this localized string.</param>
/// <param name="Map">A LanguageName : LocalizedString dictionary</param>
public sealed record class LocalizationEntry(string LocalizationID, Dictionary<string, string> Map)
{
	public LocalizationEntry(string localizationID) : this (localizationID, new()) { }

	public void Validate()
	{
		if (string.IsNullOrEmpty(LocalizationID))
		{
			throw new InvalidLocalizationKeyException("Localization ID cannot be null or empty.");
		}

		if (Map == null)
		{
			throw new InvalidLanguageMapException("Map cannot be null.");
		}

		if (Map.Count == 0)
		{
			throw new InvalidLanguageMapException("Map cannot have no contents.");
		}

		foreach (KeyValuePair<string, string> pair in Map)
		{
			if (string.IsNullOrEmpty(pair.Key))
			{
				throw new InvalidLanguageMapException("Localization language cannot be null or empty.");
			}

			if (pair.Value == null)
			{
				throw new InvalidLanguageMapException("Localized text cannot be null.");
			}
		}
	}

	public override string ToString()
	{
		return LocalizationID;
	}
}
