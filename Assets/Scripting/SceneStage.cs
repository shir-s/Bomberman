using System.Collections;
using UnityEngine;

public class SceneStage : MonoBehaviour
{
    
    [SerializeField] private float sceneTime;
    [SerializeField] private int sceneIndex;
    [SerializeField] private AudioClip sceneAudio;
    private AudioSource audioSource;
    private bool isTransitioning = false;

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
        Debug.Log($"SceneStage loaded, waiting {sceneTime} seconds before transition.");
        if (sceneAudio != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No audio clip assigned to SceneStage.");
        }

        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        if (isTransitioning) yield break;
        isTransitioning = true;
        yield return new WaitForSeconds(sceneTime);

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        Debug.Log("Transitioning to next scene...");
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }
    
  
}
