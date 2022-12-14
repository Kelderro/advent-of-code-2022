/// <summary>
/// https://adventofcode.com/2022/day/8
/// </summary>

namespace Aoc.Year2022.Day08;

public sealed class DayEight : IDay<int>
{
    /// <summary>
    /// With 16 trees visible on the edge and another 5 visible in the interior, a total of 21 trees are visible in this arrangement.
    /// Consider your map; how many trees are visible from outside the grid?.
    /// </summary>
    /// <param name="lines">Map.</param>
    /// <returns>Amount of trees that are visible from outside the grid.</returns>
    public static int PartOne(string[] lines)
    {
        var mapLineLength = lines.Length;
        var map = CreateMatrix(lines);

        // Edge trees that are always visible
        var visibleTrees = (mapLineLength * 4) - 4;

        // Count the amount of inner trees that visible from
        // the top, right, bottom or left. Skipping the outer
        // "circle" trees as they have been counted for.
        for (var r = 1; r < mapLineLength - 1; r++)
        {
            for (var c = 1; c < mapLineLength - 1; c++)
            {
                if (IsTreeVisible(map, r, c))
                {
                    visibleTrees++;
                }
            }
        }

        return visibleTrees;
    }

    /// <summary>
    /// Consider each tree on your map. What is the highest scenic score possible for any tree?.
    /// </summary>
    /// <param name="lines">Map.</param>
    /// <returns>Highest scenic score possible.</returns>
    public static int PartTwo(string[] lines)
    {
        var matrixSize = lines.Length;
        var matrix = CreateMatrix(lines);
        var maxScenicScore = 0;

        for (var r = 0; r < matrixSize; r++)
        {
            for (var c = 0; c < matrixSize; c++)
            {
                var scenicScore = CalculateScenicScore(matrix, r, c);
                maxScenicScore = int.Max(maxScenicScore, scenicScore);
            }
        }

        return maxScenicScore;
    }

    /// <summary>
    /// Transform the map from string array to a multidimensional array.
    /// </summary>
    /// <param name="mapLines">Map to transform.</param>
    /// <returns>Transformed map.</returns>
    private static int[,] CreateMatrix(string[] mapLines)
    {
        var mapLineLength = mapLines.Length;
        var matrix = new int[mapLineLength, mapLineLength];

        for (var r = 0; r < mapLineLength; r++)
        {
            for (var c = 0; c < mapLineLength; c++)
            {
                matrix[r, c] = int.Parse(mapLines[r][c].ToString());
            }
        }

        return matrix;
    }

    /// <summary>
    /// Determine if the tree is visible looking from the top, right, bottom or left.
    /// </summary>
    /// <param name="map">Map.</param>
    /// <param name="row">Row number.</param>
    /// <param name="column">Column number.</param>
    /// <returns>true if the tree is visible; otherwise, false.</returns>
    private static bool IsTreeVisible(int[,] map, int row, int column)
    {
        return IsTreeVisibleFromTop(map, row, column)
         || IsTreeVisibleFromRight(map, row, column)
         || IsTreeVisibleFromBottom(map, row, column)
         || IsTreeVisibleFromLeft(map, row, column);
    }

    /// <summary>
    /// Check if the tree is visible from the top.
    /// </summary>
    /// <param name="map">Map.</param>
    /// <param name="row">Row number.</param>
    /// <param name="column">Column number.</param>
    /// <returns>true if the tree is visible when looking from the top; otherwise, false.</returns>
    private static bool IsTreeVisibleFromTop(int[,] map, int row, int column)
    {
        var cellValue = map[row, column];

        // In the current state the method is not optimized. To optimize the method, overwrite
        // the value of the current cell when the cell value above it is higher. With this
        // you don't need to go all the way up to verify if there isn't a higher tree blocking
        // the current tree.
        for (var i = row - 1; i >= 0; i--)
        {
            if (map[i, column] >= cellValue)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Check if the tree is visible from the right.
    /// </summary>
    /// <param name="map">Map.</param>
    /// <param name="row">Row number.</param>
    /// <param name="column">Column number.</param>
    /// <returns>true if the tree is visible when looking from the right; otherwise, false.</returns>
    private static bool IsTreeVisibleFromRight(int[,] map, int row, int column)
    {
        var matrixLength = map.GetLength(0);
        var cellValue = map[row, column];

        for (var i = column + 1; i < matrixLength; i++)
        {
            if (map[row, i] >= cellValue)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Check if the tree is visible from the bottom.
    /// </summary>
    /// <param name="map">Map.</param>
    /// <param name="row">Row number.</param>
    /// <param name="column">Column number.</param>
    /// <returns>true if the tree is visible when looking from the bottom; otherwise, false.</returns>
    private static bool IsTreeVisibleFromBottom(int[,] map, int row, int column)
    {
        var matrixLength = map.GetLength(0);
        var cellValue = map[row, column];

        for (var i = row + 1; i < matrixLength; i++)
        {
            if (map[i, column] >= cellValue)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Check if the tree is visible from the left.
    /// </summary>
    /// <param name="map">Map.</param>
    /// <param name="row">Row number.</param>
    /// <param name="column">Column number.</param>
    /// <returns>true if the tree is visible when looking from the left; otherwise, false.</returns>
    private static bool IsTreeVisibleFromLeft(int[,] map, int row, int column)
    {
        var cellValue = map[row, column];

        // In the current state the method is not optimized. To optimize the method, overwrite
        // the value of the current cell when the cell value on the left is higher. With this
        // you don't need to go all the way up to verify if there isn't a higher tree blocking
        // the current tree.
        for (var i = column - 1; i >= 0; i--)
        {
            if (map[row, i] >= cellValue)
            {
                return false;
            }
        }

        return true;
    }

    private static int CalculateScenicScore(int[,] map, int row, int column)
    {
        var score = ScenicScoreUp(map, row, column);
        score *= ScenicScoreLookingRight(map, row, column);
        score *= ScenicScoreLookingDown(map, row, column);
        score *= ScenicScoreLookingLeft(map, row, column);
        return score;
    }

    private static int ScenicScoreUp(int[,] map, int row, int column)
    {
        var treeHeight = map[row, column];
        var score = 0;

        for (var r = row - 1; r >= 0; r--)
        {
            score++;

            // The tree is blocked by another tree that is taller
            if (map[r, column] >= treeHeight)
            {
                break;
            }
        }

        return score;
    }

    private static int ScenicScoreLookingRight(int[,] map, int row, int column)
    {
        var matrixLength = map.GetLength(0);
        var treeHeight = map[row, column];
        var score = 0;

        for (var c = column + 1; c < matrixLength; c++)
        {
            score++;

            // The tree is blocked by another tree that is taller
            if (map[row, c] >= treeHeight)
            {
                break;
            }
        }

        return score;
    }

    private static int ScenicScoreLookingDown(int[,] map, int row, int column)
    {
        var matrixLength = map.GetLength(0);
        var treeHeight = map[row, column];
        var score = 0;

        for (var r = row + 1; r < matrixLength; r++)
        {
            score++;

            // The tree is blocked by another tree that is taller
            if (map[r, column] >= treeHeight)
            {
                break;
            }
        }

        return score;
    }

    private static int ScenicScoreLookingLeft(int[,] map, int row, int column)
    {
        var treeHeight = map[row, column];
        var score = 0;

        for (var c = column - 1; c >= 0; c--)
        {
            score++;

            // The tree is blocked by another tree that is taller
            if (map[row, c] >= treeHeight)
            {
                break;
            }
        }

        return score;
    }
}
