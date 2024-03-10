using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
public class PromptTweens : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] private Image[] backgroundImages;
    [SerializeField] private TextMeshProUGUI[] texts;
    [SerializeField] private Button continueButton;

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

    private List<Vector3> backgroundOriginalPositions = new List<Vector3>();
    private List<Vector3> textOriginalPositions = new List<Vector3>();
    private Vector3 buttonOriginalPosition;
    private List<float> backgroundOriginalFade = new List<float>();

    private List<Tween> bgTweens = new List<Tween>();
    private List<Tween> textTweens = new List<Tween>();
    private List<Tween> buttonTweens = new List<Tween>();


    private void Awake()
    {
        SetOriginalValues();
        SetEverythingToStartingPositions();
    }

    private void OnEnable()
    {
        SetEverythingToStartingPositions();
        PlayerStateController.instance.StunPlayer();

        FadeImagesIn();
        FadeTextsIn();
        FadeButtonIn();
    }

    public void DisableTweens()
    {
        PlayerStateController.instance.UnStunPlayer();
        PauseGame.instance.DoResumeGame();
        gameObject.SetActive(false);
    }

    void SetOriginalValues()
    {
        foreach (Image image in backgroundImages)
        {
            backgroundOriginalPositions.Add(image.transform.localPosition);
            backgroundOriginalFade.Add(image.color.a);
        }
        foreach (TextMeshProUGUI tmp in texts)        
            textOriginalPositions.Add(tmp.transform.localPosition);

        buttonOriginalPosition = continueButton.transform.localPosition;   
    }

    void SetEverythingToStartingPositions()
    {
        // Set background images to starting positions
        for (int i = 0; i < backgroundImages.Length; i++)
        {
            backgroundImages[i].color = new Color(backgroundImages[i].color.r, backgroundImages[i].color.g, backgroundImages[i].color.b, 0f);
            Vector3 bgMovePos = backgroundOriginalPositions[i] + backgroundPositionOffset[i];
            backgroundImages[i].transform.localPosition = bgMovePos;
        }

        // Set texts to starting positions
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].color = new Color(texts[i].color.r, texts[i].color.g, texts[i].color.b, 0f);
            Vector3 textMovePos = textOriginalPositions[i] + textPositionOffset[i];
            texts[i].transform.localPosition = textMovePos;
        }

        // Set button to starting position
        TextMeshProUGUI buttonText = continueButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.color = new Color(buttonText.color.r, buttonText.color.g, buttonText.color.b, 0f);
        Vector3 buttonMovePos = buttonOriginalPosition + buttonPositionOffset;
        continueButton.transform.localPosition = buttonMovePos;
    }
    void FadeImagesIn()
    {
        foreach (Tween t in bgTweens)
            t.Kill();
        bgTweens.Clear();

        for (int i = 0; i < backgroundImages.Length; i++)
        {
            bgTweens.Add(backgroundImages[i].DOFade(backgroundOriginalFade[i], backgroundFadeDuration).SetDelay(backgroundFadeDelay).SetUpdate(true));
            bgTweens.Add(backgroundImages[i].transform.DOLocalMove(backgroundOriginalPositions[i], backgroundMoveDuration).SetDelay(backgroundMoveDelay).SetUpdate(true));
        }
    }

    void FadeTextsIn()
    {
        foreach (Tween t in textTweens)
            t.Kill();
        textTweens.Clear(); 

        for (int i = 0; i < texts.Length; i++)
        {
            textTweens.Add(texts[i].DOFade(1f, textFadeDuration[i]).SetDelay(textFadeDelay[i]).SetUpdate(true));
            textTweens.Add(texts[i].transform.DOLocalMove(textOriginalPositions[i], textMoveDuration[i]).SetDelay(textMoveDelay[i]).SetUpdate(true));
        }
    }

    void FadeButtonIn()
    {
        foreach (Tween t in buttonTweens)
            t.Kill();
        buttonTweens.Clear();

        TextMeshProUGUI buttonText = continueButton.GetComponentInChildren<TextMeshProUGUI>();  
        buttonText.DOFade(0f, 0f).SetUpdate(true);

        Vector3 movePos = buttonOriginalPosition + buttonPositionOffset;
        continueButton.transform.DOLocalMove(movePos, 0f).SetUpdate(true);

        buttonTweens.Add(buttonText.DOFade(1f, buttonFadeDuration).SetDelay(buttonFadeDelay).SetUpdate(true));
        buttonTweens.Add(continueButton.transform.DOLocalMove(buttonOriginalPosition, buttonMoveDuration).SetDelay(buttonMoveDelay).SetUpdate(true));
    }

}
