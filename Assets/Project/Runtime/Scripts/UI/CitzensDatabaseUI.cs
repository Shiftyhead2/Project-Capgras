using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;

public class CitzensDatabaseUI : MonoBehaviour
{
    [SerializeField] private GameObject searchPanel;
    [SerializeField] private GameObject searchingPanel;
    [SerializeField] private GameObject statusPanel;

    [SerializeField] private UIPanelEnabler panelEnabler;
    [SerializeField] private Image fillImage;

    [SerializeField] private TextMeshProUGUI searchNameText;
    [SerializeField] private TextMeshProUGUI citizenNameText;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private TextMeshProUGUI daysSinceUpdateText;

    [SerializeField] private float minWaitTime = 1f;
    [SerializeField] private float maxWaitTime = 5f;

    private string citizensName;
    private int daysSinceLastUpdate;


    private UniTask searchTask;
    private CancellationTokenSource searchCancellation;

    private StatusScriptableObject referencedStatus;

    private void OnEnable()
    {
        GameEvents.onProcessPerson += GetCitizensName;
        GameEvents.onCallPerson += SetDefault;

        disableUI();
    }

    private void OnDisable()
    {
        GameEvents.onProcessPerson -= GetCitizensName;
        GameEvents.onCallPerson -= SetDefault;
    }


    public void disableUI()
    {
        if (searchCancellation != null && !searchCancellation.Token.IsCancellationRequested)
        {
            searchCancellation.Cancel();
        }




        panelEnabler.DisableThisUI();

        referencedStatus = null;
        daysSinceLastUpdate = 0;

        StopAllCoroutines();
    }

    public void enableUI()
    {
        panelEnabler.EnableThisUI();

        transform.SetAsLastSibling();
    }

    void SetDefault()
    {
        searchPanel.SetActive(true);
        searchingPanel.SetActive(false);
        statusPanel.SetActive(false);
    }

    void GetCitizensName(bool isProcessingPerson)
    {
        if (isProcessingPerson)
        {
            citizensName = GameEvents.onGetName?.Invoke();
            SetUpCitizenNameTexts();
        }
    }


    public async void SearchCitizen()
    {
        if(citizensName != string.Empty)
        {
            if (searchCancellation != null && !searchCancellation.Token.IsCancellationRequested)
            {
                searchCancellation.Cancel();
            }


            searchCancellation = new CancellationTokenSource();
            searchTask = SearchCitizenCouritine(searchCancellation.Token);
            await searchTask;
        }
    }

    private async UniTask SearchCitizenCouritine(CancellationToken cancellationToken)
    {
        fillImage.fillAmount = 0f;
        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        searchPanel.SetActive(false);
        searchingPanel.SetActive(true);
        float elapsedTime = 0f;
        while (elapsedTime < waitTime)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                searchPanel.SetActive(true);
                searchingPanel.SetActive(false);
                statusPanel.SetActive(false);
                return;
            }

            fillImage.fillAmount = elapsedTime / waitTime;
            await UniTask.DelayFrame(1);
            elapsedTime += Time.deltaTime;
        }

        searchingPanel.SetActive(false);
        statusPanel.SetActive(true);
        referencedStatus = GameEvents.onSearchFinished?.Invoke();
        daysSinceLastUpdate = (int)GameEvents.onGetDaysSinceUpdate?.Invoke();
        SetUpStatusText();
    }

    void SetUpStatusText()
    {
        if (referencedStatus != null)
        {
            statusText.text = $"{ReturnString(LocatilazitionStrings.DATABASE_STATUS_TEXT_KEY, new object[] {referencedStatus.returnStatus()})}";
        }

        daysSinceUpdateText.text = $"{ReturnString(LocatilazitionStrings.DATABASE_DAYS_SINCE_UPDATE_KEY, new object[] { daysSinceLastUpdate })}";
    }

    void SetUpCitizenNameTexts()
    {
        searchNameText.text = $"{citizensName}";
        citizenNameText.text = $"{ReturnString(LocatilazitionStrings.DATABASE_CIVILIAN_TEXT_KEY, new object[] { citizensName })}";
    }


    string ReturnString(string string_key, object[] args = null)
    {
        return GetLocalizedText(LocatilazitionStrings.DYNAMIC_UI_TABLE_NAME, string_key, args);
    }

    string GetLocalizedText(string table_key, string string_key, object[] args = null)
    {
        return LocalizationEventManager.GetLocalizedString(table_key, string_key, args);
    }

}

