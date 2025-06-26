using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

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

[Serializable]
public class SaveData
{
    public int gameGold;
    public bool[] potsUnlocked;
    public List<PlantData> plantDataList;
}

public class Main_Manager : MonoBehaviour
{
    // player data
    public int gameGold;

    // Plant data storage
    private Dictionary<int, PlantData> plantDataDict = new Dictionary<int, PlantData>();

    // Singleton Pattern
    public static Main_Manager Instance { get; private set; }

    // Array to track unlocked pots: true = unlocked, false = locked
    public bool[] potsUnlocked = new bool[5];

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

        // Initialize pots: only Pot 1 is unlocked at the start
        if (potsUnlocked[0] == false) // Only set if not already loaded from save
        {
            potsUnlocked[0] = true;
            for (int i = 1; i < potsUnlocked.Length; i++)
                potsUnlocked[i] = false;
        }
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
        Plant_Base[] allPlants = UnityEngine.Object.FindObjectsByType<Plant_Base>(FindObjectsSortMode.None);
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

    // Call this when a pot is bought in the market
    public void UnlockPot(int potIndex)
    {
        if (potIndex >= 0 && potIndex < potsUnlocked.Length)
            potsUnlocked[potIndex] = true;
    }

    public void SaveGame()
    {
        StorePlantData(); // Ensure latest data is stored
        SaveData data = new SaveData
        {
            gameGold = gameGold,
            potsUnlocked = potsUnlocked,
            plantDataList = plantDataDict.Values.ToList()
        };
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.persistentDataPath + "/savegame.json", json);
        Debug.Log("Game Saved!");
    }

    public bool LoadGame()
    {
        string path = Application.persistentDataPath + "/savegame.json";
        if (!File.Exists(path))
            return false;

        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        gameGold = data.gameGold;
        potsUnlocked = data.potsUnlocked;
        plantDataDict = data.plantDataList.ToDictionary(p => p.potID, p => p);

        Debug.Log("Game Loaded!");
        return true;
    }

    public bool SaveFileExists()
    {
        return File.Exists(Application.persistentDataPath + "/savegame.json");
    }

    public void NewGame()
    {
        // Delete save file if it exists
        string path = Application.persistentDataPath + "/savegame.json";
        if (File.Exists(path))
            File.Delete(path);

        // Reset game data to default values
        gameGold = 0;
        for (int i = 0; i < potsUnlocked.Length; i++)
            potsUnlocked[i] = (i == 0); // Only first pot unlocked
        plantDataDict.Clear();
        Debug.Log("New Game started. Save file deleted and data reset.");
    }
}