﻿using Completed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour
{
    [SerializeField] private SpriteRenderer Maschio, Femmina;
    private bool Selected;
    private Color Enabled = new Color(47, 47, 47);
    private Color Disabled = new Color(255, 255, 255);

    public GameObject gO;
    private Text gOText;
    private Player.Gender tmpGender;

    private Vector3 defaultScale = new Vector3(5, 5, 1);
    private Vector3 finalScale = new Vector3(6, 6, 1);

    // Use this for initialization
    void Start()
    {
        /*  Maschio.color = Enabled;
          Femmina.color = Disabled;*/
        gO = GameObject.Find("Sex");
        gOText = gO.GetComponent<Text>();

        SetSelectedCharacter(Player.Gender.Male);
    }

    // Update is called once per frame
    void Update()
    {

        //Selected true = Maschio, selected false = Femmina
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetSelectedCharacter(tmpGender == Player.Gender.Male ? Player.Gender.Female : Player.Gender.Male);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Player.gender = tmpGender;

            SceneManager.LoadScene("0_TUTORIAL - Scene 1", LoadSceneMode.Single);
        }
    }

    void SetSelectedCharacter(Player.Gender gender)
    {
        tmpGender = gender;

        if (tmpGender == Player.Gender.Female)
        {
            Selected = false;
            gOText.text = "Femmina";

            for (int i = 0; i < 10; i++)
            {
                Femmina.transform.localScale = finalScale;
                Maschio.transform.localScale = defaultScale;
            }
        }
        else {
            Selected = true;
            gOText.text = "Maschio";

            for (int i = 0; i < 10; i++)
            {
                Maschio.transform.localScale = finalScale;
                Femmina.transform.localScale = defaultScale;
            }
        }
    }

}
