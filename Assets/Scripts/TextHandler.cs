using TMPro;
using UnityEngine;




public class TextHandler : MonoBehaviour
{

    // Singleton Pattern
    public static TextHandler Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // DontDestroyOnLoad(gameObject);
    }


    public TMP_Text textGold;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateGoldText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateGoldText()
    {
        textGold.text = Main_Manager.Instance.gameGold.ToString() + "g";
    }
}
