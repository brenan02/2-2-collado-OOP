using UnityEngine;
using TMPro; // Add this line to use TextMeshPro
using System.Collections; // Add this line to use Coroutines

public class PurchaseManager : MonoBehaviour
{
    public static PurchaseManager Instance { get; private set; }

    [Header("UI References")]
    public TMP_Text validationText; // <-- ADDED: The text box for validation messages

    [Header("Settings")]
    public float messageDisplayTime = 2f; // <-- ADDED: How long the message appears

    private Coroutine validationCoroutine; // <-- ADDED

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Make sure the validation text is initially hidden
        if (validationText != null)
        {
            validationText.gameObject.SetActive(false);
        }
    }

    public void BuyPlant(int price)
    {
        if (Main_Manager.Instance == null)
        {
            Debug.LogError("PurchaseManager Error: Main_Manager instance not found. Make sure you start the game from the GARDEN scene.");
            return;
        }

        if (Main_Manager.Instance.gameGold >= price)
        {
            Main_Manager.Instance.gameGold -= price;
            Debug.Log("Plant Successfully");
            ShowMessage("Plant Successfully"); // <-- ADDED: Show success message

            // Update UI or other game state
            if (TextHandler.Instance != null)
            {
                TextHandler.Instance.UpdateGoldText();
            }
        }
        else
        {
            Debug.Log("insufficient balance");
            ShowMessage("Insufficient balance"); // <-- ADDED: Show failure message
        }
    }

    // --- ADDED THIS ENTIRE SECTION ---
    private void ShowMessage(string message)
    {
        if (validationText == null)
        {
            Debug.LogWarning("Validation Text UI is not assigned in the PurchaseManager Inspector.");
            return;
        }

        if (validationCoroutine != null)
        {
            StopCoroutine(validationCoroutine);
        }
        validationCoroutine = StartCoroutine(ShowValidationMessage(message));
    }

    private IEnumerator ShowValidationMessage(string message)
    {
        validationText.text = message;
        validationText.gameObject.SetActive(true);

        yield return new WaitForSeconds(messageDisplayTime);

        validationText.gameObject.SetActive(false);
    }
    // --- END OF ADDED SECTION ---
}
