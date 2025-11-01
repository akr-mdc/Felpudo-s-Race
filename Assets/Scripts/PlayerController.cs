using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // === COMPONENTES PRINCIPAIS ===
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    // === MOVIMENTO ===
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    private float horizontalInput;

    // === CHECAGEM DE CHÃO ===
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;

    // === ESTADO DO PLAYER ===
    private bool isFacingRight = true;
    private bool isAlive = true;

    // === SISTEMA DE VIDA ===
    public int maxHealth = 3;
    private int currentHealth;

    // === ANIMAÇÃO ===
    // Parâmetros do Animator:
    // "Speed" (float), "IsGrounded" (bool), "Damage" (trigger)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (!isAlive) return;

        // Entrada horizontal (setas ou A/D)
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Movimento do personagem
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Checagem de chão
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Pulo
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Virar o personagem conforme a direção
        if (horizontalInput > 0 && !isFacingRight)
            Flip();
        else if (horizontalInput < 0 && isFacingRight)
            Flip();

        // === Atualizar o Animator ===
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("IsGrounded", isGrounded);
    }

    // === FUNÇÃO DE DANO ===
    public void TakeDamage(int damage)
    {
        if (!isAlive) return;

        currentHealth -= damage;
        anim.SetTrigger("Damage");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // === MORTE DO PLAYER ===
    private void Die()
    {
        isAlive = false;
        rb.velocity = Vector2.zero;
        // Aqui você pode adicionar uma animação ou tela de derrota
        Debug.Log("Fofuxo foi derrotado!");
    }

    // === FUNÇÃO PARA COLETAR ITENS ===
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fruit"))
        {
            // Exemplo: coleta de frutas
            Debug.Log("Fofuxo coletou uma fruta!");
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Enemy"))
        {
            // Sofre dano ao colidir com inimigos
            TakeDamage(1);
        }
    }

    // === FUNÇÃO AUXILIAR PARA VIRAR O SPRITE ===
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    // === DEBUG VISUAL (aparece na cena para ver o GroundCheck) ===
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
