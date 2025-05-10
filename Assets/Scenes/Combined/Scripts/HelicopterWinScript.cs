using UnityEngine;

public class HelicopterWinScript : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private void Start()
    {
        _canvas.enabled = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("itworks");
        _canvas.enabled = true;
    }
}
