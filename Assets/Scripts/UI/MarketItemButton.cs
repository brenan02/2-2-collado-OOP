using UnityEngine;
using UnityEngine.UI;

public class MarketItemButton : MonoBehaviour
{
    public int itemPrice;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(BuyItem);
        }
    }

    public void BuyItem()
    {
        if (PurchaseManager.Instance != null)
        {
            PurchaseManager.Instance.BuyPlant(itemPrice);
        }
        else
        {
            Debug.LogError("PurchaseManager instance not found.");
        }
    }
} 