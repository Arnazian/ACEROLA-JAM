using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonHighlightController : MonoBehaviour
{
    [SerializeField] private Transform objectToScale;
    [SerializeField] private Vector3 endScale;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private float scaleDuration;
    [SerializeField] private float moveDuration;
    [SerializeField] private float muzzleFadeDuration;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private Material muzzleMaterial;
    [SerializeField] private Material muzzleMaterial2;

    private Vector3 originalPosition;
    private Vector3 originalScale;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        originalScale = objectToScale.localScale;
        originalPosition = objectToScale.localPosition;

        if (muzzleMaterial != null)
        {
            muzzleMaterial.DOFloat(1f, "_FadeAmount", 0);
            muzzleMaterial2.DOFloat(1f, "_FadeAmount", 0);
        }
       
    }
    public void MouseEnterVisuals()
    {
        audioSource.Play();

        if (particles != null)
            particles.Play();
        if (muzzleMaterial != null)
        {
            muzzleMaterial.DOFloat(0f, "_FadeAmount", muzzleFadeDuration);
            muzzleMaterial2.DOFloat(0f, "_FadeAmount", muzzleFadeDuration);
        }
            
        objectToScale.DOScale(endScale, scaleDuration);
        Vector3 positionToMoveTo = originalPosition + positionOffset;
        objectToScale.DOLocalMove(positionToMoveTo, moveDuration);
    }

    public void MouseExitVisuals()
    {
        if (particles != null)
            particles.Stop();

        if (muzzleMaterial != null)
        {
            muzzleMaterial.DOFloat(1f, "_FadeAmount", muzzleFadeDuration);
            muzzleMaterial2.DOFloat(1f, "_FadeAmount", muzzleFadeDuration);
        }
      
        objectToScale.DOScale(originalScale, scaleDuration);
        objectToScale.DOLocalMove(originalPosition, moveDuration);
    }
}
