﻿using Completed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    //Start overrides the virtual Start function of the base class. 
    protected override void Start()
    {
        //Force Enemy Type.
        enemyTipe = EnemyType.Ranged;
        //Call the start function of our base class Enemy.
        base.Start();
        //Call custom code for this type.
        ChangeSightAnimation(EnemyAimingWay);
    }

    public override void CheckNextCell(out int xDir, out int yDir)
    {
        xDir = 0;
        yDir = 0;

        Vector3 _tempEnd = new Vector3();
        
        //Pattern RangedEnemy
        boxColliderEnemy.enabled = false;

        end = GetVectorDirection(EnemyAimingWay);

        tick++;
        if (tick == maxTicks)
        {
            bool isStoneRaycasted;

            ChangeAimingDirection(ref EnemyAimingWay);
            end = GetVectorDirection(EnemyAimingWay);

            /*
            GameObject[] DestroyDeadZone;
            DestroyDeadZone = GameObject.FindGameObjectsWithTag("DeadZone");
            if (DestroyDeadZone.Length > 0)
            {
                for (int i1 = 0; i1 < DestroyDeadZone.Length - 1; i1++)
                {
                    Destroy(DestroyDeadZone[i].gameObject);
                }
            }

            //TriggerOnce
            for (int i = 0; i < 9; i++)
            {
                Transform _temp = Instantiate<Transform>(Deadzone, this.transform.position, Quaternion.identity);
                _tempEnd = new Vector3();
                switch (EnemyAimingWay)
                {
                    case LineOfSight.down:
                        _tempEnd.y = _temp.position.y;
                        _tempEnd.x = _temp.position.x;
                        _tempEnd.y -= i;

                        _temp.position = _tempEnd;
                        break;
                    case LineOfSight.left:
                        _tempEnd.x = _temp.position.x;
                        _tempEnd.y = _temp.position.y;
                        _tempEnd.x -= i;
                        _temp.position = _tempEnd;
                        break;
                    case LineOfSight.up:
                        _tempEnd.y = _temp.position.y;
                        _tempEnd.x = _temp.position.x;
                        _tempEnd.y += i;
                        _temp.position = _tempEnd;
                        break;
                    case LineOfSight.right:
                        _tempEnd.x = _temp.position.x;
                        _tempEnd.y = _temp.position.y;
                        _tempEnd.x += i;
                        _temp.position = _tempEnd;
                        break;
                }
                _DeadZone.Add(_temp);
            }
            */

            RaycastHit2D CheckBlockingLayerObject;
            int aimingDirectionCheck = 0;
            do
            {
                CheckBlockingLayerObject = Physics2D.Raycast(transform.position, end, 1f, blockingLayer);

                isStoneRaycasted = CheckBlockingLayerObject && CheckBlockingLayerObject.transform.tag == "Stone";

                if (isStoneRaycasted)
                {
                    aimingDirectionCheck++;

                    ChangeAimingDirection(ref EnemyAimingWay);
                    end = GetVectorDirection(EnemyAimingWay);

                    // Ha fatto il giro completo e ha trovato solo muri. Cattivi level designers!
                    if (aimingDirectionCheck == 3)
                    {
                        break;
                    }
                }
            } while (isStoneRaycasted);

            ChangeSightAnimation(EnemyAimingWay);
            tick = 0;
        }

        RaycastHit2D Bullet = Physics2D.Raycast(transform.position, end, 9f, blockingLayer);
        if (Bullet.collider == null)
        {
            // Check se sto beccando la porta.
            Bullet = Physics2D.Raycast(transform.position, end, 9f, exitLayer);
        }

        if (Bullet.transform != null && Bullet.transform.tag == "Player")
        {
            Bullet.transform.GetComponent<Player>().ExecuteGameOver();
        }
        boxColliderEnemy.enabled = true;
    }
}
