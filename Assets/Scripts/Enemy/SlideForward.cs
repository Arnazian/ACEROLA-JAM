using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlideForward : MonoBehaviour
{
    [SerializeField] private float moveDuration;
    [SerializeField] private Vector2 moveCoolDown;
    [SerializeField] private Vector2 moveDistance;
    private float moveCoolDownCur;

    private bool isSliding = false;

    private bool canMove = true;

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
        Debug.Log("Started SLiding");
        isSliding = true;
        moveCoolDownCur = Random.Range(moveCoolDown.x, moveCoolDown.y);

        float randomDistance = Random.Range(moveDistance.x, moveDistance.y);
        Vector3 targetPosition = transform.position + (transform.up * randomDistance);

        transform.DOMove(targetPosition, moveDuration).OnComplete(() =>
            isSliding = false );
        Debug.Log("after started do move");
    }


}
