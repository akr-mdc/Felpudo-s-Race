using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    [Header("Dano e Vida")]
    public int danoPorInimigo = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movimento básico
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        rb.velocity = moveInput.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Se colidir com inimigo, tomar dano
        if (collision.CompareTag("Inimigo"))
        {
            if (GameManager.instance != null)
                GameManager.instance.TomarDano(danoPorInimigo);
        }

        // Se colidir com fruta, coletar (a fruta em si chama AddFruit)
        if (collision.CompareTag("Fruta"))
        {
            // Nada aqui — a fruta cuida disso
        }
    }
}
