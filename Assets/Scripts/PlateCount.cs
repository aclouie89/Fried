using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCount : MonoBehaviour
{
    public int materialsCount = 0;
    public bool[] plateflag1 = new bool[3];

    // Update is called once per frame
    public void count()
    {
        materialsCount = materialsCount + 1;
        print("plate1");
        print(materialsCount);
    }
    public void resetCount()
    {
        materialsCount = 0;
    }

    public void minCount()
    {
        materialsCount = materialsCount - 1;
    }
    void Update()
    {
        materialsCount = materialsCount;
        for(int i = 0; i < 3; i++)
        {
            plateflag1[i] = plateflag1[i];
        }
    }
    public void resetFlag1()
    {
        for (int i = 0; i < 3; i++)
        {
            plateflag1[i] = false;
        }
    }
}
