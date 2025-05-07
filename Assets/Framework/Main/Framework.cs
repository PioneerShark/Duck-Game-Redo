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

    private T CreateService<T>(string serviceName) where T : MonoBehaviour, IFrameworkService
    {
        GameObject serviceObject = new GameObject(serviceName);
        serviceObject.transform.SetParent(this.transform);
        T service = serviceObject.AddComponent<T>();
        service.Setup();
        return service;
    }

    public static Framework Instance => Game;

    private IIndicatorService IndicatorServiceSource;
    public IIndicatorService IndicatorService => IndicatorServiceSource ??= CreateService<IndicatorService>("IndicatorService");

    private IAudioService AudioServiceSource;
    public IAudioService AudioService => AudioServiceSource ??= CreateService<AudioService>("AudioService");

    private PoolService PoolServiceSource;
    public PoolService PoolService => PoolServiceSource ??= CreateService<PoolService>("PoolService");

    private PauseService PauseServiceSource;
    public PauseService PauseService => PauseServiceSource ??= CreateService<PauseService>("PauseService");
}
