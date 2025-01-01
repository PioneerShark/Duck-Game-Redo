using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    [Header("Pooling")]

    [Header("AfterImages")]

    [SerializeField] public static EffectsManager instance;
    [SerializeField] private GameObject afterImage;
    [SerializeField] private int afterImageCount;
    private List<GameObject> afterImages = new List<GameObject>();

    private void Awake()
    {
        EffectsManager.instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < afterImageCount; i++)
        {
            GameObject obj = Instantiate(afterImage);
            obj.SetActive(false);
            obj.transform.parent = GameObject.Find("AfterImages").transform;
            afterImages.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnAfterImages(GameObject image, float duration, float rate, float imageLifespan, float initialAlpha)
    {
        StartCoroutine(SpawnAfterImagesTask(image, duration, rate, imageLifespan, initialAlpha));
    }
    public IEnumerator SpawnAfterImagesTask(GameObject image, float duration, float rate, float imageLifespan, float initialAlpha)
    {
        
        if (duration > rate)
        {
            int imgCount = (int)(duration / rate);
            Debug.Log(imgCount);
            for (int i = 0; i <= imgCount; i++) {
                AfterImage _afterImage = PoolTask(afterImages).GetComponent<AfterImage>();
                _afterImage.UpdateAfterImage(imageLifespan, initialAlpha, image);
                yield return new WaitForSeconds(rate);
            }
        }
    }
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
