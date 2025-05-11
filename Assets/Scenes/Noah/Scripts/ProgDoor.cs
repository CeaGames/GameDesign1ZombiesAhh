using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ProgDoor : MonoBehaviour
{
    public GameObject countdownSystem;
    public TextMeshPro Display;

    float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = countdownSystem.GetComponent<TimerEndsWinScript>().delay/3;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        Display.text = Mathf.Floor(timer+1).ToString();

        if (timer < 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
