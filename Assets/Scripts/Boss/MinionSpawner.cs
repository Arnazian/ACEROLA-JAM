using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    [SerializeField] private int amountOfSpawns;
    [SerializeField] private Vector2 timeBetweenSpawns;
    private float spawnTimer;

    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] enemyToSpawn;
    private bool canSpawn = false;

    void Update()
    {
        HandleTimer();
    }

    void HandleTimer()
    {
        if (!canSpawn)
            return;

        if (spawnTimer <= 0)        
            DoSpawn();
        else
            spawnTimer -= Time.deltaTime;   
    }

    public void DoSpawn()
    {
        float newSpawnTime = Random.Range(timeBetweenSpawns.x, timeBetweenSpawns.y);
        spawnTimer = newSpawnTime;

        List<Transform> points = new List<Transform>(); 

        foreach (Transform t in spawnPoints)
            points.Add(t);

        for (int i = 0; i < amountOfSpawns; ++i)
        {
            GameObject randomEnemy = enemyToSpawn[Random.Range(0, enemyToSpawn.Length)];
            Transform randomSpawnPoint = points[Random.Range(0, points.Count)];
            points.Remove(randomSpawnPoint);
            Instantiate(randomEnemy, randomSpawnPoint.position, Quaternion.identity);
        }
    }

    public void SetCanSpawn(bool newValue)
    {
        canSpawn = newValue;
    }
}
