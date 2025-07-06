using UnityEngine;

public class Pot_Base : MonoBehaviour  //can be attached to objects
{

    public GameObject plantPrefab; // plant prefab the can be spawned in the pot

    public Plant_Base ownedPlant;

    public Transform spawnPoint; // defines where plant spawn in the pot

    public int potID; // pot identifier

    private static int nextPotID = 0;

    public Transform Holder; // to adjust spawnpoint in pot position in inspector

    public Transform plantHolder; // to adjust plant in spawnpoint (inside the pot) position in inspector

    private Renderer potRenderer; // Renderer component for changing pot color
    private Color originalColor; // Stores the original color of the pot
    public Color highlightColor = Color.yellow; // Color to use for highlighting

    // Static reference to the currently selected pot
    private static Pot_Base selectedPot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (plantPrefab == null)
        {
            return;
        }

        spawnPlant(plantPrefab); //spawn plant if prefab is assigned to a pot

        potRenderer = GetComponent<Renderer>(); // Get the Renderer component of pot
        if (potRenderer != null)
            originalColor = potRenderer.material.color; // Store the original color for later restoration
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        // Deselect the previously selected pot (if any and not this one)
        if (selectedPot != null && selectedPot != this)
        {
            selectedPot.Deselect();
        }

        // Select this pot and highlight it
        selectedPot = this;
        Highlight();
    }

    // Changes the pot's color to the highlight color
    public void Highlight()
    {
        if (potRenderer != null)
            potRenderer.material.color = highlightColor;
    }

    // Restores the pot's color to its original color
    public void Deselect()
    {
        if (potRenderer != null)
            potRenderer.material.color = originalColor;
    }

    public void spawnPlant(GameObject inPlantPrefab) //for plant creation
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

        //set the owned plant as the summoned plant 
        ownedPlant = plantComponent;
    }




}
