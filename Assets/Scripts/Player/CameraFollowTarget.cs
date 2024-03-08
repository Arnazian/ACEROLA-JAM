using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private Transform playerTransform;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        SetPositionToMidpoint();
    }
    void SetPositionToMidpoint()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        mousePos.z = playerTransform.position.z;

        Vector3 middlePoint = (playerTransform.position + mousePos) / 2;

        float distance = Vector3.Distance(playerTransform.position, middlePoint);

        if (distance > maxDistance)
        {
            Vector3 direction = (middlePoint - playerTransform.position).normalized;
            middlePoint = playerTransform.position + direction * maxDistance;
        }
        transform.position = middlePoint;
    }
}
