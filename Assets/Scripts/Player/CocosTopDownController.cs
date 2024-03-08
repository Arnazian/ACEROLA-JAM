using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class CocosTopDownController : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float shootStateMaxSpeed;
    private float activeMaxSpeed;

    [Header("Acceleration & Deceleration Variables")]
    [SerializeField] private float secondsToMaxSpeed;
    [SerializeField] private float secondsToZeroSpeed;

    [Header("Object References")]
    [SerializeField] private Animator anim;
    [SerializeField] private Transform cameraFollowTarget;

    private float curSpeed;

    private Vector2 curMoveDirection;
    private Vector2 diagonalMoveDirection;

    private bool previousMoveDiagonal = false;
    private bool waitForDiagonal = false;

    private Tween accelerateTween;
    private Tween decelerateTween;

    private bool isMoving = false;
    private bool canMove = true;
    private bool isAccelerating = false;
    private bool isDecelerating = false;

    private bool queueMovement = false;

    private Rigidbody2D rb;

    private void Awake()
    {
        activeMaxSpeed = maxSpeed;
        curSpeed = activeMaxSpeed;   
        rb = GetComponent<Rigidbody2D>();   
    }

    private void Update()
    {
        HandleMove();
    }

    void HandleMove()
    {
        if (!canMove)
            return;

        Vector2 activeMoveDirection = new Vector2(curMoveDirection.x, curMoveDirection.y);

        Vector2 move = activeMoveDirection * curSpeed * Time.deltaTime;

        Vector3 move3D = new Vector3(move.x, move.y, 0);

        transform.Translate(move3D, Space.World);
    }
    public void HandleInput(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            queueMovement = true;
            StartMovement(context.ReadValue<Vector2>());
        }
        else if (context.canceled)
        {
            queueMovement = false;
            StopMovement();
        }
    }    

    void StartMovement(Vector2 direction)
    {
        Vector2 newMove = direction;
        curMoveDirection = new Vector2(newMove.x, newMove.y).normalized;

        if (previousMoveDiagonal)
        {
            StopCoroutine(CoroutineControlDiagonalMovement());
            StartCoroutine(CoroutineControlDiagonalMovement());
        }

        if (Mathf.Abs(newMove.x) > 0.5f && Mathf.Abs(newMove.y) > 0.5f)
        {
            diagonalMoveDirection = newMove;
            previousMoveDiagonal = true;
        }
        else
            previousMoveDiagonal = false;

        if (!canMove)
            return;

        isMoving = true;
        isDecelerating = false;
        StartAccelerating();
    }

    void StopMovement()
    {
        if (waitForDiagonal)
            curMoveDirection = diagonalMoveDirection;

        isMoving = false;
        isAccelerating = false;
        StartDecelerating();
    }

    IEnumerator CoroutineControlDiagonalMovement()
    {
        waitForDiagonal = true;
        yield return new WaitForSeconds(0.15f);
        waitForDiagonal = false;
    }

    public void SetSpeedToMoveState()
    {
        activeMaxSpeed = maxSpeed;
        if (isMoving)
        {
            isAccelerating = false;
            StartAccelerating();
        }        
    }

    public void SetSpeedToShootState()
    {
        activeMaxSpeed = shootStateMaxSpeed;

        if (curSpeed > activeMaxSpeed)
            curSpeed = activeMaxSpeed;

        if (isMoving)
        {
            isAccelerating = false;
            StartAccelerating();
        }
    }

    public void ForceMovePlayer(float distance, float duration)
    {
        Vector3 targetPosition = transform.position + (Vector3)curMoveDirection.normalized * distance;
        rb.DOMove(targetPosition, duration);    
    }
    #region Enable / Disable Movement
    public void EnableMovement()
    {
        canMove = true;
        if (queueMovement)
            StartMovement(curMoveDirection);
    }
    public void DisableMovement()
    {
        StopMovement();
        canMove = false;
        isMoving = false;
    }
    #endregion   

    #region Acceleration And Deceleration
    void StartAccelerating()
    {
        if (isAccelerating)
            return;

        isAccelerating = true;
        KillAccelerationAndDecelerationTweens();

        if (curSpeed < activeMaxSpeed)
        {
            accelerateTween = DOTween.To(() => curSpeed, x => curSpeed = x, activeMaxSpeed, secondsToMaxSpeed).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                isAccelerating = false;
            }); ;
        }
    }

    void StartDecelerating()
    {
        if (isDecelerating)
            return;

        isDecelerating = true;
        KillAccelerationAndDecelerationTweens();


        if (curSpeed > 0f)
        {
            decelerateTween = DOTween.To(() => curSpeed, x => curSpeed = x, 0f, secondsToZeroSpeed).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                isDecelerating = false;
            }); ;
        }
    }

    void KillAccelerationAndDecelerationTweens()
    {
        accelerateTween.Kill();
        decelerateTween.Kill();
    }

    #endregion
}
