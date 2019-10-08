using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneObject : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    public Transform Player;
   // public Transform distance1;
    GameObject clone1;
    public Transform plate;
    bool flag = false;
    public float smoothTime = 0.001f;
    private Vector3 AVelocity = Vector3.zero;
    bool flag1 = false;
    public Transform TransCan;
    float delDis=3f;
    float putDis = 3f;

    // Update is called once per frame
    void Update()
    {
        float dist1 = Vector3.Distance(object1.transform.position, Player.position);
        float dist2 = Vector3.Distance(object2.transform.position, Player.position);
        float dist3 = Vector3.Distance(object3.transform.position, Player.position);
        
        //print(dist);
        if (Input.GetKeyDown(KeyCode.C) && !flag1 && dist1<2f)
        {
           clone1 = Instantiate(object1, Player.position, Quaternion.identity) as GameObject;     
            flag = true;
            flag1 = true;
            //delDis= Vector3.Distance(Player.position, TransCan.position);

        }
        else if (Input.GetKeyDown(KeyCode.T) && !flag1 && dist2<2f)
        {
            clone1 = Instantiate(object2, Player.position, Quaternion.identity) as GameObject;
            flag = true;
            flag1 = true;
            //delDis = Vector3.Distance(Player.position, TransCan.position);
        }
        else if (Input.GetKeyDown(KeyCode.L) && !flag1 && dist3 < 2f)
        {
            clone1 = Instantiate(object3, Player.position, Quaternion.identity) as GameObject;
            flag = true;
            flag1 = true;
           // delDis = Vector3.Distance(Player.position, TransCan.position);
        }
        delDis = Vector3.Distance(Player.position, TransCan.position);

        if (Input.GetKeyDown(KeyCode.Q) && flag1 && delDis<2f  )
        {
            Destroy(clone1);
            flag1 = false;
            flag = false;
            delDis=0f;
        }

        putDis= Vector3.Distance(Player.position, plate.position);
        if (Input.GetKeyDown(KeyCode.E) && flag1 && putDis < 2f )
        {
            clone1.transform.position = plate.position;
            flag1 = false;
            
            //Destroy(clone1);
            flag = false;
        }
        if (flag)
        {
            
            clone1.transform.position = Vector3.SmoothDamp(clone1.transform.position, Player.position + new Vector3(0, 2, 0), ref AVelocity, smoothTime);
        }


    }
}
