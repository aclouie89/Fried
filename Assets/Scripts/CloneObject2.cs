using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneObject2 : MonoBehaviour
{
    public GameObject tomato1, tomato2;
    public GameObject cheese1, cheese2;
    public GameObject lettuce1,lettuce2;
    public Transform Player;
    GameObject clone2;
    public Transform plate_1, plate_2, plate_3, plate_4, cutting_board1, cutting_board2;
    bool flagHold = false;
    public float smoothTime = 0.001f;
    private Vector3 AVelocity = Vector3.zero;
    //bool flagDestroy = false;
    public Transform TrashCan1, TrashCan2, TrashCan3, TrashCan4;
    float deleteDis1, deleteDis2, deleteDis3, deleteDis4 = 3f;
    float putDish1, putDish2, putDish3, putDish4, putDish5, putDish6 = 3f;

    // Update is called once per frame
    void Update()
    {
        float dist1 = Vector3.Distance(tomato1.transform.position, Player.position);
        float dist2 = Vector3.Distance(tomato2.transform.position, Player.position);
        float dist3 = Vector3.Distance(cheese1.transform.position, Player.position);
        float dist4 = Vector3.Distance(cheese2.transform.position, Player.position);
        float dist5 = Vector3.Distance(lettuce1.transform.position, Player.position);
        float dist6 = Vector3.Distance(lettuce2.transform.position, Player.position);
    
        //print(dist);
        if (Input.GetKeyDown(KeyCode.P) && !flagHold)
        {
            if (dist1 < 1.5f)
            {
                clone2 = Instantiate(tomato1, Player.position, Quaternion.identity) as GameObject;
                flagHold = true;

            }
            else if (dist2 < 1.5f)
            {
                clone2 = Instantiate(tomato2, Player.position, Quaternion.identity) as GameObject;
                flagHold = true;
            }
            else if (dist3 < 1.5f)
            {
                clone2 = Instantiate(cheese1, Player.position, Quaternion.identity) as GameObject;
                flagHold = true;
            }
            else if (dist4 < 1.5f)
            {
                clone2 = Instantiate(cheese2, Player.position, Quaternion.identity) as GameObject;
                flagHold = true;
            }
            else if (dist5 < 1.5f)
            {
                clone2 = Instantiate(lettuce1, Player.position, Quaternion.identity) as GameObject;
                flagHold = true;
            }
            else if (dist6 < 1.5f)
            {
                clone2 = Instantiate(lettuce2, Player.position, Quaternion.identity) as GameObject;
                flagHold = true;
            }
        }


        deleteDis1 = Vector3.Distance(Player.position, TrashCan1.position);
        deleteDis2 = Vector3.Distance(Player.position, TrashCan2.position);
        deleteDis3 = Vector3.Distance(Player.position, TrashCan3.position);
        deleteDis4 = Vector3.Distance(Player.position, TrashCan4.position);

        if (Input.GetKeyDown(KeyCode.P) && flagHold && deleteDis1 < 2f)
        {
            Destroy(clone2);
            flagHold = false;
            deleteDis1 = 0f;
        } else if (Input.GetKeyDown(KeyCode.P) && flagHold && deleteDis2 < 2f)
        {
            Destroy(clone2);
            flagHold = false;
            deleteDis2 = 0f;
        } else if (Input.GetKeyDown(KeyCode.P) && flagHold && deleteDis3 < 2f)
        {
            Destroy(clone2);
            flagHold = false;
            deleteDis3 = 0f;
        } else if (Input.GetKeyDown(KeyCode.P) && flagHold && deleteDis4 < 2f)
        {
            Destroy(clone2);
            flagHold = false;
            deleteDis4 = 0f;
        }

        putDish1 = Vector3.Distance(Player.position, plate_1.position);
        putDish2 = Vector3.Distance(Player.position, plate_2.position);
        putDish3 = Vector3.Distance(Player.position, plate_3.position);
        putDish4 = Vector3.Distance(Player.position, plate_4.position);
        putDish5 = Vector3.Distance(Player.position, cutting_board1.position);
        putDish6 = Vector3.Distance(Player.position, cutting_board2.position);
        if (Input.GetKeyDown(KeyCode.P) && flagHold && putDish1 < 2f)
        {
            clone2.transform.position = plate_1.position;
            flagHold = false;
        } else if (Input.GetKeyDown(KeyCode.P) && flagHold && putDish2 < 2f)
        {
            clone2.transform.position = plate_2.position;
            flagHold = false;
        } else if (Input.GetKeyDown(KeyCode.P) && flagHold && putDish3 < 2f)
        {
            clone2.transform.position = plate_3.position;
            flagHold = false;
        } else if (Input.GetKeyDown(KeyCode.P) && flagHold && putDish4 < 2f)
        {
            clone2.transform.position = plate_4.position;
            flagHold = false;
        } else if (Input.GetKeyDown(KeyCode.P) && flagHold && putDish5 < 2f)
        {
            clone2.transform.position = cutting_board1.position;
            flagHold = false;
        } else if (Input.GetKeyDown(KeyCode.P) && flagHold && putDish6 < 2f)
        {
            clone2.transform.position = cutting_board2.position;
            flagHold = false;
        }

        if (flagHold)
        {

            clone2.transform.position = Vector3.SmoothDamp(clone2.transform.position, Player.position + new Vector3(0, 2, 0), ref AVelocity, smoothTime);
        }


    }
}
