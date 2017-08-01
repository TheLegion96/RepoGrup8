using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public Animator openMap;

    private SpriteRenderer lvl1Enabled;


    private Vector3 position = new Vector3(0f, 0f, 0f);
    private Vector3 scale = new Vector3(0.56f, 0.56f, 0f);

    private float textsOpacity = 0;

    private TextMesh[] textMeshes;


    void Start()
    {
        transform.position = position;
        transform.localScale = scale;

        textMeshes = openMap.GetComponentsInChildren<TextMesh>();
        lvl1Enabled = GameObject.Find("blucube").GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        AnimatorStateInfo animatorStateInfo = openMap.GetCurrentAnimatorStateInfo(0);

        if (!animatorStateInfo.IsName("OpenMap"))
        {
            if (
                animatorStateInfo.IsName("Map1Anim") ||
                animatorStateInfo.IsName("Map2Anim") ||
                animatorStateInfo.IsName("Map3Anim") ||
                animatorStateInfo.IsName("Map4Anim")
                )
            {
                if (textsOpacity < 1)
                {
                    textsOpacity = Mathf.Clamp(textsOpacity + Time.deltaTime, 0, 1);

                    foreach (TextMesh item in textMeshes)
                    {
                        item.color = new Color(item.color.r, item.color.g, item.color.b, textsOpacity);
                    }
                }
            }
            else if (textsOpacity > 0)
            {
                textsOpacity = 0;

                foreach (TextMesh item in textMeshes)
                {
                    item.color = new Color(item.color.r, item.color.g, item.color.b, textsOpacity);
                }
            }

            // Navigation
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                openMap.SetTrigger("Next");
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                openMap.SetTrigger("Previous");
            }

            // Load levels
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (animatorStateInfo.IsName("Map1Anim"))
                {
                    SceneManager.LoadScene("Character_Selection");
                }
                else if (animatorStateInfo.IsName("Map2Anim"))
                {
                    openMap.SetTrigger("Enter");
                }
                else if (animatorStateInfo.IsName("Map3Anim"))
                {
                    SceneManager.LoadScene("Credits_simone");
                }
                else if (animatorStateInfo.IsName("Map4Anim"))
                {
                    Application.Quit();
                }
            }

            // Navigate Levels
            if (animatorStateInfo.IsName("Level1"))
            {
                lvl1Enabled.enabled = true;

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene("Livello1_Stanza1");
                }
            }
            else
            {
                lvl1Enabled.enabled = false;
            }

            if (animatorStateInfo.IsName("Level2"))
            {
                
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene("Livello1_Stanza2");
                }
                
            }

            if (animatorStateInfo.IsName("Level3") && Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Livello1_Stanza3");
            }

            if (animatorStateInfo.IsName("Level4") && Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Livello2_Stanza1");
            }

            if (animatorStateInfo.IsName("Level5") && Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Livello2_Stanza2");
            }

            if (animatorStateInfo.IsName("Level6") && Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Livello2_Stanza3");
            }

            if (animatorStateInfo.IsName("Level7") && Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Livello3_Stanza1");
            }

            if (animatorStateInfo.IsName("Level8") && Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Livello3_Stanza2");
            }

            if (animatorStateInfo.IsName("Level9") && Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Livello3_Stanza3");
            }
        }

    }
}
