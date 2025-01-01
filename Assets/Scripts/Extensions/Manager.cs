using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    private bool waiting;

    [Header("Modules")]
    public IndicatorUI IndicatorUI;

    [Header("Damage Effect")]
    public Material flash;
    [HideInInspector]
    public float gameTimeScale = 1f;
    

    [Header("Pooling")]
    [Header("Tracers")]

    [SerializeField] private int tracerAmount = 20;
    [SerializeField] private GameObject tracerPrefab;
    private List<GameObject> pooledTracers = new List<GameObject>();

    [Header("Bullets")]

    [SerializeField] private int bulletAmount = 20;
    [SerializeField] private GameObject bulletPrefab;
    private List<GameObject> pooledBullets = new List<GameObject>();

    [Header("Arrows")]

    [SerializeField] private int arrowAmount = 6;
    [SerializeField] private GameObject arrowPrefab;
    private List<GameObject> pooledArrows = new List<GameObject>();

    public enum PoolType
    {
        Tracers,
        Bullets,
        Arrows
    };

    void Awake()
    {
        Manager.instance = this;   

        this.IndicatorUI = GetComponentInChildren<IndicatorUI>();
    }

    private void Start()
    {
        for (int i = 0; i < bulletAmount; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            obj.transform.parent = GameObject.Find("Bullets").transform;
            pooledBullets.Add(obj);
        }

        for (int i = 0; i < tracerAmount; i++)
        {
            GameObject obj = Instantiate(tracerPrefab);
            obj.SetActive(false);
            obj.transform.parent = GameObject.Find("Tracers").transform;
            pooledTracers.Add(obj);
        }

        for (int i = 0; i < arrowAmount; i++)
        {
            GameObject obj = Instantiate(arrowPrefab);
            obj.SetActive(false);
            obj.transform.parent = GameObject.Find("Arrows").transform;
            pooledArrows.Add(obj);
        }


    }

    public GameObject GetPooledObject(PoolType pool)
    {
        switch (pool) 
        {
            case PoolType.Tracers:
                return PoolTask(pooledTracers);
            case PoolType.Bullets:
                return PoolTask(pooledBullets);
            case PoolType.Arrows:
                return PoolTask(pooledArrows);
        };
        return null;
        GameObject PoolTask(List<GameObject> pool)
        {
            for (int i = 0; i < pool.Count; i++)
            {
                if (!pool[i].activeInHierarchy)
                {
                    return pool[i];
                }
            }
            return null;
        }
    }

    public void HitStop(float duration)
    {
        if (waiting) return;
        gameTimeScale = 0f;
        StartCoroutine(Wait(duration));

    }
    IEnumerator Wait (float duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        gameTimeScale = 1f;
        waiting = false;
    }
}
