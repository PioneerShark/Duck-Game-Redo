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
    private bool spawnerActive = false;
    private bool waveInProgress = false;
    public GameObject barrier;
    public List<GameObject> barrierVisual;
    public List<Wave> waves = new List<Wave>();
    public List<GameObject> wave = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        currWave = -1;
        maxWaves = waves.Count;
        //NextWave();
    }
    private void Update()
    {
        if (!spawnerActive)
            return;
        barrier.SetActive(true);
        for (int i  = 0; i < barrierVisual.Count;i++)
            barrierVisual[i].SetActive(true);
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !spawnerActive)
        {
            spawnerActive = true;
            NextWave();
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
        Destroy(barrier);
        for (int i = 0; i < barrierVisual.Count; i++)
            Destroy(barrierVisual[i]);
        
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