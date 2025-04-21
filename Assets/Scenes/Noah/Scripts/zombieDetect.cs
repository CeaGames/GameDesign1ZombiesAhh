using UnityEngine;

public class zombieDetect : MonoBehaviour
{
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
        transform.parent.GetComponent<DoorBarricade>().hp -= Time.deltaTime * dpsFromZombies;
    }
}
