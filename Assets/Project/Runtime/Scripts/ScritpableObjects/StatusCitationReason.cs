using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Status Citation Reason", menuName = "Data/Citation reasons/Status Citation", order = 0)]
public class StatusCitationReason : CitationReasonBase
{
    public override bool CheckDays(int days)
    {
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
        if(status.ID != 100)
        {
            return true;
        }

        return false;
    }

    public override string ReturnString()
    {
        return string.Empty;
    }
}
