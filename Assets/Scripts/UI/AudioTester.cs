using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTester : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            audioSource.Play();
        }
    }
}
