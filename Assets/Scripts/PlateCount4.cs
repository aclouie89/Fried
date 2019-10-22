using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCount4 : MonoBehaviour
{
    public int materialsCount = 0;
    public bool[] plateflag4 = new bool[3];

    // Update is called once per frame
    public void count4()
    {
        materialsCount = materialsCount + 1;
        print("plate4");
        print(materialsCount);
    }
    public void resetCount4()
    {
        materialsCount = 0;
    }

    public void minCount4()
    {
        materialsCount = materialsCount - 1;
    }

    void Update()
    {
        materialsCount = materialsCount;
    }
    public void resetFlag4()
    {
        for (int i = 0; i < 3; i++)
        {
            plateflag4[i] = false;
        }
    }


}
