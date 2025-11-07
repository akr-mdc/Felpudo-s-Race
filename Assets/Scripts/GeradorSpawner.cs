using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] inimigos;     // lista de inimigos possíveis
    public GameObject frutaPrefab;    // prefab da fruta

    [Header("Configuração de Geração")]
    public float tempoMin = 1.5f;     // intervalo mínimo entre gerações
    public float tempoMax = 3.5f;     // intervalo máximo
    public int frutasACadaXInimigos = 4; // gera 1 fruta a cada X inimigos gerados

    private int contadorInimigos = 0;

    void Start()
    {
        StartCoroutine(Gerar());
    }

    System.Collections.IEnumerator Gerar()
    {
        while (true)
        {
            float tempoEspera = Random.Range(tempoMin, tempoMax);
            yield return new WaitForSeconds(tempoEspera);

            // Checa se é hora de gerar uma fruta
            if (contadorInimigos >= frutasACadaXInimigos)
            {
                // Gera fruta e zera o contador
                Instantiate(frutaPrefab, transform.position, Quaternion.identity);
                contadorInimigos = 0;

                // Importante: não gera inimigo nesse ciclo!
                continue;
            }

            // Caso contrário, gera um inimigo aleatório
            if (inimigos.Length > 0)
            {
                int indice = Random.Range(0, inimigos.Length);
                Instantiate(inimigos[indice], transform.position, Quaternion.identity);
                contadorInimigos++;
            }
        }
    }
}
