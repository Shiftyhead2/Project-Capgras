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
    [SerializeField]
    private StatusCitationReason statusCitation;
    [SerializeField]
    private DaysSinceUpdateCitationReason daysSinceUpdateCitationReason;


    private void OnEnable()
    {
        GameEvents.onProcessFieldData += checkFieldList;
    }

    private void OnDisable()
    {
        GameEvents.onProcessFieldData -= checkFieldList;
    }


    private void checkFieldList(List<FieldData> fieldDatas, bool isSuspicious, bool doppleganger, int daysSinceUpdate, StatusScriptableObject currentStatus)
    {
        reasons = string.Empty;
        string fullCitationtext = $"{GetLocalizedString(LocatilazitionStrings.DYNAMIC_UI_TABLE_NAME,LocatilazitionStrings.CITATION_HEADER_KEY)} \n \n";

        giveReason(fieldDatas, isSuspicious, doppleganger, daysSinceUpdate,currentStatus);

        if (reasons != string.Empty)
        {
            fullCitationtext += reasons;
            fullCitationtext += $"\n {GetLocalizedString(LocatilazitionStrings.DYNAMIC_UI_TABLE_NAME,LocatilazitionStrings.FINE_CITATION_KEY, new object[] {GameManager.instance.fine})} \n";
            giveCitation(fullCitationtext);
        }

    }

    void giveReason(List<FieldData> fieldDatas, bool isSuspicious, bool doppleganger, int daysSinceUpdate, StatusScriptableObject currentStatus)
    {

        int incorrectInformation = 0;
        FieldCitationReason _citation;

        if (statusCitation.CheckStatus(currentStatus))
        {
            reasons += $"{GetLocalizedString(LocatilazitionStrings.DYNAMIC_UI_TABLE_NAME, LocatilazitionStrings.CITATION_REASON_STATUS_KEY, new object[] {currentStatus.returnStatus()})} \n";
        }

        if (daysSinceUpdateCitationReason.CheckDays(daysSinceUpdate))
        {
            reasons += daysSinceUpdateCitationReason.ReturnString();
        }


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
                reasons += $"{GetLocalizedString(LocatilazitionStrings.DYNAMIC_UI_TABLE_NAME, LocatilazitionStrings.CITATION_REASON_SUSPICIOUS_KEY)} \n";
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
                reasons += $"{GetLocalizedString(LocatilazitionStrings.DYNAMIC_UI_TABLE_NAME, LocatilazitionStrings.CITATION_REASON_INCORRECT_USAGE_KEY)} \n";
            }

            if (genericCitation.CheckIfDoppleGanger(doppleganger))
            {
                reasons += $"{GetLocalizedString(LocatilazitionStrings.DYNAMIC_UI_TABLE_NAME,LocatilazitionStrings.CITATION_REASON_DOPPELGANGER_KEY)} \n";
            }
        }

        
    }

    void giveCitation(string fullCitationText)
    {
        GameEvents.showModal?.Invoke("CITATION", fullCitationText, "OK", GameEvents.onEnablePlayerInput, true);
        GameEvents.onCitationGiven?.Invoke();
        GameEvents.onDisablePlayerInput?.Invoke();
    }

    string GetLocalizedString(string table_key, string string_key, object[] args = null)
    {
        return LocalizationEventManager.GetLocalizedString(table_key, string_key, args);
    }
}
