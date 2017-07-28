using Completed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour
{
   // [SerializeField] private SpriteRenderer Maschio, Femmina;
    private bool Selected = false;
    private Color Enabled = new Color(47, 47, 47);
    private Color Disabled = new Color(255, 255, 255);
    public Completed.Player Test;
    public GameObject gO;
    private Text gOText;
    // Use this for initialization
    void Start()
    {
        /*  Maschio.color = Enabled;
          Femmina.color = Disabled;*/
        gO = GameObject.Find("Sex");
        gOText=gO.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //Selected true = Maschio, selected false = Femmina
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Selected)
            {
                Selected = false;
                gOText.text = "MASCHIO";
            }
            else
            {
                Selected = true;
                gOText.text = "Femmina";

            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && Selected)
        {
            float restartLevelDelay = 1f;

            if (Selected)
            {
             Player.gender=Player.Gender.Male;
            }
            else
            {

                Player.gender = Player.Gender.Female;
            }
            GameManager.instance.GoToNextScene(restartLevelDelay);
        }
    }
}
