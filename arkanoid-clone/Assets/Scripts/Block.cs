using UnityEngine;

public class Block : MonoBehaviour 
{
    private void OnCollisionEnter2D (Collision2D other)
    {
        Destroy (this.gameObject);
    }
}
