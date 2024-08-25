using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private Animator ani => GetComponent<Animator>();
   private void OnTriggerEnter2D(Collider2D collision)
    {
        Piggy piggy = collision.GetComponent<Piggy>();

        if(piggy != null)
        {

        ani.SetTrigger("activate");
        GameManager.instance.LevelFinished();

        }
           

    }
}
