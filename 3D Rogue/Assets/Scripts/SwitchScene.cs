﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {

	public void SwitchToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
