using System.Linq;
using Unity.VisualScripting;

using UnityEngine;

public class DoorBarricade : MonoBehaviour
{
    public float hp;
    public float maxHp;

    public GameObject[] planks;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
        }
        else
        {
            planks[0].SetActive(false);
            planks[1].SetActive(false);
            planks[2].SetActive(false);
        }
    }


}
