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

    // Allows S_CrankRotations to find out if the looping audioSource is playing
    public bool IsLoopingSourcePlaying()
    {
        return loopingAudioSource.isPlaying;
    }

    // Plays the looping crank sound
    public void PlayLoopingSource(AudioClip crankSound)
    {
        // If it is paused, unpause it
        if (!loopingAudioSource.isPlaying)
        {
            loopingAudioSource.UnPause();
        }

        // If the sound clip is different, play the new sound instead
        if (loopingAudioSource.clip != crankSound)
        {
            loopingAudioSource.clip = crankSound;
            loopingAudioSource.Play();
        }
    }

    // Stop playing the looping crank sound
    public void PauseLoopingSource()
    {
        if (loopingAudioSource.isPlaying)
        {
            loopingAudioSource.Pause();
        }
    }
}
