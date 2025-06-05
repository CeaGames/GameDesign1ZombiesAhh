using UnityEngine;
using UnityEngine.Audio;

public class Drawer : MonoBehaviour
{
    [SerializeField]
    float openingSpeed = 1.0f;
    public bool drawerOpen;
    public bool previouslyOpen;

    private Vector3 closedPos = new Vector3(0, -0.176579f, 0);
    private Vector3 openPos = new Vector3(0, -0.176579f, 0.6f);
    private Vector3 move;

    [SerializeField] private AudioClip OpenChest;
    [SerializeField] private AudioClip CloseChest;
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        drawerOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = drawerOpen ? openPos : closedPos;
        if (previouslyOpen != drawerOpen)
        {
            if (drawerOpen)
            {
                audioSource.clip = OpenChest;
                audioSource.Play();
            }
            else
            {
                audioSource.clip = CloseChest;
                audioSource.Play();
            }
        }
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, openingSpeed * Time.deltaTime);
        previouslyOpen = drawerOpen;
    }
}
