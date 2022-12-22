namespace Aoc.Year2022.Day12;

/// <summary>
/// https://dotnetcoretutorials.com/2020/07/25/a-search-pathfinding-algorithm-in-c/
/// </summary>

public sealed class Tile
{
    // Location on the grid
    public int X { get; set; }

    // Location on the grid
    public int Y { get; set; }

    /// <summary>
    /// how many tiles we had to traverse to reach here. So for example if this is right next to the starting tile, it would be a cost of “1”. If it was two tiles to the right, it would be a cost of 2 etc
    /// </summary>
    public int Cost { get; set; }

    /// <summary>
    /// Distance is the distance to our destination (e.g. the target tile). This is worked out using the SetDistance method where it’s basically, ignoring all walls, how many tiles left/right and up/down would it take to reach our goal.
    /// </summary>
    public int Distance { get; set; }

    /// <summary>
    /// CostDistance is essentially the Cost + the Distance. It’s useful later on because given a set of tiles, we work out which one to “work on” by ordering them by the CostDistance. e.g. How many tiles we’ve moved so far + how many tiles we think it will probably take to reach our goal. This is important!
    /// </summary>
    public int CostDistance => Cost + Distance;

    /// <summary>
    /// Parent is just the tile we came from to get here.
    /// </summary>
    public Tile? Parent { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="targetX"></param>
    /// <param name="targetY"></param>
    public void SetDistance(int targetX, int targetY)
    {
        this.Distance = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
    }
}