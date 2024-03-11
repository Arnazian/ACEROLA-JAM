using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject keyPrefab;
    [SerializeField] private float timeBetweenSpawnsMin;
    [SerializeField] private float timeBetweenSpawnsMax;
    private float spawnTimer;

    [SerializeField] private GameObject[] enemiesToSpawn;

    [SerializeField] private int spawnQueuePopulations;
    List<GameObject> spawnQueue = new List<GameObject>();

    [Header("Distance")]
    [SerializeField] private float distanceMin;
    [SerializeField] private float distanceMax;

    [Header("Duration")]
    [SerializeField] private float moveDurationMin;
    [SerializeField] private float moveDurationMax;

    private EnemyHealth enemyHealth;
    private bool canSpawn = false;


    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        enemyHealth.onEnemyDeath.AddListener(CreateKey);
    }
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
        float newSpawnTime = Random.Range(timeBetweenSpawnsMin, timeBetweenSpawnsMax);
        spawnTimer = newSpawnTime;

        if (spawnQueue.Count <= 0)
            PopulateSpawnQueue();

        GameObject enemyToSpawn = spawnQueue[0];
        spawnQueue.Remove(enemyToSpawn);

        GameObject spawnedEnemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        LaunchSpawn(spawnedEnemy.GetComponent<BaseEnemyActivation>());
    }

    void LaunchSpawn(BaseEnemyActivation enemyActivation)
    {
        float randomAngle = Random.Range(0, 360) * Mathf.Deg2Rad;
        Vector3 randomDirection = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0);

        float randomDistance = Random.Range(distanceMin, distanceMax);
        float randomDuration = Random.Range(moveDurationMin, moveDurationMax);
        Vector3 spawnPoint = transform.position + randomDirection * randomDistance;

        enemyActivation.LaunchAtStart(spawnPoint, randomDuration);
    }

    void PopulateSpawnQueue()
    {
        for (int i = 0; i < spawnQueuePopulations; i++)
        {
            foreach (GameObject go in enemiesToSpawn)
                spawnQueue.Add(go);
        }
    }

    public void CreateKey()
    {
        Instantiate(keyPrefab, transform.position, Quaternion.identity);
    }

    public void SetCanSpawn(bool newValue)
    {
        canSpawn = newValue;
    }
}
