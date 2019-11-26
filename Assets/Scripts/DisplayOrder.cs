using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayOrder : MonoBehaviour
{
    public  bool saladIsShow = false;
    public  bool cheeseBurgerIsShow = false;
    public  bool steakSaladIsShow = false;
    public  bool sandwichIsShow = false;
    public  bool lettuceBurgerIsShow = false;
    public  bool tomatoSandwichIsShow = false;
    public  bool cheeseSteakIsShow = false;
    public  bool tomatoBurgerIsShow = false;
    public  bool tomatoSteakIsShow = false;

    public GameObject salad;
    public GameObject cheeseBurger;
    public GameObject steakSalad;
    public GameObject sandwich;
    public GameObject lettuceBurger;
    public GameObject tomatoSandwich;
    public GameObject cheeseSteak;
    public GameObject tomatoBurger;
    public GameObject tomatoSteak;

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

        }else if(order== "plate_steak_salad"&& !steakSaladIsShow)
        {
            Time.timeScale = 1f;
            steakSalad.SetActive(true);
            steakSaladIsShow = true;
        }else if(order== "plate_sandwich" && !sandwichIsShow)
        {
            Time.timeScale = 1f;
            sandwich.SetActive(true);
            sandwichIsShow = true;
        }
        else if(order== "plate_lettuce_burger" && !lettuceBurgerIsShow)
        {
            Time.timeScale = 1f;
            lettuceBurger.SetActive(true);
            lettuceBurgerIsShow = true;

        }
        else if(order== "plate_tomato_sandwich" && !tomatoSandwichIsShow)
        {
            Time.timeScale = 1f;
            tomatoSandwich.SetActive(true);
            tomatoSandwichIsShow = true;
        }
        else if(order== "plate_CheeseSteak" && !cheeseSteakIsShow)
        {
            Time.timeScale = 1f;
            cheeseSteak.SetActive(true);
            cheeseSteakIsShow = true;
        }
        else if(order== "plate_tomato_burger" && !tomatoBurgerIsShow)
        {
            Time.timeScale = 1f;
            tomatoBurger.SetActive(true);
            tomatoBurgerIsShow = true;

        }
        else if(order== "plate_TomatoSteak"&& !tomatoSteakIsShow)
        {
            Time.timeScale = 1f;
            tomatoSteak.SetActive(true);
            tomatoSteakIsShow = true;
        }
        
    }

    public void DestoryOrder(string order)
    {
        if (order == "plate_tomato_lettuce_cheese" && saladIsShow)
        {
            Time.timeScale = 1f;
            salad.SetActive(false);
            saladIsShow = false;

        }
        else if (order == "plate_cheeseburger" && cheeseBurgerIsShow)
        {
            Time.timeScale = 1f;
            cheeseBurger.SetActive(false);
            cheeseBurgerIsShow = false;

        }
        else if (order == "plate_steak_salad" && steakSaladIsShow)
        {
            Time.timeScale = 1f;
            steakSalad.SetActive(false);
            steakSaladIsShow = false;
        }
        else if (order == "plate_sandwich" && sandwichIsShow)
        {
            Time.timeScale = 1f;
            sandwich.SetActive(false);
            sandwichIsShow = false;
        }
        else if (order == "plate_lettuce_burger" && lettuceBurgerIsShow)
        {
            Time.timeScale = 1f;
            lettuceBurger.SetActive(false);
            lettuceBurgerIsShow = false;

        }
        else if (order == "plate_tomato_sandwich" && tomatoSandwichIsShow)
        {
            Time.timeScale = 1f;
            tomatoSandwich.SetActive(false);
            tomatoSandwichIsShow = false;
        }
        else if (order == "plate_CheeseSteak" && cheeseSteakIsShow)
        {
            Time.timeScale = 1f;
            cheeseSteak.SetActive(false);
            cheeseSteakIsShow = false;
        }
        else if (order == "plate_tomato_burger" && tomatoBurgerIsShow)
        {
            Time.timeScale = 1f;
            tomatoBurger.SetActive(false);
            tomatoBurgerIsShow = false;

        }
        else if (order == "plate_TomatoSteak" && tomatoSteakIsShow)
        {
            Time.timeScale = 1f;
            tomatoSteak.SetActive(false);
            tomatoSteakIsShow = false;
        }


    }
}
