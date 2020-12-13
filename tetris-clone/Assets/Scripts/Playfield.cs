using UnityEngine;

public class Playfield : MonoBehaviour
{
    private static int width = 10;
    private static int height = 20;
    private static Transform[,] grid = new Transform[width, height];

    public static int Width => width;
    public static int Height => height;
    public static Transform[,] Grid { get => grid; set => grid = value; }

    // Rounds the position's x and y to int values
    public static Vector2 RoundPosition(Vector2 position)
    {
        return new Vector2(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));
    }

    // Checks if the group is inside borders
    public static bool IsInsideBorders(Vector2 position)
    {
        return ((int) position.x >= 0 && (int) position.x < width && (int) position.y >= 0);
    }

    // Deletes each object of a row
    public static void DeleteRow(int y)
    {
        for (int x = 0; x < width; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    // Decreases the current row position
    public static void DecreaseRow(int y)
    {
        for (int x = 0; x < width; ++x)
        {
            if (grid[x, y] != null)
            {
                // Move to bottom
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                // Update position
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public static void DecreaseRowAbove(int y)
    {
        for (int i = y; i < height; ++i)
        {
            DecreaseRow(i);
        }
    }

    // Checks if the row is full
    public static bool IsRowFull(int y)
    {
        for (int x = 0; x < width; ++x)
        {
            if (grid[x, y] == null) return false;
        }

        return true;
    }

    // Deletes each full rows
    public static void DeleteFullRows()
    {
        for (int y = 0; y < height; ++y)
        {
            if (IsRowFull(y))
            {
                DeleteRow(y);
                DecreaseRowAbove(y + 1);
                y--;
            }
        }
    }
}