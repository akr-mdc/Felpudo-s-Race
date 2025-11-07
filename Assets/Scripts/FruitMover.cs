using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitMover : MonoBehaviour
{
    public float speed = 3f;
    public float lifetime = 6f;
    public int cura = 1;

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
        if (collision.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.AddFruit(1);
                GameManager.instance.RecuperarVida(cura);
            }
            Destroy(gameObject);
        }

        if (collision.CompareTag("Limite"))
        {
            Destroy(gameObject);
        }
    }
}
