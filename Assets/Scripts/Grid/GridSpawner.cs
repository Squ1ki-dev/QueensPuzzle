using UnityEngine;
using GridEnums;

public static class GridSpawner
{
    public static void SpawnGrid(GameObject prefabToInstantiate, GridConfig gridConfig, ref GameObject gridParent, ColorType[,] cellColors, CellState[,] cellStates)
    {
        ClearOldGrid(ref gridParent);

        if (prefabToInstantiate == null) return;

        gridParent = new GameObject("Grid");

        for (int y = 0; y < gridConfig.GridHeight; y++)
        {
            for (int x = 0; x < gridConfig.GridWidth; x++)
            {
                CellHandler.CreateGridCell(prefabToInstantiate, x, y, gridConfig, gridParent, cellColors, cellStates);
            }
        }
    }

    private static void ClearOldGrid(ref GameObject gridParent)
    {
        if (gridParent != null)
            Object.Destroy(gridParent);
    }
}