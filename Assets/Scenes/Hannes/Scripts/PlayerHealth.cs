using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]
    private float zombieDamage = 10f;
    [SerializeField]
    private float zombieAttackCooldown = 1f;
    [SerializeField]
    private LayerMask zombieLayer;

    private float currentHealth;
    private float timer;

    private void Start()
    {
        currentHealth = maxHealth;
        timer = 0f;
    }

    private void Update()
    {
        // Timer reset each frame in case we're not in contact with a zombie
        timer += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if 'other' belongs to the zombieLayer
        if (((1 << other.gameObject.layer) & zombieLayer) != 0)
        {
            if (timer >= zombieAttackCooldown)
            {
                currentHealth -= zombieDamage;
                currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
                timer = 0f;
                Debug.Log("You have " + currentHealth + " health left");

                if (currentHealth <= 0f)
                {
                    Die();
                }
            }
        }
    }

    private void Die()
    {
        Debug.Log("Player died.");
        // Optionally disable movement, play animation, etc.
        GetComponent<PlayerMovementCC>().enabled = false;
        GetComponent<CharacterController>().enabled = false;
        // You can also trigger a respawn or game over screen here.
    }
}
