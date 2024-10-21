using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Array2DEditor;

// Class for handling cell clicks
public class CellClickHandler : MonoBehaviour
{
    private GridCreation grid;
    [SerializeField] private List<Transform> m_marker = new List<Transform>();

    private int m_tapNum = 0;
    private int x, y;

    public void Initialize(GridCreation grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public void Start()
    {
        m_tapNum = 0;
        ClearMarkers();
    }

    private void OnMouseDown()
    {
        OnTileTapped();
        Debug.Log($"Cell tapped at: ({x}, {y})");
    }

    public void ClearMarkers()
    {
        foreach (var marker in m_marker)
        {
            marker.gameObject.SetActive(false);
        }
    }

    public void ShowMarker()
    {
        ClearMarkers();
        m_marker[m_tapNum].gameObject.SetActive(true);
    }

    public void OnTileTapped()
    {
        if (grid == null)
        {
            Debug.LogError("Grid is not initialized!");
            return;
        }

        if (m_marker == null)
        {
            Debug.LogError("m_marker is not initialized!");
            return;
        }

        if (m_tapNum == 0) // First tap: place X
        {
            if (grid.PlaceX(x, y))
            {
                Debug.Log($"Placed X at ({x}, {y})");
            }
            else
            {
                Debug.Log($"Cannot place X at ({x}, {y})");
            }
        }
        else if (m_tapNum == 1) // Second tap: place Star
        {
            if (grid.PlaceStar(x, y))
            {
                Debug.Log($"Placed Star at ({x}, {y})");
            }
            else
            {
                Debug.Log($"Cannot place Star at ({x}, {y})");
            }
        }
        
        m_tapNum++;
        m_tapNum = m_tapNum % m_marker.Count; // Reset tap number after 2 taps
        ShowMarker();
    }

}