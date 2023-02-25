﻿using HarmonyLib;
using Il2Cpp;
using StringTableEntry = Il2Cpp.StringTableData.Entry;

namespace LocalizationUtilities;

internal static class LocalizationPatch
{
	
	[HarmonyPatch(typeof(Localization), nameof(Localization.LoadStringTableForLanguage), new Type[] { typeof(AssetBundleRef),typeof(string)})]
	internal class LoadStringTableForLanguage_Postfix
	{
		private static void Postfix()
		{
			StringTable strTable = Localization.s_CurrentLanguageStringTable;
			foreach (LocalizationSet set in LocalizationManager.Localizations)
			{
				AddOrUpdate(strTable, set);
			}
		}
	}

	private static void AddOrUpdate(StringTable stringTable, LocalizationSet set)
	{
		string[] languages = stringTable.GetLanguagesArray();
		foreach (LocalizationEntry entry in set.Entries)
		{
			StringTableEntry stringEntry = stringTable.GetOrAddEntryFromKey(entry.LocalizationID);
			for (int i = 0; i < languages.Length; i++)
			{
				string language = languages[i];
				if (entry.Map.TryGetValue(language, out string? text))
				{
					stringEntry.m_Languages[i] = text;
				}
				else if (set.DefaultToEnglish && entry.Map.TryGetValue("English", out string? text2))
				{
					stringEntry.m_Languages[i] = text2;
				}
			}
		}
	}

	private static string[] GetLanguagesArray(this StringTable stringTable)
	{
		return stringTable.GetLanguages().ToArray().ToArray();
	}

	private static StringTableEntry GetOrAddEntryFromKey(this StringTable stringTable, string localizationID)
	{
		return stringTable.GetEntryFromKey(localizationID) ?? stringTable.AddEntryForKey(localizationID);
	}
}
