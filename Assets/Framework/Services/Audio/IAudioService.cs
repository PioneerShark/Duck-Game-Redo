using UnityEngine;

public interface IAudioService : IFrameworkService
{
    SFXObject CreateSFX(AudioClip audioClip, float volume);
    AudioSource PlaySFX(AudioClip audioClip, Transform spawnTransform, float volume ,int pitchVariance = 0);
    AudioSource PlaySFX(AudioClip audioClip, Vector3 spawnPosition, float volume, int pitchVariance = 0);
    void PlayOST(AudioClip audioClip, float volume = 1f);

    void SetMixerParameter(string parameterName, float value);
    void SetMixerVolume(string volumeName, float level);
}

