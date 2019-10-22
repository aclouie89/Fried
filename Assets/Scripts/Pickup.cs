using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject TomatoBoxLeft, TomatoBoxRight;
    public GameObject CheeseBoxLeft, CheeseBoxRight;
    public GameObject LettuceBoxLeft, LettuceBoxRight;
    public GameObject ExportTable;
    public Transform Player;
    GameObject clone1;
    GameObject clone2;
    GameObject clone3;
    GameObject clone4;
    GameObject clone5;
    GameObject clone6;
    public GameObject plate1, plate2, plate3, plate4;
    bool flagHold = false;
    bool[] exportHold = new bool[4];
    bool exportFlag = false;
    public float smoothTime = 0.001f;
    private Vector3 AVelocity = Vector3.zero;
    public Transform TrashCan1, TrashCan2, TrashCan3, TrashCan4;
    float delDis1, delDis2, delDis3, delDis4 = 3f;
    float putDis1, putDis2, putDis3, putDis4 = 3f;

    public PlateCount p1;
    public PlateCount2 p2;
    public PlateCount3 p3;
    public PlateCount4 p4;
    bool[] Material = new bool[6];
    int HoldObject = 0;

   


    Dictionary<int, string> dict1=new Dictionary<int, string>();
    Dictionary<int, string> dict2 = new Dictionary<int, string>();
    Dictionary<int, string> dict3 = new Dictionary<int, string>();
    Dictionary<int, string> dict4 = new Dictionary<int, string>();

    public GameObject temp1;
    public GameObject temp2;
    public GameObject temp3;
    public GameObject temp4;

    public Score1 score;

    // Update is called once per frame
    void Update()
    {
        float dist1 = Vector3.Distance(TomatoBoxLeft.transform.position, Player.position);
        float dist2 = Vector3.Distance(TomatoBoxRight.transform.position, Player.position);
        float dist3 = Vector3.Distance(CheeseBoxLeft.transform.position, Player.position);
        float dist4 = Vector3.Distance(CheeseBoxRight.transform.position, Player.position);
        float dist5 = Vector3.Distance(LettuceBoxLeft.transform.position, Player.position);
        float dist6 = Vector3.Distance(LettuceBoxRight.transform.position, Player.position);

        //pick up materials
        if (Input.GetKeyDown(KeyCode.E) && !flagHold)
        {
            if (dist1 < 2f)
            {
                clone1 = Instantiate(TomatoBoxLeft, Player.position, Quaternion.identity) as GameObject;
                flagHold = true;
                Material[0] = true;


            }
            else if (dist2 < 2f)
            {
                clone2 = Instantiate(TomatoBoxRight, Player.position, Quaternion.identity) as GameObject;
                flagHold = true;
                Material[1] = true;
            }
            else if (dist3 < 2f)
            {
                clone3 = Instantiate(CheeseBoxLeft, Player.position, Quaternion.identity) as GameObject;
                flagHold = true;
                Material[2] = true;
            }
            else if (dist4 < 2f)
            {
                clone4 = Instantiate(CheeseBoxRight, Player.position, Quaternion.identity) as GameObject;
                flagHold = true;
                Material[3] = true;
            }
            else if (dist5 < 2f)
            {
                clone5 = Instantiate(LettuceBoxLeft, Player.position, Quaternion.identity) as GameObject;
                flagHold = true;
                Material[4] = true;
            }
            else if (dist6 < 2f)
            {
                clone6 = Instantiate(LettuceBoxRight, Player.position, Quaternion.identity) as GameObject;
                flagHold = true;
                Material[5] = true;
            }
        }

        //delete materials
        delDis1 = Vector3.Distance(Player.position, TrashCan1.position);
        delDis2 = Vector3.Distance(Player.position, TrashCan2.position);
        delDis3 = Vector3.Distance(Player.position, TrashCan3.position);
        delDis4 = Vector3.Distance(Player.position, TrashCan4.position);
        if (Input.GetKeyDown(KeyCode.E)&& flagHold &&delDis1<2f)
        {
            if (Material[0])
            {
                Destroy(clone1);
                flagHold = false;
                Material[0] = false;
            }
            else if (Material[1])
            {
                Destroy(clone2);
                flagHold = false;
                Material[1] = false;
            }
            else if (Material[2])
            {
                Destroy(clone3);
                flagHold = false;
                Material[2] = false;
            }
            else if (Material[3])
            {
                Destroy(clone4);
                flagHold = false;
                Material[3] = false;
            }
            else if (Material[4])
            {
                Destroy(clone5);
                flagHold = false;
                Material[4] = false;
            }
            else if (Material[5])
            {
                Destroy(clone6);
                flagHold = false;
                Material[5] = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && flagHold && delDis2 < 2f)
        {
            if (Material[0])
            {
                Destroy(clone1);
                flagHold = false;
                Material[0] = false;
            }
            else if (Material[1])
            {
                Destroy(clone2);
                flagHold = false;
                Material[1] = false;
            }
            else if (Material[2])
            {
                Destroy(clone3);
                flagHold = false;
                Material[2] = false;
            }
            else if (Material[3])
            {
                Destroy(clone4);
                flagHold = false;
                Material[3] = false;
            }
            else if (Material[4])
            {
                Destroy(clone5);
                flagHold = false;
                Material[4] = false;
            }
            else if (Material[5])
            {
                Destroy(clone6);
                flagHold = false;
                Material[5] = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && flagHold && delDis3 < 2f)
        {
            if (Material[0])
            {
                Destroy(clone1);
                flagHold = false;
                Material[0] = false;
            }
            else if (Material[1])
            {
                Destroy(clone2);
                flagHold = false;
                Material[1] = false;
            }
            else if (Material[2])
            {
                Destroy(clone3);
                flagHold = false;
                Material[2] = false;
            }
            else if (Material[3])
            {
                Destroy(clone4);
                flagHold = false;
                Material[3] = false;
            }
            else if (Material[4])
            {
                Destroy(clone5);
                flagHold = false;
                Material[4] = false;
            }
            else if (Material[5])
            {
                Destroy(clone6);
                flagHold = false;
                Material[5] = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && flagHold && delDis4 < 2f)
        {
            if (Material[0])
            {
                Destroy(clone1);
                flagHold = false;
                Material[0] = false;
            }
            else if (Material[1])
            {
                Destroy(clone2);
                flagHold = false;
                Material[1] = false;
            }
            else if (Material[2])
            {
                Destroy(clone3);
                flagHold = false;
                Material[2] = false;
            }
            else if (Material[3])
            {
                Destroy(clone4);
                flagHold = false;
                Material[3] = false;
            }
            else if (Material[4])
            {
                Destroy(clone5);
                flagHold = false;
                Material[4] = false;
            }
            else if (Material[5])
            {
                Destroy(clone6);
                flagHold = false;
                Material[5] = false;
            }
        }

        //material move with character
        if (flagHold)
        {
            if (Material[0])
            {
                clone1.transform.position = Vector3.SmoothDamp(clone1.transform.position, Player.position + new Vector3(0, 2, 0), ref AVelocity, smoothTime);
                HoldObject = 1;
                

            }
            if (Material[1])
            {
                clone2.transform.position = Vector3.SmoothDamp(clone2.transform.position, Player.position + new Vector3(0, 2, 0), ref AVelocity, smoothTime);
                HoldObject = 2;

            }
            if (Material[2])
            {
                clone3.transform.position = Vector3.SmoothDamp(clone3.transform.position, Player.position + new Vector3(0, 2, 0), ref AVelocity, smoothTime);
                HoldObject = 3;

            }
            if (Material[3])
            {
                clone4.transform.position = Vector3.SmoothDamp(clone4.transform.position, Player.position + new Vector3(0, 2, 0), ref AVelocity, smoothTime);
                HoldObject = 4;


            }
            if (Material[4])
            {
                clone5.transform.position = Vector3.SmoothDamp(clone5.transform.position, Player.position + new Vector3(0, 2, 0), ref AVelocity, smoothTime);
                HoldObject = 5;

            }
            if (Material[5])
            {
                clone6.transform.position = Vector3.SmoothDamp(clone6.transform.position, Player.position + new Vector3(0, 2, 0), ref AVelocity, smoothTime);
                HoldObject = 6;

            }


        }


        putDis1 = Vector3.Distance(Player.position, plate1.transform.position);
        putDis2 = Vector3.Distance(Player.position, plate2.transform.position);
        putDis3 = Vector3.Distance(Player.position, plate3.transform.position);
        putDis4 = Vector3.Distance(Player.position, plate4.transform.position);

        //put material to the plate
        if(Input.GetKeyDown(KeyCode.E)&& flagHold&& putDis1 < 2f)
        {
            //if (clone1 != null) clone1.active = false;
            //if (clone2 != null) clone2.active = false;
            //if (clone3 != null) clone3.active = false;
            //if (clone4 != null) clone4.active = false;
            //if (clone5 != null) clone5.active = false;
            //if (clone6 != null) clone6.active = false;
            flagHold = false;

            switch (HoldObject)
            {
                case 1:
                    {
                        Destroy(clone1);
                        if (!dict1.ContainsKey(1) && !dict1.ContainsKey(2)){
                            p1.count();
                            // p1.object1();
                            p1.plateflag1[0] = true;
                            dict1.Add(1,"tomatoes");
                            
                            
                        }
                        Material[0] = false;
                        //clone1.transform.position = plate1.position;

                    }
                    break;
                case 2:
                    {
                        Destroy(clone2);
                        if (!dict1.ContainsKey(1) && !dict1.ContainsKey(2))
                        {
                            p1.count();
                            p1.plateflag1[0] = true;
                            dict1.Add(2, "tomatoes");
                        }
                        Material[1] = false;
                       // clone2.transform.position = plate1.position;
                    }
                    break;
                case 3:
                    {
                        Destroy(clone3);
                        if (!dict1.ContainsKey(3) && !dict1.ContainsKey(4))
                        {
                            p1.count();
                            p1.plateflag1[1] = true;
                            dict1.Add(3, "cheese");
                        }
                        Material[2] = false;
                        //clone3.transform.position = plate1.position;
                    }
                    break;
                case 4:
                    {
                        Destroy(clone4);
                        if (!dict1.ContainsKey(3) && !dict1.ContainsKey(4))
                        {
                            p1.count();
                            dict1.Add(4, "cheese");
                            p1.plateflag1[1] = true;
                           // clone4.transform.position = plate1.position;
                        }
                        Material[3] = false;
                    }
                    break;
                case 5:
                    {
                        Destroy(clone5);
                        if (!dict1.ContainsKey(5) && !dict1.ContainsKey(6))
                        {
                            p1.count();
                            dict1.Add(5, "lettuce");
                            p1.plateflag1[2] = true;
                            //clone5.transform.position = plate1.position;
                        }
                        Material[4] = false;
                    }
                    break;
                case 6:
                    {
                        Destroy(clone6);
                        if (!dict1.ContainsKey(5) && !dict1.ContainsKey(6))
                        {
                            p1.count();
                            dict1.Add(6, "lettuce");
                            p1.plateflag1[2] = true;
                           // clone6.transform.position = plate1.position;
                        }
                        Material[5] = false;
                    }
                    break;
            }
            if (temp1 != plate1)
            {
                temp1.transform.position = new Vector3(100, 200, 100);
            }
            if (p1.materialsCount == 1)
            {
                print(p1.plateflag1[0]);
                if(p1.plateflag1[0] == true)
                {
                    temp1 = GameObject.Find("Plate_Tomato_Right");
                    temp1.transform.position = plate1.transform.position;

                }else if(p1.plateflag1[1] == true)
                {
                    temp1 = GameObject.Find("Plate_Cheese_Right");
                    temp1.transform.position = plate1.transform.position;
                }else if(p1.plateflag1[2] == true)
                {
                    temp1 = GameObject.Find("Plate_Lettuce_Right");
                    temp1.transform.position = plate1.transform.position;
                }
                

            }
            else if (p1.materialsCount == 2)
            {
                temp1.transform.position = new Vector3(100, 200, 100);
                if (p1.plateflag1[0] == true && p1.plateflag1[1] == true)
                {

                    temp1 = GameObject.Find("Plate_Tomato_Cheese_Right");
                    temp1.transform.position = plate1.transform.position;
                }
                else if (p1.plateflag1[0] == true && p1.plateflag1[2] == true)
                {
                    temp1 = GameObject.Find("Plate_Tomato_Lettuce_Right");
                    temp1.transform.position = plate1.transform.position;
                }
                else if (p1.plateflag1[1] == true && p1.plateflag1[2] == true)
                {
                    temp1 = GameObject.Find("Plate_Cheese_Lettuce_Right");
                    temp1.transform.position = plate1.transform.position;
                }
            }
            else if (p1.materialsCount == 3)
            {
                temp1.transform.position = new Vector3(100, 200, 100);
                temp1 = GameObject.Find("Plate_Salad_Right");
                temp1.transform.position = plate1.transform.position;
            }

        }
        else if (Input.GetKeyDown(KeyCode.E) && flagHold && putDis2 < 2f)
        {
            //if (clone1 != null) clone1.active = false;
            //if (clone2 != null) clone2.active = false;
            //if (clone3 != null) clone3.active = false;
            //if (clone4 != null) clone4.active = false;
            //if (clone5 != null) clone5.active = false;
            //if (clone6 != null) clone6.active = false;
            flagHold = false;
            switch (HoldObject)
            {
                case 1:
                    {
                        Destroy(clone1);
                        if (!dict2.ContainsKey(1) && !dict2.ContainsKey(2))
                        {
                            p2.count2();
                            p2.plateflag2[0] = true;
                            dict2.Add(1, "tomatoes");
                        }
                        Material[0] = false;
                      //  clone1.transform.position = plate2.position;

                    }
                    break;
                case 2:
                    {
                        Destroy(clone2);
                        if (!dict2.ContainsKey(1) && !dict2.ContainsKey(2))
                        {
                            p2.count2();
                            p2.plateflag2[1] = true;
                            dict2.Add(2, "tomatoes");
                        }
                        Material[1] = false;
                       // clone2.transform.position = plate2.position;
                    }
                    break;
                case 3:
                    {
                        Destroy(clone3);
                        if (!dict2.ContainsKey(3) && !dict2.ContainsKey(4))
                        {
                            p2.count2();
                            p2.plateflag2[1] = true;
                            dict2.Add(3, "cheese");
                        }
                        Material[2] = false;
                       // clone3.transform.position = plate2.position;
                    }
                    break;
                case 4:
                    {
                        Destroy(clone4);
                        if (!dict2.ContainsKey(3) && !dict2.ContainsKey(4))
                        {
                            p2.count2();
                            p2.plateflag2[1] = true;
                            dict2.Add(4, "cheese");
                           // clone4.transform.position = plate2.position;
                        }
                        Material[3] = false;
                    }
                    break;
                case 5:
                    {
                        Destroy(clone5);
                        if (!dict2.ContainsKey(5) && !dict2.ContainsKey(6))
                        {
                            p2.count2();
                            p2.plateflag2[2] = true;
                            dict2.Add(5, "lettuce");
                          //  clone5.transform.position = plate2.position;
                        }
                        Material[4] = false;
                    }
                    break;
                case 6:
                    {
                        Destroy(clone6);
                        if (!dict2.ContainsKey(5) && !dict2.ContainsKey(6))
                        {
                            p2.count2();
                            p2.plateflag2[2] = true;
                            dict2.Add(6, "lettuce");
                          //  clone6.transform.position = plate2.position;
                        }
                        Material[5] = false;
                    }
                    break;
            }

            if (temp2 != plate2)
            {
                temp2.transform.position = new Vector3(100, 200, 100);
            }
            if (p2.materialsCount == 1)
            {
               // print(p1.plateflag1[0]);
                if (p2.plateflag2[0] == true)
                {
                    temp2 = GameObject.Find("Plate_Tomato_Right (1)");
                    temp2.transform.position = plate2.transform.position;

                }
                else if (p2.plateflag2[1] == true)
                {
                    temp2 = GameObject.Find("Plate_Cheese_Right (1)");
                    temp2.transform.position = plate2.transform.position;
                }
                else if (p2.plateflag2[2] == true)
                {
                    temp2 = GameObject.Find("Plate_Lettuce_Right (1)");
                    temp2.transform.position = plate2.transform.position;
                }


            }
            else if (p2.materialsCount == 2)
            {
                temp2.transform.position = new Vector3(100, 200, 100);
                if (p2.plateflag2[0] == true && p2.plateflag2[1] == true)
                {

                    temp2 = GameObject.Find("Plate_Tomato_Cheese_Right (1)");
                    temp2.transform.position = plate2.transform.position;
                }
                else if (p2.plateflag2[0] == true && p2.plateflag2[2] == true)
                {
                    temp2 = GameObject.Find("Plate_Tomato_Lettuce_Right (1)");
                    temp2.transform.position = plate2.transform.position;
                }
                else if (p2.plateflag2[1] == true && p2.plateflag2[2] == true)
                {
                    temp2 = GameObject.Find("Plate_Cheese_Lettuce_Right (1)");
                    temp2.transform.position = plate2.transform.position;
                }
            }
            else if (p2.materialsCount == 3)
            {
                temp2.transform.position = new Vector3(100, 200, 100);
                temp2 = GameObject.Find("Plate_Salad_Right (1)");
                temp2.transform.position = plate2.transform.position;
            }

        }
        else if (Input.GetKeyDown(KeyCode.E) && flagHold && putDis3 < 2f)
        {
            //if (clone1 != null) clone1.active = false;
            //if (clone2 != null) clone2.active = false;
            //if (clone3 != null) clone3.active = false;
            //if (clone4 != null) clone4.active = false;
            //if (clone5 != null) clone5.active = false;
            //if (clone6 != null) clone6.active = false;
            flagHold = false;
            switch (HoldObject)
            {
                case 1:
                    {
                        Destroy(clone1);
                        if (!dict3.ContainsKey(1) && !dict3.ContainsKey(2))
                        {
                            p3.count3();
                            p3.plateflag3[0] = true;
                            dict3.Add(1, "tomatoes");
                        }
                        Material[0] = false;
                        //clone1.transform.position = plate3.position;

                    }
                    break;
                case 2:
                    {
                        Destroy(clone2);
                        if (!dict3.ContainsKey(1) && !dict3.ContainsKey(2))
                        {
                            p3.count3();
                            p3.plateflag3[0] = true;
                            dict3.Add(2, "tomatoes");
                        }
                        Material[1] = false;
                       // clone2.transform.position = plate3.position;
                    }
                    break;
                case 3:
                    {
                        Destroy(clone3);
                        if (!dict3.ContainsKey(3) && !dict3.ContainsKey(4))
                        {
                            p3.count3();
                            p3.plateflag3[1] = true;
                            dict3.Add(3, "cheese");
                        }
                        Material[2] = false;
                      //  clone3.transform.position = plate3.position;
                    }
                    break;
                case 4:
                    {
                        Destroy(clone4);
                        if (!dict3.ContainsKey(3) && !dict3.ContainsKey(4))
                        {
                            p3.count3();
                            p3.plateflag3[1] = true;
                            dict3.Add(4, "cheese");
                      //      clone4.transform.position = plate3.position;
                        }
                        Material[3] = false;
                    }
                    break;
                case 5:
                    {
                        Destroy(clone5);
                        if (!dict3.ContainsKey(5) && !dict3.ContainsKey(6))
                        {
                            p3.count3();
                            p3.plateflag3[2] = true;
                            dict3.Add(5, "lettuce");
                        //    clone5.transform.position = plate3.position;
                        }
                        Material[4] = false;
                    }
                    break;
                case 6:
                    {
                        Destroy(clone6);
                        if (!dict3.ContainsKey(5) && !dict3.ContainsKey(6))
                        {
                            p3.count3();
                            p3.plateflag3[2] = true;
                            dict3.Add(6, "lettuce");
                          //  clone6.transform.position = plate3.position;
                        }
                        Material[5] = false;
                    }
                    break;
            }

            if (temp3 != plate3)
            {
                temp3.transform.position = new Vector3(100, 200, 100);
            }
            if (p3.materialsCount == 1)
            {
                // print(p1.plateflag1[0]);
                if (p3.plateflag3[0] == true)
                {
                    temp3 = GameObject.Find("Plate_Tomato_Right (2)");
                    temp3.transform.position = plate3.transform.position;

                }
                else if (p3.plateflag3[1] == true)
                {
                    temp3 = GameObject.Find("Plate_Cheese_Right (2)");
                    temp3.transform.position = plate3.transform.position;
                }
                else if (p3.plateflag3[2] == true)
                {
                    temp3 = GameObject.Find("Plate_Lettuce_Right (2)");
                    temp3.transform.position = plate3.transform.position;
                }


            }
            else if (p3.materialsCount == 2)
            {
                temp3.transform.position = new Vector3(100, 200, 100);
                if (p3.plateflag3[0] == true && p3.plateflag3[1] == true)
                {

                    temp3 = GameObject.Find("Plate_Tomato_Cheese_Right (2)");
                    temp3.transform.position = plate3.transform.position;
                }
                else if (p3.plateflag3[0] == true && p3.plateflag3[2] == true)
                {
                    temp3 = GameObject.Find("Plate_Tomato_Lettuce_Right (2)");
                    temp3.transform.position = plate3.transform.position;
                }
                else if (p3.plateflag3[1] == true && p3.plateflag3[2] == true)
                {
                    temp3 = GameObject.Find("Plate_Cheese_Lettuce_Right (2)");
                    temp3.transform.position = plate3.transform.position;
                }
            }
            else if (p3.materialsCount == 3)
            {
                temp3.transform.position = new Vector3(100, 200, 100);
                temp3 = GameObject.Find("Plate_Salad_Right (2)");
                temp3.transform.position = plate3.transform.position;
            }

        }
        else if (Input.GetKeyDown(KeyCode.E) && flagHold && putDis4 < 2f)
        {
            //if (clone1 != null) clone1.active = false;
            //if (clone2 != null) clone2.active = false;
            //if (clone3 != null) clone3.active = false;
            //if (clone4 != null) clone4.active = false;
            //if (clone5 != null) clone5.active = false;
            //if (clone6 != null) clone6.active = false;
            flagHold = false;
            switch (HoldObject)
            {
                case 1:
                    {
                        Destroy(clone1);
                        if (!dict4.ContainsKey(1) && !dict4.ContainsKey(2))
                        {
                            p4.count4();
                            p4.plateflag4[0] = true;
                            dict4.Add(1, "tomatoes");
                        }
                        Material[0] = false;
                       // clone1.transform.position = plate4.position;

                    }
                    break;
                case 2:
                    {
                        Destroy(clone2);
                        if (!dict4.ContainsKey(1) && !dict4.ContainsKey(2))
                        {
                            p4.count4();
                            p4.plateflag4[0] = true;
                            dict4.Add(2, "tomatoes");
                        }
                        Material[1] = false;
                       // clone2.transform.position = plate4.position;
                    }
                    break;
                case 3:
                    {
                        Destroy(clone3);
                        if (!dict4.ContainsKey(3) && !dict4.ContainsKey(4))
                        {
                            p4.count4();
                            p4.plateflag4[1] = true;
                            dict4.Add(3, "cheese");
                        }
                        Material[2] = false;
                     //   clone3.transform.position = plate4.position;
                    }
                    break;
                case 4:
                    {
                        Destroy(clone4);
                        if (!dict4.ContainsKey(3) && !dict4.ContainsKey(4))
                        {
                            p4.count4();
                            p4.plateflag4[1] = true;
                            dict4.Add(4, "cheese");
                     //       clone4.transform.position = plate4.position;
                        }
                        Material[3] = false;
                    }
                    break;
                case 5:
                    {
                        Destroy(clone5);
                        if (!dict4.ContainsKey(5) && !dict4.ContainsKey(6))
                        {
                            p4.count4();
                            p4.plateflag4[2] = true;
                            dict4.Add(5, "lettuce");
                      //      clone5.transform.position = plate4.position;
                        }
                        Material[4] = false;
                    }
                    break;
                case 6:
                    {
                        Destroy(clone6);
                        if (!dict4.ContainsKey(5) && !dict4.ContainsKey(6))
                        {
                            p4.count4();
                            p4.plateflag4[2] = true;
                            dict4.Add(6, "lettuce");
                       //     clone6.transform.position = plate4.position;
                        }
                        Material[5] = false;
                    }
                    break;
            }

            if (temp4 != plate4)
            {
                temp4.transform.position = new Vector3(100, 200, 100);
            }
            if (p4.materialsCount == 1)
            {
                // print(p1.plateflag1[0]);
                if (p4.plateflag4[0] == true)
                {
                    temp4 = GameObject.Find("Plate_Tomato_Right (3)");
                    temp4.transform.position = plate4.transform.position;

                }
                else if (p4.plateflag4[1] == true)
                {
                    temp4 = GameObject.Find("Plate_Cheese_Right (3)");
                    temp4.transform.position = plate4.transform.position;
                }
                else if (p4.plateflag4[2] == true)
                {
                    temp4 = GameObject.Find("Plate_Lettuce_Right (3)");
                    temp4.transform.position = plate4.transform.position;
                }


            }
            else if (p4.materialsCount == 2)
            {
                temp4.transform.position = new Vector3(100, 200, 100);
                if (p4.plateflag4[0] == true && p4.plateflag4[1] == true)
                {

                    temp4 = GameObject.Find("Plate_Tomato_Cheese_Right (3)");
                    temp4.transform.position = plate4.transform.position;
                }
                else if (p4.plateflag4[0] == true && p4.plateflag4[2] == true)
                {
                    temp4 = GameObject.Find("Plate_Tomato_Lettuce_Right (3)");
                    temp4.transform.position = plate4.transform.position;
                }
                else if (p4.plateflag4[1] == true && p4.plateflag4[2] == true)
                {
                    temp4 = GameObject.Find("Plate_Cheese_Lettuce_Right (3)");
                    temp4.transform.position = plate4.transform.position;
                }
            }
            else if (p4.materialsCount == 3)
            {
                temp4.transform.position = new Vector3(100, 200, 100);
                temp4 = GameObject.Find("Plate_Salad_Right (3)");
                temp4.transform.position = plate4.transform.position;
            }

        }


        // pick up food
        float exportDis= Vector3.Distance(ExportTable.transform.position, Player.position);
        print(!exportFlag);
        print(!flagHold);
        print(!exportHold[0]);
        print(p1.materialsCount);

        if (Input.GetKeyDown(KeyCode.E) && putDis1 < 1.5f && !exportFlag && !flagHold && !exportHold[0] && p1.materialsCount >= 3)
        {
            exportHold[0] = true;
            exportFlag = true;
            p1.resetFlag1();

        }
        if (Input.GetKeyDown(KeyCode.E) && putDis2 < 1.5f && !exportFlag && !flagHold && p2.materialsCount >= 3)
        {
            exportHold[1] = true;
            exportFlag = true;
            p2.resetFlag2();

        }
        if (Input.GetKeyDown(KeyCode.E) && putDis3 < 1.5f && !exportFlag && !flagHold && p3.materialsCount >= 3)
        {
            exportHold[2] = true;
            exportFlag = true;
            p3.resetFlag3();

        }
        if (Input.GetKeyDown(KeyCode.E) && putDis4 < 1.5f && !exportFlag && !flagHold && p4.materialsCount >= 3)
        {
            exportHold[3] = true;
            exportFlag = true;
            p4.resetFlag4();

        }




        if (exportFlag)
        {
            if (exportHold[0] )
            {
                temp1.transform.position = Vector3.SmoothDamp(temp1.transform.position, Player.position + new Vector3(0, 2, 0), ref AVelocity, smoothTime);

            }
            else if (exportHold[1] )
            {
                temp2.transform.position = Vector3.SmoothDamp(temp2.transform.position, Player.position + new Vector3(0, 2, 0), ref AVelocity, smoothTime);
            }
            else if (exportHold[2] )
            {
                temp3.transform.position = Vector3.SmoothDamp(temp3.transform.position, Player.position + new Vector3(0, 2, 0), ref AVelocity, smoothTime);
            }
            else if (exportHold[3])
            {
                temp4.transform.position = Vector3.SmoothDamp(temp4.transform.position, Player.position + new Vector3(0, 2, 0), ref AVelocity, smoothTime);
            }

        }
        if (Input.GetKeyDown(KeyCode.E) && exportFlag &&!flagHold && exportDis < 2f)
        {
            string foodName = "";
            if (exportHold[0])
            {
                temp1.transform.position = ExportTable.transform.position;
                exportHold[0] = false;
                foodName = temp1.name;
                p1.resetCount();
            }
            else if (exportHold[1])
            {
                temp2.transform.position = ExportTable.transform.position;
                exportHold[1] = false;
                foodName = temp2.name;
                p2.resetCount2();
            }
            else if (exportHold[2])
            {
                temp3.transform.position = ExportTable.transform.position;
                exportHold[2] = false;
                foodName = temp3.name;
                p3.resetCount3();
            }
            else if (exportHold[3])
            {
                temp4.transform.position = ExportTable.transform.position;
                exportHold[3] = false;
                foodName = temp4.name;
                p4.resetCount4();
            }

            exportFlag = false;
            
            var food= GameObject.Find(foodName);
            food.transform.position = new Vector3(100, 200, 100);
            if(foodName == "Plate_Salad_Right")
            {
                score.Score();
                dict1.Clear();
            }
            else if(foodName == "Plate_Salad_Right (1)")
            {
                score.Score();
                dict2.Clear();
            }
            else if(foodName == "Plate_Salad_Right (2)")
            {
                score.Score();
                dict3.Clear();
            }
            else if(foodName == "Plate_Salad_Right (3)")
            {
                score.Score();
                dict4.Clear();
            }
            else
            {
                score.Wrong();
            }
         
            //Destroy(clone1);
            //Destroy(clone2);
            //Destroy(clone3);
            //Destroy(clone4);
            //Destroy(clone5);
            //Destroy(clone6);




        }



 

        
       


    }





}
