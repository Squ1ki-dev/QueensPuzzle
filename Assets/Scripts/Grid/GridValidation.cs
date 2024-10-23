using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using GridEnums;
using Tools;

public static class GridValidation
{
    public static bool IsStarInInvalidLocation(int x, int y, ColorType[,] cellColors, CellState[,] cellStates, GridConfig gridConfig, HashSet<ColorType> colorRegionsWithStars)
    {
        return IsStarInRowOrColumn(x, y, gridConfig, cellStates) || IsStarOnDiagonals(x, y, cellStates, gridConfig) || IsColorRegionOccupied(y, x, cellColors, colorRegionsWithStars);
    }

    public static bool IsValidStarPlacement(int x, int y, HashSet<ColorType> colorRegionsWithStars, ColorType[,] cellColors)
    {
        ColorType colorType = cellColors[y, x];

        if (colorRegionsWithStars.Contains(colorType))
        {
            MessageHandler.Instance.ShowMessage($"A Queen is already placed in the {colorType} region.", Color.red);
            return false;
        }

        return true;
    }

    public static bool IsStarInRowOrColumn(int x, int y, GridConfig gridConfig, CellState[,] cellStates)
    {
        if (HasStarInRow(y, gridConfig, cellStates) || HasStarInColumn(x, gridConfig, cellStates))
        {
            MessageHandler.Instance.ShowMessage("Each row and column can only have one Queen.", Color.red);
            return true;
        }

        return false;
    }

    private static bool HasStarInRow(int y, GridConfig gridConfig, CellState[,] cellStates) => Enumerable.Range(0, gridConfig.GridWidth).Any(i => cellStates[y, i] == CellState.Star);
    private static bool HasStarInColumn(int x, GridConfig gridConfig, CellState[,] cellStates) => Enumerable.Range(0, gridConfig.GridHeight).Any(i => cellStates[i, x] == CellState.Star);

    public static bool IsStarOnDiagonals(int x, int y, CellState[,] cellStates, GridConfig gridConfig)
    {
        int[][] directions = { new[] { 1, 1 }, new[] { 1, -1 }, new[] { -1, 1 }, new[] { -1, -1 } };

        foreach (var dir in directions)
        {
            int newX = x + dir[0], newY = y + dir[1];
            if (IsWithinBounds(newX, newY, gridConfig) && cellStates[newY, newX] == CellState.Star)
            {
                MessageHandler.Instance.ShowMessage("Two Queens cannot touch each other, not even diagonally.", Color.red);
                return true;
            }
        }

        return false;
    }

    private static bool IsWithinBounds(int x, int y, GridConfig gridConfig)
    {
        return x >= 0 && x < gridConfig.GridWidth && y >= 0 && y < gridConfig.GridHeight;
    }

    private static bool IsColorRegionOccupied(int y, int x, ColorType[,] cellColors, HashSet<ColorType> colorRegionsWithStars)
    {
        ColorType colorType = cellColors[y, x];

        if (colorRegionsWithStars.Contains(colorType))
        {
            MessageHandler.Instance.ShowMessage($"A Queen is already placed in the {colorType} region.", Color.red);
            return true;
        }

        return false;
    }
}