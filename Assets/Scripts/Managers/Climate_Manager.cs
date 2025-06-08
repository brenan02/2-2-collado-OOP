using TMPro;
using UnityEngine;

public class Climate_Manager : MonoBehaviour
{

    public TMP_Text ClimateTMP;
    public int ClimateIndex = 0; // 0 = Wet, 1 = Dry



    // For Timer
    public float TimeBetweenDelay;
    float NextTimeDelay;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NextTimeDelay = TimeBetweenDelay;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Time.time >= NextTimeDelay)
        {
            NextTimeDelay = Time.time + TimeBetweenDelay;

            if(ClimateIndex == 0)
            {
                SetAllPlantRequirements(5);
                ClimateIndex = 1;
                ClimateTMP.text = "Climate: Dry";
            } else
            {
                SetAllPlantRequirements(3);
                ClimateIndex = 0;
                ClimateTMP.text = "Climate: Wet";
            }
        }
    }


    public void SetAllPlantRequirements(int inGrowthRequirement)
    {
        Plant_Base[] allPlants = FindObjectsByType<Plant_Base>(FindObjectsSortMode.None);

        if (allPlants.Length >= 0)
        {
            foreach (var plant in allPlants)
            {
                if (plant.plantLevel == 1)
                {
                    plant.waterReq = inGrowthRequirement;
                }
            }
        }
        
    }


}
