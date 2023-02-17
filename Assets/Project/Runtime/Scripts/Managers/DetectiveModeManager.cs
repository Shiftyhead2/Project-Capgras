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

    

    public static bool inDetectiveMode { get; private set; } = false;
    // Start is called before the first frame update
    void Start()
    {
        fieldValues = new List<string>();
        fieldIDs = new List<int>();
        inDetectiveMode = false;
    }


    private void OnEnable()
    {
        GameEvents.onEnterDetectiveMode += setDetectiveMode;
        GameEvents.onUnselect += RemoveFields;
        GameEvents.onPassField += setUp;
        GameEvents.onNPCFullyChecked += ClearField;
    }

    private void OnDisable()
    {
        GameEvents.onEnterDetectiveMode -= setDetectiveMode;
        GameEvents.onUnselect -= RemoveFields;
        GameEvents.onPassField -= setUp;
        GameEvents.onNPCFullyChecked -= ClearField;
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
        if(fieldValues.Count != 2 && fieldIDs.Count != 2)
        {
            return;
        }

        if (fieldIDs[0] != fieldIDs[1])
        {
            GetData("DETECTIVE MODE", "No matching data found!", "OK!", GameEvents.onDetectiveModalClosed, false);
            return;
        }

        if (fieldValues[0] != fieldValues[1])
        {
            GetData("DETECTIVE MODE", "Discrepancy found!", "OK!", GameEvents.onDetectiveModalClosed, false);
            return;
        }

        GetData("DETECTIVE MODE", "No descrepancies found!", "OK!", GameEvents.onDetectiveModalClosed, false);
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


}
