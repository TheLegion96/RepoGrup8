using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpecularScript : MonoBehaviour {
    bool Moving = false;
    private int inputPlayer = 0;
    private Vector3 pos;
    public float Step = 0.1f;
    public float speed =5f;
   public  GameObject[] allPrefab;
    public int Counter=0;

    // Use this for initialization
    void Start()
    {
        pos = transform.position;
        allPrefab = Resources.LoadAll<GameObject>("Prefab");
        Debug.Log(allPrefab.Length);
    }
	
	// Update is called once per frame
	void Update () {
   
    }

public void CallSpecularMovement(int _inputPlayer)
    {
        inputPlayer = _inputPlayer;
        pos = transform.position;
        switch (inputPlayer)
        {
            case 1:
                pos += Vector3.left;
         
                break;
            case 2:
                pos += Vector3.right;
      
                break;
            case 3:
                pos += Vector3.up;
            
                break;
            case 4:
                pos += Vector3.down;
          
                break;
            default:
                inputPlayer = 0;
             
                break;
        }
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime*speed);
        inputPlayer = 0;
        Moving = false;
    }
    public int CounterUntilFlip=0;

    public void CallABMovement()
    {
       
        bool direction = true;
        pos = transform.position;
        if (direction)
        {
            pos += Vector3.left;
            CounterUntilFlip++;
        }
        else
        {
            pos += Vector3.right;
            CounterUntilFlip++;
        }
        if(CounterUntilFlip<=5)
        {
            if (direction)
                direction = false;
            else
                direction = true;

        }
    }

}
