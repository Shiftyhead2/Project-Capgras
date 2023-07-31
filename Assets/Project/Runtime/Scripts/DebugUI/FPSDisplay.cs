using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    public TextMeshProUGUI fpsText;

    private float pollingTime = 0.2f; // Corrected variable name

    private float time;
    private int frameCount;


    private void Start()
    {
#if !UNITY_EDITOR
        gameObject.SetActive(true);
        enabled = true;
#else
        gameObject.SetActive(false);
        enabled = false;
#endif
    }


    // Update is called once per frame
    void Update()
    {
#if !UNITY_EDITOR
        time += Time.unscaledDeltaTime;

        frameCount++;

        if (time >= pollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            fpsText.text = $"{frameRate} FPS";

            time -= pollingTime;
            frameCount = 0;
        }
#endif
    }
}
