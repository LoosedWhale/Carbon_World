using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour {

    public int health;
   
    public BossAttack bossAttack;
    public float moveSpeed;   
    public BossPlayerFollow bossPlayerFollow;

    public Slider healthBar;
    private Animator anim;
    private Rigidbody2D bossRigidbody;

    public bool isDead = false;


    public float hitStunDuration = 1.0f;
    public float attackDuration = 3.0f;


    public string playerTag = "Player";
    public bool canWalk = true;

    
    private void Start()
    {
        anim = GetComponent<Animator>();
        bossRigidbody = GetComponent<Rigidbody2D>();
        moveSpeed = 150f;
        StartCoroutine(AttackLoop());
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
        anim.SetBool("isWalking", false);
        
        anim.SetTrigger("Defeated");
    }

    private IEnumerator RemoveEnemyWithDelay()
    {
        healthBar.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
        SceneManager.LoadScene(7, LoadSceneMode.Single);
    }


    public void TakeDamage(int damage)
    {
        health -= damage;   
        if (health <= 0)
            {
                Defeated();
                StartCoroutine(RemoveEnemyWithDelay());
            }

        anim.SetTrigger("TakeHit");

        // Start hit stun coroutine
        StartCoroutine(HitStun());
    }

    private IEnumerator HitStun()
    {
        canWalk = false;
        yield return new WaitForSeconds(hitStunDuration);
        canWalk = true;
    }

    void FixedUpdate()
    {
        
        if (bossPlayerFollow.detectedObjs.Count > 0)
        {   
            // Calculate direction
            Vector2 direction = (bossPlayerFollow.detectedObjs[0].transform.position - transform.position).normalized;

            // Move towards detected object
            bossRigidbody.AddForce(direction * moveSpeed * Time.deltaTime);

            // Flip boss to face the player
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 0);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 0);
            }
        }
    }

    void Update()
    {
        healthBar.value = health;
    }

    public bool CanWalk()
    {
        return canWalk;
    }
    
    public void StartAttack()
    {
        bossAttack.enabled = true;
        // Select a random attack animation
        int attackNumber = Random.Range(1, 3); 
        if (attackNumber == 1) 
        {
            anim.SetTrigger("atk" + 4);
            StartCoroutine(Attack());
        }
        else if (attackNumber == 2)
        {
            anim.SetTrigger("atk_air");
            StartCoroutine(Attack());
        }

        // Trigger the attack animation
        //anim.SetTrigger("atk" + 4);

        // Start the attack coroutine
        //StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        // Enable the boss attack collider
        bossAttack.Attack.enabled = true;

        // Wait for the attack duration
        yield return new WaitForSeconds(attackDuration);

        // Disable the boss attack collider
        bossAttack.Attack.enabled = false;
    }
    
    public void StopAttack()
    {
        StopCoroutine(Attack());
        bossAttack.enabled = false;
    }

    private IEnumerator AttackLoop()
    {
        while (true)
        {
            // Wait for a random interval between 2 and 5 seconds
            yield return new WaitForSeconds(Random.Range(3, 6));

            // Start an attack if the boss can walk
            if (CanWalk())
            {
                StartAttack();
            }
        }
    }
    

}