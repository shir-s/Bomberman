using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    [SerializeField] private int sceneIndex;
    [SerializeField] private AudioClip sceneAudio;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        audioSource.clip = sceneAudio;
        audioSource.loop = true;
        audioSource.playOnAwake = false; 
    }

    private void Start()
    {
        if (sceneAudio != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No audio clip assigned to RestartGame.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            SceneManager.LoadScene(sceneIndex);
            GameManager.Instance.RestartFullGame();
        }
    }
    
}
