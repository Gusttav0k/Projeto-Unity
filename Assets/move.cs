using UnityEngine;
using UnityEngine.InputSystem;

public class move : MonoBehaviour
{
    private Playeriinputactions controls;
    private Rigidbody2D rb;
    private Animator anim;

    private bool noChao = true;

    public float velocidade = 5f;
    public float forcaPulo = 4000f;

    private Vector2 movimento;

    void Awake()
    {
        controls = new Playeriinputactions();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        controls.player.Enable();
    }

    void Update()
    {
        movimento = controls.player.move.ReadValue<Vector2>();

        anim.SetFloat("speed", Mathf.Abs(movimento.x));
        anim.SetBool("pulando", !noChao);

        if (controls.player.jump.WasPressedThisFrame() && noChao)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaPulo);
            noChao = false;
            Debug.Log("PULOU");
        }

        if (movimento.x > 0)
        {
            transform.localScale = new Vector3(5, 5, 5);
        }

        if (movimento.x < 0)
        {
            transform.localScale = new Vector3(-5, 5, 5);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movimento.x * velocidade, rb.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            noChao = true;
            Debug.Log("NO CHÃO");
        }
    }
}