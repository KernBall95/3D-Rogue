using UnityEngine;

public class PauseScreen : MonoBehaviour {

    public GameObject pauseMenu;

    private bool isPaused = false;
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused == false)
            {
                pauseMenu.SetActive(true);
                Cursor.visible = true;
                isPaused = true;
            }
            else if(isPaused == true)
            {
                pauseMenu.SetActive(false);
                Cursor.visible = false;
                isPaused = false;
            }
        }
	}
}
