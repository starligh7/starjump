using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscScript : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject Panel;

    public string sceneName = "main";

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Debug.Log("esc");
            if(GameIsPaused){
                Resume();
            }
            else{
                Pause();
            }
        }
    }
    public void Resume(){
        Panel.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause(){
        Panel.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void clickQuit(){
        SceneManager.LoadScene(sceneName);
    }
}