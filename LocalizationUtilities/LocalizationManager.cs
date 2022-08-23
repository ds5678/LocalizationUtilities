extern alias Hinterland;
using Hinterland;
using MelonLoader;
using MelonLoader.TinyJSON;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LocalizationUtilities;

public static class LocalizationManager
{
	internal static List<LocalizationSet> localizations { get; private set; } = new List<LocalizationSet>();

	public static void AddLocalizations(LocalizationSet set)
	{
		set.Validate();
		localizations.Add(set);
	}

	public static void LoadLocalization(TextAsset asset, string path)
	{
		if (path.ToLower().EndsWith(".json", System.StringComparison.Ordinal))
		{
			LoadJSONLocalization(asset);
		}
		else if (path.ToLower().EndsWith(".csv", System.StringComparison.Ordinal))
		{
			LoadCSVLocalization(asset);
		}
		else
		{
			MelonLogger.Warning($"Found localization '{path}' that could not be loaded.");
		}
	}

	public static void LoadCSVLocalization(TextAsset textAsset)
	{
		ByteReader byteReader = new ByteReader(textAsset);
		string[] languages = Trim(byteReader.ReadCSV().ToArray());
		List<LocalizationEntry> newEntries = new();

		while (true)
		{
			string[] values = byteReader.ReadCSV()?.ToArray();
			if (values == null || languages == null || values.Length == 0 || languages.Length == 0)
			{
				break;
			}

			string locID = values[0];
			Dictionary<string, string> locDict = new Dictionary<string, string>();

			int maxIndex = System.Math.Min(values.Length, languages.Length);
			for (int j = 1; j < maxIndex; j++)
			{
				if (!string.IsNullOrEmpty(values[j]) && !string.IsNullOrEmpty(languages[j]))
				{
					locDict.Add(languages[j], values[j]);
				}
			}

			newEntries.Add(new LocalizationEntry(locID, locDict));
		}

		AddLocalizations(new LocalizationSet(newEntries, true));
	}

	private static string GetText(TextAsset textAsset)
	{
		ByteReader byteReader = new ByteReader(textAsset);
		StringBuilder sb = new StringBuilder();
		while (byteReader.canRead)
		{
			sb.AppendLine(byteReader.ReadLine());
		}
		return sb.ToString();
	}

	public static bool LoadJSONLocalization(TextAsset textAsset)
	{
		string contents = GetText(textAsset);
		return LoadJSONLocalization(contents);
	}

	public static bool LoadJSONLocalization(string contents)
	{
		if (string.IsNullOrWhiteSpace(contents)) return false;
		ProxyObject dict = (ProxyObject)JSON.Load(contents);
		List<LocalizationEntry> newEntries = new List<LocalizationEntry>();
		foreach (var pair in dict)
		{
			string locID = pair.Key;
			Dictionary<string, string> locDict = pair.Value.Make<Dictionary<string, string>>();
			newEntries.Add(new LocalizationEntry(locID, locDict));
		}
		AddLocalizations(new LocalizationSet(newEntries, true));
		return true;
	}

	/// <summary>
	/// Returns an array of string variables without any leading or trailing whitespace
	/// </summary>
	/// <param name="values">An array of string variables.</param>
	/// <returns>A new array containing the trimmed values.</returns>
	private static string[] Trim(string[] values)
	{
		string[] result = new string[values.Length];

		for (int i = 0; i < values.Length; i++)
		{
			result[i] = values[i].Trim();
		}

		return result;
	}
}
