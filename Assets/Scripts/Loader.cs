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
        public string sceneSubtitle;
        public string sceneChapterText;

        void Awake()
        {
            //Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
            if (GameManager.instance == null)

                //Instantiate gameManager prefab
                Instantiate(gameManager);

            //[Verza] Added level title in order to change the scene name dynamically on new scenes load.
            GameManager.instance.Title = (sceneTitle != null && sceneTitle != string.Empty ? sceneTitle : "Unnamed Level");
            GameManager.instance.Subtitle = (sceneSubtitle != null && sceneSubtitle != string.Empty ? sceneSubtitle : "Unnamed Room");
            GameManager.instance.ChapterText = (sceneChapterText != null && sceneChapterText != string.Empty ? sceneChapterText : "Untold Story.");

            //Check if a SoundManager has already been assigned to static variable GameManager.instance or if it's still null
            if (SoundManager.instance == null)

                //Instantiate SoundManager prefab
                Instantiate(soundManager);
        }
    }
}