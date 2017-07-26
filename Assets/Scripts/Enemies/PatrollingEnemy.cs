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
        RotateTowardsNextCell();
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

    ////Overridare attempt move, quindi lui osserva se la prossima cella si deve girare, e allora cambia la direzione
    //protected override void AttemptMove<T>(int xDir, int yDir)
    //{
    //    //Executes the default function.
    //    base.AttemptMove<T>(xDir, yDir);
    //
    //    //Finished the default function, rotate the enemy towards the next position.
    //    RotateTowardsNextCell();
    //}
  
    protected override IEnumerator SmoothMovement(Vector3 end) {
        // Copy of default code.

        //Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
        //Square magnitude is used instead of magnitude because it's computationally cheaper.
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        //While that distance is greater than a very small amount (Epsilon, almost zero):
        while (sqrRemainingDistance > float.Epsilon)
        {
            //Find a new position proportionally closer to the end, based on the moveTime
            Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);

            //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
            rb2D.MovePosition(newPostion);

            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }

        // Custom Code for this mob.
        RotateTowardsNextCell();
        yield return null;
    }

    private void RotateTowardsNextCell() {
        int nextX, nextY;
        //Checks the position in which the mob ends in this turn.
        CheckNextCell(out nextX, out nextY);
        ChangeSightAnimation(nextX, nextY);
    }
         
}
