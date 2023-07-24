using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DaysSinceUpdate Citation Reason", menuName = "Data/Citation reasons/DaysSinceUpdate Citation", order = 0)]
public class DaysSinceUpdateCitationReason : CitationReasonBase
{

    [SerializeField]
    private string citationReasonKey;


    public override bool CheckDays(int days)
    {
        if(days > 7 || days == -1)
        {
            return true;
        }

        return false;
    }

    public override bool CheckIfDoppleGanger(bool doppleGanger)
    {
        return false;
    }

    public override bool CheckIfIDMatches(int id)
    {
        return false;
    }

    public override bool CheckIfInvalid(FieldData data)
    {
        return false;
    }

    public override bool CheckIfSuspicous(bool suspicious)
    {
        return false;
    }

    public override bool CheckStatus(StatusScriptableObject status)
    {
        return false;
    }

    public override string ReturnString()
    {
        return $"{GetLocalizedString(LocatilazitionStrings.DYNAMIC_UI_TABLE_NAME,citationReasonKey)} \n";
    }

    private string GetLocalizedString(string table_key, string string_key, object[] args = null)
    {
        return LocalizationEventManager.GetLocalizedString(table_key, string_key, args);
    }
}
