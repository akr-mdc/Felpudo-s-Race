using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMover : MonoBehaviour
{
    [Header("Configurações do inimigo")]
    public float speed = 3f;           // velocidade para a esquerda
    public int dano = 1;               // quanto de dano causa ao player
    public float lifetime = 10f;       // tempo até ser destruído automaticamente

    private Rigidbody2D rb;
    private bool jaCausouDano = false; // impede dano múltiplo na mesma colisão

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime); // destrói após um tempo
    }

    void Update()
    {
        rb.velocity = new Vector2(-speed, rb.velocity.y); // movimento lateral constante
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Colisão com jogador
        if (other.CompareTag("Player") && !jaCausouDano)
        {
            jaCausouDano = true;

            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(dano);
            }

            // (Opcional) destrói o inimigo logo após causar dano
            Destroy(gameObject);
        }

        // Colisão com limite (fora da tela)
        if (other.CompareTag("Limite"))
        {
            Destroy(gameObject);
        }
    }
}
