﻿using UnityEngine;
using System.Collections;
using System;

namespace Completed
{
    //Enemy inherits from MovingObject, our base class for objects that can move, Player also inherits from this.
    public class Enemy : MovingObject
    {
        //public int playerDamage;                            //The amount of food points to subtract from the player when attacking.

        private BoxCollider2D boxColliderEnemy;
        private Animator animator;                          //Variable of type Animator to store a reference to the enemy's Animator component.
        //private bool skipMove;                              //Boolean to determine whether or not enemy should skip a turn or move this turn.

        // Enumeratori
        public enum enemyType
        {
            Horizontal,     // 0 Movimento A => B su Asse X
            Vertical,       // 1 Movimento A => B su Asse Y
            Ranged,         // 2 Movimento Auto Rotativo Nemico Ranged
            /* Mimic, */    // 3 Movimento Doppleganger Comandi riflessi rispetto al player
            CustomPatrol    // 4 Movimento custom definito da Unity
        };

        //Suoni di attacco
        [Header("Sounds")]
        public AudioClip attackSound1;                      //First of two audio clips to play when attacking the player.
        public AudioClip attackSound2;                      //Second of two audio clips to play when attacking the player.

        [Header("Enemy properties")]
        public enemyType enemyTipe;                         // Indica il tipo di nemico
        public int hp = 1;                                  //hit points for the enemy.
        public Vector2 start;
        public Vector2 end;

        [Header("Horizontal/Vertical only")]
        private float step = 1f;
        public float pA;
        public float pB;
        public bool wayOfMovement;

        [Header("Ranged only")]
        public int maxTicks;
        public LineOfSight EnemyAimingWay;
        private int tick;

        [Header("Mimic only (DON'T USE IT!)")]
        public Player PlayerInfo;

        //Patrolling
        [Header("Patrolling only")]
        public Transform[] patrolPoints;
        private int patrolIndex;

        /*
        //Override the AttemptMove function of MovingObject to include functionality needed for Enemy to skip turns.
        //See comments in MovingObject for more on how base AttemptMove function works.
        protected override void AttemptMove<T>(int xDir, int yDir)
        {
            //Check if skipMove is true, if so set it to false and skip this turn.
            if (skipMove)
            {
                skipMove = false;
                return;

            }

            //Call the AttemptMove function from MovingObject.
            base.AttemptMove<T>(xDir, yDir);

            //Now that Enemy has moved, set skipMove to true to skip next move.
            //[Verza] We never skip movements maddaffakka!
            //skipMove = true;
        }
        */


        //Start overrides the virtual Start function of the base class. 
        protected override void Start()
        {
            boxColliderEnemy = GetComponent<BoxCollider2D>();
            //Register this enemy with our instance of GameManager by adding it to a list of Enemy objects. 
            //This allows the GameManager to issue movement commands.
            GameManager.instance.AddEnemyToList(this);

            //Get and store a reference to the attached Animator component.
            animator = GetComponent<Animator>();

            if (enemyTipe == enemyType.Horizontal)
            {

                pA = this.transform.position.x + 3f;
                pB = this.transform.position.x - 3f;
            }
            else if (enemyTipe == enemyType.Vertical)
            {
                pA = this.transform.position.y + 3f;
                pB = this.transform.position.y - 3f;

            }
            //Call the start function of our base class MovingObject.
            base.Start();

            if (enemyTipe == enemyType.Ranged)
            {
                ChangeSightAnimation(EnemyAimingWay);
            }
        }

