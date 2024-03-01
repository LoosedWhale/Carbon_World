// BossPlayerFollow.cs
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
        if (isPlayerInRange)
        {
            boss.GetComponent<Boss>().SetCanMove(true);
        }
        else
        {
            boss.GetComponent<Boss>().SetCanMove(false);
        }
    }
}
