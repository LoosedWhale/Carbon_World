using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class kattNPC : MonoBehaviour
{   
    public GameObject DialougePanel;
    public Image boyDialouge;
    public Text boyDialougeText;
    public string[] dialouge;
    public Sprite[] dialougeSprites;
    private int index;

    public GameObject contButton;
    public float wordSpeed;
    public bool playerIsClose;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerIsClose){

            if(DialougePanel.activeInHierarchy){
                zeroText();
            }
            else{
                DialougePanel.SetActive(true);
                StartCoroutine(Typing());
                boyDialouge.sprite = dialougeSprites[0];
            }
        }

        if(boyDialougeText.text == dialouge[index]){
            contButton.SetActive(true);
        }

    }

    public void zeroText(){

        boyDialougeText.text = "";
        index = 0;
        DialougePanel.SetActive(false);
        contButton.SetActive(false);
    }

    IEnumerator Typing(){
        foreach(char letter in dialouge[index].ToCharArray()){
            if(!playerIsClose) break;
            boyDialougeText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }

    }

    public void NextLine(){

        contButton.SetActive(false);

        if(index < dialouge.Length - 1){
            index++;
            boyDialougeText.text = "";
            StartCoroutine(Typing());
            boyDialouge.sprite = dialougeSprites[index];
        }
        else{
            zeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            playerIsClose = true;
        }
    }

        private void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Player")){
            playerIsClose = false;
            zeroText();
        }
    }
}
