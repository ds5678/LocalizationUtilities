extern alias Hinterland;
using HarmonyLib;
using Hinterland;
using System.Linq;

namespace LocalizationUtilities;

[HarmonyPatch(typeof(Localization), "LoadStringTableForLanguage")]
internal static class LocalizationPatch
{
	private static void Postfix()
	{
		StringTable strTable = Localization.s_CurrentLanguageStringTable;
		foreach (LocalizationSet set in LocalizationManager.Localizations)
		{
			AddOrUpdate(strTable, set);
		}
	}

	private static void AddOrUpdate(StringTable stringTable, LocalizationSet set)
	{
		string[] languages = stringTable.GetLanguages().ToArray().ToArray();
		foreach (LocalizationEntry entry in set.entries)
		{
			StringTableData.Entry strEntry = stringTable.GetEntryFromKey(entry.localizationID) ?? stringTable.AddEntryForKey(entry.localizationID);
			for (int i = 0; i < languages.Length; i++)
			{
				string language = languages[i];
				if (entry.map.TryGetValue(language, out string text))
				{
					strEntry.m_Languages[i] = text;
				}
				else if (set.defaultToEnglish && entry.map.TryGetValue("English", out string text2))
				{
					strEntry.m_Languages[i] = text2;
				}
			}
		}
	}
}
