using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEvent : MonoBehaviour
{
    [SerializeField] private bool singleTimeTrigger = true;
    [SerializeField] private UnityEvent onTriggerEnterEvent;
    private bool hasTriggered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasTriggered)
            return;

        if(collision.CompareTag("Player"))
        {
            onTriggerEnterEvent.Invoke();
            hasTriggered = true;
        }
    }
}
