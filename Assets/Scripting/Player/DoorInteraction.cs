using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorInteraction : MonoBehaviour
{
    public Tilemap doorTilemap;
    public GameManager gameManager;
    public AudioClip doorSound;
    private AudioSource audioSource; // AudioSource 
    private bool soundPlayed = false; 

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (doorSound != null)
        {
            audioSource.clip = doorSound;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (doorTilemap != null && gameManager.AreAllEnemiesDefeated())
            {
                if (!soundPlayed)
                {
                    soundPlayed = true;
                    audioSource.Play();
                    Debug.Log("Player touched the door. Playing sound...");

                    Invoke(nameof(TransitionToWin), 2f);
                }
            }
            else
            {
                Debug.Log("Player touched the door, but enemies are still alive.");
            }
        }
    }

    private void TransitionToWin()
    {
        gameManager.WinGame();
    }
}
