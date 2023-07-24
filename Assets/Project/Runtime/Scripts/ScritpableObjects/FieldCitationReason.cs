using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Field Citation Reason", menuName = "Data/Citation reasons/Field Citation", order = 0)]
public class FieldCitationReason : CitationReasonBase
{

    public int fieldID = 0;
    public string fieldKey;

    public override bool CheckDays(int days = 0)
    {
        return false;
    }

    public override bool CheckIfDoppleGanger(bool doppleGanger)
    {
        if (doppleGanger)
        {
            return true;
        }
        return false;
    }

    public override bool CheckIfIDMatches(int id)
    {
        if (id != fieldID)
        {
            return false;
        }

        return true;
    }

    public override bool CheckIfInvalid(FieldData data)
    {
        if (data.IsFalse)
        {
            return true;
        }

        return false;
    }

    public override bool CheckIfSuspicous(bool suspicious)
    {
        if (suspicious)
        {
            return true;
        }
        return false;
    }

    public override bool CheckStatus(StatusScriptableObject status = null)
    {
        return false;
    }

    public override string ReturnString()
    {
        return $"{GetLocalizedString(LocatilazitionStrings.DYNAMIC_UI_TABLE_NAME,LocatilazitionStrings.CITATION_REASON_FIELD_REASON_KEY, new object[] {GetLocalizedString(LocatilazitionStrings.DYNAMIC_UI_TABLE_NAME,fieldKey)})} \n";
    }

    private string GetLocalizedString(string table_key, string string_key, object[] args = null)
    {
        return LocalizationEventManager.GetLocalizedString(table_key, string_key, args);
    }
}
