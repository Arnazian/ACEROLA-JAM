using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class SlideForward : MonoBehaviour
{
    [SerializeField] private float moveDuration;
    [SerializeField] private Vector2 moveCoolDown;
    [SerializeField] private Vector2 moveDistance;
    private Rigidbody2D rb;
    private float moveCoolDownCur;

    private bool isSliding = false;

    private bool canMove = false;
    private BaseEnemyActivation baseEnemyActivation;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        baseEnemyActivation = GetComponent<BaseEnemyActivation>();
        baseEnemyActivation.onActivate.AddListener(ActivateMovement);
    }

    void ActivateMovement()
    {
        canMove = true;
    }    

    void Update()
    {
        HandleCoolDown();
    }

    void HandleCoolDown()
    {
        if (!canMove || isSliding)
            return;

        if (moveCoolDownCur <= 0)        
            DoSlideForward();        
        else
            moveCoolDownCur -= Time.deltaTime;
    }

    void DoSlideForward()
    {
        isSliding = true;
        moveCoolDownCur = Random.Range(moveCoolDown.x, moveCoolDown.y);

        float randomDistance = Random.Range(moveDistance.x, moveDistance.y);
        Vector3 targetPosition = transform.position + (transform.up * randomDistance);

        rb.DOMove(targetPosition, moveDuration).OnComplete(() =>
            isSliding = false );
    }


}
