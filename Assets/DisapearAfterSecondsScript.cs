using UnityEngine;

public class DisapearAfterSecondsScript : MonoBehaviour
{
    [SerializeField] float time = 5;

    // Update is called once per frame
    void Update()
    {
        if(time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
