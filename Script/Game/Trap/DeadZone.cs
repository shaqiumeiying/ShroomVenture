using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Piggy piggy = collision.gameObject.GetComponent<Piggy>();

        if(piggy != null)
        {
            piggy.Die();
            GameManager.instance.RespawnPiggy();
        }
    }
}
