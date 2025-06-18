using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[System.Serializable]
public class PlantData
{
    public string plantName;
    public int plantLevel;
    public int plantPhase;
    public string plantPhase_String;
    public int waterCount;
    public int fertilizerCount;
    public bool isReqTaken;
    public int potID;
}

public class Main_Manager : MonoBehaviour
{
    // player data
    public int gameGold;

    // Plant data storage
    private Dictionary<int, PlantData> plantDataDict = new Dictionary<int, PlantData>();

    // Singleton Pattern
    public static Main_Manager Instance { get; private set; }

    private void Awake()
    {
        // Preventing replication of this Manager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
    }

    // Called by MarketButton when switching to market scene
    public void OnSwitchToMarket()
    {
        StorePlantData();
    }

    // Called by BackButtonMarket when returning to plant scene
    public void OnReturnToPlant()
    {
        // No need to store data when returning to plant scene
        // The plants will restore their data when they start
    }

    public void StorePlantData()
    {
        plantDataDict.Clear();
        Plant_Base[] allPlants = Object.FindObjectsByType<Plant_Base>(FindObjectsSortMode.None);
        foreach (var plant in allPlants)
        {
            if (plant.owner != null)
            {
                PlantData data = new PlantData
                {
                    plantName = plant.plantName,
                    plantLevel = plant.plantLevel,
                    plantPhase = plant.plantPhase,
                    plantPhase_String = plant.plantPhase_String,
                    waterCount = plant.waterCount,
                    fertilizerCount = plant.fertilizerCount,
                    isReqTaken = plant.isReqTaken,
                    potID = plant.owner.potID
                };
                plantDataDict[plant.owner.potID] = data;
            }
        }
    }

    public PlantData GetPlantData(int potID)
    {
        if (plantDataDict.TryGetValue(potID, out PlantData data))
        {
            return data;
        }
        return null;
    }
}