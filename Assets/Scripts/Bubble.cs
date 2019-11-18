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
        showIngredients();
    }


    private string[] final_tag = { "plate_tomato_lettuce_cheese",
                            "plate_cheeseburger", "plate_steak_salad", "plate_sandwich", "plate_lettuce_burger",
                            "plate_tomato_sandwich", "plate_CheeseSteak", "plate_tomato_burger", "plate_TomatoSteak"};
    void showIngredients()
    {
        string ingredients = gameObject.tag;
        string[] nameComponent = ingredients.Split('_');
        bool exist=((IList)final_tag).Contains(ingredients);
        for (int i =0;i< nameComponent.Length; i++)
        {
            
            Debug.Log("plate content: "+ nameComponent[i]+ "length: "+ nameComponent.Length);
        }
        


    }

    void checkTag(string str)
    {

    }

}
