using System;


/// <summary>
/// This class is a static class to manage one event for localization
/// </summary>
public static class LocalizationEventManager
{
    public static Func<string, string, object[], string> onLocalizationNeeded;
    

    public static string GetLocalizedString(string table_key, string string_key, object[] args = null)
    {
        return onLocalizationNeeded?.Invoke(table_key, string_key, args);
    }
}
