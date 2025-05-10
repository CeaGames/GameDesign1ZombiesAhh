using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class zombieDetect : MonoBehaviour
{
    public DoorBarricade doorBarricade;
    public GameObject doorframe;

    public float dpsFromZombies;
    public float hpFromPlayer;

    private Dictionary<NavMeshAgent, float> frozenZombies = new Dictionary<NavMeshAgent, float>();
    private bool doorIsDestroyed = false;

    void Update()
    {
        if (!doorIsDestroyed && doorBarricade.hp <= 0)
        {
            RestoreZombieSpeeds();
            doorIsDestroyed = true;
        }

        if (doorBarricade.hp > 0) 
        {
            doorIsDestroyed = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (doorIsDestroyed) return;

        if (other.gameObject.layer == 9) // zombie
        {
            doorBarricade.hp -= Time.deltaTime * dpsFromZombies;
        }

        if (other.gameObject.layer == 6) // player
        {
            doorBarricade.hp += Time.deltaTime * hpFromPlayer;
            if (doorBarricade.hp > doorBarricade.maxHp)
            {
                doorBarricade.hp = doorBarricade.maxHp;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (doorIsDestroyed) return;

        if (other.gameObject.layer == 9) // zombie
        {
            NavMeshAgent agent = other.gameObject.GetComponent<NavMeshAgent>();
            if (agent != null && !frozenZombies.ContainsKey(agent))
            {
                frozenZombies[agent] = agent.speed; // store original speed
                agent.speed = 0;
            }
        }
    }

    private void RestoreZombieSpeeds()
    {
        foreach (var kvp in frozenZombies)
        {
            NavMeshAgent agent = kvp.Key;
            float originalSpeed = kvp.Value;

            if (agent != null)
            {
                agent.speed = originalSpeed;
            }
        }

        frozenZombies.Clear();
    }
}
