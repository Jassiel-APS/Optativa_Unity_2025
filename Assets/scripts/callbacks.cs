using UnityEngine;

public class callbacks : MonoBehaviour
{
    bool isJumping = false;
    float jumpSpeed = 0.05f;    // Qué tan rápido sube/baja el cubo
    float jumpHeight = 1f;      // Altura máxima del salto
    float startY;               // Posición inicial en Y
    float targetY;              // Altura a la que debe llegar
    float direction = 1f;       // 1 = subiendo, -1 = bajando

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.Translate(horizontal, 0, vertical);

        // Detectar inicio del salto
        if (!isJumping && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            targetY = startY + jumpHeight;
            direction = 1f;
        }

        // Si está saltando, mover el cubo
        if (isJumping)
        {
            float newY = transform.position.y + jumpSpeed * direction;

            // Subiendo
            if (direction > 0)
            {
                if (newY >= targetY)
                {
                    newY = targetY;
                    direction = -1f; // Cambia a bajar
                }
            }
            // Bajando
            else
            {
                if (newY <= startY)
                {
                    newY = startY;
                    isJumping = false; // Termina el salto
                }
            }

            Vector3 pos = transform.position;
            pos.y = newY;
            transform.position = pos;
        }
    }
}