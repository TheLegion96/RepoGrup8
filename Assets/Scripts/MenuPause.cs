using Completed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour
{

    public Transform[] pause;
    public Player player;
    public float bookSpeed;
    //private bool isPaused = false;
    private int pauseIndex;
    //public GameObject obj;
    private MeshRenderer bookClosedMenuSubtitleMeshRenderer;

    // Use this for initialization
    void Start()
    {

        transform.position = pause[0].position;
        pauseIndex = 0;

        bookClosedMenuSubtitleMeshRenderer = GameObject.Find("BookClosedMenuSubtitle").GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.instance.state == GameManager.State.Play || GameManager.instance.state == GameManager.State.Pause)
        {
            if (Input.GetKeyDown(KeyCode.P) && player.isStillAlive)
            {
                if (transform.position == pause[pauseIndex].position)
                {
                    //bookClosedMenuSubtitleMeshRenderer.enabled = false;
                    pauseIndex++;
                    GetComponent<AudioSource>().Play();
                    if (GameManager.instance != null) GameManager.instance.state = GameManager.State.Pause;
                }
            }
            if (Input.GetKeyDown(KeyCode.P) && player.isStillAlive)
            {
                if (pauseIndex >= pause.Length)
                {
                    //bookClosedMenuSubtitleMeshRenderer.enabled = true;
                    pauseIndex = 0;
                    if (GameManager.instance != null) GameManager.instance.state = GameManager.State.Play;
                }

            }
        }

        //if (transform.position == pause[1].position)
        //{
        //    Time.timeScale = 0;
        //}

        if (transform.position != pause[pauseIndex].position)
        { 
            transform.position = Vector3.MoveTowards(transform.position, pause[pauseIndex].position, bookSpeed * Time.deltaTime); // Move the object to Arrays
        }
        if (transform.position == pause[0].position)
        {
            bookClosedMenuSubtitleMeshRenderer.enabled = true;
        }
        else {
            bookClosedMenuSubtitleMeshRenderer.enabled = false;
        }

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
