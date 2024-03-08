using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource hitObject;

    [SerializeField] private AudioSource hitEnemy;
    [SerializeField] private AudioSource music;

    [SerializeField] private Vector2 hitObjectPitch;
    [SerializeField] private Vector2 hitEnemyPitch;

    [SerializeField] private float musicTweenDuration;
    private float musicVolume;

    private void Start()
    {
        musicVolume = music.volume;
        music.volume = 0;
    }
    public void PlayHitEnemy()
    {
        hitEnemy.pitch = Random.Range(hitEnemyPitch.x, hitEnemyPitch.y);
        hitEnemy.Play();
    }
    public void PlayHitObject()
    {
        hitObject.pitch = Random.Range(hitObjectPitch.x, hitObjectPitch.y);
        hitObject.Play();
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
