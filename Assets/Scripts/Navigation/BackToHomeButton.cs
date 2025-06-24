using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToHomeButton : MonoBehaviour
{
    // Called when the Back/Home button is pressed in the Garden scene
    public void OnBackToHomeButton()
    {
        // TODO: Add save game logic here before returning to the main menu

        // Navigate back to the Main Menu scene
        SceneManager.LoadScene("MENU");
    }
} 