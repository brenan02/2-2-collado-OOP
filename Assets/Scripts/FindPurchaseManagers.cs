using UnityEngine;

public class FindPurchaseManagers : MonoBehaviour
{
    void Awake()
    {
        Debug.LogWarning("--- RUNNING FIND PURCHASE MANAGERS SCRIPT ---");
        PurchaseManager[] allPurchaseManagers = FindObjectsByType<PurchaseManager>(FindObjectsSortMode.None);

        if (allPurchaseManagers.Length == 0)
        {
            Debug.LogWarning("Found 0 objects with PurchaseManager script in this scene.");
        }
        else
        {
            Debug.LogError("Found " + allPurchaseManagers.Length + " objects with PurchaseManager script! Their names are:");
            foreach (PurchaseManager pm in allPurchaseManagers)
            {
                Debug.LogError("--> GameObject Name: " + pm.gameObject.name);
            }
        }
        Debug.LogWarning("--- FINISHED FIND PURCHASE MANAGERS SCRIPT ---");
    }
} 