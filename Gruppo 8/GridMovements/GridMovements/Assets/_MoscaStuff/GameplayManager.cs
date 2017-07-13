using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour {

    public Sprite mySprite;

    public GameObject myPlayer;
    

    public GameObject prefab;
    public int gridX = 5;
    public int gridY = 5;

    private int playerGridX;
    private int playerGridY;

    private MovableTile[,] tiledMap;

    private bool moving = false;

    // Use this for initialization
    void Start()
    {
        
        tiledMap = new MovableTile[gridX, gridY];

        for (int y = 0; y < gridY; y++)
        {
            for (int x = 0; x < gridX; x++)
            {
                Vector3 pos = new Vector3(x, y, 0) * mySprite.rect.width/100f;
                GameObject temp = (GameObject)Instantiate(prefab, pos, Quaternion.identity);
                tiledMap[y, x] = temp.GetComponent<MovableTile>();

            }
        }

        for (int y = 0; y < gridY; y++)
        {
            for (int x = 0; x < gridX; x++)
            {
                if (x > 0)
                {
                    tiledMap[y, x].LeftTile = tiledMap[y, x - 1];
                }
                if (x < gridX-1)
                {
                    tiledMap[y, x].RightTile = tiledMap[y, x + 1];
                }
                if (y > 0)
                {
                    tiledMap[y, x].TopTile = tiledMap[y - 1, x];
                }
                if (y < gridY-1)
                {
                    tiledMap[y, x].BottomTile = tiledMap[y + 1, x];
                }

            }
        }

        myPlayer.transform.position = tiledMap[gridY / 2, gridX / 2].transform.position;
        playerGridX = gridX / 2;
        playerGridY = gridY / 2;
    }

    // Update is called once per frame
    void Update ()
    {
        if (!moving)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                StartCoroutine(MoveRight());
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                StartCoroutine(MoveLeft());
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                StartCoroutine(MoveTop());
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                StartCoroutine(MoveDown());
            }
        }
        
        // If press W 
        // Check if movabletile.top is not null
        // Lerp towards tile on top
    }

    public IEnumerator MoveRight()
    {
        moving = true;

        if (tiledMap[playerGridY, playerGridX].RightTile != null)
        {
            float elapsedTime = 0;
            float animTime = 0.2f;

            while (elapsedTime < animTime)
            {
                myPlayer.transform.position = Vector3.Lerp(myPlayer.transform.position,
                                                    tiledMap[playerGridY, playerGridX].RightTile.transform.position,
                                                    elapsedTime/animTime);

                elapsedTime += Time.deltaTime;
                yield return null;
            }


            //myPlayer.transform.position = tiledMap[playerGridY, playerGridX].RightTile.transform.position;
            playerGridX++;
        }

        yield return new WaitForSeconds(1);

        moving = false;
    }

    public IEnumerator MoveLeft()
    {
        moving = true;


        if (tiledMap[playerGridY, playerGridX].LeftTile != null)
        {
            float elapsedTime = 0;
            float animTime = 0.2f;

            while (elapsedTime < animTime)
            {
                myPlayer.transform.position = Vector3.Lerp(myPlayer.transform.position,
                                                    tiledMap[playerGridY, playerGridX].LeftTile.transform.position,
                                                    elapsedTime / animTime);

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            playerGridX--;
        }

        yield return new WaitForSeconds(1);


        moving = false;
    }

    public IEnumerator MoveTop()
    {
        if (tiledMap[playerGridY, playerGridX].TopTile != null)
        {
            myPlayer.transform.position = tiledMap[playerGridY, playerGridX].TopTile.transform.position;
            playerGridY--;
        }

        yield return null;
    }

    public IEnumerator MoveDown()
    {
        if (tiledMap[playerGridY, playerGridX].BottomTile != null)
        {
            myPlayer.transform.position = tiledMap[playerGridY, playerGridX].BottomTile.transform.position;
            playerGridY++;
        }

        yield return null;
    }
}
