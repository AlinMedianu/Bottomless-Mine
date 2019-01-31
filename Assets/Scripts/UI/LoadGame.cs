using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public  class LoadGame : MonoBehaviour{

    public void StartGame()
    {
        SceneManager.LoadScene("Mine"); //Used on buttons to Load the Game
        Time.timeScale = 1; //Makes sure game isn't paused
    }
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; //Used on quit to Stop Game/Close the application
        #else
            Application.Quit();
        #endif

    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Main menu"); //Used on buttons to Load Main Menu
    }
    public void LoadLevelSelect()
    {
        SceneManager.LoadScene("Level Select"); //Used on buttons to Load level select menu
    }

}
