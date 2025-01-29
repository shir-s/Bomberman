using System.Collections;
using UnityEngine;

public class SceneStage : MonoBehaviour
{
    
    [SerializeField] private float sceneTime;
    [SerializeField] private int sceneIndex;
    [SerializeField] private AudioClip sceneAudio; // קליפ הסאונד שיתנגן
    private AudioSource audioSource;
    private bool isTransitioning = false;

    private void Awake()
    {
        // יצירת AudioSource או שימוש בקיים
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // הגדרת פרמטרים עבור AudioSource
        audioSource.clip = sceneAudio;
        audioSource.loop = true; // הפעלה חוזרת של הסאונד במהלך הסצנה
        audioSource.playOnAwake = false; // למנוע הפעלה אוטומטית
    }

    private void Start()
    {
        Debug.Log($"SceneStage loaded, waiting {sceneTime} seconds before transition.");
        // הפעלת הסאונד כאשר הסצנה מתחילה
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

        // עצירת הסאונד לפני מעבר לסצנה הבאה
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        Debug.Log("Transitioning to next scene...");
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }
    
    // // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
    //     StartCoroutine(LoadScene());
    // }
    //
    // IEnumerator LoadScene()
    // {
    //     yield return new WaitForSeconds(sceneTime);
    //     UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    // }
  
}
