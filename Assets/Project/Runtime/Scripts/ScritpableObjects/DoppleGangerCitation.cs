using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DoopleGanger Citation Reason", menuName = "Data/Citation reasons/DoopleGanger Citation", order = 0)]
public class DoppleGangerCitation : CitationReasonBase
{
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
        return false;
    }

    public override bool CheckIfInvalid(FieldData data)
    {
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

    public override string ReturnString()
    {
        return string.Empty;
    }
}
