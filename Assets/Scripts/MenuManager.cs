using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using Completed;

public class MenuManager : MonoBehaviour
{

    public Animator openMap;

    public AudioClip switchSelection;
    public AudioClip confirmSelection;

    private bool isToken_1 = false;
    private bool isToken_2 = false;
    private bool isToken_3 = false;
    private bool isToken_4 = true;
    private bool isToken_5 = false;
    private bool isToken_6 = false;
    private bool isToken_7 = false;
    private bool isToken_8 = false;
    private bool isToken_9 = false;
    private bool isToken_10 = false;



    // Level Icons
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

    // Tokens Render
    private SpriteRenderer Token_1;
    private SpriteRenderer Token_2;
    private SpriteRenderer Token_3;
    private SpriteRenderer Token_4;
    private SpriteRenderer Token_5;
    private SpriteRenderer Token_6;
    private SpriteRenderer Token_7;
    private SpriteRenderer Token_8;
    private SpriteRenderer Token_9;
    private SpriteRenderer Token_10;

    private SpriteRenderer TokenGrey;

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

        Token_1 = GameObject.Find("Token_1").GetComponent<SpriteRenderer>();
        Token_2 = GameObject.Find("Token_2").GetComponent<SpriteRenderer>();
        Token_3 = GameObject.Find("Token_3").GetComponent<SpriteRenderer>();
        Token_4 = GameObject.Find("Token_4").GetComponent<SpriteRenderer>();
        Token_5 = GameObject.Find("Token_5").GetComponent<SpriteRenderer>();
        Token_6 = GameObject.Find("Token_6").GetComponent<SpriteRenderer>();
        Token_7 = GameObject.Find("Token_7").GetComponent<SpriteRenderer>();
        Token_8 = GameObject.Find("Token_8").GetComponent<SpriteRenderer>();
        Token_9 = GameObject.Find("Token_9").GetComponent<SpriteRenderer>();
        Token_10 = GameObject.Find("Token_10").GetComponent<SpriteRenderer>();

        TokenGrey = GameObject.Find("TokenGrey").GetComponent<SpriteRenderer>();





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
                SoundManager.instance.PlaySingle(switchSelection);
                
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                openMap.SetTrigger("Previous");
                SoundManager.instance.PlaySingle(switchSelection);
            }

            // Load levels
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SoundManager.instance.PlaySingle(confirmSelection);

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

                if (isToken_1)
                {
                    Token_1.enabled = true;
                }

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SoundManager.instance.PlaySingle(confirmSelection);
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

                if (isToken_2)
                {
                    Token_2.enabled = true;
                }

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SoundManager.instance.PlaySingle(confirmSelection);
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

                if (isToken_3)
                {
                    Token_3.enabled = true;
                }

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SoundManager.instance.PlaySingle(confirmSelection);
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

                if (isToken_4)
                {
                    Token_4.enabled = true;
                }

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SoundManager.instance.PlaySingle(confirmSelection);
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

                if (isToken_5)
                {
                    Token_5.enabled = true;
                }

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SoundManager.instance.PlaySingle(confirmSelection);
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

                if (isToken_6)
                {
                    Token_6.enabled = true;
                }

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SoundManager.instance.PlaySingle(confirmSelection);
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

                if (isToken_7)
                {
                    Token_7.enabled = true;
                }

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SoundManager.instance.PlaySingle(confirmSelection);
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

                if (isToken_8)
                {
                    Token_8.enabled = true;
                }

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SoundManager.instance.PlaySingle(confirmSelection);
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

                if (isToken_9)
                {
                    Token_9.enabled = true;
                }

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SoundManager.instance.PlaySingle(confirmSelection);
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

                if (isToken_10)
                {
                    Token_10.enabled = true;
                }

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SoundManager.instance.PlaySingle(confirmSelection);
                    SceneManager.LoadScene("");
                }
            }
            else
            {
                lvl10Enabled.enabled = false;
            }

            // Grey Token

            if (animatorStateInfo.IsName("OpenLevels"))
            {
                TokenGrey.enabled = true;
            }

            if (!animatorStateInfo.IsName("Level1"))
                Token_1.enabled = false;

            if (!animatorStateInfo.IsName("Level2"))
                Token_2.enabled = false;

            if (!animatorStateInfo.IsName("Level3"))
                Token_3.enabled = false;

            if (!animatorStateInfo.IsName("Level4"))
                Token_4.enabled = false;

            if (!animatorStateInfo.IsName("Level5"))
                Token_5.enabled = false;

            if (!animatorStateInfo.IsName("Level6"))
                Token_6.enabled = false;

            if (!animatorStateInfo.IsName("Level7"))
                Token_7.enabled = false;

            if (!animatorStateInfo.IsName("Level8"))
                Token_8.enabled = false;

            if (!animatorStateInfo.IsName("Level9"))
                Token_9.enabled = false;

            if (!animatorStateInfo.IsName("Boss"))
                Token_10.enabled = false;

        }
    }


        
}
