using UnityEngine;

public class PauseScreen : MonoBehaviour {

    public GameObject pauseMenu;

    [HideInInspector]public bool isPaused = false;
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            ChangePauseState();      

        if (isPaused == true)
        {
            pauseMenu.SetActive(true);
            Cursor.visible = true;
        }
        else if (isPaused == false)
        {
            pauseMenu.SetActive(false);
            Cursor.visible = false;
        }
    }

    public void ChangePauseState()
    {
        if(isPaused == true)
            isPaused = false;
        
        else if(isPaused == false)
            isPaused = true;
        
    }
}
