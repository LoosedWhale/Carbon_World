using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

    public int health;
    public int damage;

    public float acceleration = 5f;
    public float maxSpeed = 5f;
    public float stoppingDistance = 1f;
    private bool canMove = true;

    public Transform target;
    private bool isMoving = false;

    public Slider healthBar;
    private Animator anim;
    private Rigidbody2D bossRigidbody;

    private void Start()
    {
        anim = GetComponent<Animator>();
        bossRigidbody = GetComponent<Rigidbody2D>();
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

    private void Update()
    {
        healthBar.value = health;

        if (isMoving && target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;

            float distanceToTarget = Vector2.Distance(transform.position, target.position);
            if (distanceToTarget > stoppingDistance)
            {
                // Calculate the current speed based on acceleration and max speed
                float currentSpeed = Mathf.MoveTowards(bossRigidbody.velocity.magnitude, maxSpeed, Time.deltaTime * acceleration);

                // Apply the velocity
                bossRigidbody.velocity = direction * currentSpeed;
            }
            else
            {
                // Stop moving if close enough to the player
                isMoving = false;
                bossRigidbody.velocity = Vector2.zero;
            }
        }
    }

    public void MoveToPlayer(Transform playerTransform)
    {
        if (playerTransform != null)
        {
            target = playerTransform;
            isMoving = true;
        }
    }

    public void DetectedPlayer(Collider2D other)
    {
        if (canMove && other.CompareTag("Player"))
        {
            MoveToPlayer(other.transform);
        }
    }

    public void UnDetectedPlayer(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isMoving = false;
            target = null;
        }
    }

    public void SetCanMove(bool move)
    {
        canMove = move;
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