using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    [SerializeField] private float zombieDamage = 10f;
    [SerializeField] private float zombieAttackCooldown = 1f;
    [SerializeField] private LayerMask zombieLayer;

    [Header("Post Processing")]
    [SerializeField] private Volume postProcessVolume;
    private Vignette vignette;

    [Header("UI Overlay on Death")]
    [SerializeField] private Image redScreenOverlay;
    [SerializeField] private GameObject youAreDeadText;

    public float currentHealth;
    private float timer;
    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
        timer = 0f;

        if (postProcessVolume != null && postProcessVolume.profile.TryGet(out Vignette v))
        {
            vignette = v;
        }
        else
        {
            Debug.LogWarning("Vignette not found in post processing profile.");
        }
        if (youAreDeadText != null)
        {
            youAreDeadText.SetActive(false);
        }

        UpdateVisuals();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (isDead == true && Input.GetMouseButtonDown(0)) 
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (((1 << other.gameObject.layer) & zombieLayer) != 0 && !isDead)
        {
            if (timer >= zombieAttackCooldown)
            {
                currentHealth -= zombieDamage;
                currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
                timer = 0f;

                //Debug.Log("Health: " + currentHealth);
                UpdateVisuals();

                if (currentHealth <= 0f)
                {
                    Die();
                }
            }
        }
    }

    public void UpdateVisuals()
    {
        if (vignette == null) return;

        float healthPercent = currentHealth / maxHealth;

        // Vignette gets more red as health decreases
        vignette.intensity.value = Mathf.Lerp(0.4f, 0.7f, 1f - healthPercent);
        vignette.color.value = Color.red;

        // Red overlay alpha = 0 during life
        if (redScreenOverlay != null)
        {
            var color = redScreenOverlay.color;
            color.a = 0f;
            redScreenOverlay.color = color;
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Player died.");

        // Disable Vignette
        if (vignette != null)
        {
            vignette.intensity.value = 0f;
        }

        // Show full red screen overlay
        if (redScreenOverlay != null)
        {
            var color = redScreenOverlay.color;
            color.a = 0.5f; // Fully red
            redScreenOverlay.color = color;
        }

        if (youAreDeadText != null) 
        {
            youAreDeadText.SetActive(true);
        }

        GetComponent<PlayerMovementCC>().enabled = false;
        GetComponent<CharacterController>().enabled = false;
        transform.parent.GetComponentInChildren<PlayerLookCopy>().enabled = false;
    }
}
