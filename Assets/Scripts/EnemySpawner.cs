using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] inimigos;       // array de inimigos a gerar
    public GameObject frutaPrefab;       // prefab de fruta
    public float minTime = 1f;
    public float maxTime = 3f;
    public int frutasAposXInimigos = 3; // gera uma fruta após x inimigos

    private int inimigosGerados = 0;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    System.Collections.IEnumerator SpawnLoop()
    {
        while (true)
        {
            float wait = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(wait);

            // Spawn inimigo aleatório
            int idx = Random.Range(0, inimigos.Length);
            GameObject inimigo = Instantiate(inimigos[idx], transform.position, Quaternion.identity);

            inimigosGerados++;

            // Gerar fruta após X inimigos
            if (inimigosGerados % frutasAposXInimigos == 0)
            {
                Instantiate(frutaPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
            }
        }
    }
}
