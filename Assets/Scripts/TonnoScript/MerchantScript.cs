using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantScript : MonoBehaviour {
    [SerializeField] private GameObject playerPosition;
	// Use this for initialization
	void Start () {
        playerPosition.GetComponent<GameObject>();
	}

	// Update is called once per frame
	void Update () {
        float _x, _y;
        _x = transform.position.x;
        _y = transform.position.y;
        if (_x == playerPosition.transform.position.x && _y == playerPosition.transform.position.y + 1 && Input.GetKeyDown(KeyCode.Space))
        {
            //Controllare tutorial per far comparire un bel blocco nero con una text-box e che siano selezionabili
            Debug.Log("Ciao Sono Dante il mercante Compra qualcosa");
        }
	}
}
