using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DialogueType
{
    BEG,
    BRIBE
}

[CreateAssetMenu(fileName = "New dialogue text", menuName = "Data/Dialogue/New Dialogue", order = 0)]
public class DialogueText : ScriptableObject
{
    public string openingKey;
    public DialogueType type;
    public BribeDialogueVariation[] openingBribeVariation;
    public BribeDialogueVariation[] closingBribeVariation;
    public int minBribeAmount;
    public int maxBribeAmount;

    public string SetUpText(int bribeAmount)
    {
        switch (type)
        {
            case DialogueType.BEG:
                return ReturnFullText(openingKey);
            case DialogueType.BRIBE:
                return $"{ReturnFullText(LocatilazitionStrings.BRIDE_FULL_TEXT_KEY, new object[] {ReturnFullText(openingKey), openingBribeVariation[Random.Range(0,openingBribeVariation.Length - 1)].ReturnString(), bribeAmount, closingBribeVariation[Random.Range(0,closingBribeVariation.Length-1)].ReturnString()})}";
            default:
                return ReturnFullText(openingKey);
        }
    }

    private string ReturnFullText(string string_key, object[] args = null)
    {
        return GetLocalizedString(LocatilazitionStrings.DYNAMIC_DIALOGUE_TABLE, string_key, args);
    }


    private string GetLocalizedString(string table_key, string string_key, object[] args = null)
    {
        return LocalizationEventManager.GetLocalizedString(table_key, string_key, args);
    }
}
