using UnityEngine;

public class FloorScript : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;

    [SerializeField] private GameObject _prefabCube;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _height; z++)
            {
                Vector3 position = new Vector3(x * 2 * _prefabCube.transform.localScale.x, 0, z * 2 * _prefabCube.transform.localScale.z);
                GameObject cube = Instantiate(_prefabCube, position + transform.position, Quaternion.identity, transform);
                cube.transform.localScale = new Vector3(cube.transform.localScale.x, Random.Range(100, 1000), cube.transform.localScale.z);


            }
        }


    }
}
