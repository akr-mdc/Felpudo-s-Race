using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public float speed = 3f;
    public float lifetime = 6f;
    public int dano = 1;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Se colidir com o jogador, aplica dano via GameManager
        if (collision.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.TomarDano(dano);
            }

            Destroy(gameObject);
        }

        // Se colidir com o limite invisível, é destruído
        if (collision.CompareTag("Limite"))
        {
            Destroy(gameObject);
        }
    }
}

