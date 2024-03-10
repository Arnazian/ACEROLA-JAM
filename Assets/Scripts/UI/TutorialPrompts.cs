using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
public class TutorialPrompts : MonoBehaviour
{
    [SerializeField] private GameObject movePrompt;
    [SerializeField] private GameObject dashPrompt;
    [SerializeField] private GameObject shootPrompt;

    public void OpenMovePormpt()
    {
        movePrompt.SetActive(true);
        PauseGame.instance.DoPauseGame();
    }
    public void OpenDashPrompt()
    {
        dashPrompt.SetActive(true);
    }
    public void OpenShootPrompt()
    {
        shootPrompt.SetActive(true);
    }
 }
