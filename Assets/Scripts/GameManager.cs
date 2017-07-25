using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Completed
{
    using System.Collections.Generic;       //Allows us to use Lists. 
    using UnityEngine.UI;                   //Allows us to use UI.

    public class GameManager : MonoBehaviour
    {
        public enum State
        {
            LevelStart,
            Play,
            Pause
        }

        public State state = State.Play;

        public float levelStartDelay = 2f;                      //Time to wait before starting level, in seconds.
        public float turnDelay = 0.1f;                          //Delay between each Player turn.
        public int playerTotalTurns = 0;                        //Starting value for Player Turns.
        public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
        [HideInInspector] public bool playersTurn = true;       //Boolean to check if it's players turn, hidden in inspector but public.


        private Text levelText;                                 //Text to display current level number.
        private GameObject levelImage;                          //Image to block out level as levels are being set up, background for levelText.
        private int level = 1;                                  //Current level number, expressed in game as "Day 1".
        public List<Enemy> enemies;                             //List of all Enemy units, used to issue them move commands.
        private bool enemiesMoving;                             //Boolean to check if enemies are moving.
        private bool doingSetup = true;							//Boolean to check if we're setting up board, prevent Player from moving during setup.

        //[Verza] Added new property in order to change scene title.
        private string title;
        private bool setRestartAvailable = false;

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;

                if (levelText != null)
                    UpdateSceneTitle(levelText);
            }
        }



        //Awake is always called before any Start functions
        void Awake()
        {
            //Check if instance already exists
            if (instance == null)

                //if not, set instance to this
                instance = this;

            //If instance already exists and it's not this:
            else if (instance != this)

                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);

            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);

            //Assign enemies to a new List of Enemy objects.
            enemies = new List<Enemy>();

            //Call the InitGame function to initialize the first level 
            InitGame();
        }

        //this is called only once, and the paramter tell it to be called only after the scene was loaded
        //(otherwise, our Scene Load callback would be called the very first load, and we don't want that)
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static public void CallbackInitialization()
        {
            //register the callback to be called everytime the scene is loaded
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        //This is called each time a scene is loaded.
        static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            instance.level = SceneManager.GetActiveScene().buildIndex + 1;
            instance.InitGame();
            if (instance.level == 1)
            {
                instance.playerTotalTurns = 0;
            }
        }


        //Initializes the game for each level.
        void InitGame()
        {
            state = State.LevelStart;

            //While doingSetup is true the player can't move, prevent player from moving while title card is up.
            doingSetup = true;

            //Get a reference to our image LevelImage by finding it by name.
            levelImage = GameObject.Find("LevelImage");
            if (levelImage != null)
            {
                GameObject leveTextGameObject = GameObject.Find("LevelText");

                if (leveTextGameObject != null)
                {
                    //Get a reference to our text LevelText's text component by finding it by name and calling GetComponent.
                    levelText = leveTextGameObject.GetComponent<Text>();

                    //Set the text of levelText to the string "Day" and append the current level number.
                    //levelText.text = "Day " + level;
                    //[Verza] Added dynamic title.
                    UpdateSceneTitle(levelText);

                    //[Verza] Moving title block.
                    StartCoroutine(MoveTitleBlock());

                    //Set levelImage to active blocking player's view of the game board during setup.
                    levelImage.SetActive(true);

                    //Call the HideLevelImage function with a delay in seconds of levelStartDelay.
                    Invoke("HideLevelImage", levelStartDelay);
                }
            }

            //Clear any Enemy objects in our List to prepare for next level.
            enemies.Clear();

        }

        void UpdateSceneTitle(Text levelText)
        {
            levelText.text = title;
        }

        IEnumerator MoveTitleBlock()
        {
            /*Vector3 startPosition = new Vector3(0, 305, 0);
            Vector3 endPosition = new Vector3(0, 5, 0);
            float movingSeconds = 2f;
            
            levelImage.GetComponent<RectTransform>().position = startPosition;

            while (movingSeconds > 0)
            {
                movingSeconds -= Time.deltaTime;

                levelImage.GetComponent<RectTransform>().position = Vector3.Lerp(levelImage.GetComponent<RectTransform>().position, endPosition, movingSeconds);

                yield return null;
            }*/

            yield return null;
        }


        //Hides black image used between levels
        void HideLevelImage()
        {
            //Disable the levelImage gameObject.
            levelImage.SetActive(false);

            //Set doingSetup to false allowing player to move again.
            doingSetup = false;

            state = State.Play;
        }

        //Update is called every frame.
        void Update()
        {
            if (setRestartAvailable == true)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }

            }
            //Check that  or enemiesMoving or doingSetup are not currently true.
            if (playersTurn || enemiesMoving || doingSetup)

                //If any of these are true, return and do not start MoveEnemies.
                return;

            //Start moving enemies.
            StartCoroutine(MoveEnemies());

        }

        //Call this to add the passed in Enemy to the List of Enemy objects.
        public void AddEnemyToList(Enemy script)
        {
            //Add Enemy to List enemies.
            enemies.Add(script);
        }

        //Call this to remove the passed in Enemy from the List of Enemy objects.
        public void RemoveEnemyFromList(Enemy script)
        {
            //Remove Enemy from List enemies.
            enemies.Remove(script);
        }


        //GameOver is called when the player reaches 0 food points
        public void GameOver()
        {
            //Set levelText to display number of levels passed and game over message
            levelText.text = "Sei morto nel livello " + level + "!\n Premi R per ricominciare il quadro";

            //Enable black background image gameObject.
            levelImage.SetActive(true);

            setRestartAvailable = true;
            //Disable this GameManager.
            // enabled = false;
        }

        //Coroutine to move enemies in sequence.
        IEnumerator MoveEnemies()
        {
            //While enemiesMoving is true player is unable to move.
            enemiesMoving = true;

            //Wait for turnDelay seconds, defaults to .1 (100 ms).
            yield return new WaitForSeconds(turnDelay);

            //If there are no enemies spawned (IE in first level):
            if (enemies.Count == 0)
            {
                //Wait for turnDelay seconds between moves, replaces delay caused by enemies moving when there are none.
                yield return new WaitForSeconds(turnDelay);
            }

            //Loop through List of Enemy objects.
            for (int i = 0; i < enemies.Count; i++)
            {
                //Call the MoveEnemy function of Enemy at index i in the enemies List.
                enemies[i].MoveEnemy();

                //Wait for Enemy's moveTime before moving next Enemy, 
                yield return new WaitForSeconds(enemies[i].moveTime / 100);
            }
            yield return new WaitForSeconds(0.1f);
     
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] is RangedEnemy)
                {
                    int tickBeforeChange = enemies[i].maxTicks - 1;
                    if (enemies[i].tick == tickBeforeChange)
                    {
                        enemies[i].ChangeAimingDirection(ref enemies[i].EnemyAimingWay);
                        ((RangedEnemy)enemies[i]).InstanceDeadZone();
                        for (int j = 0; j < 3; j++)
                        {
                            enemies[i].ChangeAimingDirection(ref enemies[i].EnemyAimingWay);
                        }
                    }
                    //yield return new WaitForSeconds(enemies[i].moveTime / 100);
                }
            }
        
   
        //Once Enemies are done moving, set playersTurn to true so player can move.
        playersTurn = true;

            //Enemies are done moving, set enemiesMoving to false.
            enemiesMoving = false;
        }
        int actualTick;
        #region Go To Next Scene
        //Goes to next scene when called.
        public void GoToNextScene(float delay)
        {
            Invoke("GoToNextScene_Invoked", delay);
        }
        private void GoToNextScene_Invoked()
        {
            GoToScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        public void GoToScene(int buildIndex)
        {
            //Load the last scene loaded, in this case Main, the only scene in the game. And we load it in "Single" mode so it replace the existing one
            //and not load all the scene object in the current scene.
            SceneManager.LoadScene(buildIndex, LoadSceneMode.Single);
        }
        #endregion
    }
}

