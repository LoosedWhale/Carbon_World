using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

    public int health;
    public int damage;
    //private float timeBtwDamage = 1.5f;

    public Slider healthBar;
    private Animator anim;
    public bool isDead;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public float Health
    {
        set
        {
            health = Mathf.FloorToInt(value);

            if (health <= 0)
            {
                Defeated();
                isDead = true;
                StartCoroutine(RemoveEnemyWithDelay());
            }
        }
        get { return health; }
    }



    public void Defeated()
    {
        anim.SetTrigger("Defeated");
    }
    

    private IEnumerator RemoveEnemyWithDelay()
    {
        yield return new WaitForSeconds(1.5f);
        RemoveEnemy();
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }
    
    private void Update()
    {
        /*
        if (health <= 25) {
            anim.SetTrigger("stageTwo");
        }
        */
  
        /*
        // give the player some time to recover before taking more damage !
        if (timeBtwDamage > 0) {
            timeBtwDamage -= Time.deltaTime;
        }
        */

        healthBar.value = health;
    }
    

    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        // deal the player damage ! 
        if (other.CompareTag("Player") && isDead == false) {
            if (timeBtwDamage <= 0) {
                other.GetComponent<Player>().health -= damage;
            }
        } 
    }
    */
}