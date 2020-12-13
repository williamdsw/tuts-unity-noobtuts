using UnityEngine;

public class Group : MonoBehaviour
{
    private float lastFall = 0;
    private Spawner spawner;

    private void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
    }

    private void Start()
    {
        if (!isValidGridPosition())
        {
            Debug.Log("GAME OVER!");
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        MoveAndRotate();
    }

    private void MoveAndRotate()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Moves left
            transform.position += Vector3.left;

            if (isValidGridPosition())
            {
                UpdateGrid();
            }
            else
            {
                transform.position += Vector3.right;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Moves right
            transform.position += Vector3.right;

            if (isValidGridPosition())
            {
                UpdateGrid();
            }
            else
            {
                transform.position += Vector3.left;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Rotates
            transform.Rotate(0, 0, -90);

            if (isValidGridPosition())
            {
                UpdateGrid();
            }
            else
            {
                transform.Rotate(0, 0, 90);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - lastFall >= 1)
        {
            // Moves down
            transform.position += Vector3.down;

            if (isValidGridPosition())
            {
                UpdateGrid();
            }
            else
            {
                transform.position += Vector3.up;
                Playfield.DeleteFullRows();
                spawner.SpawnNextGroup();
                this.enabled = false;
            }

            lastFall = Time.time;
        }
    }

    private bool isValidGridPosition()
    {
        foreach (Transform child in transform)
        {
            Vector2 childPosition = Playfield.RoundPosition(child.position);

            // Not inside border?
            if (!Playfield.IsInsideBorders(childPosition)) return false;

            // Checks parent
            Transform cell = Playfield.Grid[(int)childPosition.x, (int)childPosition.y];
            if (cell != null && cell.parent != this.transform) return false;
        }

        return true;
    }

    private void UpdateGrid()
    {
        // Removes old children from grid
        for (int y = 0; y < Playfield.Height; ++y)
        {
            for (int x = 0; x < Playfield.Width; ++x)
            {
                if (Playfield.Grid[x, y] != null)
                {
                    Transform cell = Playfield.Grid[x, y];
                    if (cell.parent == this.transform)
                    {
                        Playfield.Grid[x, y] = null;
                    }
                }
            }
        }

        // Add new children to grid
        foreach (Transform child in this.transform)
        {
            Vector2 childPosition = Playfield.RoundPosition(child.position);
            Playfield.Grid[(int) childPosition.x, (int) childPosition.y] = child;
        }
    }
}