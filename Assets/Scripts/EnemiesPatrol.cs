using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPatrol : MonoBehaviour
{

    public Transform[] patrolPoints;
    public float moveSpeed;
    private int patrolIndex;

    // Use this for initialization
    void Start()
    {

        transform.position = patrolPoints[0].position;
        patrolIndex = 0;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (transform.position == patrolPoints[patrolIndex].position)
            {
                patrolIndex++;
            }
        }

        if (patrolIndex >= patrolPoints.Length)
        {
            patrolIndex = 0;
        }

        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[patrolIndex].position, moveSpeed * Time.deltaTime);
    }
}
