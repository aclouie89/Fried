﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneObject : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    public GameObject object4;
    public GameObject object5;
    public GameObject object6;
    public Transform Player;
   // public Transform distance1;
    GameObject clone1;
    public Transform plate;
    bool flagHold = false;
    public float smoothTime = 0.001f;
    private Vector3 AVelocity = Vector3.zero;
    //bool flagDestroy = false;
    public Transform TrashCan1;
    public Transform TrashCan2;
    public Transform TrashCan3;
    public Transform TrashCan4;
    float delDis=3f;
    float putDis = 3f;

    // Update is called once per frame
    void Update()
    {
        float dist1 = Vector3.Distance(object1.transform.position, Player.position);
        float dist2 = Vector3.Distance(object2.transform.position, Player.position);
        float dist3 = Vector3.Distance(object3.transform.position, Player.position);
        float dist4 = Vector3.Distance(object4.transform.position, Player.position);
        float dist5 = Vector3.Distance(object5.transform.position, Player.position);
        float dist6 = Vector3.Distance(object6.transform.position, Player.position);

        //print(dist);
        if (Input.GetKeyDown(KeyCode.E) && !flagHold) {
            if(dist1 < 2f)
            {
                clone1 = Instantiate(object1, Player.position, Quaternion.identity) as GameObject;
                flagHold = true;

            }else if (dist2< 2f)
            {
                clone1 = Instantiate(object2, Player.position, Quaternion.identity) as GameObject;
                flagHold = true;
            }else if(dist3 < 2f)
            {
                clone1 = Instantiate(object3, Player.position, Quaternion.identity) as GameObject;
                flagHold = true;
            }
            else if (dist4 < 1.5f)
            {
                clone1 = Instantiate(object4, Player.position, Quaternion.identity) as GameObject;
                flagHold = true;
            }
            else if (dist5 < 1.5f)
            {
                clone1 = Instantiate(object5, Player.position, Quaternion.identity) as GameObject;
                flagHold = true;
            }
            else if (dist6 < 1.5f)
            {
                clone1 = Instantiate(object6, Player.position, Quaternion.identity) as GameObject;
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
        delDis = Vector3.Distance(Player.position, TrashCan1.position);
        delDis = Vector3.Distance(Player.position, TrashCan2.position);
        delDis = Vector3.Distance(Player.position, TrashCan3.position);
        delDis = Vector3.Distance(Player.position, TrashCan4.position);

        if (Input.GetKeyDown(KeyCode.E) && flagHold && delDis<2f  )
        {
            Destroy(clone1);
            //flag1 = false;
            flagHold = false;
            delDis=0f;
        }

        putDis= Vector3.Distance(Player.position, plate.position);
        if (Input.GetKeyDown(KeyCode.E) && flagHold && putDis < 2f )
        {
            clone1.transform.position = plate.position;
           // flag1 = false;
            
            //Destroy(clone1);
            flagHold = false;
        }

        if (flagHold)
        {
            
            clone1.transform.position = Vector3.SmoothDamp(clone1.transform.position, Player.position + new Vector3(0, 2, 0), ref AVelocity, smoothTime);
        }


    }
}
