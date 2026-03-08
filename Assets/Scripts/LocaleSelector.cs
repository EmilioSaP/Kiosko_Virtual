using UnityEngine;
using UnityEngine.Localization.Settings;
using System.Collections;
using UnityEngine.Localization;

public class LocaleSelector : MonoBehaviour
{
    // Call this method to set a new locale by its identifier (e.g., "en", "fr", "es")
    public void ChangeLocale(string localeCode)
    {
        StartCoroutine(SetLocale(localeCode));
    }

    // Call this method to set a new locale by index from the list of available locales
    // (e.g., 0 for the first language in your list, 1 for the second, etc.)
    public void ChangeLocaleByIndex(int index)
    {
        StartCoroutine(SetLocale(index));
    }

    IEnumerator SetLocale(int index)
    {
        // Wait for the localization system to be sufficiently initialized
        yield return LocalizationSettings.InitializationOperation;
        
        // Set the selected locale using its index in the available locales list
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }

    IEnumerator SetLocale(string localeCode)
    {
        // Wait for the localization system to be sufficiently initialized
        yield return LocalizationSettings.InitializationOperation;

        // Find the locale by its identifier and set it as selected
        Locale newLocale = LocalizationSettings.AvailableLocales.Locales.Find(locale => locale.Identifier.Code == localeCode);
        if (newLocale != null)
        {
            LocalizationSettings.SelectedLocale = newLocale;
        }
        else
        {
            Debug.LogWarning($"Locale with code {localeCode} not found.");
        }
    }

    // Optional: Save the language preference so it persists across game sessions
    public void SaveLocalePreference(string localeCode)
    {
        PlayerPrefs.SetString("PreferredLocale", localeCode);
        PlayerPrefs.Save();
        ChangeLocale(localeCode);
    }
}
