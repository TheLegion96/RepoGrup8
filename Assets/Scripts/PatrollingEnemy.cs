using Completed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : Enemy
{
    [Header("Patrolling only")]
    public Transform[] patrolPoints;
    private int patrolIndex;

    public void CheckNextCell(out int xDir, out int yDir)
    {
        if (transform.position == patrolPoints[patrolIndex].position)
        {
            patrolIndex++;
        }
        if (patrolIndex >= patrolPoints.Length)
        {
            patrolIndex = 0;
        }

        xDir = (int)(patrolPoints[patrolIndex].position.x - transform.position.x);
        yDir = (int)(patrolPoints[patrolIndex].position.y - transform.position.y);

    }
}
