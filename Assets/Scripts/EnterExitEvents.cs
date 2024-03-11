using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnterExitEvents : MonoBehaviour
{
    public UnityEvent onEnter;
    public UnityEvent onExit;

    private bool playerPresent = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        playerPresent = true;
        onEnter.Invoke();   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        playerPresent = false;
        onExit.Invoke();
    }

}