        public void CheckNextCell(out int xDir, out int yDir)
        {
            xDir = 0;
            yDir = 0;

            Vector2 newPos;


            switch (enemyTipe)
            {
                case enemyType.CustomPatrol: //Patrol defined by level designers.

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
                    break;

                case enemyType.Horizontal://Pattern AB_AsseX

                    if (wayOfMovement == true)
                    {
                        xDir = (int)step;
                        newPos = new Vector2(this.transform.position.x + step, this.transform.position.y);
                    }
                    else
                    {
                        xDir = -(int)step;
                        newPos = new Vector2(this.transform.position.x - step, this.transform.position.y);
                    }
                    if (newPos.x == pA)
                    {
                        wayOfMovement = false;
                    }
                    if (newPos.x == pB)
                    {
                        wayOfMovement = true;
                    }
                    break;


                case enemyType.Vertical://Pattern AB_AsseY 

                    if (wayOfMovement == true)
                    {
                        yDir = (int)step;
                        newPos = new Vector2(this.transform.position.x, this.transform.position.y + step);
                    }
                    else
                    {
                        yDir = -(int)step;
                        newPos = new Vector2(this.transform.position.x, this.transform.position.y - step);
                    }
                    if (newPos.y == pA)
                    {
                        wayOfMovement = false;
                    }
                    if (newPos.y == pB)
                    {
                        wayOfMovement = true;
                    }
                    break;


                /*
                case enemyType.Mimic: //Pattern Mimic

                    if (PlayerInfo.new_Coordinate.x > PlayerInfo.old_Coordinate.x)
                    {
                        //MoveRight
                    }
                    else if (PlayerInfo.new_Coordinate.x < PlayerInfo.old_Coordinate.x)
                    {
                        //MoveLeft
                    }
                    else if (PlayerInfo.new_Coordinate.y > PlayerInfo.old_Coordinate.y)
                    {
                        //MoveDown
                    }
                    else if (PlayerInfo.new_Coordinate.y < PlayerInfo.old_Coordinate.y)
                    {
                        //MoveUp
                    }

                    break;
                */

                case enemyType.Ranged:
                    //Pattern RangedEnemy
                    boxColliderEnemy.enabled = false;

                    end = GetVectorDirection(EnemyAimingWay);

                    tick++;
                    if (tick == maxTicks)
                    {
                        int i = 0;
                        bool isStoneRaycasted;

                        ChangeAimingDirection(ref EnemyAimingWay);
                        end = GetVectorDirection(EnemyAimingWay);

                        RaycastHit2D CheckBlockingLayerObject;
                        do
                        {
                            CheckBlockingLayerObject = Physics2D.Raycast(transform.position, end, 1f, blockingLayer);

                            isStoneRaycasted = CheckBlockingLayerObject && CheckBlockingLayerObject.transform.tag == "Stone";

                            if (isStoneRaycasted)
                            {
                                i++;

                                ChangeAimingDirection(ref EnemyAimingWay);
                                end = GetVectorDirection(EnemyAimingWay);

                                // Ha fatto il giro completo e ha trovato solo muri. Cattivi level designers!
                                if (i == 3)
                                {
                                    break;
                                }
                            }
                        } while (isStoneRaycasted);

                        ChangeSightAnimation(EnemyAimingWay);
                        tick = 0;
                    }


                    RaycastHit2D Bullet = Physics2D.Raycast(transform.position, end, 8f, blockingLayer);
                    if (Bullet.collider == null)
                    {
                        // Check se sto beccando la porta.
                        Bullet = Physics2D.Raycast(transform.position, end, 8f, exitLayer);
                    }

                    if (Bullet.transform != null && Bullet.transform.tag == "Player")
                    {
                        Bullet.transform.GetComponent<Player>().ExecuteGameOver();
                    }
                    boxColliderEnemy.enabled = true;
                    break;
            }
        }

        private Vector2 GetVectorDirection(LineOfSight aimingDirection)
        {
            Vector2 direction = new Vector2();

            switch (aimingDirection)
            {
                case LineOfSight.down:
                    direction = -transform.up;
                    break;
                case LineOfSight.up:
                    direction = transform.up;
                    break;
                case LineOfSight.right:
                    direction = transform.right;
                    break;
                case LineOfSight.left:
                    direction = -transform.right;
                    break;
            }

            return direction;
        }

        //MoveEnemy is called by the GameManger each turn to tell each Enemy to try to move towards the player.
        public void MoveEnemy()
        {

            //Declare variables for X and Y axis move directions, these range from -1 to 1.
            //These values allow us to choose between the cardinal directions: up, down, left and right.
            int xDir = 0;
            int yDir = 0;

            CheckNextCell(out xDir, out yDir);

            AttemptMove<Player>(xDir, yDir);
        }

