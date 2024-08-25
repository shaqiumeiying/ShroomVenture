using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private Animator ani => GetComponent<Animator>();
    private void OnTriggerExit2D(Collider2D collision) {
        Piggy piggy = collision.GetComponent<Piggy>();

        if(piggy != null)
        {
            ani.SetTrigger("active");
        }
    
   
    

   }
}
