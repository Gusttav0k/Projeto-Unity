using UnityEngine;
using UnityEngine.InputSystem;

public class move : MonoBehaviour
{
    private playeriinputactions controls;
    private Rigidbody2D rb;

    public float velocidade = 5f;
    public float forcaPulo = 400f;

    private Vector2 movimento;
    private bool noChao;

    private void Awake()
    {
        controls = new playeriinputactions();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        // Movimento
        movimento = controls.move.Newaction.ReadValue<Vector2>();

        // Pulo
        if (controls.jump.Newaction.WasPressedThisFrame() && noChao)
        {
            rb.AddForce(Vector2.up * forcaPulo);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            movimento.x * velocidade,
            rb.linearVelocity.y
        );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            noChao = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            noChao = false;
        }
    }
}