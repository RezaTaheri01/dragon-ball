using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public string sceneName;
    public int sceneIndex;
    public void LoadSceneByName()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneByIndex()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
