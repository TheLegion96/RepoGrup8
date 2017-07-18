using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour {

    public Transform[] pause;
    public float bookSpeed;
    private bool isPaused = false;
    private int pauseIndex;
    //public GameObject obj;

	// Use this for initialization
	void Start () {

        transform.position = pause[0].position;
        pauseIndex = 0;

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (transform.position == pause[pauseIndex].position)
            {
                pauseIndex++;
                GetComponent<AudioSource>().Play();
                
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (pauseIndex >= pause.Length)
            {
                pauseIndex = 0;
                
            }
        }

        if (transform.position == pause[1].position)
        {
            //Time.timeScale = 0;
        }
        

        transform.position = Vector3.MoveTowards(transform.position, pause[pauseIndex].position, bookSpeed * Time.deltaTime); // Move the object to Arrays

        /*if (Input.GetKeyDown(KeyCode.P) && isPaused)
        {
                transform.position = new Vector3(8f, 10f, 0);
                isPaused = false;
                Debug.Log(isPaused);
        }
        
        if (Input.GetKeyDown(KeyCode.P) && !isPaused)
        {
            transform.position = new Vector3(8f, -10f, 0);
            isPaused = true;
            Debug.Log(isPaused);
        } /*else if(Input.GetKeyDown(KeyCode.P) && isPaused) {
            transform.position = new Vector3(0f, 10f, 0);
            isPaused = false;*/
        
	}
}
