using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static Framework;
using UnityEngine.InputSystem;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    private bool waiting;
    private Vector2 a;
    private Vector2 b;

    [Header("HUD")]
    public PlayerInput playerInput;
    public UIDocument gameUI;
    public UIDocument menuUI;
    private SegmentedMeter healthMeter;
    private SegmentedMeter ammoMeter;
    private Button resumeButton;
    private Button exitButton;

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

        var gameUIRoot = gameUI.rootVisualElement;
        healthMeter = gameUIRoot.Q<SegmentedMeter>("HealthMeter");
        ammoMeter = gameUIRoot.Q<SegmentedMeter>("AmmoMeter");
        if (healthMeter != null)
        {
            healthMeter.valueMax = 100;
            healthMeter.valueCurrent = 100;
        }

        var menuUIRoot = menuUI.rootVisualElement;
        resumeButton = menuUIRoot.Q<Button>("ResumeButton");
        resumeButton.RegisterCallback<ClickEvent>(OnResumeClicked);
        exitButton = menuUIRoot.Q<Button>("ExitButton");
        exitButton.RegisterCallback<ClickEvent>(OnExitClicked);

        Pause(false);
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

    public void OnPause(InputAction.CallbackContext context)
    {
        var pauseInput = context.action.triggered;
        if (pauseInput)
        {
            Pause(!Game.PauseService.GameIsPaused());
        }
    }

    public void OnResume(InputAction.CallbackContext context)
    {
        var resumeInput = context.action.triggered;
        if (resumeInput)
        {
            Pause(!Game.PauseService.GameIsPaused());
        }
    }

    private void Pause(bool value)
    {
        if (value)
        {
            Debug.Log("Visible");
            playerInput.SwitchCurrentActionMap("UI");
            menuUI.rootVisualElement.style.display = DisplayStyle.Flex;
            UnityEngine.Cursor.visible = true;
            Game.PauseService.PauseGame();
        }
        else
        {
            Debug.Log("Hidden");
            menuUI.rootVisualElement.style.display = DisplayStyle.None;
            playerInput.SwitchCurrentActionMap("Game");
            UnityEngine.Cursor.visible = false;
            Game.PauseService.ResumeGame();
        }
    }

    private void OnResumeClicked(ClickEvent clickEvent)
    {
        Pause(false);
    }

    private void OnExitClicked(ClickEvent clickEvent)
    {
        #if UNITY_STANDALONE
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
