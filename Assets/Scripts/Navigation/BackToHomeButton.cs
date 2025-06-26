using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToHomeButton : MonoBehaviour
{
    // Called when the Back/Home button is pressed in the Garden scene
    public void OnBackToHomeButton()
    {
        if (Main_Manager.Instance != null)
            Main_Manager.Instance.SaveGame();

        // Navigate back to the Main Menu scene
        SceneManager.LoadScene("MENU");
    }
} 