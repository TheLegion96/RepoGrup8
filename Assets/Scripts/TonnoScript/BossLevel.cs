using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevel : MonoBehaviour {
    [SerializeField]
    public  GameObject[] TNT;
     int  MaxCounter, Counter;
	// Use this for initialization
	void Start () {
        TNT =GameObject.FindGameObjectsWithTag("Soda");
        MaxCounter = TNT.Length;
    }
	
	// Update is called once per frame
	void Update ()
    {
        CannonTriggered();
    }
    public void CannonTriggered()
    {
        for (int i = 0; i < TNT.Length; i++)
        {
            if(TNT[i]==null)
            {
                Counter++;
            }
        }

    }
}
