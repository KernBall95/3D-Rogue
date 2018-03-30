using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

	public void StartGameButton(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
