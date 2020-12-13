using UnityEngine;

public class Ball : MonoBehaviour 
{
    private float movementSpeed = 5000f;
    private Rigidbody2D rigidBody2D;

    private void Awake () 
    {
        rigidBody2D = this.GetComponent<Rigidbody2D> ();
    }
    
	private void Start () 
    {
        rigidBody2D.velocity = (Vector2.up * movementSpeed * Time.deltaTime);
	}
 
    private void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.name.Equals ("Racket"))
        {
            // Calculates new trajectory
            float hitFactorX = HitFactor (transform.position, other.transform.position, other.collider.bounds.size.x);
            Vector2 newDirection = new Vector2 (hitFactorX * 2, 1).normalized;
            rigidBody2D.velocity = (newDirection * movementSpeed * Time.deltaTime);
        }
    }

    private float HitFactor (Vector2 ballPosition, Vector2 racketPosition, float racketWidth)
    {
        /* 1  -0.5  0  0.5   1  <- x value depending on where it was hit
        * ===================  <- this is the racket */
        return (ballPosition.x - racketPosition.x) / racketWidth;
    }
}