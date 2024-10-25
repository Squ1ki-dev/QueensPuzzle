using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using GridEnums;
using Tools;

public static class CellHandler
{
    public static void CreateGridCell(GameObject prefabToInstantiate, int x, int y, GridConfig gridConfig, GameObject gridParent, ColorType[,] cellColors, CellState[,] cellStates)
    {
        Vector2 position = new Vector2(x * gridConfig.CellSize.x, y * gridConfig.CellSize.y) + gridConfig.StartPosition;

        GameObject cellGO = Object.Instantiate(prefabToInstantiate, position, Quaternion.identity, gridParent.transform);
        cellGO.name = $"({x}, {y})";
        cellGO.transform.localScale = new Vector3(gridConfig.CellSize.x, gridConfig.CellSize.y, 1);

        ColorType cellColor = GetRandomColor();
        SetCellColor(cellGO, cellColor, gridConfig);
        cellColors[y, x] = cellColor;

        InitializeCellClickHandler(cellGO, x, y);
        cellStates[y, x] = CellState.Empty;
    }

    private static ColorType GetRandomColor() => (ColorType)Random.Range(1, 7);

    private static void SetCellColor(GameObject cellGO, ColorType colorType, GridConfig gridConfig)
    {
        Renderer renderer = cellGO.GetComponent<Renderer>();
        if (renderer != null)
            renderer.material.color = gridConfig.ColorMappings[(int)colorType];
    }

    private static void InitializeCellClickHandler(GameObject cellGO, int x, int y)
    {
        CellClickHandler clickHandler = cellGO.GetComponent<CellClickHandler>();
        //if (clickHandler != null)
            //clickHandler.Initialize(GridMap.Instance, x, y);
        //else
        //    Debug.LogError($"CellClickHandler component missing on {cellGO.name}");
    }
}