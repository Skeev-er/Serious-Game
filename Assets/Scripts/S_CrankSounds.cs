using UnityEngine;

public class S_CrankSounds : MonoBehaviour
{
    private AudioSource audioSource;

    public bool crankBRPaused = false;
    public bool crankBLPaused = false;
    public bool crankTLPaused = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    public void PlaySound()
    {
        audioSource.UnPause();
    }

    public void PauseSound(string crankName)
    {
        switch (crankName)
            {
            case "Crank BR":
                crankBRPaused = true;
                break;

            case "Crank BL":
                crankBLPaused = true;
                break;

            case "Crank TL":
                crankTLPaused = true;
                break;

            default:
                break;
        }
        if (crankBRPaused && crankBLPaused && crankTLPaused)
        {
            audioSource.Pause();
            crankBRPaused = false;
            crankBLPaused = false;
            crankTLPaused = false;
        }   
    }

    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }
}
