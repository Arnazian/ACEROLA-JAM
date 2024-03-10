using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PauseGame : MonoBehaviour
{
    public UnityEvent onGamePause;
    public UnityEvent onGameResume;

    public void DoPauseGame()
    {
        onGamePause.Invoke();
        Time.timeScale = 0f;
    }

    public void DoResumeGame()
    {
        onGameResume.Invoke();
        Time.timeScale = 1f;
    }

    public static PauseGame instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
}