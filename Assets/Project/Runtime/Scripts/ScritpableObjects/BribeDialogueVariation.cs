using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Bribe Variation", menuName = "Data/Dialogue/Bribe Variation", order = 0)]
public class BribeDialogueVariation : ScriptableObject
{
    public string key;

    public string ReturnString()
    {
        return GetLocalizedString(LocatilazitionStrings.DYNAMIC_DIALOGUE_TABLE, key);
    }

    private string GetLocalizedString(string table_key, string string_key, object[] args = null)
    {
        return LocalizationEventManager.GetLocalizedString(table_key, string_key, args);
    }
}
