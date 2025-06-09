using TMPro;
using UnityEngine;

public class Plant_Base : MonoBehaviour
{

    public Pot_Base owner;
    public GameObject indicator;
    GameObject indicatorUsed;

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








    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        plantPhase_String = "Seedling";
        originalSprite = GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
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
        if(waterCount >= waterReq)
        {
            // water count reset, level up plant
            waterCount = 0;
            plantPhase++;

            switch (plantPhase)
            {
                case 1:
                    plantPhase_String = "Premature";
                    GetComponent<SpriteRenderer>().sprite = spritePhases[0];
                    break;
                case 2:
                    plantPhase_String = "Mature";
                    GetComponent<SpriteRenderer>().sprite = spritePhases[1];
                    gameObject.tag = "ReadyHarvest"; // marks an object to add functionality
                    break;
                default:
                    break;
            }
 
        }

        //INDICATOR
        if(isReqTaken == false)
        {
            if(indicatorUsed != null || this.CompareTag("ReadyHarvest"))
            {
                return;
            }

            Vector3 offset = new Vector3(1, 1, 0);
            Vector3 waterOffset = owner.spawnPoint.position + offset;
            indicatorUsed = Instantiate(indicator,waterOffset, Quaternion.identity);

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
