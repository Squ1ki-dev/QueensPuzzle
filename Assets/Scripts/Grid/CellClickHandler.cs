using System.Collections;
using System.Collections.Generic;
using GridEnums;
using UnityEngine;

public class CellClickHandler : MonoBehaviour
{
    private GridMap cellHandler;
    private ColorType colorType;
    [SerializeField] private List<Transform> m_marker = new List<Transform>();

    private int m_tapNum = 0;
    private int x, y;

    public void Initialize(GridMap cellHandler, int x, int y, ColorType colorType)
    {
        this.cellHandler = cellHandler;
        this.x = x;
        this.y = y;
        this.colorType = colorType;
    }

    public void Start()
    {
        m_tapNum = 0;
        ClearMarkers();
    }

    private void OnMouseDown() => OnTileTapped();

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

        // Check if m_marker has any elements to avoid DivideByZeroException
        if (m_marker.Count > 0)
            m_marker[m_tapNum].gameObject.SetActive(true);
        else
            Debug.LogWarning("No markers available in m_marker.");
    }

    public void OnTileTapped()
    {
        // Check if there are markers before incrementing and using m_tapNum
        if (m_marker.Count > 0)
        {
            m_tapNum++;
            m_tapNum = m_tapNum % m_marker.Count; // Reset tap number after reaching count
            ShowMarker();
        }
        else
            Debug.LogWarning("No markers to place on tile tap.");

        if (m_tapNum == 1) // Example: m_tapNum 1 means placing a star
        { // Notify the Grid that a Star has been placed
            Debug.Log("Here" + x + "" + y);
        }
    }
}