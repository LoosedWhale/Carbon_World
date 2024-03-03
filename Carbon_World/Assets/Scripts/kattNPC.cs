using System.Collections;
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
    public Text dialogTextInteract;

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

    // Cooldown variables
    public float interactionCooldown = 0.5f;
    private float lastInteractionTime;

   public void Start(){
        dialogTextInteract.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        // Check if the interaction cooldown has passed
        if (Time.time - lastInteractionTime >= interactionCooldown)
        {
            // If 'E' is pressed and player is close
            if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
            {
                // Update last interaction time
                lastInteractionTime = Time.time;

                // If dialogue panel is active, reset the text
                if (DialougePanel.activeInHierarchy)
                {
                    zeroText();
                }
                else
                {
                    // Otherwise, start the dialogue
                    dialogTextInteract.gameObject.SetActive(false);
                    DialougePanel.SetActive(true);
                    StartCoroutine(Typing());
                    boyDialouge.sprite = dialougeSprites[0];
                    boyNameText.text = boyName[0];
                }
            }
        }

        // If the current dialogue is complete, activate the continue button
        if (boyDialougeText.text == dialouge[index])
        {
            contButton.SetActive(true);
        }
    }

    // Method to reset the dialogue
    public void zeroText()
    {
        boyDialougeText.text = "";
        index = 0;
        DialougePanel.SetActive(false);
        contButton.SetActive(false);
    }

    // Coroutine to type out the dialogue
    IEnumerator Typing()
    {
        foreach (char letter in dialouge[index].ToCharArray())
        {
            if (!playerIsClose) break;
            boyDialougeText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    // Method to go to the next line of dialogue
    public void NextLine()
    {
        contButton.SetActive(false);

        // If there are more dialogues, go to the next one
        if (index < dialouge.Length - 1)
        {
            index++;
            boyDialougeText.text = "";
            StartCoroutine(Typing());
            boyDialouge.sprite = dialougeSprites[index];
            boyNameText.text = boyName[index];
        }
        else
        {
            // Otherwise, reset the dialogue
            zeroText();
        }
    }

    // Method called when player enters the NPC's trigger area
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            dialogTextInteract.gameObject.SetActive(true);
            playerIsClose = true;
            
        }
    }

    // Method called when player exits the NPC's trigger area
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogTextInteract.gameObject.SetActive(false);
            playerIsClose = false;
            
            zeroText();
        }
    }
}