        //MoveEnemy is called by the GameManger each turn to tell each Enemy to try to move towards the player.
        public void TryToKillPlayer(Player player, out bool isStillAlive)
        {

            //Declare variables for X and Y axis move directions, these range from -1 to 1.
            //These values allow us to choose between the cardinal directions: up, down, left and right.
            int xDir = 0;
            int yDir = 0;

            CheckNextCell(out xDir, out yDir);

            AttemptAttack(xDir, yDir, player, out isStillAlive);
        }

        private void ChangeAimingDirection(ref LineOfSight posizione)
        {
            switch (posizione)
            {
                case LineOfSight.down:
                    posizione = LineOfSight.left;
                    break;
                case LineOfSight.left:
                    posizione = LineOfSight.up;
                    break;
                case LineOfSight.up:
                    posizione = LineOfSight.right;
                    break;
                case LineOfSight.right:
                    posizione = LineOfSight.down;
                    break;
            }
        }

        Player hitPlayer;
        //OnCantMove is called if Enemy attempts to move into a space occupied by a Player, it overrides the OnCantMove function of MovingObject
        //and takes a generic parameter T which we use to pass in the component we expect to encounter, in this case Player
        //Player hitPlayer;
        protected override void OnCantMove<T>(T component)
        {
            //Declare hitPlayer and set it to equal the encountered component.
            Player hitPlayer = component as Player;

            //Call the LoseFood function of hitPlayer passing it playerDamage, the amount of foodpoints to be subtracted.
            //	hitPlayer.LoseFood (playerDamage);

            //Set the attack trigger of animator to trigger Enemy attack animation.
            animator.SetTrigger("enemyAttack");

            //Call the RandomizeSfx function of SoundManager passing in the two audio clips to choose randomly between.
            SoundManager.instance.RandomizeSfx(attackSound1, attackSound2);

            hitPlayer.ExecuteGameOver();

        }






        //DamageWall is called when the player attacks a wall.
        public void DamageEnemy(int loss)
        {
            //Call the RandomizeSfx function of SoundManager to play one of two chop sounds.
            //SoundManager.instance.RandomizeSfx(chopSound1, chopSound2);

            //Set spriteRenderer to the damaged wall sprite.
            //spriteRenderer.sprite = dmgSprite;

            //Subtract loss from hit point total.
            hp -= loss;

            //If hit points are less than or equal to zero:
            if (hp <= 0)
            {
                //Disable the gameObject.
                GameManager.instance.RemoveEnemyFromList(this);
                gameObject.SetActive(false);
                //Destroy(gameObject);

                //[Verza]   Verifico esistenza del componente CameraShake sulla Main Camera.
                //          Se esiste, allora shakero la camera alla morte del player.
                //          Altrimenti il personaggio sta fermo, la camera non shakera ma non si spacca niente.
                CameraShake cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
                if (cameraShake != null)
                {
                    // Call al metodo di Shake.
                    cameraShake.ShakeCamera(0.1f, 0.1f);
                }
            }
        }


        //The virtual keyword means AttemptMove can be overridden by inheriting classes using the override keyword.
        //AttemptMove takes a generic parameter T to specify the type of component we expect our unit to interact with if blocked (Player for Enemies, Wall for Player).
        private void AttemptAttack(int xDir, int yDir, Player player, out bool isStillAlive)
        {
            //Hit will store whatever our linecast hits when Move is called.
            RaycastHit2D hit;
            Vector2 end;
            isStillAlive = true;

            //Set canMove to true if Move was successful, false if failed.
            bool canMove = CanMove(xDir, yDir, out hit, out end);

            //Check if nothing was hit by linecast
            if (hit.transform == null)
                //If nothing was hit, return and don't execute further code.
                return;

            //Get a component reference to the component of type T attached to the object that was hit
            Player hitComponent = hit.transform.GetComponent<Player>();

            //If canMove is false and hitComponent is not equal to null, meaning MovingObject is blocked and has hit something it can interact with.
            if (!canMove && object.Equals(hitComponent, player))
            {
                isStillAlive = false;

                //Call the OnCantMove function and pass it hitComponent as a parameter.
                OnCantMove(player);
            }
        }
    }

}
