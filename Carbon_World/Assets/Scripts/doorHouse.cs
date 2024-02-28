using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class doorHouse : MonoBehaviour
{

    public Text interactTextDoor;
    public Text interactWarningTextDoor;
    public bool closeEnoughDoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update(){
        if(closeEnoughDoor && Input.GetKeyDown(KeyCode.F)) {
            if(Stats.pickupCounter < 5) {
                interactWarningTextDoor.gameObject.SetActive(true);
            } else {
                Destroy(gameObject);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.name.Equals("Player"))
        {
            interactTextDoor.gameObject.SetActive(true);
            closeEnoughDoor = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.name.Equals("Player")){
            interactWarningTextDoor.gameObject.SetActive(false);
            interactTextDoor.gameObject.SetActive(false);
            closeEnoughDoor = false;
        }
    }
}