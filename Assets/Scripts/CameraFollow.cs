using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Configuração de Seguimento")]
    public Transform target;          // o objeto a ser seguido (Fofuxo)
    public float smoothSpeed = 0.15f; // suavidade do movimento
    public Vector3 offset;            // deslocamento da câmera em relação ao player

    void LateUpdate()
    {
        if (target == null) return;

        // posição desejada com deslocamento
        Vector3 desiredPosition = target.position + offset;

        // suaviza a transição entre posição atual e desejada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // aplica movimento somente no plano X/Y, mantendo o Z fixo
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
