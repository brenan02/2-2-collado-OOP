using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonMarket : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
    }

    public void BackToGame()
    {
        // Notify Main_Manager that we're returning to plant scene
        if (Main_Manager.Instance != null)
        {
            Main_Manager.Instance.OnReturnToPlant();
        }
        SceneManager.LoadScene("GARDEN");
    }
}