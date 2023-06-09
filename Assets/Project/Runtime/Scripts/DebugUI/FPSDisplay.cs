using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    public TextMeshProUGUI fpsText;

    private float poolingTime = 0.5f;
    private float time;
    private int frameCount;

    // Update is called once per frame
    void Update()
    {
        time += Time.unscaledDeltaTime;

        frameCount++;

        if(time >= poolingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            fpsText.text = $"{frameRate} FPS";

            time -= poolingTime;
            frameCount = 0;
        } 
    }
}
