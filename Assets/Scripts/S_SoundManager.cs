using UnityEngine;

// This class is part of the scene
// It manages all sound effects in the game

public class S_SoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // This is called from various other scripts
    // It just allows for simple sound effects to be played
    public void PlaySound(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }
}
