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

    private Renderer potRenderer; // Renderer component for changing pot color
    private Color originalColor; // Stores the original color of the pot
    public Color highlightColor = Color.yellow; // Color to use for highlighting

    // Static reference to the currently selected pot
    private static Pot_Base selectedPot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        // Debug

        if (plantPrefab == null)
        {
            return;
        }

        spawnPlant(plantPrefab);

        potRenderer = GetComponent<Renderer>(); // Get the Renderer component attached to this GameObject
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
