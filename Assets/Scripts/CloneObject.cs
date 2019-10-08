using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneObject : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    public Transform Player;
    GameObject clone1;
    public Transform plate;
    bool flag = false;
    public float smoothTime = 0.001f;
    private Vector3 AVelocity = Vector3.zero;
    bool flag1 = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !flag1)
        {
           clone1 = Instantiate(object1, Player.position, Quaternion.identity) as GameObject;     
            flag = true;
            flag1 = true;
        }
        else if (Input.GetKeyDown(KeyCode.T) && !flag1)
        {
            clone1 = Instantiate(object2, Player.position, Quaternion.identity) as GameObject;
            flag = true;
            flag1 = true;
        }
        else if (Input.GetKeyDown(KeyCode.L) && !flag1)
        {
            clone1 = Instantiate(object3, Player.position, Quaternion.identity) as GameObject;
            flag = true;
            flag1 = true;
        }

        if (Input.GetKeyDown(KeyCode.E) && flag1)
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
