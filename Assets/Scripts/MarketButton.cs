using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MarketButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMarket()
    {
        SceneManager.LoadScene("MarketScene");
    }   
}
