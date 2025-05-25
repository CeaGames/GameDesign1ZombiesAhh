using UnityEngine;
using UnityEngine.SceneManagement;

public class HelicopterWinScript : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private void Start()
    {
        _canvas.enabled = false;

    }

    private void Update()
    {
        if (_canvas.enabled == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _canvas.enabled = true;
    }
}
