using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
   [SerializeField] private int sceneIndex;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
          SceneManager.LoadScene(sceneIndex);
        }
        
    }
}
