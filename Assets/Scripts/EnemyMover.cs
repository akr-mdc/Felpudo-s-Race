using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public float speed = 3f;
    public int dano = 1;
    public float lifeTime = 8f; // segundos até auto-destruição
    public bool useTrigger = true; // se true, require Collider2D isTrigger = true

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // schedule destruction
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // move para a esquerda no eixo X (frame independent)
        rb.velocity = new Vector2(-speed, rb.velocity.y);
    }

    // se usarmos triggers:
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // causar dano ao jogador via GameManager/Player
            if (other.TryGetComponent<PlayerController>(out var player))
            {
                player.RecebeDano(dano);
            }
            else
            {
                GameManager.instance?.TomarDano(dano);
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("Limite"))
        {
            Destroy(gameObject);
        }
    }

    // caso erro e estiver usando colisão normal:
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            if (col.collider.TryGetComponent<PlayerController>(out var player))
            {
                player.RecebeDano(dano);
            }
            else
            {
                GameManager.instance?.TomarDano(dano);
            }
            Destroy(gameObject);
        }
    }
}
