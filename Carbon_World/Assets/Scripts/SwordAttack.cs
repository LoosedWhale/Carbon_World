using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Collider2D swordCollider;
    [SerializeField] private float damage = 5;
    public float attackOffset;

    public void AttackRight()
    {
        print("Attack right");
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(attackOffset, 0);
    }

    public void AttackLeft()
    {
        print("Attack left");
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(-attackOffset, 0);
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("TRIGGER ENTER!!!!!!!!!!!!!");
        if (other.CompareTag("Enemy"))
        {
            // Access the health property directly
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                // Subtract damage from enemy's health
                enemy.Health -= damage;
                print("enemy health: " + enemy.Health);
       
            }
           
        

        }else if (other.CompareTag("Boss"))
        {
            Boss boss = other.GetComponent<Boss>();
            
            if (boss != null)
            {
                boss.Health -= damage;
                print("boss health: " + boss.Health);
            }

            //Destroy(other.gameObject);
        }
    }


}
