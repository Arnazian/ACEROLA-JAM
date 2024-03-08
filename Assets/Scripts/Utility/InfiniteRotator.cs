using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteRotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f;

    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime, Space.Self);
    }
}