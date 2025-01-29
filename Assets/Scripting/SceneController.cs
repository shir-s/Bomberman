using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
   [SerializeField] private int sceneIndex;
   [SerializeField] private AudioClip sceneAudio;
   private AudioSource audioSource;
   
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Return))
    //     {
    //       SceneManager.LoadScene(sceneIndex);
    //     }
    //     
    // }
    
    private void Awake()
    {
        // יצירת AudioSource או שימוש קיים
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // הגדרת פרמטרים עבור AudioSource
        audioSource.clip = sceneAudio;
        audioSource.loop = true; // להפעיל לופ אם הסאונד צריך להמשיך
        audioSource.playOnAwake = false; // למנוע הפעלה אוטומטית
    }

    private void Start()
    {
        // הפעלת הסאונד כאשר הסצנה מתחילה
        if (sceneAudio != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No audio clip assigned to SceneController.");
        }
    }

    private void Update()
    {
        // מעבר לסצנה הבאה כאשר לוחצים על Enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // עצירת הסאונד לפני המעבר
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
