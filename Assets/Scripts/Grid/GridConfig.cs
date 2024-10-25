using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Array2DEditor;

[CreateAssetMenu(fileName = "GridConfig", menuName = "Game/GridConfig", order = 1)]
public class GridConfig : ScriptableObject
{
    public Array2DInt GridMap;
    public Array2DBool WinCondition;
    public Vector2 CellSize;
    public Vector2 StartPosition = Vector2.zero;
    public Color[] ColorMappings;
    public int GridWidth;
    public int GridHeight;
}