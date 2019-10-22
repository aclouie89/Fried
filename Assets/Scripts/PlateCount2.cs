using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCount2 : MonoBehaviour
{
    public int materialsCount = 0;
    public bool[] plateflag2 = new bool[3];

    // Update is called once per frame
    public void count2()
    {
        materialsCount = materialsCount + 1;
        print("plate2");
        print(materialsCount);
    }
    public void resetCount2()
    {
        materialsCount = 0;
    }

    public void minCount2()
    {
        materialsCount = materialsCount - 1;
    }

    void Update()
    {
        materialsCount = materialsCount;
    }
    public void resetFlag2()
    {
        for (int i = 0; i < 3; i++)
        {
            plateflag2[i] = false;
        }
    }
}
