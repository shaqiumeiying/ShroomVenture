using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShroom : MonoBehaviour
{
    [SerializeField] private GameObject collectedVFX;
    private GameManager gameManager;
    private Animator ani;

    private void Awake()
    {
        ani = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        
        gameManager = GameManager.instance;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Piggy piggy = collision.GetComponent<Piggy>();

        if(piggy != null)
            gameManager.AddShroom();
            Destroy(gameObject);

            GameObject newFX = Instantiate(collectedVFX, transform.position, Quaternion.identity);

    }

}
