using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExpandOnEnable : MonoBehaviour
{
    [SerializeField] private Transform transformToScale;
    [SerializeField] private float scaleDuration;
    private Vector3 growToSize;
    private Vector3 startSize;


    void Awake()
    {
        if (transformToScale == null)
            transformToScale = transform;
        startSize = new Vector3(0f, 0f, 1f);
        growToSize = transformToScale.localScale;
        transformToScale.DOScale(startSize, 0f);
    }

    private void OnEnable()
    {
        transformToScale.DOScale(growToSize, scaleDuration);
    }
    private void OnDisable()
    {
        transformToScale.DOScale(startSize, 0f);
    }

}
