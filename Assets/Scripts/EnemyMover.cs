using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public float speed = 3f;
    public float lifetime = 6f; // tempo até ser destruído

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
        // Dano ao player
        if (collision.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.TomarDano(1);
            }
            Destroy(gameObject);
        }

        // Se atingir o limite invisível, é destruído
        if (collision.CompareTag("Limite"))
        {
            Destroy(gameObject);
        }
    }
}
