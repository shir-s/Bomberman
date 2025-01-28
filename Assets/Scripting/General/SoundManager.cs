using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private AudioClip backgroundMusic;
    public AudioClip footstepHorizontal;
    public AudioClip footstepVertical;
    public AudioClip placementBomb;
    public AudioClip deathSound;
    public AudioClip explosionSound;
    //public AudioClip powerUpSound;

    private AudioSource backgroundAudioSource;
    private AudioSource effectsAudioSource;

    private void Start()
    {
        // הגדרת מקורות אודיו
        backgroundAudioSource = gameObject.AddComponent<AudioSource>();
        effectsAudioSource = gameObject.AddComponent<AudioSource>();

        PlayBackgroundMusic();
    }

    private void PlayBackgroundMusic()
    {
        backgroundAudioSource.clip = backgroundMusic;
        backgroundAudioSource.loop = true;
        backgroundAudioSource.spatialBlend = 0f; // Non-spatial (constant volume)
        backgroundAudioSource.volume = 0.3f;
        backgroundAudioSource.Play();
    }

    public void PlayFootstepSound(string direction)
    {
        AudioClip sound = direction == "horizontal" ? footstepHorizontal : footstepVertical;
        if (!effectsAudioSource.isPlaying) // לוודא שלא משמיעים פעמיים
        {
            effectsAudioSource.clip = sound;
            effectsAudioSource.Play();
        }
    }

    public void PlayExplosionSound(Vector3 position)
    {
        effectsAudioSource.transform.position = position;
        effectsAudioSource.clip = placementBomb;
        effectsAudioSource.Play();
    }
    
    public void PlayBombPlacementSound()
    {
        if (placementBomb != null)
        {
            effectsAudioSource.PlayOneShot(placementBomb);
        }
    }
    
    public void PlayDeathSound()
    {
        if (deathSound != null)
        {
            effectsAudioSource.PlayOneShot(deathSound);
        }
    }
    
    public void PlayExplosionSound()
    {
        if (explosionSound != null)
        {
            effectsAudioSource.PlayOneShot(explosionSound);
        }
    }
}
