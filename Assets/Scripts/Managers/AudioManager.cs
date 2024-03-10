using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource hitObject;

    [SerializeField] private AudioSource hitEnemy;
    [SerializeField] private AudioSource hitEnemyStrong;
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource enemyDeath;
    [SerializeField] private AudioSource closeUI;
    [SerializeField] private AudioSource bossScream;

    [SerializeField] private Vector2 hitObjectPitch;
    [SerializeField] private Vector2 hitEnemyPitch;
    [SerializeField] private Vector2 hitEnemyStrongPitch;

    [SerializeField] private float musicTweenDuration;
    private float musicVolume;

    private void Start()
    {
        musicVolume = music.volume;
        music.volume = 0;
    }
    public void PlayBossScream()
    {
        bossScream.Play();
    }
    public void PlayCloseUI()
    {
        closeUI.Play();
    }
    public void PlayHitEnemy()
    {
        hitEnemy.pitch = Random.Range(hitEnemyPitch.x, hitEnemyPitch.y);
        hitEnemy.Play();
        hitEnemyStrong.pitch = Random.Range(hitEnemyStrongPitch.x, hitEnemyStrongPitch.y);
        hitEnemyStrong.Play();
    }
    public void PlayHitObject()
    {
        hitObject.pitch = Random.Range(hitObjectPitch.x, hitObjectPitch.y);
        hitObject.Play();
    }

    public void PlayEnemyDeath()
    {
        enemyDeath.Play();
    }

    public void PlayMusic()
    {        
        music.Play();
        music.DOFade(musicVolume, musicTweenDuration);
    }
    #region Singleton Implementation
    public static AudioManager instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject); 
        instance = this;
    }
    #endregion
}
