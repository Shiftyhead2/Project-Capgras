using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DaysSinceUpdate Citation Reason", menuName = "Data/Citation reasons/DaysSinceUpdate Citation", order = 0)]
public class DaysSinceUpdateCitationReason : CitationReasonBase
{
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
        return $"Outdated citizen database! \n";
    }
}
