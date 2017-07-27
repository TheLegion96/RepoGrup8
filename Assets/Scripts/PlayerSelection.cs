using Completed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelection : MonoBehaviour {


    [SerializeField] private SpriteRenderer Maschio, Femmina;
    private bool Selected = false;
    private Color Enabled = new Color(47, 47, 47);
    private Color Disabled = new Color(255, 255, 255);
   public  Completed.Player Test;
  // Use this for initialization
	void Start () {
        Maschio.color = Enabled;

        Femmina.color = Disabled;
    }
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Selected)
            {
          
                Selected = false;
       
          }
          else
            {
         
                Selected = true;
   
        }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                float restartLevelDelay = 1f;
                Test.GetGender(Selected);
                GameManager.instance.GoToNextScene(restartLevelDelay);
            }
        }
    }
}
