using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using GridEnums;
using Tools;
using Array2DEditor;

public class GridHandler : MonoBehaviour
{
    public static GridHandler Instance { get; private set; }

    private static readonly int[] GridSizes = { 7, 8, 9 }; // Levels 1-5, 6-10, 11-15, and 16+
    private const int MinLevel = 1;
    private const int MaxLevel = 15;
    private const int MaxGridSize = 10;


    private CellState[,] cellStates;
    private ColorType[,] cellColors;
    private Array2DInt array2DInt;
    private HashSet<ColorType> colorRegionsWithQueens;
    
    [SerializeField] private GameObject prefabToInstantiate;
    [SerializeField] private GridConfig gridConfig;
    private GameObject gridParent;

    private void Awake() => Instance = this;

    private void Start()
    {
        InitializeGrid();
        RespawnGrid();
    }

    private void InitializeGrid()
    {
        int currentLevel = GameSaves.Instance.LoadCurrentLevel("Level");
        int gridSize = GetGridSizeByLevel(currentLevel);

        gridConfig.GridHeight = gridSize;
        gridConfig.GridWidth = gridSize;

        cellStates = new CellState[gridConfig.GridHeight, gridConfig.GridWidth];
        cellColors = new ColorType[gridConfig.GridHeight, gridConfig.GridWidth];
        colorRegionsWithQueens = new HashSet<ColorType>();
    }

    private int GetGridSizeByLevel(int level)
    {
        if (level < MinLevel)
        {
            gridConfig.CellSize = new Vector2(0.5f, 0.5f);
            gridConfig.StartPosition = new Vector2(-1.5f, -2f);
            return GridSizes[0];
        }
        if (level > MaxLevel)
        {
            gridConfig.CellSize = new Vector2(0.38f, 0.38f);
            gridConfig.StartPosition = new Vector2(-1.5f, -2f);
            return GridSizes[MaxGridSize - 1];
        }

        gridConfig.CellSize = new Vector2(0.4f, 0.4f);
        gridConfig.StartPosition = new Vector2(-1.4f, -2f);
        return GridSizes[(level - 1) / 5];
    }


    public bool CanPlaceStar(int x, int y)
    {
        if (!GridValidation.IsValidStarPlacement(x, y, colorRegionsWithQueens, cellColors))
            return false;

        return !GridValidation.IsStarInRowOrColumn(x, y, gridConfig, cellStates) && !GridValidation.IsStarOnDiagonals(x, y, cellStates, gridConfig);
    }

    public bool PlaceStar(int x, int y)
    {
        if (!CanPlaceStar(x, y)) return false;

        if(CheckWinCondition())
            WindowManager.Instance.Show<EndScreen>().Show();

        cellStates[y, x] = CellState.Star;
        colorRegionsWithQueens.Add(cellColors[y, x]);

        return true;
    }

    public bool PlaceX(int x, int y)
    {
        if (cellStates[y, x] == CellState.Empty)
        {
            cellStates[y, x] = CellState.X;
            return true;
        }
        return false;
    }

    public bool CheckWinCondition() => colorRegionsWithQueens.Count == 7;

    public void RespawnGrid()
    {
        InitializeGrid();
        GridSpawner.SpawnGrid(prefabToInstantiate, gridConfig, ref gridParent, cellColors, cellStates);
    }
}