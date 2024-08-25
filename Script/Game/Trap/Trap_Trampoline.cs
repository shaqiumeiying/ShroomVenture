using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Trampoline : MonoBehaviour
{
    protected Animator ani;
    [SerializeField] private float pushPower;
    [SerializeField] private float duration = .5f;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Piggy piggy= collision.gameObject.GetComponent<Piggy>();

        if (piggy != null)
        {
            piggy.Push(transform.up * pushPower,duration);
            ani.SetTrigger("active");
        }
    }
}
