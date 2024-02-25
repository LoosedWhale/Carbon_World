using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mazeDOOR : MonoBehaviour
{

    public Text interactTextDoorMaze;
    public Text interactWarningTextDoorMaze;
    public bool closeEnoughDoorMaze;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update(){
        if(closeEnoughDoorMaze && Input.GetKeyDown(KeyCode.F)) {
            if(Stats.keyCounter < 1) {
                interactWarningTextDoorMaze.gameObject.SetActive(true);
            } else {
                Destroy(gameObject);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.name.Equals("Player"))
        {
            interactTextDoorMaze.gameObject.SetActive(true);
            closeEnoughDoorMaze = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.name.Equals("Player")){
            interactWarningTextDoorMaze.gameObject.SetActive(false);
            interactTextDoorMaze.gameObject.SetActive(false);
            closeEnoughDoorMaze = false;
        }
    }
}
