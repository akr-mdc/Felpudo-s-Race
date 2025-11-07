using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    public float jumpForce = 8f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.12f;
    public LayerMask groundLayer;

    [Header("FX")]
    public GameObject danoParticlesPrefab;
    public Animator animator; // opcional

    Rigidbody2D rb;
    bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (groundCheck != null)
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            if (animator != null) animator.SetTrigger("Jump");
        }

        // Mantém o jogador fixo no X
        rb.velocity = new Vector2(0f, rb.velocity.y);

        if (animator != null)
        {
            animator.SetBool("IsGrounded", isGrounded);
            animator.SetFloat("VerticalSpeed", rb.velocity.y);
        }
    }

    // Método existente que você já usa internamente
    public void RecebeDano(int dano)
    {
        if (GameManager.instance != null)
            GameManager.instance.TomarDano(dano);

        if (danoParticlesPrefab != null)
            Instantiate(danoParticlesPrefab, transform.position, Quaternion.identity);

        if (animator != null)
            animator.SetTrigger("Hit");
    }

    // --- Método adicional para compatibilidade com scripts que chamam TakeDamage ---
    // Apenas encaminha para RecebeDano para evitar duplicação de lógica.
    public void TakeDamage(int damage)
    {
        RecebeDano(damage);
    }

    // Caso haja colisões normais (não trigger)
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Inimigo"))
        {
            RecebeDano(1);
        }
    }

    // Caso objetos usem trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Inimigo"))
        {
            RecebeDano(1);
        }
    }
}
