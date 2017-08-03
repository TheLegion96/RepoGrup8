using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {

   public static bool Go = false;
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
    void Start() {
        for (int i = 0; i < Proiettile.Length; i++)
        {
            Proiettile[i].x = Mathf.Round(Random.Range(10, 130)/10);
            Proiettile[i].x = Mathf.Round(Proiettile[i].x );
            Proiettile[i].x += 0.5f;

            Proiettile[i].y = Mathf.Round(Random.Range(10, 70));
            Proiettile[i].y= Mathf.Round(Proiettile[i].y /= 10);
            Proiettile[i].y += 0.5f;

            Proiettile[i].Turni = Mathf.Round(Random.Range(0, 15));

            //while((Proiettile[i].x == BossLevel.TNT[i].transform.position.x) && (Proiettile[i].y == BossLevel.TNT[i].transform.position.y))      
            // if((Proiettile[i].x == BossLevel.TNT[i].transform.position.x) && (Proiettile[i].y == BossLevel.TNT[i].transform.position.y))
            // {
            //    Proiettile[i].x = Mathf.Round(Random.Range(10, 130) / 10);
            //    Proiettile[i].x = Mathf.Round(Proiettile[i].x);
            //    Proiettile[i].x += 0.5f;

            //    Proiettile[i].y = Mathf.Round(Random.Range(10, 70));
            //    Proiettile[i].y = Mathf.Round(Proiettile[i].y /= 10);
            //    Proiettile[i].y += 0.5f;
            //}

        }


    }
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Soda")
        {
            Destroy(this.gameObject);
        }
    }
    public GameObject DeadZone;
    public CustomVector2[] Proiettile;
    [SerializeField] private GameObject Object;

    // Update is called once per frame
    void Update() {

        if (Go)
        {
            for (int i = 0; i < Proiettile.Length; i++)
            {
                if (Proiettile[i].Turni != 0)
                {
                    Proiettile[i].Turni -= 1;
                }
                else if (Proiettile[i].Turni == 1)
                {
                    Transform _Temp = Instantiate<Transform>(DeadZone.transform, new Vector3(Proiettile[i].x, Proiettile[i].y,3), Quaternion.identity);
                }
                else
                {
                    Transform _TempStalaggmite = Instantiate(Object.transform, this.transform.position, Quaternion.identity);
                    _TempStalaggmite.position = new Vector3(Proiettile[i].x, Proiettile[i].y, -2);
                    Proiettile[i].Turni -= 1;
                }
            }
            Go = false;
        }
    }

}
