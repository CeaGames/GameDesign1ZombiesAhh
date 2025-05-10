using UnityEngine;

[RequireComponent(typeof(Camera))]
public class AspectRatioEnforcer : MonoBehaviour
{
    private const float targetAspectRatio = 16f / 9f;
    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();
        UpdateCameraViewport();
    }

    void Update()
    {
        // Re-check in case window size changes (like resizing the game window)
        UpdateCameraViewport();
    }

    void UpdateCameraViewport()
    {
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspectRatio;

        if (scaleHeight < 1.0f)
        {
            // Letterbox (add black bars on top and bottom)
            Rect rect = _camera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            _camera.rect = rect;
        }
        else
        {
            // Pillarbox (add black bars on left and right)
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = _camera.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            _camera.rect = rect;
        }
    }
}
