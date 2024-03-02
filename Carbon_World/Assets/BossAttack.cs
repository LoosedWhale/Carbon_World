using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public Collider2D Attack;
    public string playerTag = "Player";

    public int damage;


    // Update is called once per frame
    void Start()
    {
        Attack = GetComponent<Collider2D>();
    }

       private void OnTriggerEnter2D(Collider2D Attack)
    {
        if (Attack.CompareTag("Player"))
        {
            PlayerHealth player = Attack.GetComponent<PlayerHealth>();

            if (player != null && player.Health > 0)
            {
                player.Health -= damage;
                print("player health: " + player.Health);
            }
        }
    }

 

}
