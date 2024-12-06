using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{

    public GameObject[] enemyPrefabs; // Reference to enemy Prefab
    public Transform[] spawnPoints; // array of spawn locations

    public float minSpawnInterval = 0.5f;
    public float maxSpawnInterval = 2f;

    private int waveNumber = 1; // Current wave number
    public int enemiesPerWave = 20; // enemeies
    private int enemiesSpawned;
    private List<GameObject> activeEnemies = new List<GameObject>();


    private bool isSpawning = false; // don't allow overlap
    private bool isNextWaveStarting = false;

    public TMPro.TextMeshProUGUI waveText; // inspector

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // start wave
        StartWave();
    }

    private void StartWave()
    {
        if (waveText != null)
        {
            waveText.text = "Wave: " + waveNumber;
        }

        enemiesSpawned = 0;
        StartCoroutine(SpawnEnemy());
    }
    private IEnumerator SpawnEnemy()
    {
        isSpawning = true;

        while (enemiesSpawned < enemiesPerWave)
        {

            // choose random spawn Point
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Add enemy to list
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            activeEnemies.Add(newEnemy);

            // subscribe to enemy death event
            EnemyScript enemyScript = newEnemy.GetComponent<EnemyScript>();
            if (enemyScript != null)
            {
                enemyScript.OnEnemyDeath += HandleEnemyDeath;
            }
            
            // increment spawn count and wait before spawning
            enemiesSpawned++;
            float spawnDelay = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(spawnDelay);
        }

        isSpawning = false;
    }
    

    

    private void HandleEnemyDeath(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
        Debug.Log("Enemy Destroyed! Remaining: " + activeEnemies.Count);

        // check if all enemies are ded before starting next
        if (activeEnemies.Count == 0 && !isSpawning && enemiesSpawned >= enemiesPerWave)
        {
            StartCoroutine(StartNextWave());
        }
    }

    // start next wave on delay (CoRoutine)
    private IEnumerator StartNextWave()
    {
        if (isNextWaveStarting) yield break;

        isNextWaveStarting = true;
        
        Debug.Log("Wave" + waveNumber + "COmplete! Next wave in 5 seconds...");
        yield return new WaitForSeconds(5f);

        waveNumber++;
        enemiesPerWave += 5;

        isNextWaveStarting = false;
        StartWave();
    }
}
