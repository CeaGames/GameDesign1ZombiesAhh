using UnityEngine;

public class BasicLevelGeneration : MonoBehaviour
{


    public GameObject groundLevel;
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    //put in move levels when more designed
    public GameObject roof;
    public int floorAmount;


    private int levelAmount = 3; //put in the amount of designed levels appart from ground level
    private float locationHeight = 0f; //don't change unless you want to recreate UP


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Instantiate(groundLevel, new Vector3(0, 0, 0), Quaternion.identity);
        locationHeight += 3f; //groundfloor is 300cm

        for (int i = 0; i < floorAmount; i++)
        {
            if ((Random.Range(0, levelAmount)) == 0)
            {
                if (i % 2 == 0)
                {
                    Instantiate(level1, new Vector3(0, locationHeight, 0), Quaternion.Euler(new Vector3(0, 180, 0)));
                }
                else
                {
                    Instantiate(level1, new Vector3(0, locationHeight, 0), Quaternion.identity);
                }
                locationHeight += 2.7f; //floorheight is 270cm
            }
            else if ((Random.Range(0, levelAmount)) == 1)
            {
                if (i % 2 == 0)
                {
                    Instantiate(level2, new Vector3(0, locationHeight, 0), Quaternion.Euler(new Vector3(0, 180, 0)));
                }
                else
                {
                    Instantiate(level2, new Vector3(0, locationHeight, 0), Quaternion.identity);
                }
                locationHeight += 2.7f; //floorheight is 270cm
            }
            else if ((Random.Range(0, levelAmount)) == 2)
            {
                if (i % 2 == 0)
                {
                    Instantiate(level3, new Vector3(0, locationHeight, 0), Quaternion.Euler(new Vector3(0, 180, 0)));
                }
                else
                {
                    Instantiate(level3, new Vector3(0, locationHeight, 0), Quaternion.identity);
                }
                locationHeight += 2.7f; //floorheight is 270cm
            }
            //copy paste 'else if' here to add more floors
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
