using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [Range(0, 20)] [SerializeField] private float smoothSpeed = 10f;

    private Vector3 offset;
    private void Start()
    {
        offset = transform.position;
    }

    private void Update()
    {
        Vector3 desiredPos = target.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);

        transform.position = smoothPos;
    }
}
