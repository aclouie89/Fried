using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneObject2 : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    public Transform Player;
    GameObject clone2;
    public Transform plate;
    bool flagHold = false;
    public float smoothTime = 0.001f;
    private Vector3 AVelocity = Vector3.zero;
    //bool flagDestroy = false;
    public Transform TransCan;
    float delDis = 3f;
    float putDis = 3f;

    // Update is called once per frame
    void Update()
    {
        float dist1 = Vector3.Distance(object1.transform.position, Player.position);
        float dist2 = Vector3.Distance(object2.transform.position, Player.position);
        float dist3 = Vector3.Distance(object3.transform.position, Player.position);

        //print(dist);
        if (Input.GetKeyDown(KeyCode.P) && !flagHold)
        {
            if (dist1 < 1.5f)
            {
                clone2 = Instantiate(object1, Player.position, Quaternion.identity) as GameObject;
                flagHold = true;

            }
            else if (dist2 < 1.5f)
            {
                clone2 = Instantiate(object2, Player.position, Quaternion.identity) as GameObject;
                flagHold = true;
            }
            else if (dist3 < 1.5f)
            {
                clone2 = Instantiate(object3, Player.position, Quaternion.identity) as GameObject;
                flagHold = true;

            }
        }


        delDis = Vector3.Distance(Player.position, TransCan.position);

        if (Input.GetKeyDown(KeyCode.P) && flagHold && delDis < 2f)
        {
            Destroy(clone2);
            //flag1 = false;
            flagHold = false;
            delDis = 0f;
        }

        putDis = Vector3.Distance(Player.position, plate.position);
        if (Input.GetKeyDown(KeyCode.P) && flagHold && putDis < 2f)
        {
            clone2.transform.position = plate.position;

            flagHold = false;
        }

        if (flagHold)
        {

            clone2.transform.position = Vector3.SmoothDamp(clone2.transform.position, Player.position + new Vector3(0, 2, 0), ref AVelocity, smoothTime);
        }


    }
}
