﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  public void LoadGame()
    {
        SceneManager.LoadScene(1); //game scene
    }

    public void Quit()
    {
        Application.Quit();
    }
}
