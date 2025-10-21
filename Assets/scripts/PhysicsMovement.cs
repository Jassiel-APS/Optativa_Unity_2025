using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float jumpForce = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            //rb.AddForce(0f, jumpForce, 0f, ForceMode.VelocityChange);
            //rb.AddForce(x: 0f, y: jumpForce, z: jumpForce, mode: ForceMode.VelocityChange);
        }*/
        var Horizontal = Input.GetAxis("Horizontal");
        var Vertical = Input.GetAxis("Vertical");
        var direction = new Vector3(Horizontal, 0f, Vertical).normalized;
        rb.AddForce(direction * (Time.deltaTime * jumpForce), ForceMode.VelocityChange);
            
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
        
    }
}
