using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    private int index = 0;

    public Sprite map_1;
    public Sprite map_2;
    public Sprite map_3;
    public Sprite map_4;

    private Vector3 position = new Vector3(0f, 0f, 0f);
    private Vector3 scale = new Vector3(0.56f, 0.56f, 0);

    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = map_1;
        transform.position = position;
        transform.localScale = scale;
    }

    void Update()
    {

        // Navigation
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (index < 3)
            {
                index++;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (index > 0)
            {
                index--;
            }
        }

        // Load levels
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (index == 0)
            {
                SceneManager.LoadScene("0_TUTORIAL - Scene 1");
            }
            else if (index == 1)
            {
                SceneManager.LoadScene("Select_level_simone");
            }
            else if (index == 2)
            {
                //Credits
            }
            else if (index == 3)
            {
                Application.Quit();
            }
        }

        // Background changes
        if (index == 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = map_1;
        }
        else if (index == 1)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = map_2;     
        }
        else if (index == 2)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = map_3;
        }
        else if (index == 3)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = map_4;
        }

    }
}
