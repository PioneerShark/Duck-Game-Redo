using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class AudioService : FrameworkService
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource ostSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource soundObject;

    public override void Init()
    {
        audioMixer = Resources.Load<AudioMixer>("Audio/MainMixer");
        GameObject ostSourceObject = new GameObject("ostSource");
        GameObject sfxSourceObject = new GameObject("sfxSource");
    }

    public void PlaySound(AudioClip audioClip, Transform spawnTransform, float volume)
    {

    }
}
