using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Completed
{
    public class Loader : MonoBehaviour
    {
        public GameObject gameManager;          //GameManager prefab to instantiate.
        public GameObject soundManager;         //SoundManager prefab to instantiate.
        public string sceneTitle;

        [Header("NON ANCORA IMPLEMENTATO ma potete riempirlo")]
        public Vector2[] minimumSteps;

        void Awake()
        {
            //Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
            if (GameManager.instance == null)

                //Instantiate gameManager prefab
                Instantiate(gameManager);

            //[Verza] Added level title in order to change the scene name dynamically on new scenes load.
            GameManager.instance.Title = (sceneTitle != null && sceneTitle != string.Empty ? sceneTitle : "Unnamed scene");

            //Check if a SoundManager has already been assigned to static variable GameManager.instance or if it's still null
            if (SoundManager.instance == null)

                //Instantiate SoundManager prefab
                Instantiate(soundManager);
        }
    }
}