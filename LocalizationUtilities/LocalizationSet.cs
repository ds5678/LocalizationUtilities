using LocalizationUtilities.Exceptions;
using System.Collections.Generic;

namespace LocalizationUtilities
{
	public class LocalizationSet
	{
		public bool defaultToEnglish = true;
		public List<LocalizationEntry> entries;

		public LocalizationSet() => entries = new List<LocalizationEntry>();

		public LocalizationSet(LocalizationEntry entry, bool defaultToEnglish = true)
		{
			this.defaultToEnglish = defaultToEnglish;
			entries = new List<LocalizationEntry>();
			entries.Add(entry);
		}

		public LocalizationSet(List<LocalizationEntry> entries, bool defaultToEnglish = true)
		{
			this.entries = entries;
			this.defaultToEnglish = defaultToEnglish;
		}

		public LocalizationSet(LocalizationEntry[] entries, bool defaultToEnglish = true) : this(new List<LocalizationEntry>(entries), defaultToEnglish) { }

		public void Validate()
		{
			if (entries == null)
				throw new InvalidEntryListException("Entry list cannot be null");
			if (entries.Count == 0)
				throw new InvalidEntryListException("Entry list cannot be empty");
			foreach (var entry in entries)
			{
				entry.Validate();
			}
		}
	}
}
