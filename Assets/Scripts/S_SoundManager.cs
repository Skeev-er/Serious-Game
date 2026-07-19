using UnityEngine;

// This class is part of the scene
// It manages all sound effects in the game

public class S_SoundManager : MonoBehaviour
{
    private AudioSource[] audioSources;
    private AudioSource simpleAudioSource;
    private AudioSource loopingAudioSource;

    private void Start()
    {
        audioSources = GetComponents<AudioSource>();
        simpleAudioSource = audioSources[0];
        loopingAudioSource = audioSources[1];

        loopingAudioSource.Play();
        loopingAudioSource.Pause();
    }

    // This is called from various other scripts
    // It just allows for simple sound effects to be played
    public void PlaySound(AudioClip sound)
    {
        simpleAudioSource.PlayOneShot(sound);
    }

    public bool IsLoopingSourcePlaying()
    {
        return loopingAudioSource.isPlaying;
    }

    public void PlayLoopingSource(AudioClip crankSound)
    {
        if (!loopingAudioSource.isPlaying)
        {
            loopingAudioSource.UnPause();
        }

        if (loopingAudioSource.clip != crankSound)
        {
            loopingAudioSource.clip = crankSound;
            loopingAudioSource.Play();
        }
    }

    public void PauseLoopingSource()
    {
        if (loopingAudioSource.isPlaying)
        {
            loopingAudioSource.Pause();
        }
    }
}
