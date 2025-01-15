using UnityEngine;

public interface IPoolObject
{
    void SetPoolID(string newPoolID);
    string GetPoolID();

    void ResetState();
    void ScheduleRelease(float delay);
}
