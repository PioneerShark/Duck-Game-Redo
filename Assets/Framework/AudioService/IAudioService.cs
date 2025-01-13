using UnityEngine;

public interface IAudioService : IFrameworkService
{
    AudioSource PlaySFX(AudioClip audioClip, Transform spawnTransform, float volume = 1f);
    void PlayOST(AudioClip audioClip, float volume = 1f);

    void SetMixerParameter(string parameterName, float value);
    void SetMixerVolume(string volumeName, float level);
}

