using System.Linq;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.Audio;

public class DoorBarricade : MonoBehaviour
{
    public float hp;
    public float maxHp;
    public bool isInTimedDoor;

    bool previouslyBroken = true;

    public GameObject[] planks;

    private AudioSource audioSource;
    [SerializeField] private AudioClip Destroyed;

    private GameObject player;
    private float distance = 100;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);

        if (hp > maxHp)
        {
            hp = maxHp;
        }

        if (hp > maxHp * 0.667)
        {
            planks[0].SetActive(true);
            planks[1].SetActive(true);
            planks[2].SetActive(true);
        }
        else if (hp > maxHp * 0.334)
        {
            planks[0].SetActive(false);
            planks[1].SetActive(true);
            planks[2].SetActive(true);
        }
        else if (hp > 0)
        {
            planks[0].SetActive(false);
            planks[1].SetActive(false);
            planks[2].SetActive(true);

            //to keep track of whether if the barricade was already broken
            previouslyBroken = false;
        }
        else
        {
            planks[0].SetActive(false);
            planks[1].SetActive(false);
            planks[2].SetActive(false);

            //to keep track of whether if the barricade was already broken, and play the break sound only if it wasn't
            if (!previouslyBroken)
            {
                //play sound
                audioSource.clip = Destroyed;
                audioSource.volume = 1f / (distance * 1f);
                audioSource.Play();
            }
            previouslyBroken = true;
        }
    }


}
