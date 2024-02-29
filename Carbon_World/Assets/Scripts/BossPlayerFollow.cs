using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlayerFollow : MonoBehaviour
{
    public GameObject boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        boss.GetComponent<Boss>().DetectedPlayer(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        boss.GetComponent<Boss>().UnDetectedPlayer(collision);
    }
}
