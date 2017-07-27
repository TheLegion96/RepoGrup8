using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public Animator openMap;

    private Vector3 position = new Vector3(0f, 0f, 0f);
    private Vector3 scale = new Vector3(0.56f, 0.56f, 0);

    void Start()
    {
        transform.position = position;
        transform.localScale = scale;
    }

    void Update()
    {

        // Navigation
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            openMap.SetTrigger("Next");
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            openMap.SetTrigger("Previous");
        }

        // Load levels
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (openMap.GetCurrentAnimatorStateInfo(0).IsName("Map1Anim"))
            {
                SceneManager.LoadScene("0_TUTORIAL - Scene 1"); 
            }
            else if (openMap.GetCurrentAnimatorStateInfo(0).IsName("Map2Anim"))
            {
                SceneManager.LoadScene("Select_level_simone");
            }
            else if (openMap.GetCurrentAnimatorStateInfo(0).IsName("Map3Anim"))
            {
                SceneManager.LoadScene("Credits_simone");
            }
            else if (openMap.GetCurrentAnimatorStateInfo(0).IsName("Map4Anim"))
            {
                Application.Quit();
            }
        }

    }
}
