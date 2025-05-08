using UnityEngine;

public class TimerEndsWinScript : MonoBehaviour
{
    public GameObject objectToSpawn;     // The prefab to spawn
    public float delay = 5f;             // Delay in seconds before spawning
    public Transform spawnPosition;        // Position where the object will spawn

    private void Start()
    {
        // Start the coroutine that handles delayed spawning
        StartCoroutine(SpawnObjectAfterDelay());
    }

    private System.Collections.IEnumerator SpawnObjectAfterDelay()
    {
        // Wait for the delay
        yield return new WaitForSeconds(delay);

        // Spawn the object
        Debug.Log("Youre mother won");
        Instantiate(objectToSpawn, new Vector3(spawnPosition.position.x, spawnPosition.position.y + 2.5f, spawnPosition.position.z), Quaternion.identity);
    }
}
