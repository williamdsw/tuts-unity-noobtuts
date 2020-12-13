using UnityEngine;

public class MoveRacket : MonoBehaviour
{
    private float moveSpeed = 3000f;
    [SerializeField] private string verticalAxis = "Vertical";
    private Rigidbody2D rigidBody2D;

    private void Awake () 
    {
        rigidBody2D = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate () 
    {
        float vertical = Input.GetAxisRaw (verticalAxis);
        rigidBody2D.velocity = (new Vector2 (0, vertical) * moveSpeed * Time.fixedDeltaTime);
    }
}