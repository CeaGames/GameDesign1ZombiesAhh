using UnityEngine;

public class HasItemScript : MonoBehaviour
{
    public bool HasItem = false;
    [SerializeField] private GameObject item;

    // Update is called once per frame
    void Update()
    {
        if (HasItem)
        {
            item.SetActive(true);
        }
        else
        {
            item.SetActive(false);
        }
    }
}
