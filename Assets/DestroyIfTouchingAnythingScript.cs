using UnityEngine;

public class DestroyIfTouchingAnythingScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
