using UnityEngine;

public class ItemRandomizer : MonoBehaviour
{
    public GameObject prefabInstance; // Assign the prefab instance in the inspector
    private HasItemScript[] itemScripts;

    void Start()
    {
        if (prefabInstance == null)
        {
            Debug.LogError("Prefab instance not assigned.");
            return;
        }

        // Get all HasItemScript components in the prefab's children
        itemScripts = prefabInstance.GetComponentsInChildren<HasItemScript>();

        if (itemScripts.Length == 0)
        {
            Debug.LogWarning("No HasItemScript components found in the prefab.");
            return;
        }

        // Pick a random one
        int randomIndex = Random.Range(0, itemScripts.Length);
        itemScripts[randomIndex].HasItem = true;

        Debug.Log($"Random item assigned: {itemScripts[randomIndex].gameObject.name}");
    }
}

