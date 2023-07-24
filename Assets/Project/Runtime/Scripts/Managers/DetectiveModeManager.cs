using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectiveModeManager : MonoBehaviour
{

    [SerializeField]
    private List<string> fieldValues;
    [SerializeField]
    private List<int> fieldIDs;

    string detectiveModeHeader;

    

    private bool inDetectiveMode  = false;
    // Start is called before the first frame update
    void Start()
    {
        fieldValues = new List<string>();
        fieldIDs = new List<int>();
        inDetectiveMode = false;
        detectiveModeHeader = ReturnString(LocatilazitionStrings.MODUL_DETECTIVE_MODE_HEADER_KEY);
    }


    private void OnEnable()
    {
        GameEvents.onEnterDetectiveMode += setDetectiveMode;
        GameEvents.onUnselect += RemoveFields;
        GameEvents.onPassField += setUp;
        GameEvents.onNPCFullyChecked += ClearField;
        GameEvents.onGetDetectiveMode += ReturnCurrentDetectiveModeStatus;
    }

    private void OnDisable()
    {
        GameEvents.onEnterDetectiveMode -= setDetectiveMode;
        GameEvents.onUnselect -= RemoveFields;
        GameEvents.onPassField -= setUp;
        GameEvents.onNPCFullyChecked -= ClearField;
        GameEvents.onGetDetectiveMode -= ReturnCurrentDetectiveModeStatus;
    }


    void setDetectiveMode()
    {
        inDetectiveMode = !inDetectiveMode;
        if (inDetectiveMode)
        {
            Debug.Log("We are in detective mode");
        }
        else
        {
            Debug.Log("We are not in detective mode");
            GameEvents.onExitDetectiveMode?.Invoke();
        }
    }

    void setUp(int ID, string value)
    {
        if(fieldIDs.Count != 2 && fieldValues.Count != 2)
        {
            fieldIDs.Add(ID);
            fieldValues.Add(value);
        }

        checkForDiscrepancy();
    }

    void checkForDiscrepancy()
    {

        detectiveModeHeader = ReturnString(LocatilazitionStrings.MODUL_DETECTIVE_MODE_HEADER_KEY);

        if (fieldValues.Count != 2 && fieldIDs.Count != 2)
        {
            return;
        }

        if (fieldIDs[0] != fieldIDs[1])
        {
            GetData(detectiveModeHeader, ReturnString(LocatilazitionStrings.DETECTIVE_NO_MATCH_KEY), "OK!", GameEvents.onDetectiveModalClosed, false);
            return;
        }

        if (fieldValues[0] != fieldValues[1])
        {
            GetData(detectiveModeHeader,ReturnString(LocatilazitionStrings.DETECTIVE_DISCREPANCY_KEY), "OK!", GameEvents.onDetectiveModalClosed, false);
            return;
        }

        GetData(detectiveModeHeader, ReturnString(LocatilazitionStrings.DETECTIVE_NO_DISCREPANCY_KEY), "OK!", GameEvents.onDetectiveModalClosed, false);
    }

    void RemoveFields(int ID,string value)
    {
        if(fieldIDs.Contains(ID) && fieldValues.Contains(value))
        {
            fieldIDs.Remove(ID);
            fieldValues.Remove(value);
        }
    }

    void ClearField()
    {
        inDetectiveMode = false;
        fieldIDs.Clear();
        fieldValues.Clear();
    }

    void GetData(string headerText,string bodyText,string confirmationText,Action action, bool hideMouse)
    {
        GameEvents.showModal?.Invoke(headerText, bodyText,confirmationText, action,hideMouse);
    }

    string ReturnString(string string_key, object[] args = null)
    {
        return GetLocalizedText(LocatilazitionStrings.DYNAMIC_UI_TABLE_NAME, string_key, args);
    }


    string GetLocalizedText(string table_key, string string_key, object[] args = null)
    {
        return LocalizationEventManager.GetLocalizedString(table_key,string_key, args);
    }

    bool ReturnCurrentDetectiveModeStatus()
    {
        return inDetectiveMode;
    }


}
