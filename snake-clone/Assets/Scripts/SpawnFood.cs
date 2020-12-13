using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private Transform borderTop;
    [SerializeField] private Transform borderBottom;
    [SerializeField] private Transform borderLeft;
    [SerializeField] private Transform borderRight;
    private float timeToStart = 3f;
    private float timeToRepeat = 4f;

    private void Start () 
    {
        // Calls the method first at "timeToStart" and repeat at "timeToRepeat"
        InvokeRepeating ("Spawn", timeToStart, timeToRepeat);
    }

    // Spawns the food at random position
    private void Spawn ()
    {
        int x = (int) Random.Range (borderLeft.position.x, borderRight.position.x);
        int y = (int) Random.Range (borderBottom.position.y, borderTop.position.y);
        Instantiate (foodPrefab, new Vector3 (x, y, 0), Quaternion.identity);
    }
}