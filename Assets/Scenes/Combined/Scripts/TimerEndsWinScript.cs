using UnityEngine;
using UnityEngine.UI; // Needed for UI elements

public class TimerEndsWinScript : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float delay = 5f;
    public lvlGenCopy lvlGenScript;
    public Text countdownText; // Reference to the UI Text

    private Transform spawnPosition;
    private float countdown;

    private void Start()
    {
        spawnPosition = lvlGenScript.roof.transform;
        countdown = delay;

        // Start the coroutine
        StartCoroutine(CountdownAndSpawn());
    }

    private System.Collections.IEnumerator CountdownAndSpawn()
    {
        while (countdown > 0)
        {
            countdown -= Time.deltaTime;
            countdownText.text = "Time until helicopter arrives: " + Mathf.CeilToInt(countdown).ToString();
            yield return null; // Wait for next frame
        }

        countdownText.text = "Touch blue helicopter on roof!";

        Instantiate(
            objectToSpawn,
            new Vector3(spawnPosition.position.x, lvlGenScript.locationHeight + objectToSpawn.transform.localScale.y / 2, spawnPosition.position.z),
            Quaternion.identity
        );
    }
}
