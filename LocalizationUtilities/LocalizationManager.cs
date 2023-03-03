using MelonLoader;
using MelonLoader.TinyJSON;
using System.Text;
using UnityEngine;

namespace LocalizationUtilities;

public static class LocalizationManager
{
	internal static HashSet<LocalizationSet> Localizations { get; private set; } = new();

	public static void AddLocalizations(LocalizationSet set)
	{
		set.Validate();
		Localizations.Add(set);
	}

	public static void LoadLocalization(TextAsset asset, string path)
	{
		if (path.ToLower().EndsWith(".json", System.StringComparison.Ordinal))
		{
			LoadJsonLocalization(asset);
		}
		else
		{
			MelonLogger.Warning($"Found localization '{path}' that could not be loaded.");
		}
	}

	
	private static string GetText(TextAsset textAsset)
	{
		const byte leftCurlyBracket = (byte)'{';
		byte[] bytes = textAsset.bytes;
		int index = Array.IndexOf(bytes, leftCurlyBracket);
		if (index < 0)
		{
			throw new ArgumentException("TextAsset has no Json content.", nameof(textAsset));
		}
		return Encoding.UTF8.GetString(new ReadOnlySpan<byte>(bytes, index, bytes.Length - index));
	}

	public static bool LoadJsonLocalization(TextAsset textAsset)
	{
		string contents = GetText(textAsset);
		return LoadJsonLocalization(contents);
	}

	public static bool LoadJsonLocalization(string contents)
	{
		if (string.IsNullOrWhiteSpace(contents))
		{
			return false;
		}

		ProxyObject dict = (ProxyObject)JSON.Load(contents);
		List<LocalizationEntry> newEntries = new();
		foreach (KeyValuePair<string, Variant> pair in dict)
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
