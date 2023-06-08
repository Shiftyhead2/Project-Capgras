using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Field Citation Reason", menuName = "Data/Citation reasons/Field Citation", order = 0)]
public class FieldCitationReason : CitationReasonBase
{

    public int fieldID = 0;
    public string fieldName;

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

    public override string ReturnString()
    {
        return $"{fieldName} is incorrect! \n";
    }
}
