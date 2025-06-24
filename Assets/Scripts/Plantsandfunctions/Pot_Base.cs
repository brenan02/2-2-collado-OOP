using UnityEngine;

public class Pot_Base : MonoBehaviour
{

    // Debug
    public GameObject plantPrefab;

    public Plant_Base ownedPlant;

    public Transform spawnPoint;

    public int potID;

    private static int nextPotID = 0;

    public Transform Holder; // Assign in Inspector

    public Transform plantHolder; // Assign this in Inspector

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
        GameObject newPlant = Instantiate(plantPrefab, spawnPoint.position, Quaternion.identity, plantHolder);
        newPlant.transform.localPosition = Vector3.zero; // or adjust as needed
        newPlant.transform.localScale = Vector3.one; // or your desired scale

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
