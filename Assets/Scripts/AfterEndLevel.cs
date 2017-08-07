using Completed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AfterEndLevel : MonoBehaviour {

    public TextMesh stepsValueTextMesh;
    public SpriteRenderer endSceneFader;

    // Use this for initialization
    void Start () {
        stepsValueTextMesh.text = stepsValueTextMesh.text.Replace("@", GameManager.instance.playerTotalMoney.ToString());

        endSceneFader.DOFade(0, 3);
    }
	
	//// Update is called once per frame
	//void Update () {
		
	//}
}
