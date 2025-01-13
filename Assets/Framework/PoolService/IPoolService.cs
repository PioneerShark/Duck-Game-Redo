using UnityEngine;

public interface IPoolService : IFrameworkService
{
    void CreatePool<T>(T prefab, int initialSize) where T : MonoBehaviour, IPoolObject;

    void DestroyPool<T>() where T : MonoBehaviour, IPoolObject;

    T GetObject<T>() where T : MonoBehaviour, IPoolObject;

    void ReturnObject<T>(T pooledObject) where T : MonoBehaviour, IPoolObject;
}
