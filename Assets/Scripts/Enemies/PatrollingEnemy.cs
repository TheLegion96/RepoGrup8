using Completed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : Enemy
{
    //Start overrides the virtual Start function of the base class. 
    protected override void Start()
    {
        //Force Enemy Type.
        enemyTipe = EnemyType.CustomPatrol;
        //Call the start function of our base class Enemy.
        base.Start();
        //Call custom code for this type.
        //(...)
    }

    public override void CheckNextCell(out int xDir, out int yDir)
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
