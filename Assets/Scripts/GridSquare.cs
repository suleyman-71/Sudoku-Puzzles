using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static GameEvents;

public class GridSquare : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler
{
    public GameObject number_text;
    private int _number = 0;
    private int _correct_number = 0;
    private bool _selected = false;
    private int _square_index = -1;
    public bool IsSelected() { return _selected; }
    public void SetSquareIndex(int index)
    { 
        _square_index = index; 
    }

    public void SetCorrectNumber(int number) 
    { 
        _correct_number = number; 
    }
    void Start()
    {
        _selected = false;
    }

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

    public void OnPointerClick(PointerEventData eventData)
    {
        _selected= true;
        GameEvents.SquareSelectedMethod(_square_index);
    }
    public void OnSubmit(BaseEventData eventData)
    {

    }

    private void OnEnable()
    {
        GameEvents.OnUpdateSquareNumber += OnSetNumber;
        GameEvents.OnSquareSelected += OnSquareSelected;
    }

    private void OnDisable()
    {
        GameEvents.OnUpdateSquareNumber -= OnSetNumber;
        GameEvents.OnSquareSelected += OnSquareSelected;
    }

    public void OnSetNumber(int number)
    {
        if (_selected)
        {
            SetNumber(number);

            if (_number != _correct_number)
            {
                var colors = this.colors;
                colors.normalColor = Color.red;
                this.colors = colors;

                GameEvents.OnWrongNumberMethod();
            }
            else
            {
                var colors = this.colors;
                colors.normalColor = Color.white;
                this.colors = colors;
            }
        }
    }

    public void OnSquareSelected(int square_index)
    {
        if (_square_index != square_index)
        {
            _selected = false;
        }
    }
}
