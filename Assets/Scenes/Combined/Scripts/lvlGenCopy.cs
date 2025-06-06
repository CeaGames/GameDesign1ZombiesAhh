using Unity.AI.Navigation;
using UnityEngine;

public class lvlGenCopy : MonoBehaviour
{
    public NavMeshSurface _navMeshSurface;

    public GameObject groundLevel;
    public GameObject[] levels;
    [SerializeField] private GameObject timedDoor;
    //put in move levels when more designed
    public GameObject roof;
    public int floorAmount;


    private int levelAmount = 4; //put in the amount of designed levels appart from ground level
    public float locationHeight = 0f; //don't change unless you want to recreate UP

    private float timeToOpenDoor;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        timeToOpenDoor = timedDoor.GetComponent<ProgDoor>().TimeToOpen;
        if (SettingsCarrier.levelAmount != 0)
            floorAmount = SettingsCarrier.levelAmount;
        GenerateLevel();
        _navMeshSurface.BuildNavMesh();
    }

    // Update is called once per frame
    void GenerateLevel()
    {
        float timedDoorAmount = 0;

        Instantiate(groundLevel, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject firstTimedDoor = Instantiate(timedDoor, new Vector3(0, 0, 0), Quaternion.identity);
        timedDoorAmount++;
        firstTimedDoor.GetComponent<ProgDoor>().TimeToOpen = timeToOpenDoor * timedDoorAmount;
        locationHeight += 3f; //groundfloor is 300cm

        for (int i = 0; i < floorAmount; i++)
        {
            int level = (Random.Range(0, levelAmount));
            {
                if (i % 2 == 0)
                {
                    Instantiate(levels[level], new Vector3(0, locationHeight, 0), Quaternion.Euler(new Vector3(0, 180, 0)));
                    GameObject timedDoorInstance = Instantiate(timedDoor, new Vector3(0, locationHeight, 0), Quaternion.Euler(new Vector3(0, 180, 0)));
                    timedDoorAmount++;
                    timedDoorInstance.GetComponent<ProgDoor>().TimeToOpen = timeToOpenDoor * timedDoorAmount;
                }
                else
                {
                    Instantiate(levels[level], new Vector3(0, locationHeight, 0), Quaternion.identity);
                    GameObject timedDoorInstance = Instantiate(timedDoor, new Vector3(0, locationHeight, 0), Quaternion.identity);
                    timedDoorAmount++;
                    timedDoorInstance.GetComponent<ProgDoor>().TimeToOpen = timeToOpenDoor * timedDoorAmount;
                }
                locationHeight += 2.7f; //floorheight is 270cm
            }

            //add new levels to the public "levels" array
            //change 'levelx' to levelname
            //raise treshhold in else if statement by one

            if (i == (floorAmount - 1)) //adding roof
            {
                if (i % 2 == 0)
                {
                    Instantiate(roof, new Vector3(0, locationHeight, 0), Quaternion.identity);
                }
                else
                {
                    Instantiate(roof, new Vector3(0, locationHeight, 0), Quaternion.Euler(new Vector3(0, 180, 0)));
                }
            }

        }
    }
}
