﻿using Completed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    public static bool Go = false;
    [Header("IMPOSTARE I VALORI A n.5")]
    public CustomVector2[] Proiettile;
    [Header("NON TOCCATE")]
    [SerializeField]
    private GameObject DeadZone;
    private Player player;
    [SerializeField] private GameObject PlayerREF;
    [SerializeField] private GameObject Object;
    [System.Serializable]
    public struct CustomVector2
    {
        public float x;
        public float y;
        public float Turni;

        public CustomVector2(float xx, float yy, float Turn)
        {
            x = xx;
            y = yy;
            Turni = Turn;
        }
    }

    // Use this for initialization
    void Start()
    {

        #region Instanziazione Casuale Proiettili Disabilitiata (Abilitare solo in casi estremi togliendo i commenti)   
        //for (int i = 0; i < Proiettile.Length; i++)
        //{
        //    Proiettile[i].x = Mathf.Round(Random.Range(10, 130) / 10);
        //    Proiettile[i].x = Mathf.Round(Proiettile[i].x);
        //    Proiettile[i].x += 0.5f;

        //    Proiettile[i].y = Mathf.Round(Random.Range(10, 70));
        //    Proiettile[i].y = Mathf.Round(Proiettile[i].y /= 10);
        //    Proiettile[i].y += 0.5f;

        //    Proiettile[i].Turni = Mathf.Round(Random.Range(2, 15));

        //}

        #endregion


        Go = false;

        for (int i = 0; i < Proiettile.Length; i++)
        {
            if ((int)Proiettile[i].Turni == 0)
            {

                Transform _Temp = Instantiate(DeadZone.transform, new Vector3(Proiettile[i].x, Proiettile[i].y, -1), Quaternion.identity);
                _Temp.position = new Vector3(Proiettile[i].x, Proiettile[i].y, -1);
            }
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Soda")
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            player.ExecuteGameOver();
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (Go)
        {
            for (int i = 0; i < Proiettile.Length; i++)
            {
                switch ((int)Proiettile[i].Turni)
                {
                    case 0:
                        Transform _TempTentacle = Instantiate(Object.transform, this.transform.position, Quaternion.identity);
                        _TempTentacle.position = new Vector3(Proiettile[i].x, Proiettile[i].y, -1);
                        if (_TempTentacle.position == PlayerREF.transform.position)
                        {
                            player.ExecuteGameOver();
                        }

                        Proiettile[i].Turni -= 1; break;
                    case 1:
                        Transform _Temp = Instantiate(DeadZone.transform, new Vector3(Proiettile[i].x, Proiettile[i].y, -1), Quaternion.identity);
                        _Temp.position = new Vector3(Proiettile[i].x, Proiettile[i].y, -1);
                        Proiettile[i].Turni -= 1; break;

                    default: Proiettile[i].Turni -= 1; break;

                }
            }
            Go = false;
        }
    }

}
