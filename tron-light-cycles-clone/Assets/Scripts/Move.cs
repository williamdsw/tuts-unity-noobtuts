using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private KeyCode upKey;
    [SerializeField] private KeyCode downKey;
    [SerializeField] private KeyCode rightKey;
    [SerializeField] private KeyCode leftKey;
    [SerializeField] private GameObject wallPrefab;
    private float moveSpeed = 1000f;
    private Vector2 currentDirection = Vector2.up;

    private Vector2 lastWallEnd;

    private Rigidbody2D rigidBody2D;
    private Collider2D wallCollider;

    private void Awake()
    {
        rigidBody2D = this.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Initial movement
        rigidBody2D.velocity = currentDirection * moveSpeed * Time.fixedDeltaTime;
        SpawnWall();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != wallCollider)
        {
            print("Player Lost: " + name);
            Destroy(this.gameObject);
        }
    }

    // Movement, spawns the wall and fixes colliders
    private void Movement()
    {
        if (Input.GetKeyDown(upKey))
        {
            currentDirection = Vector2.up; SpawnWall();
        }
        else if (Input.GetKeyDown(downKey))
        {
            currentDirection = Vector2.down; SpawnWall();
        }
        else if (Input.GetKeyDown(rightKey))
        {
            currentDirection = Vector2.right; SpawnWall();
        }
        else if (Input.GetKeyDown(leftKey))
        {
            currentDirection = Vector2.left; SpawnWall();
        }

        rigidBody2D.velocity = currentDirection * moveSpeed * Time.fixedDeltaTime;
        FitColliderBetween(wallCollider, lastWallEnd, this.transform.position);
    }

    // Spawns the wall
    private void SpawnWall()
    {
        lastWallEnd = this.transform.position;
        GameObject wall = Instantiate(wallPrefab, transform.position, Quaternion.identity) as GameObject;
        wallCollider = wall.GetComponent<Collider2D>();
    }

    private void FitColliderBetween(Collider2D collider, Vector2 a, Vector2 b)
    {
        // New position
        collider.transform.position = a + (b - a) * 0.5f;

        // Calc new scale
        float distance = Vector2.Distance(a, b);
        if (a.x != b.x)
        {
            collider.transform.localScale = new Vector3(distance + 1f, 1f, 1f);
        }
        else
        {
            collider.transform.localScale = new Vector3(1f, distance + 1f, 1f);
        }
    }
}