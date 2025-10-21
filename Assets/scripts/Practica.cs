using UnityEngine;

public class Practica : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public float rotationSpeed = 10f; // velocidad de giro al moverse

    private Rigidbody rb;
    private bool isGrounded = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning("Practica: No se encontró Rigidbody en el GameObject. Añade un Rigidbody para que el movimiento físico funcione correctamente.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Movimiento usando transform cuando no hay Rigidbody
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        if (rb != null)
        {
            // Movimiento físico: ajustar velocidad horizontal sin afectar la componente Y
            Vector3 velocity = rb.linearVelocity;
            Vector3 move = transform.TransformDirection(direction) * speed;
            rb.linearVelocity = new Vector3(move.x, velocity.y, move.z);

            // Rotar hacia la dirección de movimiento (solo en XZ)
            Vector3 horizontalMove = new Vector3(move.x, 0f, move.z);
            if (horizontalMove.sqrMagnitude > 0.0001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(horizontalMove);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
            }

            // Salto
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }
        }
        else
        {
            // Movimiento por transformación cuando no hay Rigidbody (no recomendado para física)
            Vector3 movement = transform.TransformDirection(direction) * speed * Time.deltaTime;
            transform.Translate(movement, Space.World);

            // Rotar hacia dirección de movimiento en fallback
            Vector3 horizontalMove = new Vector3(movement.x, 0f, movement.z);
            if (horizontalMove.sqrMagnitude > 0.0001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(horizontalMove);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                // Simular salto simple
                transform.position += Vector3.up * (jumpForce * Time.deltaTime);
                isGrounded = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Considerar cualquier colisión con objeto 'Ground' o con layer de suelo
        if (collision.collider.CompareTag("Ground") || collision.contacts.Length > 0)
        {
            // Mejor comprobación: confirmar que el contacto viene desde abajo
            foreach (var contact in collision.contacts)
            {
                if (Vector3.Dot(contact.normal, Vector3.up) > 0.5f)
                {
                    isGrounded = true;
                    break;
                }
            }
        }
    }
}
