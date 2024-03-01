using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlayerFollow : MonoBehaviour
{
    public GameObject boss;
    private bool isPlayerInRange = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            boss.GetComponent<Boss>().DetectedPlayer(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            boss.GetComponent<Boss>().UnDetectedPlayer(collision);
        }
    }

    private void Update()
    {
        // Check if the player is in range and update the boss's movement accordingly
        if (isPlayerInRange)
        {
            // Enable boss movement towards the player
            boss.GetComponent<Boss>().SetCanMove(true);
        }
        else
        {
            // Disable boss movement if the player is out of range
            boss.GetComponent<Boss>().SetCanMove(false);
        }
    }
}
