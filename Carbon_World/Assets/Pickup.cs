using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    //Den countar faktiskt inte.... den går tillbaks till 0 efter varje skript
    public int pickUpCounter = 0;
    [SerializeField]
    public Text pickUpText;
    public bool pickUpAllowed;
    public Text ItemsPickedUp;
    

    public void Start(){
        pickUpText.gameObject.SetActive(false);
    }

    public void Update(){
        if(pickUpAllowed && Input.GetKeyDown(KeyCode.F))
        PickUp();
    }

    public void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.name.Equals("Player"))
        {
            pickUpText.gameObject.SetActive(true);
            pickUpAllowed = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.name.Equals("Player")){
            pickUpText.gameObject.SetActive(false);
            pickUpAllowed = false;
        }
    }

    public void PickUp(){
        pickUpCounter = pickUpCounter + 1;
        ItemsPickedUp.text = "Items: " + pickUpCounter + "/5";
        Destroy(gameObject);
    }
}
