using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource stepSound;
    public AudioSource landSound;
    public AudioSource jumpSound;

    public void PlayStepSound()
    {
        if (stepSound != null && !stepSound.isPlaying)
        {
            stepSound.Play();
        }
    }

    public void PlayLandSound()
    {
        if (stepSound != null && !stepSound.isPlaying)
        {
            landSound.Play();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpSound.Play();
        }
    }
}
