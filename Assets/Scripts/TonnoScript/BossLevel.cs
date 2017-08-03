using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevel : MonoBehaviour {
    [SerializeField]
   static public  GameObject[] TNT;
    [SerializeField]
    private GameObject BossNotStatic;

	// Use this for initialization
	void Start () {
        TNT =GameObject.FindGameObjectsWithTag("Soda");
        MaxCounter = TNT.Length;
    
    }

  //  private float nextActionTime = 0.0f;
   // public float period = 0.1f;

    void Update()
    {
        if (MaxCounter <= Counter)
        {
            Counter = 0;
            Destroy(BossNotStatic.gameObject);
            
        }
        else
            CannonTriggered();

    }
    static int MaxCounter;
   public int Counter = 0;
    public void CannonTriggered()
    {
        Counter = 0;
        for (int i = 0; i < TNT.Length; i++)
        {
            if(TNT[i]==null)
            {
                Counter++;
            }

        }
      
    }
}
