using UnityEngine;
using static UnityEngine.UI.Image;

public class PickingUpFurniture : MonoBehaviour
{
    [SerializeField] private GameObject _holdArea;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private float _grabDistance;

    private bool _isHoldingObject;
    private GameObject _heldObject;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        Debug.DrawRay(transform.position, transform.forward * _grabDistance, Color.red);

        if (Input.GetKeyDown(KeyCode.E) && !_isHoldingObject)
        {
            Debug.Log("e was pressed");
            if (Physics.Raycast(ray, out RaycastHit hit, _grabDistance, _layer))
            {
                Debug.Log("picked up");
                _heldObject = hit.collider.gameObject;

                _isHoldingObject = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.E)) 
        {
            _heldObject = null;

            _isHoldingObject = false;
        }

        if (_heldObject != null)
        {
            Debug.Log("moving");

            Vector3 holdPosition = Vector3.MoveTowards(_heldObject.transform.position, _holdArea.transform.position, 50 * Time.deltaTime);
            _heldObject.GetComponent<Rigidbody>().Move(holdPosition, _heldObject.transform.rotation);
        }
    }
}
