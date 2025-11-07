using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] inimigosPrefabs; // arraste inimigos (terrestre/aereo)
    public GameObject frutaPrefab;       // opcional

    [Header("Timing")]
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 3f;

    [Header("Frutas por inimigos")]
    public int frutasPorInimigos = 4;
    private int inimigosGerados = 0;

    [Header("Parent (opcional)")]
    public Transform spawnParent;

    void Start()
    {
        StartCoroutine(LoopSpawn());
    }

    IEnumerator LoopSpawn()
    {
        while (true)
        {
            float wait = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(wait);

            if (inimigosPrefabs.Length > 0)
            {
                int idx = Random.Range(0, inimigosPrefabs.Length);
                GameObject spawned = Instantiate(inimigosPrefabs[idx], transform.position, Quaternion.identity, spawnParent);
                inimigosGerados++;
            }

            if (frutaPrefab != null && inimigosGerados % frutasPorInimigos == 0)
            {
                Vector3 pos = transform.position + new Vector3(1.5f, Random.Range(-0.5f, 1.5f), 0f);
                Instantiate(frutaPrefab, pos, Quaternion.identity, spawnParent);
            }
        }
    }
}
