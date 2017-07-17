﻿using UnityEngine;
using System.Collections;

namespace Completed
{
    //Enemy inherits from MovingObject, our base class for objects that can move, Player also inherits from this.
    public class Enemy : MovingObject
    {
        public int playerDamage;                            //The amount of food points to subtract from the player when attacking.
        public AudioClip attackSound1;                      //First of two audio clips to play when attacking the player.
        public AudioClip attackSound2;                      //Second of two audio clips to play when attacking the player.

        private BoxCollider2D boxColliderEnemy;
        private Animator animator;                          //Variable of type Animator to store a reference to the enemy's Animator component.
        private Transform target;                           //Transform to attempt to move toward each turn.
        private bool skipMove;                              //Boolean to determine whether or not enemy should skip a turn or move this turn.
        public GameObject Aim;
        public enum enemyType { Horizzontal, Vertical, Ranged, Mimic };

        public enemyType enemyTipe;                               // Indica il tipo di nemico
        /*
         0 Movimento A => B su Asse X
         1 Movimento A => B su Asse Y
         2 Movimento Doppleganger Comandi riflessi rispetto al player
         3 Movimento Auto Rotativo Nemico Ranged
         ...
             */

        public float pA, pB;
        private float step = 1f;
        public bool wayOfMovement;
        public int tick;
        public int hp = 1;                          //hit points for the enemy.
        public enum AIMING { up, down, left, right };
        public AIMING EnemyAimingWay;
        public Player PlayerInfo;
        public Vector2 start;
        public Vector2 end;


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

        public void CheckNextCell(out int xDir, out int yDir)
        {
            xDir = 0;
            yDir = 0;

            Vector2 newPos;


            switch (enemyTipe)
            {
                case enemyType.Horizzontal://Pattern AB_AsseX

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


                case enemyType.Mimic: //Pattern Mimic

                   if(PlayerInfo.new_Coordinate.x>PlayerInfo.old_Coordinate.x)
                      {
                      //MoveRight
                      }
                      else if(PlayerInfo.new_Coordinate.x<PlayerInfo.old_Coordinate.x)
                      {
                      //MoveLeft
                      }
                      else if(PlayerInfo.new_Coordinate.y>PlayerInfo.old_Coordinate.y)
                      {
                      //MoveDown
                      }
                      else if(PlayerInfo.new_Coordinate.y<PlayerInfo.old_Coordinate.y)
                      {
                      //MoveUp
                      }
                  
                    break;
               case enemyType.Ranged:
                    //Pattern RangedEnemy
                //    if (tick == 0)
                //    {
                //        tick++;
                //    }
                //    else if (tick == 1)
                //    {
                //        ChangeAwayAiming(ref EnemyAimingWay);
                //        tick = 0;
                //    }
                //    ////DA CONTROLLARE 
                //    RaycastHit2D Bullet;
                //    Bullet = new RaycastHit2D();
                //    boxColliderEnemy.enabled = false;
                //    end = new Vector2(0, 0);
                //    switch (EnemyAimingWay)
                //    {

                //        case AIMING.down:
                //            end = -transform.up;
                //            break;
                //        case AIMING.up:
                //            end = transform.up;
                //            break;
                //        case AIMING.right:
                //            end = transform.right;

                //            break;
                //        case AIMING.left:
                //            end = -transform.right;
                //            break;
                //    }
                //    Bullet = Physics2D.Raycast(transform.position, end, 8f, blockingLayer);
                //    /*  string name = Bullet.transform.tag;
                //      if(name=="Player")
                //      { hitPlayer.ExecuteGameOver(); }*/
                //    // Bullet = Physics2D.Linecast(start, end,blockingLayer);
                //   if (Bullet.transform == null)
                //    {
                //       //   hitPlayer.ExecuteGameOver();
                //    }
                //    else
                //        Debug.Log(Bullet.transform.name);
           
                //    boxColliderEnemy.enabled = true;
                    break;
            }
        }
        public void getPlayerInfo(Player informazioni)
        {


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

        private void ChangeAwayAiming(ref AIMING posizione)
        {
            switch (posizione)
            {
                case AIMING.down:
                    posizione = AIMING.left;
                    break;
                case AIMING.left:
                    posizione = AIMING.up;
                    break;
                case AIMING.up:
                    posizione = AIMING.right;
                    break;
                case AIMING.right:
                    posizione = AIMING.down;
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




        //Start overrides the virtual Start function of the base class. 
        protected override void Start()
        {
            boxColliderEnemy = GetComponent<BoxCollider2D>();
            //Register this enemy with our instance of GameManager by adding it to a list of Enemy objects. 
            //This allows the GameManager to issue movement commands.
            GameManager.instance.AddEnemyToList(this);

            //Get and store a reference to the attached Animator component.
            animator = GetComponent<Animator>();

            //Find the Player GameObject using it's tag and store a reference to its transform component.
            target = GameObject.FindGameObjectWithTag("Player").transform;

            if (enemyTipe == enemyType.Horizzontal)
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
                isStillAlive = false;

            //Call the OnCantMove function and pass it hitComponent as a parameter.
            OnCantMove(player);
        }
    }

}
