using UnityEngine;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField]
    float maxHealth;
    [SerializeField]
    float zombieDamage;
    [SerializeField]
    float zombieAttackCooldown;
    [SerializeField]
    int zombieLayer;

    
    private float currentHealth;
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == zombieLayer)
        {
            timer += Time.deltaTime;
            if (timer >= zombieAttackCooldown)
            {
                currentHealth -= zombieDamage;
                timer = 0f;
                Debug.Log("you have " + currentHealth + " left");
            }
        }
    }
}
