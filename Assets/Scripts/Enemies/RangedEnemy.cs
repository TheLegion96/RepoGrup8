using Completed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] protected GameObject Deadzone;

    public bool DoThisOnlyWhenAllDeadZoneAreON = false;

    //Start overrides the virtual Start function of the base class. 
    protected override void Start()
    {
        //Force Enemy Type.
        enemyTipe = EnemyType.Ranged;
        //Call the start function of our base class Enemy.
        base.Start();
        //Call custom code for this type.
        ChangeSightAnimation(EnemyAimingWay);
        InstanzaDeadZone();
    }

    public override void CheckNextCell(out int xDir, out int yDir)
    {
        xDir = 0;
        yDir = 0;



        //Pattern RangedEnemy
        boxColliderEnemy.enabled = false;

        end = GetVectorDirection(EnemyAimingWay);

        tick++;

        if (tick == maxTicks)
        {
            bool isStoneRaycasted;

            ChangeAimingDirection(ref EnemyAimingWay);
            end = GetVectorDirection(EnemyAimingWay);

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
        InstanzaDeadZone();
        RaycastHit2D Bullet = Physics2D.Raycast(transform.position, end, 9f, blockingLayer);
        if (Bullet.collider == null)
        {
            // Check se sto beccando la porta.
            Bullet = Physics2D.Raycast(transform.position, end, 9f, exitLayer);
        }

        if (Bullet.transform != null && Bullet.transform.tag == "Player")
        {
            //Set the attack trigger of animator to trigger Enemy attack animation.
            animator.SetTrigger("Attack");

            Bullet.transform.GetComponent<Player>().ExecuteGameOver();
        }
        boxColliderEnemy.enabled = true;
    }


    private void InstanzaDeadZone()
    {
        Vector3 _TempEndPosition = new Vector3();
        for (int i = 1; i < 9; i++)
        {

            Transform _TempDeadZone = Instantiate(Deadzone.transform, this.transform.position, Quaternion.identity);
            _TempEndPosition = new Vector3();
            switch (EnemyAimingWay)
            {
                case LineOfSight.down:
                    _TempEndPosition = _TempDeadZone.position;
                    //_TempEndPosition.y -= 0.35f;

                    _TempEndPosition.y -= i;
                    _TempDeadZone.position = _TempEndPosition;
                    break;
                case LineOfSight.left:

                    _TempEndPosition = _TempDeadZone.position;
                    //_TempEndPosition.y -= 0.35f;
                    _TempEndPosition.x -= i;
                    _TempDeadZone.position = _TempEndPosition;
                    break;
                case LineOfSight.up:
                    _TempEndPosition = _TempDeadZone.position;
                    _TempEndPosition.y += i;
                    //_TempEndPosition.y -= 0.35f;

                    _TempDeadZone.position = _TempEndPosition;
                    break;
                case LineOfSight.right:
                    _TempEndPosition = _TempDeadZone.position;
                    //_TempEndPosition.y -= 0.35f;
                    _TempEndPosition.x += i;
                    _TempDeadZone.position = _TempEndPosition;
                    break;
            }
            RaycastHit2D checkCollision;
            checkCollision = Physics2D.Linecast(_TempDeadZone.position, _TempDeadZone.position);
            if (checkCollision.transform != null)
            {
                if (checkCollision.transform.tag == "Stone" || checkCollision.transform.tag == "Enemy")
                {
                    Destroy(_TempDeadZone.gameObject);
                    break;
                }
                else if (checkCollision.transform.tag == "DeadZone")
                {
                    Destroy(_TempDeadZone.gameObject);
                }
                else
                {
                    _TempDeadZone.GetComponent<BoxCollider2D>().enabled = true;
                }
            }

            if (_TempDeadZone != null)
            {
                _TempDeadZone.position = _TempEndPosition;
                _DeadZone.Add(_TempDeadZone);
            }

        }


    }
    //void CheckIfStone()
    //{

    //  

    //}



}
