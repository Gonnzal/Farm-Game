using UnityEngine;

public class Cammer : MonoBehaviour
{
    [SerializeField] Transform player;
    Vector3 offset;
    // Update is called once per frame
    void Update()
    {
        offset = new Vector3(1, 15.3f, -7);
        transform.position = player.transform.position + offset;
    }
}
