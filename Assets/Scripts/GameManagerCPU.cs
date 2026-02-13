using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class GameManagerCPU : MonoBehaviour
{
    public static GameManagerCPU instance;
    public GameObject winText;
    private int correctPlacementCounter = 0; //counts number of correct slots

    public List<CPUSlotScript> cpuSlots; //list of CPU slots
    public TextMeshPro livesText; //text to show remaining lives

    public GameObject lossText; 

    public int lives = 3; //number of lives 


    void Start()
    {
        instance = this;
        winText.SetActive(false); //hide win text at start of game
        lossText.SetActive(false); //hide loss text at start of game
        updateLivesText();
    }

    public void updateLivesText()
    {
        livesText.text = "Lives: " + lives; //update the lives text
    }
    public void loseLife()
    {
        lives--; //decrease lives
        updateLivesText(); //update the lives text

        if (lives <= 0)
        {
            lossText.SetActive(true); //display loss text if no more lives
        }
    }

    public void checkCompletion()
    {
        if (lives <= 0) return; //if player has already lost, return

        correctPlacementCounter++;

        //if all 4 components are placed in correct slot, display win text
        if (correctPlacementCounter == 4)
        {
            winText.SetActive(true); 
        }
        
    }

}