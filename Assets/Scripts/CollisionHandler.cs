using UnityEngine.SceneManagement;
using UnityEngine;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay= 2f;
    void OnCollisionEnter(Collision other) 
    {
       switch (other.gameObject.tag) 
       {
           case "Friendly":
                Debug.Log("This thing is friendly");
                break;
           case "Finish":
                StartSuccessSqeuence();
                break;
           default :
               StartCrashSequence();
               break;
       }    
    }

    void StartSuccessSqeuence()
    {
        //todo add SFX upon crash
        //todo add particle effect upon crash
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel",levelLoadDelay);
    }
    void StartCrashSequence()
    {
        //todo add SFX upon crash
        //todo add particle effect upon crash
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel",levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
