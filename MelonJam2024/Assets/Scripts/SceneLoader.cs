using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenLoader : MonoBehaviour
{
    public void LoadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
}


