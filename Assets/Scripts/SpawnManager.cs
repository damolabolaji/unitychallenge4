using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float spawnRange = 9;

    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    public int enemyCount;
    public int enemySpawnCount = 1;


    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(enemySpawnCount);
        Instantiate(powerupPrefab, GenerateRandomPosition(), powerupPrefab.transform.rotation);

    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length; //add the "Length" method to return an integer instead of an array
        
        if (enemyCount == 0)
        {
            enemySpawnCount++;
            SpawnEnemyWave(enemySpawnCount);
            Instantiate(powerupPrefab, GenerateRandomPosition(), powerupPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateRandomPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }

    public void SpawnEnemyWave(int spawnEnemies)
    {
        for (int i = 0; i < spawnEnemies; i++)
        {
            Instantiate(enemyPrefab, GenerateRandomPosition(), enemyPrefab.transform.rotation);
        }
    }

}
