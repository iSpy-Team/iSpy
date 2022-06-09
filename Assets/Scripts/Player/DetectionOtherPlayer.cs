using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class DetectionOtherPlayer : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponentInParent<PlayerShoot>().TargetPlayer(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponentInParent<PlayerShoot>().TargetPlayer(null);
        }
    }
}
