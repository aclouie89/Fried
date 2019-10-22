using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCount3 : MonoBehaviour
{
    public int materialsCount = 0;
    public bool[] plateflag3 = new bool[3];

    // Update is called once per frame
    public void count3()
    {
        materialsCount = materialsCount + 1;
        //print("plate3");
        //print(materialsCount);
    }
    public void resetCount3()
    {
        materialsCount = 0;
    }

    public void minCount3()
    {
        materialsCount = materialsCount - 1;
    }
    void Update()
    {
        materialsCount = materialsCount;
    }
    public void resetFlag3()
    {
        for (int i = 0; i < 3; i++)
        {
            plateflag3[i] = false;
        }
    }


}
