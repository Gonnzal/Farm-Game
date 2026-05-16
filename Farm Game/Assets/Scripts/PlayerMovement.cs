using UnityEngine;
using UnityEngine.InputSystem;

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
        speed = Input.GetKey(KeyCode.LeftShift) ? speedRun : speedWalk;
        
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        if(moveDirection.magnitude > 0)
        {
            rb.MovePosition(transform.position + moveDirection * speed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }
}
