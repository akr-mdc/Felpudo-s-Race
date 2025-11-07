using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLooper : MonoBehaviour
{
    public float speed = 2f;             // Velocidade de deslocamento (ajuste por camada)
    private float startX;                // Posição inicial
    private float width;                 // Largura da imagem

    void Start()
    {
        // Guarda posição inicial
        startX = transform.position.x;

        // Tenta descobrir a largura automaticamente
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
            width = sprite.bounds.size.x;
        else
            width = 10f; // valor padrão caso não tenha sprite
    }

    void Update()
    {
        // Move o fundo para a esquerda
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Se a imagem saiu completamente da tela, reseta posição
        if (transform.position.x < startX - width)
        {
            Vector3 newPos = transform.position;
            newPos.x += width * 2f; // reposiciona à direita
            transform.position = newPos;
        }
    }
}
