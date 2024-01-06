using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is used to handle scene loading and quitting the game.
/// </summary>
public class SceneHandler : MonoBehaviour
{
    /// <summary>
    /// Loads a scene by name.
    /// <paramref name="sceneToLoad">The name of the scene to load.</paramref>
    /// </summary>
    /// <param name="sceneToLoad"></param>
    public void LoadSceneByName(string sceneToLoad)
    {
        SceneManager.LoadSceneAsync(sceneToLoad);
    }

    /// <summary>
    /// Loads a scene by index.
    /// <paramref name="sceneToLoad">The index of the scene to load.</paramref>
    /// </summary>
    /// <param name="sceneToLoad"></param>
    public void LoadSceneByIndex(int sceneToLoad)
    {
        SceneManager.LoadSceneAsync(sceneToLoad);
    }

    /// <summary>
    /// Quits the game.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
