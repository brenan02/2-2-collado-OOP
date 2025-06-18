using UnityEngine;

public class Pot_Base : MonoBehaviour
{

    // Debug
    public GameObject plantPrefab;

    public Plant_Base ownedPlant;

    public Transform spawnPoint;

    public int potID;

    private static int nextPotID = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        // Debug

        if (plantPrefab == null)
        {
            return;
        }

        spawnPlant(plantPrefab);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void spawnPlant(GameObject inPlantPrefab)
    {
        // instantiate =  spawn,  quaternion = rotation, identity = no rotation
        GameObject newPlant = Instantiate(plantPrefab, spawnPoint.position, Quaternion.identity);

        // getting plantbase component from spawned plant 
        Plant_Base plantComponent = newPlant.GetComponent<Plant_Base>();

        if (plantComponent != null)
        {
            //set the owner of the plant to this pot base, assign to specific pot
            plantComponent.owner = this;
        }

        //set teh owned plant as the summoned plant 
        ownedPlant = plantComponent;
    }




}
