using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitMover : MonoBehaviour
{
    public float speed = 3f;
    public float lifetime = 6f;
    public int valorFruta = 1;
    public int vidaRecuperada = 1;

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
        // Se tocar no jogador
        if (collision.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.AddFruit(valorFruta);
                GameManager.instance.RecuperarVida(vidaRecuperada);
            }

            Destroy(gameObject);
        }

        // Se sair da tela
        if (collision.CompareTag("Limite"))
        {
            Destroy(gameObject);
        }
    }
}
