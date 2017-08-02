using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public Animator openMap;

    private SpriteRenderer lvl1Enabled;
    private SpriteRenderer lvl2Enabled;
    private SpriteRenderer lvl3Enabled;
    private SpriteRenderer lvl4Enabled;
    private SpriteRenderer lvl5Enabled;
    private SpriteRenderer lvl6Enabled;
    private SpriteRenderer lvl7Enabled;
    private SpriteRenderer lvl8Enabled;
    private SpriteRenderer lvl9Enabled;
    private SpriteRenderer lvl10Enabled;


    private Vector3 position = new Vector3(0f, 0f, 0f);
    private Vector3 scale = new Vector3(0.56f, 0.56f, 0f);

    private float textsOpacity = 0;

    private TextMesh[] textMeshes;


    void Start()
    {
        transform.position = position;
        transform.localScale = scale;

        textMeshes = openMap.GetComponentsInChildren<TextMesh>();

        lvl1Enabled = GameObject.Find("Selezione1").GetComponent<SpriteRenderer>();
        lvl2Enabled = GameObject.Find("Selezione2").GetComponent<SpriteRenderer>();
        lvl3Enabled = GameObject.Find("Selezione3").GetComponent<SpriteRenderer>();
        lvl4Enabled = GameObject.Find("Selezione4").GetComponent<SpriteRenderer>();
        lvl5Enabled = GameObject.Find("Selezione5").GetComponent<SpriteRenderer>();
        lvl6Enabled = GameObject.Find("Selezione6").GetComponent<SpriteRenderer>();
        lvl7Enabled = GameObject.Find("Selezione7").GetComponent<SpriteRenderer>();
        lvl8Enabled = GameObject.Find("Selezione8").GetComponent<SpriteRenderer>();
        lvl9Enabled = GameObject.Find("Selezione9").GetComponent<SpriteRenderer>();
        lvl10Enabled = GameObject.Find("Selezione10").GetComponent<SpriteRenderer>();


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
                lvl2Enabled.enabled = true;

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene("Livello1_Stanza2");
                }
            }
            else
            {
                lvl2Enabled.enabled = false;
            }

            if (animatorStateInfo.IsName("Level3"))
            {
                lvl3Enabled.enabled = true;

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene("Livello1_Stanza3");
                }
            }
            else
            {
                lvl3Enabled.enabled = false;
            }

            if (animatorStateInfo.IsName("Level4"))
            {
                lvl4Enabled.enabled = true;

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene("Livello2_Stanza1");
                }
            }
            else
            {
                lvl4Enabled.enabled = false;
            }

            if (animatorStateInfo.IsName("Level5"))
            {
                lvl5Enabled.enabled = true;

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene("Livello2_Stanza2");
                }
            }
            else
            {
                lvl5Enabled.enabled = false;
            }

            if (animatorStateInfo.IsName("Level6"))
            {
                lvl6Enabled.enabled = true;

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene("Livello2_Stanza3");
                }
            }
            else
            {
                lvl6Enabled.enabled = false;
            }

            if (animatorStateInfo.IsName("Level7"))
            {
                lvl7Enabled.enabled = true;

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene("Livello3_Stanza1");
                }
            }
            else
            {
                lvl7Enabled.enabled = false;
            }

            if (animatorStateInfo.IsName("Level8"))
            {
                lvl8Enabled.enabled = true;

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene("Livello3_Stanza2");
                }
            }
            else
            {
                lvl8Enabled.enabled = false;
            }

            if (animatorStateInfo.IsName("Level9"))
            {
                lvl9Enabled.enabled = true;

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene("Livello3_Stanza3");
                }
            }
            else
            {
                lvl9Enabled.enabled = false;
            }

            if (animatorStateInfo.IsName("Boss"))
            {
                lvl10Enabled.enabled = true;

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene("");
                }
            }
            else
            {
                lvl10Enabled.enabled = false;
            }
        }

    }
}
