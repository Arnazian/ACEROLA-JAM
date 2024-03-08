using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveTowardsPlayer : MonoBehaviour
{
   private Transform player;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float distanceToPlayer = 2f;
    [SerializeField] private bool canMove = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        if (!canMove)
            return;

        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer > this.distanceToPlayer)
        {
            transform.position += directionToPlayer.normalized * speed * Time.deltaTime;
        }
    }

    public void SetCanMove(bool newValue)
    {
        canMove = newValue;
    }

    public void MoveToPosition(Vector3 targetPosition, float moveDuration)
    {       
        transform.DOMove(targetPosition, moveDuration);
    }
}
