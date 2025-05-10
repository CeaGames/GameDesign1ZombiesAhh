using System.Linq;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
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

        if (hp == maxHp)
        {
            planks[0].SetActive(true);
        }

        if (hp < maxHp/1.5)
        {
            planks[0].SetActive(false);
            planks[1].SetActive(true);
        }

        if (hp < maxHp/2)
        {
            planks[1].SetActive(false);
            planks[2].SetActive(true);
        }

        if (hp < maxHp/4)
        {
            planks[2].SetActive(false);
        }

        if (hp <= 0)
        {

        }
    }


}
