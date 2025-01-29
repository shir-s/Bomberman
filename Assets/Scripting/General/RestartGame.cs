using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    [SerializeField] private int sceneIndex; // אינדקס הסצנה לטעינה
    [SerializeField] private AudioClip sceneAudio; // קליפ הסאונד שיתנגן
    private AudioSource audioSource;

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
        audioSource.loop = true; // הפעלה חוזרת של הסאונד
        audioSource.playOnAwake = false; // למנוע הפעלה אוטומטית
    }

    private void Start()
    {
        // הפעלת הסאונד
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
            // עצירת הסאונד לפני מעבר לסצנה
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            SceneManager.LoadScene(sceneIndex);
            GameManager.Instance.RestartFullGame();
        }
    }
    
    // [SerializeField] private int sceneIndex;
    // // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Return))
    //     {
    //         SceneManager.LoadScene(sceneIndex);
    //         GameManager.Instance.RestartFullGame();
    //     }
    //     
    // }
}
