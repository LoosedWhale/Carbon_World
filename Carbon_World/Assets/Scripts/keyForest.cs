using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class keyForest : MonoBehaviour
{
    //THIS CODE IS USED FOR BOTH THE MAZE KEY BUT AS WELL AS THE TRASH IN THE THIRD PUZZLE!
    // THERE ARE DIFFERENT DOOR MECHANISMS BOTH TRASHDOOR AND MAZEDOOR THESE CAN BE MEREGED INTO ONE
    //WITH BETTER CODE BUT I SUCK SORRY
    [SerializeField]
    public Text pickUpTextKey;
    public bool pickUpAllowedKey;


    public void Start(){
        pickUpTextKey.gameObject.SetActive(false);
    }

    public void Update(){
        if(pickUpAllowedKey && Input.GetKeyDown(KeyCode.E))
        PickUpKey();
    }

    public void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.name.Equals("Player"))
        {
            pickUpTextKey.gameObject.SetActive(true);
            pickUpAllowedKey = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.name.Equals("Player")){
            pickUpTextKey.gameObject.SetActive(false);
            pickUpAllowedKey = false;
        }
    }

    public void PickUpKey(){
        //nedan finns i playercontroller
        Stats.trashCounter++; //denna används till enemy på samma sätt när man dödat alla kan man gå vidare i pusslet
        //ovan finns i playercontroller
        
 
        Destroy(gameObject);
    }
}
