using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorSpawner : MonoBehaviour
{
    [Header("Prefabs a serem gerados")]
    public GameObject inimigoPrefab;
    public GameObject frutaPrefab;

    [Header("Configurações de tempo")]
    public float tempoMin = 1.5f;  // intervalo mínimo entre gerações
    public float tempoMax = 3.5f;  // intervalo máximo entre gerações

    [Header("Configurações de posição")]
    public float posYMin = -2f;    // posição vertical mínima
    public float posYMax = 2f;     // posição vertical máxima

    [Header("Probabilidades de geração")]
    [Range(0, 100)]
    public int chanceDeFruta = 30; // porcentagem de chance de gerar fruta
    // o restante (100 - chanceDeFruta) será chance de gerar inimigo

    void Start()
    {
        StartCoroutine(GerarObjetos());
    }

    System.Collections.IEnumerator GerarObjetos()
    {
        while (true)
        {
            float tempo = Random.Range(tempoMin, tempoMax);
            yield return new WaitForSeconds(tempo);

            int sorteio = Random.Range(0, 100);

            if (sorteio < chanceDeFruta && frutaPrefab != null)
            {
                GerarFruta();
            }
            else if (inimigoPrefab != null)
            {
                GerarInimigo();
            }
        }
    }

    void GerarFruta()
    {
        Vector3 pos = new Vector3(transform.position.x, Random.Range(posYMin, posYMax), 0);
        Instantiate(frutaPrefab, pos, Quaternion.identity);
    }

    void GerarInimigo()
    {
        Vector3 pos = new Vector3(transform.position.x, Random.Range(posYMin, posYMax), 0);
        Instantiate(inimigoPrefab, pos, Quaternion.identity);
    }
}
