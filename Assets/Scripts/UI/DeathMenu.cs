using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] private GameObject deathMenu;

    [SerializeField] private Material bgMaterial;
    [SerializeField] private Material fgMaterial;

    [SerializeField] private TextMeshProUGUI deathText;
    [SerializeField] private TextMeshProUGUI[] buttonTexts;


    [Header("Durations")]
    [SerializeField] private float bgFadeInDuration;
    [SerializeField] private float fgFadeInDuration;
    [SerializeField] private float textFadeInDuration;
    [SerializeField] private float buttonFadeInDuration;


    void Start()
    {
          deathMenu.SetActive(false);
    }

    public void EnableDeathMenu()
    {
        deathMenu.SetActive(true);
        AudioManager.instance.FadeMusicOut();
        FindObjectOfType<CursorController>().EnableFancyMouse();
        FindObjectOfType<DirectionArrow>().gameObject.SetActive(false);
        bgMaterial.DOFloat(1f, "_FadeAmount", 0f);
        fgMaterial.DOFloat(1f, "_FadeAmount", 0f);

        deathText.DOFade(0f, 0f);
        foreach (TextMeshProUGUI t in buttonTexts)
            t.DOFade(0f, 0f);

        bgMaterial.DOFloat(0f, "_FadeAmount", bgFadeInDuration);
        deathText.DOFade(1f, textFadeInDuration);
        foreach (TextMeshProUGUI t in buttonTexts)
            t.DOFade(1f, buttonFadeInDuration);
    }

    public void ReloadScene()
    {
        int curScene = SceneManager.GetActiveScene().buildIndex;

        fgMaterial.DOFloat(0f, "_FadeAmount", fgFadeInDuration).OnComplete(() =>
            SceneManager.LoadScene(curScene));        
    }

    public void MainMenu()
    {
        fgMaterial.DOFloat(0f, "_FadeAmount", fgFadeInDuration).OnComplete(() =>
          SceneManager.LoadScene(0));
    }
}
