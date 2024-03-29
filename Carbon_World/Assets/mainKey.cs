using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainKey: MonoBehaviour
{
    //CODE TO KEYS THAT OPEN THE DIFFERENT "LEVELS"
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
        Stats.keys++; //denna används till enemy på samma sätt när man dödat alla kan man gå vidare i pusslet
        //ovan finns i playercontroller
        
 
        Destroy(gameObject);
    }
}
