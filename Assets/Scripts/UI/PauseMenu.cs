using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    private Canvas CanvasObject;

    void Start()
    {
        CanvasObject = GetComponent<Canvas>(); //Gets Canvas Object
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("Main menu"); //Used on quit to load the main menu
        Time.timeScale = 1; //Makes sure the game doesnt get paused on new game
    }

    public void ResumeGame()
    {
        Time.timeScale = 1; //Used to unpause the game
        CanvasObject.enabled = false; //Hides the pause menu
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale = 1; //Used to unpause the game
            CanvasObject.enabled = !CanvasObject.enabled; //Shows/Hides the pause Menu
        }
        else
        {
            if (CanvasObject.enabled == true)
            {
                Time.timeScale = 0; //Used to pause the game
            }
        }
    }
}
