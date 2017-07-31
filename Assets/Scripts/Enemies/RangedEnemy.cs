using Completed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] protected GameObject Deadzone;
    [SerializeField] protected GameObject LaserDeadzone;
    [SerializeField] private TextMesh CountDownMesh;
    public bool DoThisOnlyWhenAllDeadZoneAreON = false;
    private int CDTick;
  

    //Start overrides the virtual Start function of the base class. 
    protected override void Start()
    {
        //Force Enemy Type.
        enemyTipe = EnemyType.Ranged;
        //Call the start function of our base class Enemy.
        base.Start();
        //Call custom code for this type.
        ChangeSightAnimation(EnemyAimingWay);
        InstanceLaserDeadZone(EnemyAimingWay);
        InstanceDeadZone(EnemyAimingWay);
    }

    public override void CheckNextCell(out int xDir, out int yDir)
    {
        xDir = 0;
        yDir = 0;

        //Pattern RangedEnemy
        boxColliderEnemy.enabled = false;

        end = GetVectorDirection(EnemyAimingWay);
       
        tick++;            
        CDTick = maxTicks - tick;
      
        if(CDTick==0)
        {
            CDTick = maxTicks;
        }
        CountDownMesh.text = CDTick.ToString();
        if (tick == maxTicks)
        {
            ChangeAimingDirection(ref EnemyAimingWay);
            end = GetVectorDirection(EnemyAimingWay);

            CheckStoneRaycast(ref end, ref EnemyAimingWay);

            ChangeSightAnimation(EnemyAimingWay);
            tick = 0;
        }


        //[Verza] Spostato nel Game Manager.
        //InstanceDeadZone();

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
        InstanceLaserDeadZone(EnemyAimingWay);
    }

    public void CheckStoneRaycast(ref Vector2 parEnd, ref LineOfSight parEnemyAimingWay)
    {
        //Check se devo disabilitare e riattivare il box collider o, se è già spento, lasciarlo così perché se ne occupa qualcun altro.
        bool isBoxColliderToManageHere = boxColliderEnemy.enabled;

        bool isStoneRaycasted;
        RaycastHit2D CheckBlockingLayerObject;
        int aimingDirectionCheck = 0;

        if (isBoxColliderToManageHere)
        {
            boxColliderEnemy.enabled = false;
        }

        do
        {
            CheckBlockingLayerObject = Physics2D.Raycast(transform.position, parEnd, 1f, blockingLayer);

            isStoneRaycasted = CheckBlockingLayerObject && CheckBlockingLayerObject.transform.tag == "Stone";
            if (isStoneRaycasted)
            {
                aimingDirectionCheck++;

                ChangeAimingDirection(ref parEnemyAimingWay);
                parEnd = GetVectorDirection(parEnemyAimingWay);

                // Ha fatto il giro completo e ha trovato solo muri. Cattivi level designers!
                if (aimingDirectionCheck == 3)
                {
                    break;
                }
            }

        } while (isStoneRaycasted);

        if (isBoxColliderToManageHere)
        {
            boxColliderEnemy.enabled = true;
        }
    }

    public void InstanceLaserDeadZone(LineOfSight parEnemyAimingWay)
    {

        Transform _TempLaserDeadZone;
        float newY;
        switch (parEnemyAimingWay)
        {
                case LineOfSight.down:
              _TempLaserDeadZone  = Instantiate(LaserDeadzone.transform, new Vector3(this.transform.position.x , this.transform.position.y - 4f), Quaternion.identity);

              newY= _TempLaserDeadZone.position.y + 0.35f;
                _TempLaserDeadZone.position = new Vector3(_TempLaserDeadZone.position.x, newY, _TempLaserDeadZone.position.z);
                _TempLaserDeadZone.Rotate(0, 0, 90);
                break;
                case LineOfSight.left:
                _TempLaserDeadZone = Instantiate(LaserDeadzone.transform, new Vector3(this.transform.position.x -4, this.transform.position.y), Quaternion.identity);

                break;
                case LineOfSight.up:
                 _TempLaserDeadZone = Instantiate(LaserDeadzone.transform, new Vector3(this.transform.position.x , this.transform.position.y + 4f), Quaternion.identity);      
                newY = _TempLaserDeadZone.position.y + 0.35f;
                _TempLaserDeadZone.position = new Vector3(_TempLaserDeadZone.position.x, newY, _TempLaserDeadZone.position.z);
                _TempLaserDeadZone.Rotate(0, 0, 90);
                break;
                case LineOfSight.right:
                 _TempLaserDeadZone = Instantiate(LaserDeadzone.transform, new Vector3(this.transform.position.x + 4, this.transform.position.y), Quaternion.identity);

                break;
        }
    }




    public void InstanceDeadZone(LineOfSight parEnemyAimingWay)
    {
       // Transform _TempLaserDeadZone = Instantiate(LaserDeadzone.transform, new Vector3(this.transform.position.x + 4, this.transform.position.y), Quaternion.identity);
  
        Vector3 _TempEndPosition = new Vector3();
        for (int i = 1; i < 9; i++)
        {
            Transform _TempDeadZone = Instantiate(Deadzone.transform, this.transform.position, Quaternion.identity);
            _TempEndPosition = new Vector3();
            switch (parEnemyAimingWay)
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
  


}
