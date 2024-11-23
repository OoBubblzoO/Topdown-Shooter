using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab; // Reference to enemy Prefab
    public Transform[] spawnPoints; // array of spawn locations
    public float timeBetweenWaves = 5f; // break for player


    private int waveNumber = 1; // Current wave number
    public int enemiesPerWave = 5; // enemeies
    private List<GameObject> activeEnemies = new List<GameObject>();


    private bool isSpawning = false; // don't allow overlap

    public TMPro.TextMeshProUGUI waveText; // inspector

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // start wave
        SpawnWave();
    }

    void SpawnWave()
    {
        waveText.text = "Wave " + waveNumber;
        Debug.Log("Wave " + waveNumber + " starting!");

        // spawn enemies
        for (int i = 0; i < enemiesPerWave; i++)
        {
            SpawnEnemy();
        }

        // difficulty increase during waves
        enemiesPerWave += 2;
        waveNumber++;
    }


    void SpawnEnemy()
    {
        // choose random spawn Point
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        // add enemy to activeEnemies list
        activeEnemies.Add(newEnemy);

        // subscribe to enemy death event
        EnemyScript enemyScript = newEnemy.GetComponent<EnemyScript>();
        if (enemyScript != null)
        {
            enemyScript.OnEnemyDeath += HandleEnemyDeath;
        }
    }

    private void HandleEnemyDeath(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
        Debug.Log("Enemy Destroyed! Remaining: " + activeEnemies);

        if (activeEnemies.Count == 0 && !isSpawning)
        {
            StartCoroutine(StartNextWave());
        }
    }

    // start next wave on delay (CoRoutine)
    private IEnumerator StartNextWave()
    {
        isSpawning = true;
        Debug.Log("Wave Complete! Next wave in " + timeBetweenWaves + " seconds...");
        yield return new WaitForSeconds(timeBetweenWaves);

        SpawnWave();
        isSpawning = false;
    }
}
