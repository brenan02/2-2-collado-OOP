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
                SetAllPlantRequirements(true);
                ClimateIndex = 1;
                ClimateTMP.text = "Climate: Dry";
                ClimateTMP.color = Color.red;
            } else
            {
                SetAllPlantRequirements(false);
                ClimateIndex = 0;
                ClimateTMP.text = "Climate: Wet";
                ClimateTMP.color = Color.blue;
            }

            //Debug.Log("ClimateIndex: " + ClimateIndex + ", NextTimeDelay: " + NextTimeDelay + ", Time: " + Time.time);
        }
    }


    public void SetAllPlantRequirements(bool isDry)
    {
        Plant_Base[] allPlants = FindObjectsByType<Plant_Base>(FindObjectsSortMode.None);

        foreach (var plant in allPlants)
        {
            if (plant.plantLevel == 1)
            {
                if (isDry)
                    plant.waterReq = plant.defaultWaterReq + 1;
                else
                    plant.waterReq = plant.defaultWaterReq;
            }
        }
    }


}
