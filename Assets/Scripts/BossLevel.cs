using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevel : MonoBehaviour {

    public class BossThingElement {
        public int turnNumber;
        public Vector2 position;
    }

    //x e y sono le coordinate, z è il numero del turno.
    public Vector3[] bossPattern;

    public GameObject bossThing;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
