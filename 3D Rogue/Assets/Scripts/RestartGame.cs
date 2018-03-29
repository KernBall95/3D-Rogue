using UnityEngine.SceneManagement;
using UnityEngine;

public class RestartGame : MonoBehaviour {

	public void RestartScene()
    {
        SceneManager.LoadScene("Scene 1");
    }
	
}
