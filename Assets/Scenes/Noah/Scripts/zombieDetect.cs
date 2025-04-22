using UnityEngine;

public class zombieDetect : MonoBehaviour
{
    public GameObject doorframe;
    public object doorcode;

    public float dpsFromZombies;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 9) //zombie
        {
          doorframe.GetComponent<DoorBarricade>().hp -= Time.deltaTime * dpsFromZombies;
        }
    }
}
