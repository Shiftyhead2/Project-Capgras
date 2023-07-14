using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class DynamicLocalizationManager : MonoBehaviour
{

    private void OnEnable()
    {
        LocalizationEventManager.onLocalizationNeeded += ReturnLocalizedString;
    }

    private void OnDisable()
    {
        LocalizationEventManager.onLocalizationNeeded -= ReturnLocalizedString;
    }


    private string ReturnLocalizedString(string tableKey, string string_key, object[] args = null)
    {
        string localized_string = LocalizationSettings.StringDatabase.GetLocalizedString(tableKey, string_key , args);

        return localized_string;
    }

}
