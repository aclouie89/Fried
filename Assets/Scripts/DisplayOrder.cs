using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayOrder : MonoBehaviour
{
    public static bool saladIsShow = false;
    public static bool cheeseBurgerIsShow = false;
    public static bool BurgerIsShow = false;
    public GameObject salad;
    public GameObject cheeseBurger;
    public GameObject burger;




    public void display(string order)
    {
        if(order== "plate_tomato_lettuce_cheese" && ! saladIsShow)
        {
            Time.timeScale = 1f;
            salad.SetActive(true);
            saladIsShow = true;
            

        }
        else if(order== "plate_cheeseburger" && !cheeseBurgerIsShow)
        {
            Time.timeScale = 1f;
            cheeseBurger.SetActive(true);
            cheeseBurgerIsShow = true;

        }else if(order== "plate_burger" && !BurgerIsShow)
        {
            Time.timeScale = 1f;
            burger.SetActive(true);
            BurgerIsShow = true;
        }
        
    }

    public void DestoryOrder(string order)
    {
        if (order == "plate_tomato_lettuce_cheese" && saladIsShow)
        {
            salad.SetActive(false);
            saladIsShow = false;

        }
        else if (order == "plate_cheeseburger" && cheeseBurgerIsShow)
        {
            cheeseBurger.SetActive(false);
            cheeseBurgerIsShow = false;

        }
        else if (order == "plate_burger" && BurgerIsShow)
        {
            burger.SetActive(false);
            BurgerIsShow = false;
        }

    }
}
