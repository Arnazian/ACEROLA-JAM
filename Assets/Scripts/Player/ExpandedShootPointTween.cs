using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExpandedShootPointTween : MonoBehaviour
{
    [SerializeField] private Transform transformToModify;
    [SerializeField] private Material materialToModify;

    [SerializeField] private float fadeDuration;
    [SerializeField] private float scaleDuration;

    [SerializeField] private float materialStartFade = 0.6f;
    [SerializeField] private Vector3 endSize;

    private Tween scaleTween;
    private Tween fadeTween;    
    private Vector3 startSize;
    void Start()
    {
        startSize = transformToModify.localScale;
        materialToModify.DOFloat(materialStartFade, "_FadeAmount", 0f);

        PlayerStateController.instance.onMoveStateEnter.AddListener(DoShrink);
        PlayerStateController.instance.onShootStateEnter.AddListener(DoExpand);

    }

    public void DoExpand()
    {
        KillAllTweens();
        scaleTween = transformToModify.DOScale(endSize, scaleDuration);
        fadeTween = materialToModify.DOFloat(0f, "_FadeAmount", fadeDuration);
    }

    public void DoShrink()
    {
        KillAllTweens();
        scaleTween = transformToModify.DOScale(startSize, scaleDuration);
        fadeTween = materialToModify.DOFloat(materialStartFade, "_FadeAmount", fadeDuration);
    }

    void KillAllTweens()
    {
        fadeTween.Kill();
        scaleTween.Kill();
    }
}
