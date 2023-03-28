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


    private void checkFieldList(List<FieldData> fieldDatas, bool isSuspicious, bool doppleganger)
    {
        reasons = string.Empty;
        string fullCitationtext = "You have been given a citation for the following reasons: \n \n";

        giveReason(fieldDatas,isSuspicious,doppleganger);

        if(reasons != string.Empty)
        {
            fullCitationtext += reasons;
            fullCitationtext += $"\n A fine of {GameManager.instance.fine} credits has been added to your account!";
            fullCitationtext += "\n Do not dissapoint us again!";
            giveCitation(fullCitationtext); 
        }
        
    }

    void giveReason(List<FieldData> fieldDatas,bool isSuspicious, bool doppleganger)
    {

        int incorrectInformation = 0;

        foreach (FieldData fieldData in fieldDatas)
        {
            if (fieldData._isFalse)
            {
                reasons += $"{fieldData._fieldName} is incorrect! \n";
                incorrectInformation++;
            }
        }

        if(incorrectInformation > 0)
        {
            if (!isSuspicious)
            {
                reasons += "Suspicious person not tagged with the suspicious tag! \n";
            }
            else if(doppleganger)
            {
                reasons = string.Empty;
            }
        }else if(incorrectInformation == 0)
        {
            if (isSuspicious && !doppleganger)
            {
                reasons += "Incorrect usage of the suspicious tag! \n";
            }

            if (doppleganger)
            {
                reasons += "Possible doppleganger! \n";
            }  
        }
    }

    void giveCitation(string fullCitationText)
    {
        GameEvents.showModal?.Invoke("CITATION", fullCitationText, "OK", GameEvents.onCitationModalClosed, true);
        GameEvents.onCitationGiven?.Invoke();
    }
}
