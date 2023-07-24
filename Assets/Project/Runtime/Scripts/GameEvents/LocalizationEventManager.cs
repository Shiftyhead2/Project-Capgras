using System;


/// <summary>
/// This class is a static class to manage one event for localization
/// </summary>
public static class LocalizationEventManager
{
    public static Func<string, string, object[], string> onLocalizationNeeded;
    
    /// <summary>
    /// This function fires an event for localizing strings
    /// </summary>
    /// <param name="table_key">The key for the localization table</param>
    /// <param name="string_key">The key for the string being localized</param>
    /// <param name="args">Smart format arguments</param>
    /// <returns>A localized string</returns>
    public static string GetLocalizedString(string table_key, string string_key, object[] args = null)
    {
        return onLocalizationNeeded?.Invoke(table_key, string_key, args);
    }
}
