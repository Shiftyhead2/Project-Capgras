using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIUpdateText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        HideText();
    }

    private void OnEnable()
    {
        GameEvents.onUpdateText += UpdateText;
        GameEvents.onComputerInteraction += ShowText;
        GameEvents.onNPCFullyChecked += HideText;
    }

    private void OnDisable()
    {
        GameEvents.onUpdateText -= UpdateText;
        GameEvents.onComputerInteraction -= ShowText;
        GameEvents.onNPCFullyChecked -= HideText;
    }

    void UpdateText(int i)
    {
        text.text = i.ToString();
    }

    void HideText()
    {
        text.color = new Color(text.color.r,text.color.g,text.color.b,0f);
    }

    void ShowText()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
    }
}
