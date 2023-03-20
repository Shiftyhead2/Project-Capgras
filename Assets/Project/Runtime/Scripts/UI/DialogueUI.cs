using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public TextMeshProUGUI dialogueText;
    public Button[] buttons;
    public List<ChoiceButton> buttonScripts;
    private int bribeAmount = 0;

    private void Start()
    {
        foreach(var button in buttons)
        {
            buttonScripts.Add(button.GetComponent<ChoiceButton>());
        }
        HideUI();
    }


    private void OnEnable()
    {
        GameEvents.onNPCSituation += SetUpDialogue;
        GameEvents.onMicrophoneInteracted += ShowUI;
        GameEvents.onSituationResolved += HideUI;
        GameEvents.onChoiceButtonClicked += ChoiceMade;
    }

    private void OnDisable()
    {
        GameEvents.onNPCSituation -= SetUpDialogue;
        GameEvents.onMicrophoneInteracted -= ShowUI;
        GameEvents.onSituationResolved -= HideUI;
        GameEvents.onChoiceButtonClicked -= ChoiceMade;
    }


    void SetUpDialogue(SituationObject situationObject)
    {
        DialogueText dialogue = situationObject.dialogueTexts[Random.Range(0, situationObject.dialogueTexts.Length - 1)];
        switch (dialogue.type)
        {
            case DialogueType.BRIBE:
                bribeAmount = Random.Range(dialogue.minBribeAmount, dialogue.maxBribeAmount);
                break;
            default:
                bribeAmount = 0;
                break;
        }
        
        dialogueText.text = dialogue.SetUpText(bribeAmount);

        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }

        for(int i = 0; i < situationObject.dialogueChoices.Length; i++)
        {
            buttons[i].gameObject.SetActive(true);
            buttonScripts[i].SetUpText(situationObject.dialogueChoices[i].choiceText, situationObject.dialogueChoices[i].ID);
        }
    }

    void HideUI()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        GameEvents.onHideMouse?.Invoke();
        GameEvents.onEnablePlayerInput?.Invoke();
    }

    void ShowUI()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        GameEvents.onShowMouse?.Invoke();
        GameEvents.onDisablePlayerInput?.Invoke();
    }

    public void ChoiceMade(int choice)
    {
        GameEvents.onSituationChoice?.Invoke(choice);
    }
}
