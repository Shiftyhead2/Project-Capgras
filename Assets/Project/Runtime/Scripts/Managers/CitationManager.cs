using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitationManager : MonoBehaviour
{

    [SerializeField]
    [TextArea(10, 100)]
    private string reasons;
    

    private void OnEnable()
    {
        GameEvents.onProcessFieldData += checkFieldList;
    }

    private void OnDisable()
    {
        GameEvents.onProcessFieldData -= checkFieldList;
    }


    private void checkFieldList(List<FieldData> fieldDatas)
    {
        reasons = string.Empty;
        string fullCitationtext = "You have been given a citation for the following reasons: \n \n";
        

        foreach(FieldData fieldData in fieldDatas)
        {
            if (fieldData._isFalse)
            {
                reasons += $"{fieldData._fieldName} is incorrect! \n";
            }
        }

            

        if(reasons != string.Empty)
        {
            fullCitationtext += reasons;
            fullCitationtext += "\n Do not dissapoint us again!";

            GameEvents.showModal?.Invoke("CITATION", fullCitationtext, "OK", GameEvents.onCitationModalClosed,true);
            GameEvents.onCitationGiven?.Invoke();
        }
    }
}
