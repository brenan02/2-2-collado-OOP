using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class MainMenu : MonoBehaviour
{
    [Header("UI References")]
    public GameObject playPopup; // Assign the popup panel in the inspector

    private void Start()
    {
        MusicManager.Instance.PlayMusic("MenuMusic");
    }

    void Update()
    {
    }

    // Called when Play button is pressed
    public void OnPlayButton()
    {
        if (playPopup != null)
            playPopup.SetActive(true);
    }

    // Called when New Game is pressed
    public void OnNewGameButton()
    {
        // TODO: Add logic to reset all data for a new game
        // For now, just load the Garden scene
        SceneManager.LoadScene("GARDEN");
    }

    // Called when Load Game is pressed
    public void OnLoadGameButton()
    {
        // TODO: Add logic to load saved data
        // For now, just load the Garden scene
        SceneManager.LoadScene("GARDEN");
    }

    // Optionally, a method to close the popup
    public void OnClosePopup()
    {
        if (playPopup != null)
            playPopup.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
} 