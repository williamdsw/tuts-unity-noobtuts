using UnityEngine;

public class Racket : MonoBehaviour 
{
    private float movementSpeed = 5000f;
    private Rigidbody2D rigidBody2D;

	private void Awake () 
    {
        rigidBody2D = this.GetComponent<Rigidbody2D> (); 
    }

    private void FixedUpdate ()
    {
        // -1 = left, 0 = no direction, 1 = right
        // Mouse X
        float horizontal = Input.GetAxisRaw ("Horizontal");

        // Vector2.right * -1 = Vector2.left
        // Vector2.right * 0 = Vector2.zero
        // Vector2.right * 1 = Vector2.right
        rigidBody2D.velocity = (Vector2.right * horizontal * movementSpeed * Time.deltaTime);
    }
}