using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;

public class DisablePromptTweens : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] private Image[] backgroundImages;
    [SerializeField] private TextMeshProUGUI[] texts;
    [SerializeField] private Button continueButton;
    [SerializeField] private float secondsToDisable;
    [Header("Duration Variables")]
    [SerializeField] private float backgroundFadeDuration;
    [SerializeField] private float backgroundMoveDuration;

    [SerializeField] private float[] textFadeDuration;
    [SerializeField] private float[] textMoveDuration;

    [SerializeField] private float buttonFadeDuration;
    [SerializeField] private float buttonMoveDuration;

    [Header("Delay Variables")]
    [SerializeField] private float backgroundFadeDelay;
    [SerializeField] private float backgroundMoveDelay;

    [SerializeField] private float[] textFadeDelay;
    [SerializeField] private float[] textMoveDelay;

    [SerializeField] private float buttonFadeDelay;
    [SerializeField] private float buttonMoveDelay;

    [Header("Move Offsets")]
    [SerializeField] private Vector3[] backgroundPositionOffset;
    [SerializeField] private Vector3[] textPositionOffset;
    [SerializeField] private Vector3 buttonPositionOffset;

    [Header("Other")]
    [SerializeField] private ParticleSystem disableParticles;
    private List<Tween> bgTweens = new List<Tween>();
    private List<Tween> textTweens = new List<Tween>();
    private List<Tween> buttonTweens = new List<Tween>();

    private void OnEnable()
    {
        StopCoroutine(CoroutineDisableGameObject());
        KillAllTweens();
    }
    public void FadeEverythingOut()
    {
        EventSystem.current.SetSelectedGameObject(null);
        FadeImagesOut();
        FadeTextsOut();
        FadeButtonOut();
        disableParticles.Play();
        StartCoroutine(CoroutineDisableGameObject());
    }

    IEnumerator CoroutineDisableGameObject()
    {
        yield return new WaitForSeconds(secondsToDisable);
        gameObject.SetActive(false);
    }
    void FadeImagesOut()
    {
        foreach (Tween t in bgTweens)
            t.Kill();
        bgTweens.Clear();

        for (int i = 0; i < backgroundImages.Length; i++)
        {
            bgTweens.Add(backgroundImages[i].DOFade(0f, backgroundFadeDuration).SetDelay(backgroundFadeDelay).SetUpdate(true));
            Vector3 newPos = backgroundImages[i].transform.localPosition + backgroundPositionOffset[i];
            bgTweens.Add(backgroundImages[i].transform.DOLocalMove(newPos, backgroundMoveDuration).SetDelay(backgroundMoveDelay).SetUpdate(true).SetEase(Ease.InCirc));
        }
    }

    void FadeTextsOut()
    {
        foreach (Tween t in textTweens)
            t.Kill();
        textTweens.Clear();

        for (int i = 0; i < texts.Length; i++)
        {
            textTweens.Add(texts[i].DOFade(0f, textFadeDuration[i]).SetDelay(textFadeDelay[i]).SetUpdate(true));
            Vector3 newPos = texts[i].transform.localPosition + textPositionOffset[i];
            textTweens.Add(texts[i].transform.DOLocalMove(newPos, textMoveDuration[i]).SetDelay(textMoveDelay[i]).SetUpdate(true).SetEase(Ease.InCirc));
        }
    }

    void FadeButtonOut()
    {
        foreach (Tween t in buttonTweens)
            t.Kill();
        buttonTweens.Clear();

        TextMeshProUGUI buttonText = continueButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.DOFade(0f, 0f).SetUpdate(true);

        Vector3 movePos = continueButton.transform.localPosition + buttonPositionOffset;
        buttonTweens.Add(buttonText.DOFade(0f, buttonFadeDuration).SetDelay(buttonFadeDelay).SetUpdate(true));
        buttonTweens.Add(continueButton.transform.DOLocalMove(movePos, buttonMoveDuration).SetDelay(buttonMoveDelay).SetUpdate(true).SetEase(Ease.InCirc));
    }

    void KillAllTweens()
    {
        foreach (Tween t in bgTweens)
            t.Kill();
        bgTweens.Clear();

        foreach (Tween t in textTweens)
            t.Kill();
        textTweens.Clear();

        foreach (Tween t in buttonTweens)
            t.Kill();
        buttonTweens.Clear();
    }
}
