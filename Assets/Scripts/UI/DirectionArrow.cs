using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DirectionArrow : MonoBehaviour
{
    [SerializeField] private Transform arrowToScale;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform target;

    [SerializeField] private float highlightDuration;
    [SerializeField] private Vector3 highlightScale;
    private Vector3 originalScale;
    private Tween scalingTween;

    void Start()
    {
        originalScale = arrowToScale.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        FaceTarget();
    }

    void FaceTarget()
    {
        if (target == null)
            return;

        Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(target.position);
        Vector3 direction = (target.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void HighlightArrow()
    {
        scalingTween = arrowToScale.DOScale(highlightScale, highlightDuration)
            .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetUpdate(true);
    }

    public void UnHighlightArrow()
    {
        scalingTween.Kill();
        arrowToScale.DOScale(originalScale, highlightDuration);
    }
}
