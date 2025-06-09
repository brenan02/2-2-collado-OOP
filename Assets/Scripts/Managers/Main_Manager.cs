using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Main_Manager : MonoBehaviour
{
    // player data
    // Plant Harvests
    public int gameGold;



    // Singleton Pattern
    public static Main_Manager Instance { get; private set; }
    private void Awake()
    {
        // Preventing replication of this Manager
        if (Instance != null)
        {
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (SceneManager.GetActiveScene().name == "SampleScene")
            {
                SceneManager.LoadScene("MarketScene");
            }
            else
            {
                SceneManager.LoadScene("SampleScene");
            }
            
        }
    }

}
