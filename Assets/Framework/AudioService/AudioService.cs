using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class AudioService : FrameworkService
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource ostSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource uiSource;

    public override void Init()
    {
        audioMixer = Resources.Load<AudioMixer>("Audio/MainMixer");

        GameObject ostSourceObject = new GameObject("ostSource");
        ostSourceObject.transform.parent = this.transform;
        ostSourceObject.AddComponent<AudioSource>();
        this.ostSource = ostSourceObject.GetComponent<AudioSource>();
        this.ostSource.playOnAwake = false;
        this.ostSource.outputAudioMixerGroup = this.audioMixer.FindMatchingGroups("OST")[0];

        GameObject sfxSourceObject = new GameObject("sfxSource");
        sfxSourceObject.transform.parent = this.transform;
        sfxSourceObject.AddComponent<AudioSource>();
        this.uiSource = sfxSourceObject.GetComponent<AudioSource>();
        this.uiSource.playOnAwake = false;
        this.uiSource.outputAudioMixerGroup = this.audioMixer.FindMatchingGroups("UI")[0];

        sfxSource = Resources.Load<AudioSource>("Audio/SoundObject");
    }

    public void PlaySound(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(sfxSource, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        float audioLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, audioLength);
    }

    public void PlayOST(AudioClip audioClip, float volume = 1)
    {
        this.ostSource.clip = audioClip;
        this.ostSource.loop = true;
        this.ostSource.volume = volume;
        this.ostSource.Play();
    }

    public void SetOSTVolume(float volume)
    {
        // Change to alter mixer in future
        this.ostSource.volume = volume;
    }

    public void SetUIVolume(float volume)
    {
        // Change to alter mixer in future
        this.uiSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {

    }
}
