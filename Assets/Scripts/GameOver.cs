using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI textClock;
    void Start()
    {
        textClock.text = Clock.instance.GetCurrentTimeText().text;
    }
}
