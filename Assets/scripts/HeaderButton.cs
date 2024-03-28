using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeaderButton : MonoBehaviour
    
{
    public void SwitchToHomePage()
    {
        // Replace "Scene2" with the name of your destination scene
        SceneManager.LoadScene("gameExcercises");
    }

    public void backButton()
    {
        // Replace "Scene2" with the name of your destination scene
        SceneManager.LoadScene("gameExcercises");
    }
    public void SwitchToGameScreen()
    {
        // Replace "Scene2" with the name of your destination scene
        SceneManager.LoadScene("WelcometoGames");
    }

}
