using LocalizationUtilities.Exceptions;

namespace LocalizationUtilities;

public sealed record class LocalizationSet(HashSet<LocalizationEntry> Entries, bool DefaultToEnglish)
{
	public LocalizationSet(bool defaultToEnglish = true) : this(new HashSet<LocalizationEntry>(), defaultToEnglish) { }

	public LocalizationSet(LocalizationEntry entry, bool defaultToEnglish = true) : this(new HashSet<LocalizationEntry> { entry }, defaultToEnglish) { }

	public LocalizationSet(List<LocalizationEntry> entries, bool defaultToEnglish = true) : this(entries.ToHashSet(), defaultToEnglish) { }

	public LocalizationSet(LocalizationEntry[] entries, bool defaultToEnglish = true) : this(new List<LocalizationEntry>(entries), defaultToEnglish) { }

	public LocalizationSet(HashSet<LocalizationEntry> entries) : this(entries, true) { }

	public void Validate()
	{
		if (Entries == null)
		{
			throw new InvalidEntryListException("Entry list cannot be null");
		}

		if (Entries.Count == 0)
		{
			throw new InvalidEntryListException("Entry list cannot be empty");
		}

		foreach (LocalizationEntry entry in Entries)
		{
			entry.Validate();
		}
	}
}
