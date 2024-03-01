// BossPlayerFollow.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlayerFollow : MonoBehaviour
{

    public string tagTarget = "Player";
    public List<Collider2D> detectedObjs = new List<Collider2D>();

    private Animator animator;
    public Collider2D col;

    void Start()
    {
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == tagTarget)
        {
            detectedObjs.Add(collider);
            
        }
        if (animator != null)
        {
            animator.SetBool("isWalking", true);
        }
    }


    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == tagTarget)
        {
            detectedObjs.Remove(collider);
            
        }
        if (animator != null)
        {
            animator.SetBool("isWalking", false);
        }
    }
}
