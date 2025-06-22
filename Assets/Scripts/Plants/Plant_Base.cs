using TMPro;
using UnityEngine;

public class Plant_Base : MonoBehaviour
{
    public Pot_Base owner;
    public GameObject indicator;
    GameObject indicatorUsed;

    public GameObject harvestIndicator;
    GameObject harvestIndicatorUsed;

    public string plantName;
    public Sprite originalSprite;
    public Sprite[] spritePhases;

    public int plantLevel;
    public int plantPhase;
    public string plantPhase_String;

    public int waterReq;
    public int fertilizerReq;

    public int waterCount;
    public int fertilizerCount;

    public int cooldown;
    public bool isReqTaken;

    public int produceValue;

    int TimePassed = 0;

    // Timer
    public float TimeBetweenDelay;
    float NextTimeDelay;

    public int defaultWaterReq = 2; 

    void Start()
    {
        plantPhase_String = "Seedling";
        originalSprite = GetComponent<SpriteRenderer>().sprite;

        // Restore plant data if available
        RestorePlantData();
    }

    private void RestorePlantData()
    {
        if (Main_Manager.Instance != null && owner != null)
        {
            PlantData data = Main_Manager.Instance.GetPlantData(owner.potID);
            if (data != null)
            {
                // Restore plant state
                plantName = data.plantName;
                plantLevel = data.plantLevel;
                plantPhase = data.plantPhase;
                plantPhase_String = data.plantPhase_String;
                waterCount = data.waterCount;
                fertilizerCount = data.fertilizerCount;
                isReqTaken = data.isReqTaken;

                Debug.Log("Restored plantPhase: " + plantPhase);

                // Update sprite based on phase
                if (plantPhase > 0 && spritePhases != null && spritePhases.Length > 0)
                {
                    int spriteIndex = Mathf.Min(plantPhase - 1, spritePhases.Length - 1);
                    //Debug.Log("Setting sprite to index: " + spriteIndex);
                    GetComponent<SpriteRenderer>().sprite = spritePhases[spriteIndex];
                }

                // Update tag if plant is ready for harvest
                if (plantPhase >= 2)
                {
                    gameObject.tag = "ReadyHarvest";
                    //Debug.Log(gameObject.name + " tag set to ReadyHarvest!");
                }
            }
        }
    }

    void Update()
    {
   
        // checks if plant is watered
        if (isReqTaken == true)
        {
            // slow down time 
            if (Time.time >= NextTimeDelay)
            {
                NextTimeDelay = Time.time + TimeBetweenDelay;

                // counts time that passed
                TimePassed += 1;

                // if cd is finished
                if (TimePassed >= cooldown)
                {
                    //reset timer and add water
                    TimePassed = 0;
                    isReqTaken = false;
                    waterCount++;
                }
            }
        }

        //TIMER
        // checks if water count meets requirement of plant
        if (waterCount >= waterReq)
        {
            // water count reset, level up plant
            waterCount = 0;
            plantPhase++;
            //Debug.Log("Level up! New plantPhase: " + plantPhase);

            switch (plantPhase)
            {
                case 1:
                    plantPhase_String = "Premature";
                    //Debug.Log("Setting sprite to index 1 (Premature)");
                    GetComponent<SpriteRenderer>().sprite = spritePhases[0];
                    break;
                case 2:
                    plantPhase_String = "Mature";
                    //Debug.Log("Setting sprite to index 1 (Mature)");
                    GetComponent<SpriteRenderer>().sprite = spritePhases[1];
                    gameObject.tag = "ReadyHarvest";
                    break;
                default:
                    break;
            }
        }

        // HARVEST INDICATOR (check this first)
        if (this.CompareTag("ReadyHarvest"))
        {
            if (harvestIndicatorUsed == null)
            {
                //Debug.Log("Spawning harvest indicator!");
                Vector3 offset = new Vector3(0, 0, 0); 
                Vector3 harvestOffset = owner.spawnPoint.position + offset;
                harvestIndicatorUsed = Instantiate(harvestIndicator, harvestOffset, Quaternion.identity, this.transform);
            }
        }
        else
        {
            if (harvestIndicatorUsed != null)
            {
                //Debug.Log("Destroying harvest indicator!");
                Destroy(harvestIndicatorUsed);
            }
        }

        //INDICATOR
        if (isReqTaken == false)
        {
            if (indicatorUsed != null || this.CompareTag("ReadyHarvest"))
            {
                return;
            }

            Vector3 offset = new Vector3(1, -3, 0);
            Vector3 waterOffset = owner.spawnPoint.position + offset;
            indicatorUsed = Instantiate(indicator, waterOffset, Quaternion.identity);

        }
        else
        {
            if (indicatorUsed == null)
            {
                return;
            }
            Destroy(indicatorUsed);
        }
    }

    public void Harvest()
    {
        plantPhase = 0;
        plantPhase_String = "Seedling";
        GetComponent<SpriteRenderer>().sprite = originalSprite;
        gameObject.tag = "Untagged";

        print("Harvest Completed!");

        // gets singleton main manager in level, gets harvest count of a plant
        Main_Manager.Instance.gameGold += produceValue;
        TextHandler.Instance.UpdateGoldText();
    }
}