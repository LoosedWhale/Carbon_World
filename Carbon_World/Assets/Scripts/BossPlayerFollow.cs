// BossPlayerFollow.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Random = UnityEngine.Random;

public class BossPlayerFollow : MonoBehaviour
{

    public string tagTarget = "Player";
    public List<Collider2D> detectedObjs = new List<Collider2D>();

    public Boss boss;
    private Animator animator;
    public Collider2D col;


    void Start()
    {
        col = GetComponent<Collider2D>();
        animator = boss.GetComponent<Animator>();

        //In case the animator is not found
        if (animator == null)
        {
            Debug.LogError("Animator not found on " + boss.gameObject.name);
        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == tagTarget)
        {
            detectedObjs.Add(collider);
            if (boss.canWalk)
            {
                animator.SetBool("isWalking", true);
                boss.StartAttack();
            }
        }
        
    }


    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == tagTarget)
        {
            detectedObjs.Remove(collider);
            if (boss.canWalk)
            {
                animator.SetBool("isWalking", false);
                boss.StopAttack();  // Add this line
            }
        }
    }
}
