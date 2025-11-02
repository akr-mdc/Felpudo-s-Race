using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruta : MonoBehaviour
{
    [Header("Configuração da Fruta")]
    public int valor = 1; // quantidade de frutas que conta para a vitória
    public int cura = 1;  // quanto de vida recupera ao coletar
    public GameObject coletaParticlesPrefab; // partículas opcionais

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Recupera vida e incrementa frutas no GameManager
            GameManager.instance.RecuperarVida(cura);
            GameManager.instance.AddFruit(valor);

            // Instancia partículas de coleta
            if (coletaParticlesPrefab != null)
                Instantiate(coletaParticlesPrefab, transform.position, Quaternion.identity);

            // Destroi o objeto
            Destroy(gameObject);
        }
    }
}
