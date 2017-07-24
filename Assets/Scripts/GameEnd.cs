using Completed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        //GameObject.Find("LevelText").GetComponent<Text>().text = "Bravo! Ce l'hai fatta!\n\nPremi \"R\" e riparti!";
        /*foreach (Object item in FindObjectsOfType<GameManager>()) {
            Destroy(((GameManager)item).gameObject);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.instance.GoToScene(0);
        }
    }
}
