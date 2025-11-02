using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    public float forwardSpeed = 5f;   // velocidade constante para frente
    public float jumpForce = 8f;
    private Rigidbody2D rb;

    [Header("Vida")]
    public int maxHealth = 3;
    public int currentHealth;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.12f;
    public LayerMask groundLayer;
    private bool isGrounded;

    [Header("FX")]
    public GameObject coletaParticlesPrefab;
    public GameObject danoParticlesPrefab;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Verifica se está no chão
        if (groundCheck != null)
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Pulo
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        // Movimento contínuo para frente
        rb.velocity = new Vector2(forwardSpeed, rb.velocity.y);
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if (danoParticlesPrefab != null)
            Instantiate(danoParticlesPrefab, transform.position, Quaternion.identity);

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("Fofuxo morreu!");
        gameObject.SetActive(false);
        GameManager.instance.GameOver();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fruta"))
        {
            // Recupera vida
            currentHealth = Mathf.Min(currentHealth + 1, maxHealth);

            if (coletaParticlesPrefab != null)
                Instantiate(coletaParticlesPrefab, transform.position, Quaternion.identity);

            GameManager.instance.AddFruit(1);
            Destroy(other.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Inimigo"))
        {
            TakeDamage(1);
        }
    }
}