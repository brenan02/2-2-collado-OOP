using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class MainMenu : MonoBehaviour
{
    [Header("UI References")]
    public GameObject playPopup; // Assign the popup panel in the inspector
    public GameObject newGameConfirmPopup; // Assign this in the inspector to NewGameConfirmPopup
    public GameObject darkOverlay; // Assign the overlay panel here
    public GameObject noSaveDialog; // Reference to the no save dialog

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
        if (Main_Manager.Instance != null && Main_Manager.Instance.SaveFileExists())
        {
            if (darkOverlay != null) darkOverlay.SetActive(true);
            if (newGameConfirmPopup != null) newGameConfirmPopup.SetActive(true);
        }
        else
        {
            StartNewGame();
        }
    }

    public void OnConfirmNewGame()
    {
        StartNewGame();
        if (newGameConfirmPopup != null) newGameConfirmPopup.SetActive(false);
        if (darkOverlay != null) darkOverlay.SetActive(false);
    }

    public void OnCancelNewGame()
    {
        if (newGameConfirmPopup != null) newGameConfirmPopup.SetActive(false);
        if (darkOverlay != null) darkOverlay.SetActive(false);
    }

    private void StartNewGame()
    {
        if (Main_Manager.Instance != null)
            Main_Manager.Instance.NewGame();
        SceneManager.LoadScene("GARDEN");
    }

    // Called when Load Game is pressed
    public void OnLoadGameButton()
    {
        if (Main_Manager.Instance != null && Main_Manager.Instance.SaveFileExists())
        {
            SceneManager.LoadScene("GARDEN");
        }
        else
        {
            if (darkOverlay != null)
                darkOverlay.SetActive(true);
            if (noSaveDialog != null)
                noSaveDialog.SetActive(true);
        }
    }

    // Add this method for the exit button
    public void OnCloseNoSaveDialog()
    {
        if (noSaveDialog != null)
            noSaveDialog.SetActive(false);
        if (darkOverlay != null)
            darkOverlay.SetActive(false);
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