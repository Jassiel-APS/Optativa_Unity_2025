using System.Diagnostics.Contracts;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    /*-----Variables de movimiento-----*/
    public Transform cameraTransform;
    public float movementSpeed = 5f;
    private CharacterController controller;
    private float gravity;
    /*-----Variables de animacion-----*/
    public Animator animator;
    public readonly int movementSpeedHash = Animator.StringToHash("MovementSpeed");
    
    
// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            gravity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            gravity += Physics.gravity.y * Time.deltaTime;
        }
        
        var gravityVector = Vector3.up * gravity;
        
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        var cameraForward = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z);
        var cameraRight = new Vector3(cameraTransform.right.x, 0, cameraTransform.right.z);
        var direction = cameraForward * vertical + cameraRight * horizontal;
        controller.Move((direction.normalized + gravityVector) * (movementSpeed * Time.deltaTime));
        
        if (direction != Vector3.zero)
        {
         var targetRotation = Quaternion.LookRotation(direction);
         transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f );
        }

        /*-----Actualizar el slider del animator con el valor del movimiento-----*/
        animator.SetFloat(movementSpeedHash, direction.normalized.magnitude);
    }
}