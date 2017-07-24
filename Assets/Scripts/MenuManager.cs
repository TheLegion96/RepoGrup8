using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    private int index = 0;
    public int totalLevels = 3;
    public float yOffset = 1f;
    public Text[] menuItems;

    void Start()
    {

        menuItems[index].color = Color.red;

    }

    void Update()
    {



        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            foreach (Text item in menuItems)
            {
                item.color = Color.black;
            }

            if (index < totalLevels - 1)
            {
                index++;
                menuItems[index].color = Color.red;
                Vector2 position = transform.position;
                position.y -= yOffset;
                transform.position = position;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            foreach (Text item in menuItems)
            {
                item.color = Color.black;
            }

            if (index > 0)
            {
                index--;
                menuItems[index].color = Color.red;
                Vector2 position = transform.position;
                position.y += yOffset;
                transform.position = position;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (index == 0)
            {
                SceneManager.LoadScene("0_TUTORIAL - Scene 1");
            }
            else if (index == 1)
            {
                //SceneManager.LoadScene("");
            }
            else if (index == 2)
            {
                Application.Quit();
            }
        }

    }
}
