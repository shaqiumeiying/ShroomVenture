using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Animator ani => GetComponent<Animator>();
    private bool active;
    [SerializeField] private bool canBeReactivated;

    private void Start()
    {
        canBeReactivated = GameManager.instance.canReactivate;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(active)
            return;

        Piggy piggy = collision.GetComponent<Piggy>();

        if (piggy != null)
            ActivateCheckPoint();
    }

    private void ActivateCheckPoint()
    {
        active = true;
        ani.SetTrigger("active");
        GameManager.instance.UpdateRespawnPosition(transform);
    }

}
