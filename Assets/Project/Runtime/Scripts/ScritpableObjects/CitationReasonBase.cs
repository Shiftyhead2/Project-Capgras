using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CitationReasonBase : ScriptableObject
{
    public abstract bool CheckIfInvalid(FieldData data);
    public abstract bool CheckIfSuspicous(bool suspicious);
    public abstract bool CheckIfDoppleGanger(bool doppleGanger);

    public abstract bool CheckIfIDMatches(int id);
    public abstract string ReturnString();
}
