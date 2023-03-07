using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFlagToggles : MonoBehaviour
{
    private Toggle flagToggle;

    private void Awake()
    {
        flagToggle = GetComponentInChildren<Toggle>();
    }

    private void OnEnable()
    {
        GameEvents.onAIWaypointReached += resetFlag;
    }

    private void OnDisable()
    {
        GameEvents.onAIWaypointReached -= resetFlag;
    }


    public void onFlag(bool toggle)
    {
        GameEvents.onSuspiciousFlag?.Invoke(flagToggle.isOn);
    }

    private void resetFlag()
    {
        flagToggle.isOn = false;
    }
}
