using System;
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
    private bool waveInProgress = false;
    public List<Wave> waves = new List<Wave>();
    public List<GameObject> wave = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        currWave = -1;
        maxWaves = waves.Count;
        NextWave();
    }
    private void Update()
    {
        if (waveInProgress)
        {
            if (wave.Count <= 0)
            {
                NextWave();
                waveInProgress = false;
                return;
            }
            for (int i = 0; i < wave.Count; i++)
            {
                if (wave[i].activeSelf == false)
                {
                    wave.Remove(wave[i]);
                }
            }
        }
        
    }

    private void NextWave()
    {
        currWave++;
        if (currWave == maxWaves)
        {
            StopSpawner();
            return;
        }
        wave.Clear();
        Invoke("SpawnWave", 2);

    }
    private void SpawnWave()
    {
        for (int i = 0; i < waves[currWave].enemies.Count; i++)
        {
            Vector3 pos = new Vector3(waves[currWave].enemies[i].spawnPoint.position.x, waves[currWave].enemies[i].spawnPoint.position.y, 0);
            GameObject obj = Instantiate(waves[currWave].enemies[i].enemyPrefab, pos, Quaternion.identity);
            wave.Add(obj);
        }
        waveInProgress = true;
    }
    private void StopSpawner()
    {
        Destroy(gameObject);
    }
}
[Serializable]
public class Wave
{
    public List<Mob> enemies = new List<Mob>();
}
[Serializable]
public class Mob
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
}