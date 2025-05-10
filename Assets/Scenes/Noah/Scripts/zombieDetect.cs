using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class zombieDetect : MonoBehaviour
{
    public DoorBarricade doorBarricade;
    public GameObject doorframe;

    public float dpsFromZombies;
    public float hpFromPlayer;

    private bool doorIsDestroyed = false;

    // Struct to hold zombie data
    private struct FrozenZombieData
    {
        public NavMeshAgent agent;
        public CeAiBehaviourScriptCopy aiScript;
        public float originalSpeed;
    }

    // Store frozen zombies
    private Dictionary<GameObject, FrozenZombieData> frozenZombies = new Dictionary<GameObject, FrozenZombieData>();

    void Update()
    {
        // Door gets destroyed
        if (!doorIsDestroyed && doorBarricade.hp <= 0)
        {
            RestoreZombieSpeeds();
            doorIsDestroyed = true;
        }

        // Door is being repaired
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
            NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
            CeAiBehaviourScriptCopy aiScript = other.GetComponent<CeAiBehaviourScriptCopy>();

            if (agent != null && aiScript != null && !frozenZombies.ContainsKey(other.gameObject))
            {
                FrozenZombieData data = new FrozenZombieData
                {
                    agent = agent,
                    aiScript = aiScript,
                    originalSpeed = agent.speed
                };

                frozenZombies.Add(other.gameObject, data);

                agent.speed = 0f;
                agent.enabled = false;
                aiScript.enabled = false;
            }
        }
    }

    private void RestoreZombieSpeeds()
    {
        foreach (var kvp in frozenZombies)
        {
            var data = kvp.Value;

            if (data.agent != null)
            {
                data.agent.enabled = true;
                data.agent.speed = data.originalSpeed;
            }

            if (data.aiScript != null)
            {
                data.aiScript.enabled = true;
            }
        }

        frozenZombies.Clear();
    }
}
