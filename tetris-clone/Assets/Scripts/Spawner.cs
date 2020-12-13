using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] groups;

    private void Start()
    {
        SpawnNextGroup();
    }

    // Spawns a random group of blocks
    public void SpawnNextGroup()
    {
        int index = Random.Range(0, groups.Length);
        Instantiate(groups[index], this.transform.position, Quaternion.identity);
    }
}