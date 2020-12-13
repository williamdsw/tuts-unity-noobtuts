
public class Playfield
{
    private static int width = 10;
    private static int height = 13;
    private static Element[,] elements = new Element[width, height];

    public static int Width { get => width; set => width = value; }
    public static int Height { get => height; set => height = value; }
    public static Element[,] Elements { get => elements; set => elements = value; }

    public static void UncoverMines()
    {
        foreach (Element element in elements)
        {
            if (element.IsMine)
            {
                element.LoadTexture(0);
            }
        }
    }

    // Checks if the mine is at determined position
    public static bool IsMineAt(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return elements[x, y].IsMine;
        }

        return false;
    }

    // Verifies and counts all adjacent mines
    public static int AdjacentMines(int x, int y)
    {
        int count = 0;

        // Check surrounds
        if (IsMineAt(x, y + 1)) count++;              // Top
        if (IsMineAt(x + 1, y + 1)) count++;          // Top-Right
        if (IsMineAt(x + 1, y)) count++;              // Right
        if (IsMineAt(x + 1, y - 1)) count++;          // Bottom-Right
        if (IsMineAt(x, y - 1)) count++;              // Bottom
        if (IsMineAt(x - 1, y - 1)) count++;          // Bottom-Left
        if (IsMineAt(x - 1, y)) count++;              // Left
        if (IsMineAt(x - 1, y + 1)) count++;          // Top-Left

        return count;
    }

    // Checks and uncovers all neighbors
    public static void FloodFillUncover(int x, int y, bool[,] hasVisited)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            // Cancels
            if (hasVisited[x, y]) return;

            elements[x, y].LoadTexture(AdjacentMines(x, y));

            if (AdjacentMines(x, y) > 0) return;

            hasVisited[x, y] = true;

            // Check again for neighbors
            FloodFillUncover(x - 1, y, hasVisited);    // Left
            FloodFillUncover(x + 1, y, hasVisited);    // Right
            FloodFillUncover(x, y - 1, hasVisited);    // Bottom
            FloodFillUncover(x, y + 1, hasVisited);    // Top
        }
    }

    // Checks all elements to finish the game
    public static bool IsFinished()
    {
        foreach (Element element in elements)
        {
            if (element.IsCovered() && !element.IsMine) return false;
        }

        return true;
    }
}