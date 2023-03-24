using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEvents;

public class SudokuGrid : MonoBehaviour
{
    public int columns = 0;
    public int rows = 0;
    public float squareOffset = 0.0f;
    public float squareScale = 1.0f;
    public float squareGap = 0.1f;
    public Color lineHighlightColor = Color.red;
    public Vector2 startPosition = new Vector2(0.0f, 0.0f);
    public GameObject gridSquare;

    private List<GameObject> _gridSquares = new List<GameObject>();
    private int _selectedGridData = -1;
    void Start()
    {
        if (gridSquare.GetComponent<GridSquare>() == null)
        {
            Debug.LogError("This GameObject need to have GridSquare script attached !");
        }

        CreateGrid();
        SetGridNumber(GameSettings.Instance.GetGameMode());
    }
    
    void Update()
    {
        
    }

    private void CreateGrid()
    {
        SpawnGridSquares();
        SetSquarePosition();
    }

    private void SetSquarePosition()
    {
        var squareRect = _gridSquares[0].GetComponent<RectTransform>();
        Vector2 offset = new Vector2();
        Vector2 squareGapNumber = new Vector2(0.0f, 0.0f);
        bool rowMoved = false;
        offset.x = squareRect.rect.width * squareRect.transform.localScale.x + squareOffset;
        offset.y = squareRect.rect.height * squareRect.transform.localScale.y + squareOffset;

        int columnNumber = 0;
        int rowNumber = 0;

        foreach (GameObject square in _gridSquares)    
        {
            if (columnNumber +1 > columns)
            {
                rowNumber++;
                columnNumber = 0;
                squareGapNumber.x = 0;
                rowMoved = false;
            }

            var posXOffset = offset.x * columnNumber + (squareGapNumber.x * squareGap);
            var posYOffset = offset.y * rowNumber + (squareGapNumber.y * squareGap);

            if (columnNumber > 0 && columnNumber % 3 == 0)
            {
                squareGapNumber.x++;
                posXOffset += squareGap;
            }

            if (rowNumber > 0 && rowNumber % 3 == 0 && rowMoved == false)
            {
                rowMoved = true;
                squareGapNumber.y++;
                posYOffset += squareGap;
            }

            square.GetComponent<RectTransform>().anchoredPosition = new Vector2(startPosition.x + posXOffset, startPosition.y - posYOffset);
            columnNumber++;
        }
    }

    private void SpawnGridSquares()
    {
        int square_index = 0;
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                _gridSquares.Add(Instantiate(gridSquare) as GameObject);
                _gridSquares[_gridSquares.Count - 1].GetComponent<GridSquare>().SetSquareIndex(square_index);
                //_grid_squares[_grid_squares.Count - 1].transform.parent = this.transform; //instantiate this game object as a child of the object holding this script.
                _gridSquares[_gridSquares.Count - 1].transform.SetParent(this.transform); //instantiate this game object as a child of the object holding this script.
                _gridSquares[_gridSquares.Count - 1].transform.localScale = new Vector3(squareScale, squareScale, squareScale);
            }
        }
    }

    private void SetGridNumber(string level)
    {
        _selectedGridData = UnityEngine.Random.Range(0, SudokuData.Instance.sudoku_game[level].Count);
        var data = SudokuData.Instance.sudoku_game[level][_selectedGridData];

        setGridSquareData(data);
        //foreach (var square in grid_squares_)
        //{
        //    square.GetComponent<GridSquare>().SetNumber(2);
        //}
    }

    private void setGridSquareData(SudokuData.SudokuBoardData data)
    {
        for (int index = 0; index < _gridSquares.Count; index++)
        {
            _gridSquares[index].GetComponent<GridSquare>().SetNumber(data.unsolved_data[index]);
            _gridSquares[index].GetComponent<GridSquare>().SetCorrectNumber(data.solved_data[index]);
            _gridSquares[index].GetComponent<GridSquare>().SetHasDefaultValue(data.unsolved_data[index] != 0 && data.unsolved_data[index] == data.solved_data[index]);
        }
    }

    private void OnEnable()
    {
        GameEvents.OnSquareSelected += OnSquareSelected;
    }
    private void OnDisable()
    {
        GameEvents.OnSquareSelected -= OnSquareSelected;
    }

    private void SetSquaresColor(int[] data, Color col)
    {
        foreach (var index in data)
        {
            var comp = _gridSquares[index].GetComponent<GridSquare>();
            if (comp.IsSelected() == false)
            {
                comp.SetSquareColor(col);
            }
        }
    }

    public void OnSquareSelected(int squareIndex)
    {
        var horizontalLine = LineIndicator.instance.GetHorizontalLine(squareIndex);
        var verticalLine = LineIndicator.instance.GetVerticalLine(squareIndex);
        var square = LineIndicator.instance.GetSquare(squareIndex);
        
        SetSquaresColor(LineIndicator.instance.GetAllSquareIndexes(), Color.white);

        SetSquaresColor(horizontalLine, lineHighlightColor);
        SetSquaresColor(verticalLine, lineHighlightColor);
        SetSquaresColor(square, lineHighlightColor);

    }
}
