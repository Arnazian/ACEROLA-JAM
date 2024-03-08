using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    [SerializeField] private float secondsToDestroy; 
    void Start()
    {
        StartCoroutine(CoroutineDestroyOverTime());
    }

    IEnumerator CoroutineDestroyOverTime()
    {
        yield return new WaitForSeconds(secondsToDestroy);
        Destroy(gameObject);
    }
}
