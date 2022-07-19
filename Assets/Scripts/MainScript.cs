using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour
{
    public string sceneName = "Stage1";
    
    public void ClickStart()
    {
        Debug.Log("Loading");
        SceneManager.LoadScene(sceneName);
    }
    public void ClickLoad()
    {

    }
    public void ClickExit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
