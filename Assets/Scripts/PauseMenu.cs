using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public void DisplayTime()
    {
        timeText.text = Clock.instance.GetCurrentTimeText().text;
    }
}
