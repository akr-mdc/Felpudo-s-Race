using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Atributos do Jogador")]
    public int vidaMaxima = 3;
    public int vidaAtual;
    public float forcaPulo = 8f;
    public float invulnerabilidadeTempo = 1.0f;

    [Header("Detecção de Chão")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Efeitos e Sons (opcional)")]
    public GameObject hitParticlesPrefab;
    public AudioSource puloSom;
    public AudioSource danoSom;
    public AudioSource coletaSom;

    private Rigidbody2D rb;
    private bool estaNoChao;
    private bool invulneravel = false;
    private float tempoInvulneravel;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        vidaAtual = vidaMaxima;
        GameManager.instance.AtualizarVida(vidaAtual);
    }

    void Update()
    {
        // Verifica chão
        estaNoChao = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Pulo
        if (Input.GetButtonDown("Jump") && estaNoChao)
        {
            rb.velocity = new Vector2(rb.velocity.x, forcaPulo);
            if (puloSom != null) puloSom.Play();
        }

        // Timer de invulnerabilidade
        if (invulneravel && Time.time - tempoInvulneravel > invulnerabilidadeTempo)
            invulneravel = false;
    }

    // Chamado pelos inimigos ao colidir
    public void TakeDamage(int dano)
    {
        if (invulneravel) return;

        vidaAtual -= dano;
        if (vidaAtual < 0) vidaAtual = 0;

        GameManager.instance.AtualizarVida(vidaAtual);

        if (hitParticlesPrefab != null)
            Instantiate(hitParticlesPrefab, transform.position, Quaternion.identity);

        if (danoSom != null) danoSom.Play();

        invulneravel = true;
        tempoInvulneravel = Time.time;

        if (vidaAtual <= 0)
            GameManager.instance.GameOver();
    }

    // Chamado pelas frutas
    public void Curar(int quantidade)
    {
        vidaAtual += quantidade;
        if (vidaAtual > vidaMaxima) vidaAtual = vidaMaxima;

        GameManager.instance.AtualizarVida(vidaAtual);

        if (coletaSom != null) coletaSom.Play();
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
