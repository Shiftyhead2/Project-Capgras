using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Status Object", menuName = "Data/New Status/Status Object",order = 0)]
public class StatusScriptableObject : ScriptableObject
{
    public string statusText;
    public Color statusColor;

    public string returnStatus()
    {
        return $"<color=#{ColorUtility.ToHtmlStringRGBA(statusColor)}>{statusText}</color>";
    }
}
