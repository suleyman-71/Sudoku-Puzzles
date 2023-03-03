using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SudokuGrid : MonoBehaviour
{
    public int columns = 0;
    public int rows = 0;
    public float square_offset = 0.0f;
    public float square_scale = 1.0f;
    public Vector2 start_position = new Vector2(0.0f, 0.0f);
    public GameObject grid_square;

    private List<GameObject> _grid_squares = new List<GameObject>();
    private int _selected_grid_data = -1;
    void Start()
    {
        if (grid_square.GetComponent<GridSquare>() == null)
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
        var square_rect = _grid_squares[0].GetComponent<RectTransform>();
        Vector2 offset = new Vector2();
        offset.x = square_rect.rect.width * square_rect.transform.localScale.x + square_offset;
        offset.y = square_rect.rect.height * square_rect.transform.localScale.y + square_offset;

        int column_number = 0;
        int row_number = 0;

        foreach (GameObject square in _grid_squares)    
        {
            if (column_number +1 > columns)
            {
                row_number++;
                column_number = 0;
            }

            var pos_x_offset = offset.x * column_number;
            var pos_y_offset = offset.y * row_number;
            square.GetComponent<RectTransform>().anchoredPosition = new Vector2(start_position.x + pos_x_offset, start_position.y - pos_y_offset);
            column_number++;
        }
    }

    private void SpawnGridSquares()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                _grid_squares.Add(Instantiate(grid_square) as GameObject);
                //_grid_squares[_grid_squares.Count - 1].transform.parent = this.transform; //instantiate this game object as a child of the object holding this script.
                _grid_squares[_grid_squares.Count - 1].transform.SetParent(this.transform); //instantiate this game object as a child of the object holding this script.
                _grid_squares[_grid_squares.Count - 1].transform.localScale = new Vector3(square_scale, square_scale, square_scale);
            }
        }
    }

    private void SetGridNumber(string level)
    {
        _selected_grid_data = UnityEngine.Random.Range(0, SudokuData.Instance.sudoku_game[level].Count);
        var data = SudokuData.Instance.sudoku_game[level][_selected_grid_data];

        setGridSquareData(data);
        //foreach (var square in grid_squares_)
        //{
        //    square.GetComponent<GridSquare>().SetNumber(2);
        //}
    }

    private void setGridSquareData(SudokuData.SudokuBoardData data)
    {
        for (int index = 0; index < _grid_squares.Count; index++)
        {
            _grid_squares[index].GetComponent<GridSquare>().SetNumber(data.unsolved_data[index]);
        }
    }
}
