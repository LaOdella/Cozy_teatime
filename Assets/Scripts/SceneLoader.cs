using UnityEngine;
using UnityEngine.SceneManagement; // CRUCIAL: Needed to load scenes

public class SceneLoader : MonoBehaviour
{
    // We will call this function when the player clicks a button/icon
    public void LoadSceneByName(string sceneName)
    {
        // This is the command that switches the active scene
        SceneManager.LoadScene(sceneName);
    }
}