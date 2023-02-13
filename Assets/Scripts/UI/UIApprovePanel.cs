using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIApprovePanel : MonoBehaviour
{
    public void OnTriggered(bool approved)
    {
        GameEvents.onNPCDocumentsChecked?.Invoke(approved);
        GameEvents.onNPCFullyChecked?.Invoke();
    }
}
