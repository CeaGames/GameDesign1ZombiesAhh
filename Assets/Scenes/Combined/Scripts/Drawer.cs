using UnityEngine;

public class Drawer : MonoBehaviour
{
    [SerializeField]
    float openingSpeed = 1.0f;
    public bool drawerOpen;

    private Vector3 closedPos = new Vector3(0, -0.176579f, 0);
    private Vector3 openPos = new Vector3(0, -0.176579f, 0.6f);
    private Vector3 move;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        drawerOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = drawerOpen ? openPos : closedPos;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, openingSpeed * Time.deltaTime);
    }
}
