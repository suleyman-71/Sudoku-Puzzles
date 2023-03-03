using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    public List<GameObject> error_images;
    int _lives = 0;
    int _error_number = 0;
    void Start()
    {
        _lives = error_images.Count;
        _error_number = 0;
    }

    
    private void WrongNumber()
    {
        if (_error_number < error_images.Count)
        {
            error_images[_error_number].SetActive(true);
            _error_number++;
            _lives--;
        }
    }

    private void OnEnable()
    {
        GameEvents.OnWrongNumber += WrongNumber;
    }

    private void OnDisable()
    {
        GameEvents.OnWrongNumber -= WrongNumber;
    }
}
