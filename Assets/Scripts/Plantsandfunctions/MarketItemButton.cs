using UnityEngine;
using UnityEngine.UI;

public class MarketItemButton : MonoBehaviour
{
    public int itemPrice;
    public int potIndexToUnlock; // 1 for Pot 2, 2 for Pot 3, etc.

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(BuyItem);

            // Disable if already unlocked
            if (Main_Manager.Instance != null && Main_Manager.Instance.potsUnlocked[potIndexToUnlock])
            {
                button.interactable = false;
            }
        }
    }

    public void BuyItem()
    {
        if (PurchaseManager.Instance != null)
        {
            PurchaseManager.Instance.BuyPlant(itemPrice, potIndexToUnlock);

            // After purchase, disable the button
            if (Main_Manager.Instance != null && Main_Manager.Instance.potsUnlocked[potIndexToUnlock])
            {
                button.interactable = false;
            }
        }
        else
        {
            Debug.LogError("PurchaseManager instance not found.");
        }
    }
} 