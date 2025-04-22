using UnityEngine;

public class zombieDetect : MonoBehaviour
{
<<<<<<< Updated upstream
<<<<<<< Updated upstream
    public GameObject doorframe;
    public object doorcode;

    public float dpsFromZombies;

    void Start()
    {

=======
=======
>>>>>>> Stashed changes
    public float dpsFromZombies;
    void Start()
    {
        
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        if (other.gameObject.layer == 9) //zombie
        {
          doorframe.GetComponent<DoorBarricade>().hp -= Time.deltaTime * dpsFromZombies;
        }
=======
        transform.parent.GetComponent<DoorBarricade>().hp -= Time.deltaTime * dpsFromZombies;
>>>>>>> Stashed changes
=======
        transform.parent.GetComponent<DoorBarricade>().hp -= Time.deltaTime * dpsFromZombies;
>>>>>>> Stashed changes
    }
}
