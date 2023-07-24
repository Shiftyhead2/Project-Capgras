using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New dialogue choice", menuName = "Data/Dialogue/New Choice", order = 0)]
public class DialogueChoice : ScriptableObject
{
    public string choiceKey;
    public int ID;


    public string ReturnString()
    {
        return $"{GetLocalizedString(LocatilazitionStrings.DYNAMIC_DIALOGUE_TABLE,choiceKey)}";
    }

    private string GetLocalizedString(string table_key , string string_key, object[] args = null)
    {
        return LocalizationEventManager.GetLocalizedString(table_key, string_key, args);
    }
}
