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
    [SerializeField] private GameObject compassPrompt;

    public void OpenMovePormpt()
    {
        movePrompt.SetActive(true);
        PauseGame.instance.DoPauseGame();
        PlayerStateController.instance.StunPlayer();
    }
    public void OpenDashPrompt()
    {
        dashPrompt.SetActive(true);
        PauseGame.instance.DoPauseGame();
        PlayerStateController.instance.StunPlayer();
    }
    public void OpenShootPrompt()
    {
        shootPrompt.SetActive(true);
        PauseGame.instance.DoPauseGame();
        PlayerStateController.instance.StunPlayer();
    }
    public void OpenCompassPrompt()
    {
        compassPrompt.SetActive(true);
        PauseGame.instance.DoPauseGame();
        PlayerStateController.instance.StunPlayer();
    }
}
