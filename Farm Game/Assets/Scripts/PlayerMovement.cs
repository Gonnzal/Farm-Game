using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speedWalk;
    [SerializeField] float speedRun;
    float speed;
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = speedRun;
        }
        else
        {
            speed = speedWalk;
        }
        if(Input.GetKey(KeyCode.W))
        {
            rb.MovePosition(transform.position + Vector3.forward * speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.S))
        {
            rb.MovePosition(transform.position - Vector3.forward * speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.A))
        {
            rb.MovePosition(transform.position - Vector3.right * speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.D))
        {
            rb.MovePosition(transform.position + Vector3.right * speed * Time.deltaTime);
        }

        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.rotation = Quaternion.LookRotation(moveDirection);
    }
}
