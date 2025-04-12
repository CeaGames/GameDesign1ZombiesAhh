using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlowField
{
    public Cell[,] grid { get; private set; }
    public Vector2Int gridsize { get; private set; }
    public float cellRadius { get; private set; }

    private float cellDiameter;

    public FlowField(float _cellRadius, Vector2Int _gridSize)
    {
        cellRadius = _cellRadius;
        cellDiameter = cellRadius * 2f;
        gridsize = _gridSize;
    }

    public void CreateGrid()
    {
        grid = new Cell[gridsize.x, gridsize.y];

        for(int x = 0; x < gridsize.x; x++) 
        {
            for (int y = 0; y < gridsize.y; y++)
            {
                Vector3 worldPos = new Vector3(cellDiameter * x + cellRadius, 0, cellRadius * y + cellRadius);
                grid[x, y] = new Cell(worldPos, new Vector2Int(x, y));
            }
        }
    }
}