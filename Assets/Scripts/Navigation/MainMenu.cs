using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        MusicManager.Instance.PlayMusic("MenuMusic");
    }

    void Update()
    {
    }

    public void Play()
    {
        //Debug.Log("Play button clicked! Attempting to load GARDEN scene...");
        SceneManager.LoadScene("GARDEN");
        //Debug.Log("SceneManager.LoadScene called for GARDEN");
        MusicManager.Instance.PlayMusic("GardenMusic");
    }
 
    public void Quit()
    {
        Application.Quit();
    }
} 