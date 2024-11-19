using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab; // Reference to enemy Prefab
    public Transform[] spawnPoints; // array of spawn locations
    public float timeBetweenWaves = 5f;
    public int enemiesPerWave = 5;

    private float countdown = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0f)
        {
            SpawnWave();
            countdown = timeBetweenWaves;
        }
    }

    void SpawnWave()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        // choose random spawn Point
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // Spawn enemy at spawn point

        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
