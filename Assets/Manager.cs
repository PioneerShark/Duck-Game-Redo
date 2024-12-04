using System.Collections;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    private bool waiting;
    public Material flash;
    void Start()
    {
        Manager.instance = this;   
    }
    public void HitStop(float duration)
    {
        if (waiting) return;
        Time.timeScale = 0f;
        StartCoroutine(Wait(duration));

    }
    IEnumerator Wait (float duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
        waiting = false;
    }
}
