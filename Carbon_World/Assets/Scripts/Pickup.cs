using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    //bajsa på dig
    [SerializeField]
    public Text pickUpText;
    public bool pickUpAllowed;
    public Text ItemsPickedUp;
  
    public void Start(){
        pickUpText.gameObject.SetActive(false);
    }

    public void Update(){
        if(pickUpAllowed && Input.GetKeyDown(KeyCode.E)){
        
        PickUp();

    }
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
        //nedan finns i playercontroller
        Stats.pickupCounter++; //denna används till enemy på samma sätt när man dödat alla kan man gå vidare i pusslet
        //ovan finns i playercontroller
        ItemsPickedUp.text = "Items: " + Stats.pickupCounter + "/5";
        Destroy(gameObject);
    }
}
