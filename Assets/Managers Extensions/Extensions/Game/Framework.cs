using UnityEngine;

// NOTE Lazy Loading is being used, the framework itself, nor the services it has won't be instanced unless needed.

public sealed class Framework : MonoBehaviour
{
    private Framework() {}
    private static Framework GameSource = null;
    private static readonly object threadlock = new object();

    public static Framework Game
    {
        get
        {
            lock (threadlock) 
            {
                if (GameSource == null)
                {
                   GameObject frameworkObject = new GameObject("Framework");
                   GameSource = frameworkObject.AddComponent<Framework>();
                }

                return GameSource;
            }  
        }
    }

    void Awake()
    {
        transform.SetParent(null);
        DontDestroyOnLoad(this);
    }

    public static Framework Instance => Game;

    private IndicatorService IndicatorServiceSource;
    public IndicatorService IndicatorService
    {
        get
        {
            if (IndicatorServiceSource == null)
            {
                GameObject serviceObject = new GameObject("IndicatorService");
                serviceObject.transform.SetParent(this.transform);
                IndicatorServiceSource = serviceObject.AddComponent<IndicatorService>();
                IndicatorServiceSource.Init();
            }
            return IndicatorServiceSource;
        }
    }
}
