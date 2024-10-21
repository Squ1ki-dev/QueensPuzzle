using System.Collections.Generic;
using UnityEngine;
using Tools;

public class GridCreation : MonoBehaviour
{
    public static GridCreation Instance { get; private set;}
    private CellState[,] cellStates;
    private ColorType[,] cellColors; // To store colors for each cell
    private HashSet<ColorType> colorRegionsWithStars; // To track colors with placed stars

    public enum ColorType
    {
        None = 0, // No color (or default color)
        Green = 1,
        Red = 2,
        Blue = 3,
        Yellow = 4,
        Pink = 5,
        Turquoise = 6,
        BrightRed = 7,
    }

    public enum CellState
    {
        Empty,
        X,
        Star
    }

    [SerializeField] private GameObject prefabToInstantiate;
    [SerializeField] private GridConfig gridConfig;
    private GameObject gridParent;
    protected LevelModel model;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cellStates = new CellState[gridConfig.GridHeight, gridConfig.GridWidth];
        cellColors = new ColorType[gridConfig.GridHeight, gridConfig.GridWidth]; // Initialize colors array
        colorRegionsWithStars = new HashSet<ColorType>(); // Initialize the color regions set
        SpawnGrid();
    }

    private void SpawnGrid()
    {
        if (gridParent != null)
            Destroy(gridParent);

        if (prefabToInstantiate == null)
            return;

        // Create a new game object to hold all grid pieces
        gridParent = new GameObject("Piece");

        for (var y = 0; y < gridConfig.GridHeight; y++)
        {
            for (var x = 0; x < gridConfig.GridWidth; x++)
            {
                Vector2 position = new Vector2(x * gridConfig.CellSize.x, y * gridConfig.CellSize.y) + gridConfig.StartPosition;

                var prefabGO = Instantiate(prefabToInstantiate, position, Quaternion.identity, gridParent.transform);
                prefabGO.name = $"({x}, {y})";

                prefabGO.transform.localScale = new Vector3(gridConfig.CellSize.x, gridConfig.CellSize.y, 1);

                int colorIndex = Random.Range(1, 7);
                ColorType colorType = (ColorType)colorIndex;

                Renderer renderer = prefabGO.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = gridConfig.ColorMappings[(int)colorType];
                }

                // Store the color in the cellColors array
                cellColors[y, x] = colorType;

                CellClickHandler clickHandler = prefabGO.GetComponent<CellClickHandler>();
                if (clickHandler != null)
                    clickHandler.Initialize(this, x, y);
                else
                    Debug.LogError($"CellClickHandler component missing on {prefabGO.name}");

                // Set initial state of the cell
                cellStates[y, x] = CellState.Empty;
            }
        }
    }

    public bool CanPlaceStar(int x, int y)
    {
        // Check if a star can be placed in the cell
        ColorType colorType = cellColors[y, x]; // Get the color of the cell

        // Check if there's already a star in the same color region
        if (colorRegionsWithStars.Contains(colorType))
        {
            MessageHandler.Instance.UpdateText($"A Queen is already placed in the {colorType} region.", Color.red);
            return false;
        }

        for (int i = 0; i < gridConfig.GridWidth; i++)
        {
            // Check row
            if (cellStates[y, i] == CellState.Star)
            {
                MessageHandler.Instance.UpdateText("Each row can only have one Queen", Color.red);
                return false;
            }
            else
                MessageHandler.Instance.messageText.gameObject.SetActive(false);
        }

        for (int i = 0; i < gridConfig.GridHeight; i++)
        {
            // Check column
            if (cellStates[i, x] == CellState.Star)
            {
                MessageHandler.Instance.UpdateText("Each column can also only have one Queen", Color.red);
                return false;
            }
            else
                MessageHandler.Instance.messageText.gameObject.SetActive(false);
        }

        // Check diagonals
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (Mathf.Abs(i) != Mathf.Abs(j)) continue; // Only check diagonal
                int newX = x + j;
                int newY = y + i;
                if (newX >= 0 && newX < gridConfig.GridWidth && newY >= 0 && newY < gridConfig.GridHeight)
                {
                    if (cellStates[newY, newX] == CellState.Star) 
                    {
                        MessageHandler.Instance.UpdateText("Two Queens cannot touch each other, not even diagonally.", Color.red);
                        return false; // Check diagonals
                    }
                    else
                        MessageHandler.Instance.messageText.gameObject.SetActive(false);
                }
            }
        }
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

    public bool PlaceStar(int x, int y)
    {
        if (CanPlaceStar(x, y))
        {
            cellStates[y, x] = CellState.Star;
            ColorType colorType = cellColors[y, x];
            colorRegionsWithStars.Add(colorType);
            
            if (CheckWinCondition())
            {
                WindowManager.Instance.Show<EndScreen>().Show(model);
            }
            return true;
        }
        return false;
    }

    private bool CheckWinCondition()
    {
        return colorRegionsWithStars.Count == 7; // Assuming there are 5 color regions
    }

    public void RespawnGrid()
    {
        for (int y = 0; y < gridConfig.GridHeight; y++)
        {
            for (int x = 0; x < gridConfig.GridWidth; x++)
            {
                cellStates[y, x] = CellState.Empty;
            }
        }
        colorRegionsWithStars.Clear();
        SpawnGrid();
    }
}