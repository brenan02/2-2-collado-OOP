using UnityEngine;

public class PurchaseManager : MonoBehaviour
{
    public static PurchaseManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void BuyPlant(int price)
    {
        if (Main_Manager.Instance.gameGold >= price)
        {
            Main_Manager.Instance.gameGold -= price;
            Debug.Log("Plant Successfully");
            // Update UI or other game state
            if (TextHandler.Instance != null)
            {
                TextHandler.Instance.UpdateGoldText();
            }
        }
        else
        {
            Debug.Log("insufficient balance");
        }
    }
} 