using UnityEngine;

public class GameQuitter : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Quit Game called!");
        Application.Quit();

#if UNITY_EDITOR
        // This will stop play mode in the editor
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
} 