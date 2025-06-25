using UnityEngine;

public class PotActivator : MonoBehaviour
{
    public GameObject[] pots; // Assign your Pot GameObjects in the Inspector

    void Start()
    {
        for (int i = 0; i < pots.Length; i++)
        {
            if (Main_Manager.Instance != null)
                pots[i].SetActive(Main_Manager.Instance.potsUnlocked[i]);
            else
                pots[i].SetActive(i == 0); // Fallback: only Pot 1 active
        }
    }
}