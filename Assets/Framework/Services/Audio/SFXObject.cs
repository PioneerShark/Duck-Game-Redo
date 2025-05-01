using UnityEngine;
using static Framework;

public class SFXObject : MonoBehaviour, IPoolObject
{
    [SerializeField] private string poolID;

    public void SetPoolID(string newPoolID)
    {
        poolID = newPoolID;
    }

    public string GetPoolID()
    {
        return poolID;
    }

    public void ResetState()
    {
        
    }

    public void ScheduleRelease(float delay)
    {
        Game.PoolService.ReleaseAfterDelay(this, delay);
    }
}
