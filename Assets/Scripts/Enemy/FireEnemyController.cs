using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnemyController : MonoBehaviour
{
    [SerializeField] private float forwardMoveSpeed;
    [SerializeField] private float backwardsMoveSpeed;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    private Transform playerTransform;

    private bool canMove = false;
    private BaseEnemyActivation baseEnemyActivation;

    private void Start()
    {
        baseEnemyActivation = GetComponent<BaseEnemyActivation>();
        baseEnemyActivation.onActivate.AddListener(ActivateMovement);
        playerTransform = FindObjectOfType<CocosTopDownController>().transform;
    }

    private void Update()
    {
        HandleMovement();
    }
    void HandleMovement()
    {
        if (!canMove)
            return;
        if (DistanceToPlayer() < minDistance)
            transform.Translate(-Vector2.up * backwardsMoveSpeed * Time.deltaTime);
        else if (DistanceToPlayer() > maxDistance)
            transform.Translate(Vector2.up * forwardMoveSpeed * Time.deltaTime);
    }

    float DistanceToPlayer()
    {
        return Vector2.Distance(transform.position, playerTransform.position);  
    }

    void ActivateMovement()
    {
        canMove = true;
    }
}
