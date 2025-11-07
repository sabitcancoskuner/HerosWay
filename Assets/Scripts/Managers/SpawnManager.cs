using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    private WaveManager waveManager;

    [SerializeField] private int enemiesToSpawn;
    public int killedEnemies;
    [SerializeField] private float spawnCooldownMinValue;
    [SerializeField] private float spawnCooldownMaxValue;
    public List<EnemySO> enemies;
    private List<GameObject> enemyPrefabs;
    private List<int> enemySpawnWeights;
    public List<Transform> spawnPoints;

    private int defaultEnemyCount;
    public int lastWaveEnemyCount = 0;
    private int enemyAmountIncrement;

    private float spawnRate;
    private Transform randomSpawnPoint;
    private GameObject enemyToSpawn;

    private List<GameObject> spawnedEnemies;

    private void Awake() {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else 
        {
            instance = this; 
        }
        DisableSpawner();
    }

    private void Start() {
        waveManager = WaveManager.instance;
        spawnedEnemies = new List<GameObject>();
        enemyPrefabs = new List<GameObject>();
        enemySpawnWeights = new List<int>();

        defaultEnemyCount = enemiesToSpawn;
        lastWaveEnemyCount = enemiesToSpawn;
        enemyAmountIncrement = Mathf.RoundToInt(defaultEnemyCount * Random.Range(0.5f, 1f));
        GetPrefabsAndWeights();
    }

    private void Update()
    {
        if (enemiesToSpawn != 0)
        {
            enemyToSpawn = ChooseRandomEnemy();
            SpawnEnemy(enemyToSpawn);
        }
        else {
            CheckAliveEnemies();
        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        spawnRate -= Time.deltaTime;

        if (spawnRate < 0)
        {
            Instantiate(enemy, randomSpawnPoint.position, Quaternion.identity);
            enemiesToSpawn -= 1;
            spawnRate = Random.Range(spawnCooldownMinValue, spawnCooldownMaxValue);

            if (enemiesToSpawn == 0)
            {
                spawnRate = Mathf.Infinity;
            }
        }
    }

    private GameObject ChooseRandomEnemy()
    {
        int totalWeight = 0;

        foreach (int weight in enemySpawnWeights)
        {
            totalWeight += weight;
        }

        int cumulativeWeight = 0;
        int randomValue = Random.Range(0, totalWeight);

        foreach (GameObject enemyPrefab in enemyPrefabs)
        {
            cumulativeWeight += enemySpawnWeights[enemyPrefabs.IndexOf(enemyPrefab)];

            if (randomValue <= cumulativeWeight)
            {
                return enemyPrefab;
            }
        }

        return null;
    }

    private void CheckAliveEnemies()
    {
        if (spawnedEnemies.Count == 0)
        {
            WaveManager.instance.StartCoroutine("WaveCleared", true);
            DisableSpawner();
        }
    }

    public void DisableSpawner()
    {
        gameObject.SetActive(false);
    }

    public void EnableSpawner()
    {
        gameObject.SetActive(true);
    }

    public void AddEnemyInstance(GameObject enemy)
    {
        spawnedEnemies.Add(enemy);
    }

    public void RemoveEnemyInstance(GameObject enemy)
    {
        spawnedEnemies.Remove(enemy);
        killedEnemies++;
    }

    public void SetupSpawner()
    {
        killedEnemies = 0;
        IncreaseSpawnCount();
        spawnRate = spawnCooldownMinValue;
        AdjustWeights();
        EnableSpawner();
    }

    private void AdjustWeights()
    {
        int cumulativeWeight = 0;

        foreach (int weight in enemySpawnWeights)
        {
            cumulativeWeight += weight;
        }

        int increment = Mathf.RoundToInt(cumulativeWeight * 0.05f);

        enemySpawnWeights[0] -= increment;
        enemySpawnWeights[1] += increment;
    }

    private void IncreaseSpawnCount() 
    {
        enemyAmountIncrement = Mathf.RoundToInt(defaultEnemyCount * Random.Range(0.5f, 1f));
        enemiesToSpawn = lastWaveEnemyCount;
        enemiesToSpawn += enemyAmountIncrement;
        lastWaveEnemyCount = enemiesToSpawn;
    }

    private void GetPrefabsAndWeights()
    {
        foreach (EnemySO enemyData in enemies)
        {
            enemyPrefabs.Add(enemyData.enemyPrefab);
            enemySpawnWeights.Add(enemyData.spawnWeight);
        }
    }
}
