using LocalizationUtilities.Exceptions;
using System.Collections.Generic;

namespace LocalizationUtilities
{
	public class LocalizationEntry
	{
		public string localizationID;
		public Dictionary<string, string> map;

		public LocalizationEntry()
		{
			localizationID = null;
			map = new Dictionary<string, string>();
		}

		public LocalizationEntry(string id)
		{
			localizationID = id;
			map = new Dictionary<string, string>();
		}

		public LocalizationEntry(string id, Dictionary<string, string> map)
		{
			localizationID = id;
			this.map = map;
		}

		public void Validate()
		{
			if (string.IsNullOrEmpty(localizationID))
				throw new InvalidLocalizationKeyException("Localization ID cannot be null or empty.");
			if (map == null)
				throw new InvalidLanguageMapException("Map cannot be null.");
			if (map.Count == 0)
				throw new InvalidLanguageMapException("Map cannot have no contents.");
			foreach (var pair in map)
			{
				if (string.IsNullOrEmpty(pair.Key))
					throw new InvalidLanguageMapException("Localization language cannot be null or empty.");
				if (pair.Value == null)
					throw new InvalidLanguageMapException("Localized text cannot be null.");
			}
		}

		public override string ToString()
		{
			return localizationID ?? base.ToString();
		}
	}
}
