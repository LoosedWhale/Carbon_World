using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class puzzle_open_kill : MonoBehaviour
{
    public Text interactText;
    public Text interactWarningText;
    public bool closeEnough;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update(){
        if(closeEnough && Input.GetKeyDown(KeyCode.F)) {
            if(Stats.fireballKills < 6) {
                interactWarningText.gameObject.SetActive(true);
            } else {
                Destroy(gameObject);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.name.Equals("Player"))
        {
            interactText.gameObject.SetActive(true);
            closeEnough = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.name.Equals("Player")){
            interactWarningText.gameObject.SetActive(false);
            interactText.gameObject.SetActive(false);
            closeEnough = false;
        }
    }
}
