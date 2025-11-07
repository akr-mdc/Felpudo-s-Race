using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FruitMover : MonoBehaviour
{
    public float speed = 3f;
    public int valor = 1;     // quantas frutas conta
    public int cura = 1;      // vida recuperada ao coletar
    public float lifeTime = 8f; // auto-destrói após x segundos
    public GameObject coletaParticlesPrefab;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        rb.velocity = new Vector2(-speed, rb.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // contabiliza frutas usando o singleton (assegure que GameManager.instance existe)
            if (GameManager.instance != null)
                GameManager.instance.AddFruit(valor);
            else
                Debug.LogWarning("FruitMover: GameManager.instance é nulo.");

            // cura o player (se existir)
            var player = other.GetComponent<PlayerController>();
            if (player != null)
                player.Curar(cura);

            if (coletaParticlesPrefab != null)
                Instantiate(coletaParticlesPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
        else if (other.CompareTag("Limite"))
        {
            Destroy(gameObject);
        }
    }
}
