using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Trap_Saw : MonoBehaviour
{
    private Animator ani;
    private SpriteRenderer sr;
   [SerializeField] private float moveSpeed = 3;
   [SerializeField] private float cooldown = 1;
   [SerializeField] private Transform[] wayPoint;
   private Vector3[] wayPointPosition;

    private int wayPointIndex = 1;
    private int moveDirection = 1;
    private bool canMove = true;

    private void Awake()
    {
        ani =  GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        
    }

    private void Start()
    {
        UpdateWayPointInfo();
        transform.position = wayPointPosition[0];

       
    }

    private void UpdateWayPointInfo()
    {
        List<Trap_SawWayPoints> wayPointList = new List<Trap_SawWayPoints>(GetComponentsInChildren<Trap_SawWayPoints>());
        
        if (wayPointList.Count != wayPoint.Length)
        {
            wayPoint = new Transform[wayPointList.Count];

            for(int i = 0; i < wayPointList.Count; i++)
            {
                wayPoint[i] = wayPointList[i].transform;
            }
        }
        
        wayPointPosition = new Vector3[wayPoint.Length];

        for (int i = 0; i < wayPoint.Length; i++)
        {
            wayPointPosition[i] = wayPoint[i].position;
        }
    }

    void Update()
    {
        ani.SetBool("active", canMove);

        if(canMove == false)
            return;

        transform.position = Vector2.MoveTowards(transform.position, wayPointPosition[wayPointIndex], moveSpeed * Time.deltaTime);
        
        if(Vector2.Distance(transform.position, wayPointPosition[wayPointIndex]) < 0.1f)
        {
            if(wayPointIndex == wayPointPosition.Length - 1 || wayPointIndex == 0)
                {moveDirection = moveDirection * (-1);
                StartCoroutine(StopMovement(cooldown));
                }
            
            wayPointIndex = wayPointIndex + moveDirection;
        }
    }

    private IEnumerator StopMovement(float delay)
    {
        canMove = false;
        yield return new WaitForSeconds(delay);

        canMove = true;

        sr.flipX = !sr.flipX;

    }

}
