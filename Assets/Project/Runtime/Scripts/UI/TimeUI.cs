using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        HideText();
    }

    private void OnEnable()
    {
        GameEvents.onTimePassed += UpdateTimeUI;
        GameEvents.onComputerInteraction += ShowText;
        GameEvents.onNPCFullyChecked += HideText;
    }

    private void OnDisable()
    {
        GameEvents.onTimePassed -= UpdateTimeUI;
        GameEvents.onComputerInteraction -= ShowText;
        GameEvents.onNPCFullyChecked -= HideText;
    }

    void HideText()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
    }

    void ShowText()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
    }

    void UpdateTimeUI(int hour, int minute)
    {
        text.text = $"{hour:00}:{minute:00}";
    }
}
