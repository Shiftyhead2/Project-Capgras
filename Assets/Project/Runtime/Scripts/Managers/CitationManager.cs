using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CitationManager : MonoBehaviour
{

    [SerializeField]
    [TextArea(10, 100)]
    private string reasons;

    [SerializeField]
    List<FieldCitationReason> fieldCitationReasons = new List<FieldCitationReason>();
    [SerializeField]
    private DoppleGangerCitation genericCitation;


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

        giveReason(fieldDatas, isSuspicious, doppleganger);

        if (reasons != string.Empty)
        {
            fullCitationtext += reasons;
            fullCitationtext += $"\n A fine of {GameManager.instance.fine} credits has been added to your account!";
            fullCitationtext += "\n Do not dissapoint us again!";
            giveCitation(fullCitationtext);
        }

    }

    void giveReason(List<FieldData> fieldDatas, bool isSuspicious, bool doppleganger)
    {

        int incorrectInformation = 0;
        FieldCitationReason _citation;


        foreach (FieldData data in fieldDatas)
        {
            _citation = fieldCitationReasons.First(field => field.fieldID == data.Id);

            if(_citation != null && _citation.CheckIfInvalid(data))
            {
                reasons += _citation.ReturnString();
                incorrectInformation++;
            }
        }

        if (incorrectInformation > 0)
        {
            if (!genericCitation.CheckIfSuspicous(isSuspicious))
            {
                reasons += "Suspicious person not tagged with the suspicious tag! \n";
            }
            else
            {
                reasons = string.Empty;
            }
        }
        else if (incorrectInformation == 0)
        {
            if (genericCitation.CheckIfSuspicous(isSuspicious) && !genericCitation.CheckIfDoppleGanger(doppleganger))
            {
                reasons += "Incorrect usage of the suspicious tag! \n";
            }

            if (genericCitation.CheckIfDoppleGanger(doppleganger))
            {
                reasons += "Possible doppleganger! \n";
            }
        }
    }

    void giveCitation(string fullCitationText)
    {
        GameEvents.showModal?.Invoke("CITATION", fullCitationText, "OK", GameEvents.onEnablePlayerInput, true);
        GameEvents.onCitationGiven?.Invoke();
        GameEvents.onDisablePlayerInput?.Invoke();
    }
}
