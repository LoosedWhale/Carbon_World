using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

    public int health;
    public int damage;

    public float acceleration = 5f;
    public float deceleration = 5f;
    public float maxSpeed = 5f;
    public float stoppingDistance = 1f;


    public Transform target;
    public bool isMoving = false;

    //private float timeBtwDamage = 1.5f;

    public GameObject playerFollowArea;

    private GameObject player;

    public Slider healthBar;
    private Animator anim;
    public bool isDead;
   

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
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
        healthBar.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        RemoveEnemy();
        
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }

    private void Update()
    {

        if (health <= 25)
        {
            print("You can't hurt me [dialog here for epic]");
            //anim.SetTrigger("stageTwo");
        }


        /*
        // give the player some time to recover before taking more damage !
        if (timeBtwDamage > 0) {
            timeBtwDamage -= Time.deltaTime;
        }
        */

        healthBar.value = health;


        if (isMoving && target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;

            // Calculate the current speed based on acceleration and deceleration
            float currentSpeed = Mathf.MoveTowards(GetComponent<Rigidbody>().velocity.magnitude, maxSpeed, Time.deltaTime * acceleration);

            // Apply the velocity
            GetComponent<Rigidbody>().velocity = direction * currentSpeed;

            // Rotate towards the player
            transform.rotation = Quaternion.LookRotation(direction);

            // Check if close enough to stop
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget < stoppingDistance)
            {
                isMoving = false;
            }
        }

    }
    public void MoveToPlayer(Transform playerTransform)
    {
        player = playerTransform.gameObject;
        isMoving = true;

    }


    public void DetectedPlayer(Collider2D other)
    {
        print("This shit worked lmao");
        if (other.CompareTag("Player") && isDead == false)
        {
            MoveToPlayer(other.transform);
        }
    }

    public void UnDetectedPlayer(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isMoving = false;
            player = null;
        }
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