using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
public class TutorialPrompts : MonoBehaviour
{
    [Header("Move References")]
    [SerializeField] private Image moveBG;
    [SerializeField] private TextMeshProUGUI moveUpperText;
    [SerializeField] private TextMeshProUGUI moveLowerText;
    [SerializeField] private TextMeshProUGUI moveButtonText;
    [SerializeField] private GameObject moveButton;

    [Header("Move Variables")]
    [SerializeField] private float upperTextMoveAmount;
    [SerializeField] private float lowerTextMoveAmount;
    [SerializeField] private float buttonTextMoveAmount;

    [SerializeField] private float bgFadeDuration;
    [SerializeField] private float upperTextMoveDuration;
    [SerializeField] private float lowerTextMoveDuration;
    [SerializeField] private float buttonMoveDuration;

    [SerializeField] private float bgFadeDelay;
    [SerializeField] private float upperTextMoveDelay;
    [SerializeField] private float lowerTextMoveDelay;
    [SerializeField] private float buttonMoveDelay;


    [Header("Move Variables")]
    [SerializeField] private float two;

    [Header("Move Variables")]
    [SerializeField] private float three;
    public void CloseMove()
    {
        moveBG.DOFade(0f, bgFadeDuration).SetDelay(bgFadeDelay);

        moveUpperText.DOFade(0, upperTextMoveDuration).SetDelay(upperTextMoveDelay);
        moveUpperText.transform.DOLocalMoveY(upperTextMoveAmount, upperTextMoveDuration).SetDelay(upperTextMoveDelay);

        moveButtonText.DOFade(0, lowerTextMoveDuration).SetDelay(lowerTextMoveDelay);
        moveButton.transform.DOMoveY(lowerTextMoveAmount, lowerTextMoveDuration).SetDelay(lowerTextMoveDelay);

        moveUpperText.DOFade(0, buttonMoveDuration).SetDelay(buttonMoveDelay);
        moveUpperText.transform.DOMoveY(buttonTextMoveAmount, buttonMoveDuration).SetDelay(buttonMoveDelay);
    }
    public void CloseDash()
    {

    }
    public void CloseShoot()
    {

    }
}
