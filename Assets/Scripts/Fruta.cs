using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruta : MonoBehaviour
{
    public int valorFruta = 1;   // Pontos de fruta
    public int vidaRecuperada = 1; // Vida que o jogador recupera ao coletar

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.AddFruit(valorFruta);
                GameManager.instance.RecuperarVida(vidaRecuperada);
            }

            Destroy(gameObject); // Destroi a fruta após coletar
        }
    }
}
