using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public float speed = 3f;
    public float lifetime = 6f;
    public int dano = 1;

    private void Start()
    {
        // Destrói o inimigo após um tempo para economizar processamento
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Move o inimigo continuamente da direita para a esquerda
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Se colidir com o jogador, aplica dano e destrói o inimigo
        if (collision.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.TomarDano(dano);
            }

            Destroy(gameObject);
        }

        // Se colidir com o limite invisível, destrói o inimigo
        if (collision.CompareTag("Limite"))
        {
            Destroy(gameObject);
        }
    }
}
