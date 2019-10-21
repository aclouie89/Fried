using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneObject : MonoBehaviour
{
    public GameObject TomatoBoxLeft, TomatoBoxRight;
    public GameObject CheeseBoxLeft, CheeseBoxRight;
    public GameObject LettuceBoxLeft, LettuceBoxRight;
    public Transform Player;
    GameObject clone1;
    public Transform plate1, plate2, plate3, plate4;
    bool flagHold = false;
    public float smoothTime = 0.001f;
    private Vector3 AVelocity = Vector3.zero;
    public Transform TrashCan1, TrashCan2, TrashCan3, TrashCan4;
    float delDis1, delDis2, delDis3, delDis4 = 3f;
    float putDis1, putDis2, putDis3, putDis4 = 3f;

    // Update is called once per frame
    void Update()
    {
        float dist1 = Vector3.Distance(TomatoBoxLeft.transform.position, Player.position);
        float dist2 = Vector3.Distance(TomatoBoxRight.transform.position, Player.position);
        float dist3 = Vector3.Distance(CheeseBoxLeft.transform.position, Player.position);
        float dist4 = Vector3.Distance(CheeseBoxRight.transform.position, Player.position);
        float dist5 = Vector3.Distance(LettuceBoxLeft.transform.position, Player.position);
        float dist6 = Vector3.Distance(LettuceBoxRight.transform.position, Player.position);

        //print(dist);
        if (Input.GetKeyDown(KeyCode.E) && !flagHold) {
            if(dist1 < 2f)
            {
                clone1 = Instantiate(TomatoBoxLeft, Player.position, Quaternion.identity) as GameObject;
              
                if (clone1.gameObject.GetComponent<Rigidbody>()) {
                    Debug.Log("rigidbody");
                }
                flagHold = true;

            }else if (dist2< 2f)
            {
                clone1 = Instantiate(TomatoBoxRight, Player.position, Quaternion.identity) as GameObject;
                
                if (clone1.gameObject.GetComponent<Rigidbody>())
                {
                    Debug.Log("rigidbody");
                }
                flagHold = true;
            }else if(dist3 < 2f)
            {
                clone1 = Instantiate(CheeseBoxLeft, Player.position, Quaternion.identity) as GameObject;
              
                if (clone1.gameObject.GetComponent<Rigidbody>())
                {
                    Debug.Log("rigidbody");
                }
                flagHold = true;
            }
            else if (dist4 < 1.5f)
            {
                clone1 = Instantiate(CheeseBoxRight, Player.position, Quaternion.identity) as GameObject;
               
                if (clone1.gameObject.GetComponent<Rigidbody>())
                {
                    Debug.Log("rigidbody");
                }
                flagHold = true;
            }
            else if (dist5 < 1.5f)
            {
                clone1 = Instantiate(LettuceBoxLeft, Player.position, Quaternion.identity) as GameObject;
                
                if (clone1.gameObject.GetComponent<Rigidbody>())
                {
                    Debug.Log("rigidbody");
                }
                flagHold = true;
            }
            else if (dist6 < 1.5f)
            {
                clone1 = Instantiate(LettuceBoxRight, Player.position, Quaternion.identity) as GameObject;
               
                if (clone1.gameObject.GetComponent<Rigidbody>())
                {
                    Debug.Log("rigidbody");
                }

                flagHold = true;
            }
        }
        
        //if (Input.GetKeyDown(KeyCode.C) && !flag1 && dist1<2f)
        //{
        //   clone1 = Instantiate(object1, Player.position, Quaternion.identity) as GameObject;     
        //    flagHold = true;
        //    flag1 = true;
        //    //delDis= Vector3.Distance(Player.position, TransCan.position);

        //}
        //else if (Input.GetKeyDown(KeyCode.T) && !flag1 && dist2<2f)
        //{
        //    clone1 = Instantiate(object2, Player.position, Quaternion.identity) as GameObject;
        //    flagHold = true;
        //    flag1 = true;
        //    //delDis = Vector3.Distance(Player.position, TransCan.position);
        //}
        //else if (Input.GetKeyDown(KeyCode.L) && !flag1 && dist3 < 2f)
        //{
        //    clone1 = Instantiate(object3, Player.position, Quaternion.identity) as GameObject;
        //    flagHold = true;
        //    flag1 = true;
        //   // delDis = Vector3.Distance(Player.position, TransCan.position);
        //}
        delDis1 = Vector3.Distance(Player.position, TrashCan1.position);
        delDis2 = Vector3.Distance(Player.position, TrashCan2.position);
        delDis3 = Vector3.Distance(Player.position, TrashCan3.position);
        delDis4 = Vector3.Distance(Player.position, TrashCan4.position);

        if (Input.GetKeyDown(KeyCode.E) && flagHold && delDis1<2f  )
        {
            Destroy(clone1);
            flagHold = false;
            delDis1=0f;
        } else if (Input.GetKeyDown(KeyCode.E) && flagHold && delDis2 < 2f)
        {
            Destroy(clone1);
            flagHold = false;
            delDis2 = 0f;
        } else if (Input.GetKeyDown(KeyCode.E) && flagHold && delDis3 < 2f)
        {
            Destroy(clone1);
            flagHold = false;
            delDis3 = 0f;
        } else if (Input.GetKeyDown(KeyCode.E) && flagHold && delDis4 < 2f)
        {
            Destroy(clone1);
            flagHold = false;
            delDis4 = 0f;
        }


        putDis1 = Vector3.Distance(Player.position, plate1.position);
        putDis2 = Vector3.Distance(Player.position, plate2.position);
        putDis3 = Vector3.Distance(Player.position, plate3.position);
        putDis4 = Vector3.Distance(Player.position, plate4.position);
        if (Input.GetKeyDown(KeyCode.E) && flagHold && putDis1 < 2f )
        {
            clone1.transform.position = plate1.position;
            flagHold = false;
        } else if (Input.GetKeyDown(KeyCode.E) && flagHold && putDis2 < 2f)
        {
            clone1.transform.position = plate2.position;
            flagHold = false;
        } else if (Input.GetKeyDown(KeyCode.E) && flagHold && putDis3 < 2f)
        {
            clone1.transform.position = plate3.position;
            flagHold = false;
        } else if (Input.GetKeyDown(KeyCode.E) && flagHold && putDis4 < 2f)
        {
            clone1.transform.position = plate4.position;
            flagHold = false;
        }

        if (flagHold)
        {
            clone1.transform.position = Vector3.SmoothDamp(clone1.transform.position, Player.position + new Vector3(0, 2, 0), ref AVelocity, smoothTime);
        }

      
    }
   
}
