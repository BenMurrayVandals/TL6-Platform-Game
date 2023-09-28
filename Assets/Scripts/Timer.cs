using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float time = 0;
    public Text timerText;

    // Update is called once per frame
    void Update()
    {
        if (time >= 0)
        {
            time += Time.deltaTime;
        }
        else
        {
            time = 0;
        }

        DisplayTime();
    }

    void DisplayTime()
    {
        if (time < 0)
        {
            time = 0;
        }

        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
