using UnityEngine;
using UnityEngine.Audio;

public class spin : MonoBehaviour
{
    [SerializeField] private AudioClip Chopper;
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // Configure the audio source
        audioSource.clip = Chopper;
        audioSource.loop = true; // Enable looping
        audioSource.playOnAwake = false;

        // Start playing the sound
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 1000 * Time.deltaTime, 0);
    }
}
