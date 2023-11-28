using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    public float health = 1;

    public float Health {
        set {
            health = value;

            if (health <= 0)
            {
                //When the enemy's health is 0 or less, call the Defeated() method and start the coroutine [https://docs.unity3d.com/ScriptReference/Coroutine.html] to remove the enemy
                Defeated();
                StartCoroutine(RemoveEnemyWithDelay());
            }
        }
        get {
            return health;
        }
    }

    

    private IEnumerator RemoveEnemyWithDelay() // This is a coroutine method that will wait for a certain amount of time before removing the enemy
    {
        yield return new WaitForSeconds(1.5f); // Adjust the delay time as needed 
        RemoveEnemy(); 
    }
    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void Defeated(){
        animator.SetTrigger("Defeated");
    }

        public void RemoveEnemy() {
            Destroy(gameObject);
            //Remove the enemy from the scene
        }
    }
