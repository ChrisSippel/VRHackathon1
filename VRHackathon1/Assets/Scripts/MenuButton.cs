using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{

    public string levelName = "Test Menu";

    public void SwitchLevel()
    {
        if (levelName != "Quit")
        {
            SceneManager.LoadScene(levelName);
        }
        else
        {
            Application.Quit();
        }               
    }
}
