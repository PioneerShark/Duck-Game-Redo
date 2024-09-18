using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    private int currWave;
    private int maxWaves;
    private int enemyCount;
    public List<Wave> waves = new List<Wave>();
    // Start is called before the first frame update
    void Start()
    {
        currWave = -1;
        maxWaves = waves.Count;
        NextWave();
    }

    public void RemoveEnemy()
    {
        enemyCount--;
        if (enemyCount <= 0)
        {
            NextWave();
        }
    }
    private void NextWave()
    {
        currWave++;
        if (currWave == maxWaves) {
            StopSpawner();
            return;
        }
        enemyCount = waves[currWave].enemies.Count;
        if (enemyCount <= 0)
        {
            NextWave();
            return;
        }
        SpawnWave();
        
    }
    private void SpawnWave()
    {
        for (int i = 0; i < enemyCount; i++) {

            _ = Instantiate(waves[currWave].enemies[i].enemyPrefab, waves[currWave].enemies[i].spawnPoint.position, Quaternion.identity);
        }
    }
    private void StopSpawner()
    {
        Destroy(gameObject);
    }
}
[Serializable]
public class Wave {
    public List<Mob> enemies = new List<Mob>();
}
[Serializable]
public class Mob {
    public GameObject enemyPrefab;
    public Transform spawnPoint;
}