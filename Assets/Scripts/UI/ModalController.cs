using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModalController : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private TextMeshProUGUI headerText;
    [SerializeField]
    private TextMeshProUGUI bodyText;
    [SerializeField]
    private TextMeshProUGUI buttonText;

    private void OnEnable()
    {
        GameEvents.showModal += ShowModal;
    }

    private void OnDisable()
    {
        GameEvents.showModal -= ShowModal;
    }

    // Start is called before the first frame update
    void Start()
    {
        CloseModal();
    }

    void ShowModal(string header,string body,string textConfirm)
    {
        transform.SetAsLastSibling();
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        headerText.text = header;
        bodyText.text = body;
        buttonText.text = textConfirm;
    }


    public void CloseModal()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        GameEvents.onModalClosed?.Invoke();
    }
}
