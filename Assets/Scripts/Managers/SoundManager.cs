using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource _audioSource;

    public void PlaySoundOneShot(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }
}
