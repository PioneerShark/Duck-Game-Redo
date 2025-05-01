using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Framework;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    private bool waiting;
    private Vector2 a;
    private Vector2 b;

    [Header("HUD")]
    //public Slider healthSlider;
    //public Slider ammoSlider;
    public UIDocument gameUI;
    private SegmentedMeter healthMeter;
    private SegmentedMeter ammoMeter;

    [Header("Camera Effects")]
    [SerializeField]
    private GameObject cameraToShake;

    [Header("Damage Effect")]
    public Material flash;
    //[HideInInspector]
    public float gameTimeScale = 1f;
    

    [Header("Pooling")]


    [Header("Bullets")]

    [SerializeField] private int bulletAmount = 20;
    [SerializeField] private GameObject bulletPrefab;
    private List<GameObject> pooledBullets = new List<GameObject>();

    [Header("Arrows")]

    [SerializeField] private int arrowAmount = 6;
    [SerializeField] private GameObject arrowPrefab;
    private List<GameObject> pooledArrows = new List<GameObject>();

    [SerializeField] private GameObject shurikenPrefab;

    [Header("Other")]
    [SerializeField] private AudioClip ost;

    public enum PoolType
    {
        Bullets,
        Arrows
    };
    public Transform FindTransform(string location)
    {

        return null;
    }
    void Awake()
    {
        Manager.instance = this;   

        Time.timeScale = gameTimeScale;
    }
    public void ShakeCamera(float duration, float intensity)
    {
        StartCoroutine(StartShakeCamera(duration, intensity));
    }

    private IEnumerator StartShakeCamera(float duration, float intensity)
    {

        GameObject cam = Camera.main.gameObject;
        Vector3 origin = cam.transform.localPosition;

        float elasped = 0.0f;

        while (elasped < duration) 
        { 
            float x = Random.Range(-0.2f, 0.2f) * intensity;
            float y = Random.Range(-0.2f, 0.2f) * intensity;
            cam.transform.localPosition = new Vector3(x, y, origin.z);

            elasped += Time.deltaTime;
            yield return null;
        }

        cam.transform.localPosition = origin;

    }

    private void Start()
    {
        Game.AudioService.PlayOST(this.ost);
        Game.AudioService.SetMixerVolume("OSTVolume", 0.08f);

        Projectile arrowObject = arrowPrefab.GetComponent<Projectile>();
        Game.PoolService.CreatePool(arrowObject, 20, "Arrow");

        Projectile bulletObject = bulletPrefab.GetComponent<Projectile>();
        Game.PoolService.CreatePool(bulletObject, 20, "Bullet");
        
        Projectile shurikenObject = shurikenPrefab.GetComponent<Projectile>();
        Game.PoolService.CreatePool(shurikenObject, 20, "Shuriken");

        var root = gameUI.rootVisualElement;

        healthMeter = root.Q<SegmentedMeter>("HealthMeter");
        if (healthMeter != null)
        {
            healthMeter.valueMax = 100;
            healthMeter.valueCurrent = 100;
        }
        ammoMeter = root.Q<SegmentedMeter>("AmmoMeter");
    }

    public GameObject GetPooledObject(PoolType pool)
    {
        switch (pool) 
        {
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

    public void UpdateHealthSlider(float maxHealth, float currentHealth)
    {
        //healthSlider.value = currentHealth/maxHealth;
        if (healthMeter != null)
        {
            healthMeter.valueMax = maxHealth;
            healthMeter.valueCurrent = currentHealth;
        }
    }

    public void UpdateAmmoSlider(float progress)
    {
        //ammoSlider.value = progress;
        if (ammoMeter != null)
        {
            ammoMeter.valueCurrent = progress * 100;
        }
    }

    public void GizmoCapsule(Vector2 start, Vector2 end)
    {
        a = start;
        b = end;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(a, 1f);
        Gizmos.DrawSphere(b, 1f);
    }
}
