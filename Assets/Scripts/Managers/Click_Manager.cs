using JetBrains.Annotations;
using System.Buffers;
using UnityEngine;

public class Click_Manager : MonoBehaviour
{

    public LayerMask clickableLayer;
    public Pot_Base selectedPot;

    public bool isButtonHovered = false;
    
    Plant_Base plant;





    // Singleton Pattern
    public static Click_Manager Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // DontDestroyOnLoad(gameObject);
    }




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isButtonHovered != true && Input.GetMouseButton(0))
        {
            // Get the mouse position in world coordinates
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D[] hits = Physics2D.RaycastAll(cursorPos, Vector2.zero, Mathf.Infinity, clickableLayer);

            if(hits.Length > 0)
            {
                foreach(var hit in hits)
                {
                    
                    selectedPot = hit.collider.GetComponent<Pot_Base>();


                    if(selectedPot.ownedPlant.CompareTag("ReadyHarvest"))
                    {
                        selectedPot.ownedPlant.Harvest();
                    }


                }
            } else
            {
                selectedPot = null;
            }
        }

    }



    public void waterPlant()
    {
        if (selectedPot != null)
        {
            if (selectedPot.ownedPlant != plant)
            {
                plant = selectedPot.ownedPlant;
            }

            if (plant.isReqTaken == false)
            {
                plant.isReqTaken = true;
            }
        }
    }





}
