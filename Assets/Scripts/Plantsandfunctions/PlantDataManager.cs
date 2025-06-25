// GameManager.cs
using UnityEngine;
using System.Collections.Generic; // To use Dictionary

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // This dictionary will store the state of each plant based on its potID
    // Key: potID (int)
    // Value: PlantState (a new class to hold plant data)
    public Dictionary<int, PlantState> plantStates = new Dictionary<int, PlantState>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Ensures only one GameManager exists
        }
    }

    // Call this method from your Plant_Base when its state changes
    public void SavePlantState(int potId, int level, int phase, int water, int fertilizer, string phaseString)
    {
        if (plantStates.ContainsKey(potId))
        {
            plantStates[potId].plantLevel = level;
            plantStates[potId].plantPhase = phase;
            plantStates[potId].waterCount = water;
            plantStates[potId].fertilizerCount = fertilizer;
            plantStates[potId].plantPhase_String = phaseString;
            Debug.Log($"[GameManager] Updated Plant State for Pot {potId}: Level {level}, Phase {phase}");
        }
        else
        {
            // If it's a new plant/pot, add it to the dictionary
            plantStates.Add(potId, new PlantState(level, phase, water, fertilizer, phaseString));
            Debug.Log($"[GameManager] Added new Plant State for Pot {potId}: Level {level}, Phase {phase}");
        }
    }

    // Call this method from Pot_Base to retrieve a plant's state
    public PlantState GetPlantState(int potId)
    {
        if (plantStates.ContainsKey(potId))
        {
            return plantStates[potId];
        }
        return null; // No state found for this pot
    }

    // Optional: Call this to clear all plant states (e.g., for a "New Game" option)
    public void ClearAllPlantStates()
    {
        plantStates.Clear();
        Debug.Log("[GameManager] All plant states cleared.");
    }
}

// A simple class to hold the relevant plant data
[System.Serializable] // Make it serializable if you ever want to save it to disk (not needed for DDOL)
public class PlantState
{
    public int plantLevel;
    public int plantPhase;
    public string plantPhase_String;
    public int waterCount;
    public int fertilizerCount;
    // Add any other state variables you need to persist

    public PlantState(int level, int phase, int water, int fertilizer, string phaseString)
    {
        plantLevel = level;
        plantPhase = phase;
        waterCount = water;
        fertilizerCount = fertilizer;
        plantPhase_String = phaseString;
    }
}