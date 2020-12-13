using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private GameObject tailPrefab;
    private float timeToStart = 0.3f;
    private float timeToRepeat = 0.1f;
    private Vector2 direction = Vector2.right;

    // State
    private List<Transform> tails = new List<Transform> ();
    private bool hasAte = false;

    private void Start () 
    {
        InvokeRepeating ("Move", timeToStart, timeToRepeat);
    }

    private void Update () 
    {
        ControlDirection ();
    }

    private void OnTriggerEnter2D (Collider2D other) 
    {
        if (other.name.StartsWith ("Food"))
        {
            hasAte = true;
            Destroy (other.gameObject);
        }
        else 
        {
            Debug.Log ("You lose!");
        }
    }

    // Controls current direction with keys
    private void ControlDirection ()
    {
        if (Input.GetKey (KeyCode.RightArrow))
        {
            direction = Vector2.right;
        }
        else if (Input.GetKey (KeyCode.DownArrow))
        {
            direction = Vector2.down;
        }
        else if (Input.GetKey (KeyCode.LeftArrow))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKey (KeyCode.UpArrow))
        {
            direction = Vector2.up;
        }
    }

    private void Move ()
    {
        // Current position
        Vector2 position = this.transform.position;

        // Moves to next position
        transform.Translate (direction);

        if (hasAte)
        {
            GameObject tail = Instantiate (tailPrefab, position, Quaternion.identity) as GameObject;
            tails.Insert (0, tail.transform);
            hasAte = false;
        }
        else if (tails.Count > 0)
        {
            // Updates position
            tails.Last ().position = position;

            // Set positions on list
            tails.Insert (0, tails.Last ());
            tails.RemoveAt (tails.Count - 1);
        }
    }
}