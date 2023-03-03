using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridSquare : Selectable
{
    public GameObject number_text;
    private int _number = 0;
    

    
    void Update()
    {
        
    }

    public void DisplayText()
    {
        if (_number <= 0)
            number_text.GetComponent<TextMeshProUGUI>().text = " ";
        else
            number_text.GetComponent<TextMeshProUGUI>().text = _number.ToString();
    }

    public void SetNumber(int number)
    {
        _number = number;
        DisplayText();
    }
}
