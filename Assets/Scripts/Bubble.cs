using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{

    public GameObject plate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkTag();
    }


    private string[] final_tag = { "plate_tomato_lettuce_cheese",
                            "plate_cheeseburger", "plate_steak_salad", "plate_sandwich", "plate_lettuce_burger",
                            "plate_tomato_sandwich", "plate_CheeseSteak", "plate_tomato_burger", "plate_TomatoSteak"};
    void showIngredients(string[] nameComponents)
    {
        for(int i = 0; i < nameComponents.Length; i++)
        {
            if (nameComponents[i] == "cheese")
            {

            }else if(nameComponents[i] == "tomato")
            {

            }else if (nameComponents[i] == "bread")
            {

            }else if (nameComponents[i] == "steak")
            {

            }else if (nameComponents[i] == "lettuce")
            {

            }

        }


    }

    string[] checkTag()
    {
        string ingredients = gameObject.tag;
        string[] nameComponent= new string[3];
        string[] cheeseBurger= { "cheese", "bread", "steak" };
        string[] steakSalad = { "steak", "lettuce", "tomato" };
        string[] sandwich = { "bread", "lettuce", "cheese" };
        string[] lettuceBurger = { "bread", "lettuce", "steak" };
        string[] tomatoSandwich = { "bread", "tomato", "lettuce" };
        string[] cheeseSteak = { "cheese", "steak", "lettuce" };
        string[] tomatoBurger = { "bread", "tomato", "steak" };
        string[] tomatoSteak = { "tomato", "steak", "cheese" };
        if (ingredients == "plate_cheeseburger")
            cheeseBurger.CopyTo(nameComponent, 0);
        else if (ingredients== "plate_steak_salad")
            steakSalad.CopyTo(nameComponent, 0);
        else if (ingredients == "plate_sandwich")
            sandwich.CopyTo(nameComponent, 0);
        else if (ingredients == "plate_lettuce_burger")
            lettuceBurger.CopyTo(nameComponent, 0);
        else if (ingredients == "plate_tomato_sandwich")
            tomatoSandwich.CopyTo(nameComponent, 0);
        else if (ingredients == "plate_CheeseSteak")
            cheeseSteak.CopyTo(nameComponent, 0);
        else if (ingredients == "plate_tomato_burger")
            tomatoBurger.CopyTo(nameComponent, 0);
        else if (ingredients == "plate_TomatoSteak")
            tomatoSteak.CopyTo(nameComponent, 0);
        else
        {
            string[] ingredient = ingredients.Split('_');
            
            if (ingredients.Length > 1)
            {

                string[] result = new string[ingredient.Length - 1];
                for(int j = 0; j < result.Length-1; j++)
                {
                    result[j] = ingredient[j+1];

                }
                return result;
            }
            return null;
           
        }
                              

        return nameComponent;

    }



}
