using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Load the next scene
        //changed to scene 5 ? idk but it needs to play the cutscene before.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 6);
    }

    public void QuitGame()
    {
        // Quit the game
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void RePlayGame()
    {
        // Load the next scene
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

}
