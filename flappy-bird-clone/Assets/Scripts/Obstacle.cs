using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float moveSpeed = 0;
    private float timeToCall = 2f;
    private Rigidbody2D rigidBody2D;

    private void Awake () 
    {
        rigidBody2D = this.GetComponent<Rigidbody2D>();
    }

    private void Start () 
    {
        ChooseVelocity ();
        InvokeRepeating ("SwitchPosition", 0f, timeToCall);
    }

    // Picks random initial velocity
    private void ChooseVelocity ()
    {
        moveSpeed = Random.Range (50f, 200f);
        rigidBody2D.velocity = (Vector2.up * moveSpeed * Time.fixedDeltaTime);
    }

    // Switch position up / down
    private void SwitchPosition ()
    {
        rigidBody2D.velocity *= -1;
    }
}