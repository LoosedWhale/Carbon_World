using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

    public int health;
   

    public float moveSpeed;   
    public BossPlayerFollow bossPlayerFollow;

    public Slider healthBar;
    private Animator anim;
    private Rigidbody2D bossRigidbody;

    public bool isDead = false;

    public float timeBtwDamage = 1.5f;
    

    public string playerTag = "Player";

    
    private void Start()
    {
        anim = GetComponent<Animator>();
        bossRigidbody = GetComponent<Rigidbody2D>();
        moveSpeed = 50f;
    }

    public float Health
    {
        set
        {
            health = Mathf.FloorToInt(value);

            if (health <= 0)
            {
                Defeated();
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
        healthBar.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    void FixedUpdate()
    {

        if (bossPlayerFollow.detectedObjs.Count > 0)
        //move towards detected object
        {
            //calculate direction
            Vector2 direction = (bossPlayerFollow.detectedObjs[0].transform.position - transform.position).normalized;

            //move towards detected object
            bossRigidbody.AddForce(direction * moveSpeed * Time.deltaTime);
        }
    }


 
    

}