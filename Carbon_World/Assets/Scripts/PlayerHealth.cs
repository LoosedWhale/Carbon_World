using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    
    public float health = 10f;  // Player health
    private Animator animator;
    

    private PlayerController player;  




    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<PlayerController>();
    }
    public float Health
    {
        set
        {
            health = value;

            if (health <= 0)
            {
                Defeated();
                StartCoroutine(RemovePlayerWithDelay());
            }
        }
        get { return health; }
    }

    public void Defeated()
    {
        // Player defeated
        animator.SetTrigger("player_death");
    }

    private IEnumerator RemovePlayerWithDelay()
    {
        yield return new WaitForSeconds(1.5f);
        RemovePlayer();
    }

    public void RemovePlayer()
    {
        // Remove player
        print("Player defeated");
        player.GetComponent<PlayerController>().enabled = false;
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        //Destroy(gameObject);

       
    }
   



    
}
