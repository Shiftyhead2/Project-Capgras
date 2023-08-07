using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Status Object", menuName = "Data/New Status/Status Object",order = 0)]
public class StatusScriptableObject : ScriptableObject
{
    public string statusKey;
    public Color statusColor;
    public int ID;

    public float maxChanceToOccur;
    public float minChanceToOccur;

    public string returnStatus()
    {
        return $"<color=#{ColorUtility.ToHtmlStringRGBA(statusColor)}>{GetLocalizedString(LocatilazitionStrings.DYNAMIC_UI_TABLE_NAME,statusKey)}</color>";
    }

    private string GetLocalizedString(string table_key, string string_key)
    {
        return LocalizationEventManager.GetLocalizedString(table_key, string_key);
    }
}
