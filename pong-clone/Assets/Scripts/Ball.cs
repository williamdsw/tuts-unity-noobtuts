using UnityEngine;

public class Ball : MonoBehaviour
{
    private float ballSpeed = 3000f;
    private Rigidbody2D rigidBody2D;

    private void Awake () 
    {
        rigidBody2D = this.GetComponent<Rigidbody2D>();
    }

    private void Start () 
    {
        rigidBody2D.velocity = (Vector2.right * ballSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D (Collision2D other) 
    {
        string otherName = other.gameObject.name;
        if (otherName.StartsWith("Racket"))
        {
            float x = (otherName.Equals("RacketLeft") ? 1 : -1);
            float yHitFactor = HitFactor (this.transform.position, other.transform.position, other.collider.bounds.size.y);
            Vector2 newDirection = new Vector2 (x, yHitFactor).normalized;
            rigidBody2D.velocity = (newDirection * ballSpeed * Time.fixedDeltaTime);
        }
    }

    // Calculates hit factor with rackets
    private float HitFactor (Vector2 ballPosition,  Vector2 racketPosition, float racketHeight)
    {
        return (ballPosition.y - racketPosition.y) / racketHeight;
    }
}