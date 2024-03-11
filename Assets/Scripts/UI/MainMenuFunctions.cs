using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class MainMenuFunctions : MonoBehaviour
{
    [SerializeField] private float gameStartFadeDuration;
    [SerializeField] private Material gameStartFade;
    [SerializeField] private Material foreGroundFade;
    [SerializeField] private float fadeDuration;
    [SerializeField] private float secondsBeforeLoadingScene;

    [SerializeField] private AudioSource ambiance;

    private void Start()
    {
       
       foreGroundFade.DOFloat(1f, "_FadeAmount", 0);

       gameStartFade.DOFloat(0f, "_FadeAmount", 0f).OnComplete(() =>
       gameStartFade.DOFloat(1f, "_FadeAmount", gameStartFadeDuration));
       FindObjectOfType<CursorController>().EnableFancyMouse();
    }
    public void StartGame()
    {
        ambiance.DOFade(0f, fadeDuration);
        foreGroundFade.DOFloat(0f, "_FadeAmount", fadeDuration).OnComplete(() =>
        {
            StartCoroutine(LoadGameScene());
        });
    }

    IEnumerator LoadGameScene()
    {
        yield return new WaitForSeconds(secondsBeforeLoadingScene);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
