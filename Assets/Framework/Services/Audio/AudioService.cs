using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using static Framework;

public class AudioService : MonoBehaviour, IAudioService
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource ostSource;
    [SerializeField] private AudioSource uiSource;
    
    [SerializeField] private SFXObject sfxObjectPrefab;

    public void Setup()
    {
        audioMixer = Resources.Load<AudioMixer>("Audio/MainMixer");

        GameObject ostSourceObject = new GameObject("ostSource");
        ostSourceObject.transform.parent = this.transform;
        ostSourceObject.AddComponent<AudioSource>();
        this.ostSource = ostSourceObject.GetComponent<AudioSource>();
        this.ostSource.playOnAwake = false;
        this.ostSource.outputAudioMixerGroup = this.audioMixer.FindMatchingGroups("OST")[0];

        GameObject sfxSourceObject = new GameObject("uiSource");
        sfxSourceObject.transform.parent = this.transform;
        sfxSourceObject.AddComponent<AudioSource>();
        this.uiSource = sfxSourceObject.GetComponent<AudioSource>();
        this.uiSource.playOnAwake = false;
        this.uiSource.outputAudioMixerGroup = this.audioMixer.FindMatchingGroups("UI")[0];

        sfxObjectPrefab = Resources.Load<AudioSource>("Audio/SFXObject").GetComponent<SFXObject>();
        Game.PoolService.CreatePool(sfxObjectPrefab, 32);
    }

    public SFXObject CreateSFX(AudioClip audioClip, float volume)
    {
        AudioSource audioSource = Instantiate(sfxObjectPrefab, Vector3.zero, Quaternion.identity).GetComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = this.audioMixer.FindMatchingGroups("SFX")[0];
        audioSource.clip = audioClip;
        audioSource.volume = volume;

        return audioSource.gameObject.GetComponent<SFXObject>();
    }

    public AudioSource PlaySFX(AudioClip audioClip, Transform spawnTransform, float volume, int pitchVariance = 0)
    {
        return PlaySFX(audioClip, spawnTransform.position, volume, pitchVariance);
    }

    public AudioSource PlaySFX(AudioClip audioClip, Vector3 spawnPosition, float volume, int pitchVariance = 0)
    {
        SFXObject sfxObject = Game.PoolService.FetchObject<SFXObject>();
        sfxObject.transform.position = spawnPosition;

        AudioSource audioSource = sfxObject.GetComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = this.audioMixer.FindMatchingGroups("SFX")[0];
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.pitch = 1;
        for (int i = 0; i < pitchVariance; i++)
        {
            audioSource.pitch *= 1.059463f;
        }
        audioSource.Play();

        float audioLength = audioSource.clip.length;
        sfxObject.ScheduleRelease(audioLength);
        return audioSource;
    }

    public void PlayOST(AudioClip audioClip, float volume = 1)
    {
        this.ostSource.clip = audioClip;
        this.ostSource.loop = true;
        this.ostSource.volume = volume;
        this.ostSource.Play();
    }

    public void SetMixerParameter(string parameterName, float value)
    {
        this.audioMixer.SetFloat(parameterName, value);
    }

    public void SetMixerVolume(string volumeName, float level)
    {
        level = Mathf.Clamp(level, 0.0001f, 1);
        float scaledLevel = Mathf.Log10(level) * 20f;
        SetMixerParameter(volumeName, scaledLevel);
    }
}
