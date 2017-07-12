using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DIRECTION { UP, DOWN, LEFT, RIGHT}

public class GridMovement : MonoBehaviour {

    private bool canMove = true;
    private bool moving = false;
    private float speed = 5f;
    private float buttonCooldown = 0f;
    private DIRECTION dir = DIRECTION.DOWN;
    private Vector3 pos;
    private int inputPlayer = 0;
    public EnemySpecularScript[] en;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        buttonCooldown--;

        if (canMove)
        {
            pos = transform.position;
            move();
        }

        if (moving)
        {
            if(transform.position == pos)
            {
                moving = false;
                canMove = true;

                move();
             
            }

            transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
            foreach (EnemySpecularScript em in en)
            {
                em.CallSpecularMovement(inputPlayer);
                em.CallABMovement();
            }
        }
		
	}

    private void move()
    {
        if(buttonCooldown <= 0)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (dir != DIRECTION.UP)
                {
                    //buttonCooldown = 0.3f;
                    inputPlayer = 4;
                    dir = DIRECTION.UP;
                }
                else
                {
                    canMove = false;
                    moving = true;
                    pos += Vector3.up;
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (dir != DIRECTION.DOWN)
                {
                    inputPlayer = 3;
                    //buttonCooldown = 0.3f;
                    dir = DIRECTION.DOWN;
                }
                else
                {
                    canMove = false;
                    moving = true;
                    pos += Vector3.down;
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if(dir != DIRECTION.LEFT)
                {
                    inputPlayer = 2;
                    //buttonCooldown = 0.3f;
                    dir = DIRECTION.LEFT;
                }
                else
                {
                    canMove = false;
                    moving = true;
                    pos += Vector3.left;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (dir != DIRECTION.RIGHT)
                {
                    inputPlayer = 1;
                    //buttonCooldown = 0.3f;
                    dir = DIRECTION.RIGHT;
                }
                else
                {
                    canMove = false;
                    moving = true;
                    pos += Vector3.right;
                }

            }

        }
    }

}
