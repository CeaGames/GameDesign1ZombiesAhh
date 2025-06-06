using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class WeepingAngelZombie : MonoBehaviour
{
    public Transform playerCamera;            // Assign the player's camera
    public Transform target;                  // The destination the zombie walks toward
    public lvlGenCopy lvlGenScript;           // Spawning info
    public float viewAngle = 70f;             // Field of view angle
    public float detectionDistance = 30f;     // Max detection distance

    //public LayerMask layerToIgnore;

    public float maxHealth = 100f;            // Total health
    public float damagePerSecond = 10f;       // Damage per second when being looked at

    [SerializeField] private Material yellowMaterial;
    [SerializeField] private Material greyMaterial;

    private float currentHealth;
    private NavMeshAgent agent;
    private MeshRenderer meshRenderer;

    [SerializeField] private AudioClip ApproachSound;
    private AudioSource audioSource;

    [SerializeField] private GameObject player;

    private Vector3 distancevector;
    private float perceivedDistance = 100;

    void Start()
    {

        audioSource = GetComponent<AudioSource>();

        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = yellowMaterial;
        currentHealth = maxHealth;

        agent = GetComponent<NavMeshAgent>();
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        agent.avoidancePriority = 99;

        agent.Warp(new Vector3(0, lvlGenScript.locationHeight + transform.localScale.y / 2, 0));
    }

    void Update()
    {
        //distance vector made custom to flatten the area in which zombie can be heard
        distancevector = (player.transform.position - transform.position);
        perceivedDistance = Vector3.Magnitude(new Vector3(distancevector.x, distancevector.y * 3, distancevector.z));

        if (IsVisibleToPlayer())
        {
            agent.isStopped = true;
            meshRenderer.material = greyMaterial;
        }
        else
        {
            audioSource.volume = 1.5f - (perceivedDistance * .15f);


            if (!audioSource.isPlaying)
            {
                audioSource.clip = ApproachSound;
                audioSource.Play();
            }

            meshRenderer.material = yellowMaterial;
            agent.isStopped = false;
            if (target != null)
            {
                agent.SetDestination(target.position);
            }
        }
    }

    bool IsVisibleToPlayer()
    {
        Vector3 directionToZombie = transform.position - playerCamera.position;
        float distance = directionToZombie.magnitude;

        if (distance > detectionDistance)
            return false;

        float angle = Vector3.Angle(playerCamera.forward, directionToZombie);

        if (angle < viewAngle / 2f)
        {
            // Raycast to check line of sight
            Ray ray = new Ray(playerCamera.position, directionToZombie.normalized);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, detectionDistance))
            {
                if (hit.transform == transform)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void Die()
    {
        float randomX = Random.Range(-2.5f, 2.5f);
        float randomZ = Random.Range(-2.5f, 2.5f);
        float y = lvlGenScript.locationHeight + transform.localScale.y / 2;

        Vector3 randomPosition = new Vector3(randomX, y, randomZ);

        agent.Warp(randomPosition);
    }


}
