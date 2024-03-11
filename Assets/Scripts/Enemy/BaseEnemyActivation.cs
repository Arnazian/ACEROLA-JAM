using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class BaseEnemyActivation : MonoBehaviour
{
    [SerializeField] private bool activateOnStart = true;
    public UnityEvent onActivate;

    [Header("Growing")]
    [SerializeField] private bool doGrow = true;
    [SerializeField] private Transform[] objectsToGrow;
    [SerializeField] private float growDuration;
    private List<Vector3> originalScales = new List<Vector3>();

    [Header("Activation Variables")]
    [SerializeField] private float secondsToActivate;
    [SerializeField] private GameObject[] objectsToEnable;

    
    void Start()
    {
        foreach (Transform t in objectsToGrow)
            originalScales.Add(t.localScale);

        foreach (GameObject go in objectsToEnable)
            go.SetActive(false);

        if (activateOnStart)
            DoActivate();
    }

    public void DoActivate()
    {
        if (doGrow)
            GrowIntoExistance();
        StartCoroutine(CoroutineDoActivate());
    }

    IEnumerator CoroutineDoActivate()
    {
        yield return new WaitForSeconds(secondsToActivate);
        foreach (GameObject go in objectsToEnable)
            go.SetActive(true);
        onActivate.Invoke();
    }

    public void LaunchAtStart(Vector3 position, float duration)
    { 
        transform.DOMove(position, duration);   
    }
    void GrowIntoExistance()
    {
        Vector3 sizeToGrowFrom = new Vector3(0f, 0f, 1f);
        for (int i = 0; i < objectsToGrow.Length; i++)
        {
            objectsToGrow[i].localScale = sizeToGrowFrom;
            objectsToGrow[i].DOScale(originalScales[i], growDuration);
        }

    }
}
