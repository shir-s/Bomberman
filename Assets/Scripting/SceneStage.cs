using System.Collections;
using UnityEngine;

public class SceneStage : MonoBehaviour
{
    
    [SerializeField] private float sceneTime;
    [SerializeField] private int sceneIndex;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(sceneTime);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }
  
}
