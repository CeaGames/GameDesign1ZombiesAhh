using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class ProgDoor : MonoBehaviour
{
    public float TimeToOpen;
    public TextMeshPro Display;

    float timer;

    LayerMask doorFrameLayerMask = 10;
    GameObject doorFrame;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = TimeToOpen;

        //below I'm trying to get the doorframe the progress door is inside of, to make it not barricadable.
        RaycastHit barricade;
        if (Physics.Raycast(transform.position, transform.forward, out barricade, 2, doorFrameLayerMask))
        {
            doorFrame = barricade.transform.gameObject;
            doorFrame.GetComponent<DoorBarricade>().isInTimedDoor = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        Display.text = Mathf.Floor(timer+1).ToString();

        if (timer < 0)
        {
            //doorFrame.GetComponent<DoorBarricade>().isInTimedDoor = false;
            this.gameObject.SetActive(false);
        }
    }

}
