using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public Animator openMap;

    private Vector3 position = new Vector3(0f, 0f, 0f);
    private Vector3 scale = new Vector3(0.56f, 0.56f, 0f);

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
                SceneManager.LoadScene("Character_Selection"); 
            }
            else if (openMap.GetCurrentAnimatorStateInfo(0).IsName("Map2Anim"))
            {
                openMap.SetTrigger("Enter");
                //SceneManager.LoadScene("Select_level_simone");
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

        // Open Levels
        if (openMap.GetCurrentAnimatorStateInfo(0).IsName("Level1") && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Livello1_Stanza1");
        }

        if (openMap.GetCurrentAnimatorStateInfo(0).IsName("Level2") && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Livello1_Stanza2");
        }

        if (openMap.GetCurrentAnimatorStateInfo(0).IsName("Level3") && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Livello1_Stanza3");
        }

        if (openMap.GetCurrentAnimatorStateInfo(0).IsName("Level4") && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Livello2_Stanza1");
        }

        if (openMap.GetCurrentAnimatorStateInfo(0).IsName("Level5") && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Livello2_Stanza2");
        }

        if (openMap.GetCurrentAnimatorStateInfo(0).IsName("Level6") && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Livello2_Stanza3");
        }

        if (openMap.GetCurrentAnimatorStateInfo(0).IsName("Level7") && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Livello3_Stanza1");
        }

        if (openMap.GetCurrentAnimatorStateInfo(0).IsName("Level8") && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Livello3_Stanza2");
        }

        if (openMap.GetCurrentAnimatorStateInfo(0).IsName("Level9") && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Livello3_Stanza3");
        }

    }
}
