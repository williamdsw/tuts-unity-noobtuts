using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    private float moveSpeed = 200f;
    private float force = 300f;
    private Rigidbody2D rigidBody2D;

    private void Awake()
    {
        rigidBody2D = this.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        InitialVelocity();
    }

    private void FixedUpdate()
    {
        Flap();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ReloadLevel();
    }

    private void InitialVelocity()
    {
        rigidBody2D.velocity = (Vector2.right * moveSpeed * Time.fixedDeltaTime);
    }

    private void Flap()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody2D.AddForce(Vector2.up * force);
        }
    }

    private static void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
