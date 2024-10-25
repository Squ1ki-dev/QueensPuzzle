using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using GridEnums;
using Array2DEditor;

public class GridMap : MonoBehaviour
{
    public static GridMap Instance { get; private set; }

    [SerializeField] private GameObject prefabToInstantiate = null;
    [SerializeField] private GridConfig gridConfig = null;

    private void Awake() => Instance = this;

    private void Start()
    {
        if (gridConfig.GridMap == null || prefabToInstantiate == null || gridConfig == null || gridConfig.WinCondition == null)
        {
            Debug.LogError("Fill in all the fields in order to start this example.");
            return;
        }

        SpawnGrid();
    }

    private void SpawnGrid()
    {
        var cells = gridConfig.GridMap.GetCells();
        var piece = new GameObject("Piece");

        for (var y = 0; y < gridConfig.GridHeight; y++)
        {
            for (var x = 0; x < gridConfig.GridWidth; x++)
            {
                Vector3 position = new Vector3(
                    gridConfig.StartPosition.x + x * gridConfig.CellSize.x,
                    gridConfig.StartPosition.y + y * gridConfig.CellSize.y,
                    0);

                var prefabGO = Instantiate(prefabToInstantiate, position, Quaternion.identity, piece.transform);
                prefabGO.name = $"({x}, {y})";
                prefabGO.transform.localScale = new Vector3(gridConfig.CellSize.x, gridConfig.CellSize.y, 0);

                int colorIndex = cells[y, x];
                ColorType colorType = (ColorType)colorIndex;

                if (colorIndex >= 0 && colorIndex < gridConfig.ColorMappings.Length)
                {
                    var renderer = prefabGO.GetComponent<Renderer>();
                    if (renderer != null)
                        renderer.material.color = gridConfig.ColorMappings[(int)colorType];
                    else
                        Debug.LogWarning($"Prefab {prefabGO.name} does not have a Renderer component.");
                }
                else
                    Debug.LogWarning($"Invalid color index {colorIndex} at ({x}, {y}).");

                // Attach CellClickHandler to each instantiated prefab
                var clickHandler = prefabGO.AddComponent<CellClickHandler>();
                clickHandler.Initialize(this, x, y, colorType); // Pass colorType to the handler
            }
        }
    }
}