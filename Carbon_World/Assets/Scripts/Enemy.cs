using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private float health = 4f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public float Health
    {
        set
        {
            health = value;

            if (health <= 0)
            {
                Defeated();
                StartCoroutine(RemoveEnemyWithDelay());
            }
        }
        get { return health; }
    }

    private IEnumerator RemoveEnemyWithDelay()
    {
        yield return new WaitForSeconds(1.5f);
        RemoveEnemy();
    }

    public void Defeated()
    {
        animator.SetTrigger("Defeated");
    }

    public void RemoveEnemy()
    {
        Stats.fireballKills++;
        Destroy(gameObject);
    }

}
