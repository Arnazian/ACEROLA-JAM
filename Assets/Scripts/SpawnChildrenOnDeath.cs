using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpawnChildrenOnDeath : MonoBehaviour
{
    [SerializeField] private int spawnCount;
    [SerializeField] private GameObject childToSpawn;

    [Header("Distance")]
    [SerializeField] private float distanceMin;
    [SerializeField] private float distanceMax;

    [Header("Duration")]
    [SerializeField] private float moveDurationMin;
    [SerializeField] private float moveDurationMax; 


    public void DoSpawnChildren()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject newChild = Instantiate(childToSpawn, transform.position, Quaternion.identity);

            float randomAngle = Random.Range(0, 360) * Mathf.Deg2Rad;
            Vector3 randomDirection = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0);

            float randomDistance = Random.Range(distanceMin, distanceMax);
            float randomDuration = Random.Range(moveDurationMin, moveDurationMax);
            Vector3 spawnPoint = transform.position + randomDirection * randomDistance;

            newChild.GetComponent<BaseEnemyActivation>().LaunchAtStart(spawnPoint, randomDuration);
        }
    }
}
