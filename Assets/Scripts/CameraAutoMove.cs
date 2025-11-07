using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAutoMove : MonoBehaviour
{
    public Vector3 startPosition = new Vector3(0f, 0f, -10f);
    public float moveSpeed = 1f; // velocidade de movimento da câmera (horizontal)
    public bool moveHorizontally = true;
    public bool moveVertically = false;

    void Start()
    {
        transform.position = startPosition;
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;

        if (moveHorizontally)
            pos.x += moveSpeed * Time.deltaTime;

        if (moveVertically)
            pos.y += Mathf.Sin(Time.time * 0.5f) * 0.01f; // movimento suave opcional

        transform.position = pos;
    }
}
