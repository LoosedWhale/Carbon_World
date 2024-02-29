using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class kattNPC : MonoBehaviour
{   

    // UI elements and related variables
    public GameObject DialougePanel;
    public Image boyDialouge;
    public Text boyDialougeText;
    public Text boyNameText;


    // Dialogue and name arrays
    public string[] dialouge;
    public string[] boyName;

    // Array of sprites for dialogue
    public Sprite[] dialougeSprites;

    // Index to keep track of current dialogue
    private int index;

    // Continue button and typing speed
    public GameObject contButton;
    public float wordSpeed;

    // Flag to check if player is close
    public bool playerIsClose;

    // Update is called once per frame
    void Update()
    {
        // If 'E' is pressed and player is close
        if(Input.GetKeyDown(KeyCode.E) && playerIsClose){
    

            // If dialogue panel is active, reset the text
            if(DialougePanel.activeInHierarchy){
                zeroText();
            }
            else{
                // Otherwise, start the dialogue
                DialougePanel.SetActive(true);
                StartCoroutine(Typing());
                boyDialouge.sprite = dialougeSprites[0];
                boyNameText.text = boyName[0];
            }
        }

        // If the current dialogue is complete, activate the continue button
        if(boyDialougeText.text == dialouge[index]){
            contButton.SetActive(true);
        }
    }

    // Method to reset the dialogue
    public void zeroText(){
        boyDialougeText.text = "";
        index = 0;
        DialougePanel.SetActive(false);
        contButton.SetActive(false);
    }

    // Coroutine to type out the dialogue
    IEnumerator Typing(){
        foreach(char letter in dialouge[index].ToCharArray()){
            if(!playerIsClose) break;
            boyDialougeText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    // Method to go to the next line of dialogue
    public void NextLine(){
        contButton.SetActive(false);

        // If there are more dialogues, go to the next one
        if(index < dialouge.Length - 1){
            index++;
            boyDialougeText.text = "";
            StartCoroutine(Typing());
            boyDialouge.sprite = dialougeSprites[index];
            boyNameText.text = boyName[index];
        }
        else{
            // Otherwise, reset the dialogue
            zeroText();
        }
    }

    // Method called when player enters the NPC's trigger area
    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            playerIsClose = true;
        }
    }

    // Method called when player exits the NPC's trigger area
    private void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Player")){
            playerIsClose = false;
            zeroText();
        }
    }
}