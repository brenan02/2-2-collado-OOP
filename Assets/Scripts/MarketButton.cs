using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarketButton : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
    }

    public void GoToMarket()
    {
        // Store plant data before switching scenes
        if (Main_Manager.Instance != null)
        {
            Main_Manager.Instance.OnSwitchToMarket();
        }
        SceneManager.LoadScene("MarketScene");
    }
}