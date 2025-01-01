using UnityEngine;

public sealed class Game : MonoBehaviour
{
    private Game() {}
    private static Game source = null;
    private static readonly object threadlock = new object();

    public static Game Main
    {
        get
        {
            lock (threadlock) 
            {
                if (source == null)
                {
                   GameObject singleton = new GameObject("__SINGLETON__");
                   source = singleton.AddComponent<Game>();
                }

                return source;
            }  
        }
    }

    void Awake()
    {
        transform.SetParent(null);
        DontDestroyOnLoad(this);
    }

    public static Game Instance => Main;

    public void Ping()
    {
        Debug.Log("Pong");
    }
}
