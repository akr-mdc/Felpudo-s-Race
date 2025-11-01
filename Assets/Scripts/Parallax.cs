using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float startPosX, length;
    public GameObject cam;
    [Range(0f, 1f)] public float parallaxEffect = 0.5f;

    void Start()
    {
        startPosX = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        if (cam == null)
            cam = Camera.main.gameObject; // pega a Main Camera se não definido
    }

    void Update()
    {
        float camX = cam.transform.position.x;
        float dist = camX * parallaxEffect;
        float temp = camX * (1 - parallaxEffect);

        transform.position = new Vector3(startPosX + dist, transform.position.y, transform.position.z);

        if (temp > startPosX + length) startPosX += length;
        else if (temp < startPosX - length) startPosX -= length;
    }
}