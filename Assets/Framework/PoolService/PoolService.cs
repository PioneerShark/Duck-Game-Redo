using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolService : MonoBehaviour, IFrameworkService
{
    private Dictionary<(System.Type, string), Transform> poolStorage = new Dictionary<(System.Type, string), Transform>();
    private Dictionary<(System.Type, string), Queue<IPoolObject>> poolPrefabs  = new Dictionary<(System.Type, string), Queue<IPoolObject>>();
    private Dictionary<(System.Type, string), Queue<IPoolObject>> pools = new Dictionary<(System.Type, string), Queue<IPoolObject>>();

    public void Setup(){}

    private string GetPoolIDFromPrefab<T>(T prefab) where T : MonoBehaviour, IPoolObject
    {
        if (prefab.GetPoolID() != null) return prefab.GetPoolID();
        return "";
    }

    private Transform CreatePoolTransform<T>(T prefab) where T : MonoBehaviour, IPoolObject
    {
        string variant = GetPoolIDFromPrefab(prefab);
        GameObject poolParent = new GameObject($"{typeof(T).Name}Pool_{variant}");
        poolParent.transform.SetParent(this.transform);
        return poolParent.transform;
    }

    private GameObject CreatePoolObject<T>(T prefab, string variant = null) where T : MonoBehaviour, IPoolObject
    {
        if (variant != null)
        {
            prefab.SetPoolID(variant);
        }
        GameObject poolObject = Instantiate(prefab.gameObject);
        poolObject.SetActive(false);
        return poolObject;
    }

    public void CreatePool<T>(T prefab, int initialSize = 32, string variant = null) where T : MonoBehaviour, IPoolObject
    {
        if (variant != null)
        {
            prefab.SetPoolID(variant);
        }
        else
        {
            variant = GetPoolIDFromPrefab(prefab);
        }

        (System.Type, string) key = (typeof(T), variant);

        if (!pools.ContainsKey(key))
        {
            pools[key] = new Queue<IPoolObject>();
            Transform newPoolTransform = CreatePoolTransform(prefab);
            poolStorage[key] = newPoolTransform;

            GameObject prefabClone = this.CreatePoolObject(prefab, variant);
            prefabClone.name = "Prefab";
            prefabClone.transform.SetParent(newPoolTransform);

            poolPrefabs[key] = new Queue<IPoolObject>();
            poolPrefabs[key].Enqueue(prefabClone.GetComponent<IPoolObject>());

            for (int i = 0; i < initialSize; i++)
            {
                GameObject newPoolObject = this.CreatePoolObject(prefab, variant);
                pools[key].Enqueue(newPoolObject.GetComponent<IPoolObject>());
                newPoolObject.transform.SetParent(newPoolTransform);
            }
        }
    }

    public void DestroyPool<T>(string variant = null) where T : MonoBehaviour, IPoolObject
    {
        if (variant == null) variant = "";
        (System.Type, string) key = (typeof(T), variant);

        if (!pools.ContainsKey(key))
        {
            return;
        }

        foreach (var obj in pools[key])
        {
            Destroy(((MonoBehaviour)obj).gameObject);
        }

        if (poolStorage.TryGetValue(key, out var poolParent))
        {
            Destroy(poolParent.gameObject);
        }

        pools.Remove(key);
        poolStorage.Remove(key);
        poolPrefabs.Remove(key);
    }

    public T FetchObject<T>(string variant = null) where T : MonoBehaviour, IPoolObject
    {
        if (variant == null) variant = "";
        (System.Type, string) key = (typeof(T), variant);

        if (!pools.ContainsKey(key))
        {
            return null;
        }

        if (pools[key].Count == 0)
        {
            Debug.Log("Expanding pool");

            T prefabObject = poolPrefabs[key].Peek() as T;
            GameObject newObject = this.CreatePoolObject(prefabObject, variant);
            T clonedObject = newObject.GetComponent<T>();

            clonedObject.gameObject.SetActive(true);
            pools[key].Enqueue(clonedObject); // Add the new object back to the pool
            return clonedObject;
        }

        T fetchedObject = (T)pools[key].Dequeue();
        fetchedObject.gameObject.SetActive(true);
        return fetchedObject;
    }

    public void ReleaseObject<T>(T pooledObject) where T : MonoBehaviour, IPoolObject
    {
        string variant = GetPoolIDFromPrefab(pooledObject);
        (System.Type, string) key = (typeof(T), variant);

        if (!pools.ContainsKey(key))
        {
            Destroy(pooledObject);
            return;
        }

        pooledObject.ResetState();
        pooledObject.gameObject.SetActive(false);
        pooledObject.transform.SetParent(poolStorage[key]);
        pools[key].Enqueue(pooledObject);
    }

    public void ReleaseAfterDelay<T>(T pooledObject, float delay) where T : MonoBehaviour, IPoolObject
    {
        StartCoroutine(ReleaseAfterDelayCoroutine(pooledObject, delay));
    }

    private IEnumerator ReleaseAfterDelayCoroutine<T>(T pooledObject, float delay) where T : MonoBehaviour, IPoolObject
    {
        yield return new WaitForSeconds(delay);
        ReleaseObject(pooledObject);
    }
}
