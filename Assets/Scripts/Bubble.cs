using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public GameObject plateName;
    public GameObject bubble;
    public GameObject[] food = new GameObject[5];


    string[] ingredientsComponents = { "cheese", "tomato", "bread", "lettuce", "steak" };
    private string result;

    public float smoothTime = 0.001f;
    private Vector3 AVelocity = Vector3.zero;

    private string[] final_tag = { "plate_tomato_lettuce_cheese",
                            "plate_cheeseburger", "plate_steak_salad", "plate_sandwich", "plate_lettuce_burger",
                            "plate_tomato_sandwich", "plate_CheeseSteak", "plate_tomato_burger", "plate_TomatoSteak"};
    private string[] finalPlate = {"salad","cheeseBurger", "steakSalad", "sandwich", "lettuceBurger","tomatoSandwich",
                                     "cheeseSteak","tomatoBurger","tomatoSteak"};


    private void Start()
    {
        hideIngredients();
    }
    //Update is called once per frame
    void Update()
    {
        ArrayList arr = checkTag();
        if(showIngredients(arr))
        bubble.transform.position=Vector3.SmoothDamp(bubble.transform.position, plateName.transform.position +new Vector3(0, 2, 0), ref AVelocity, smoothTime);

    }


    void hideIngredients()
    {
        for (int i = 0; i < 5; i++)
        {
            food[i].SetActive(false);
        }
    }
    bool showIngredients(ArrayList nameComponents)
    {
        
        if (nameComponents != null && nameComponents.Count > 0)
        {
            for (int j = 0; j < nameComponents.Count; j++)
            {
                
                for (int i = 0; i < food.Length; i++)
                {

                    if (nameComponents[j].ToString() == food[i].name)
                    {
                        //Time.timeScale = 1f;
                        // Debug.Log("gggggggggggggggggg" + nameComponents[j].ToString());
                        food[i].SetActive(true);
                    }


                }
            }
            return true;

        }else
        {
            return false;
        }
       


    }


    string checkIngredient(string str)
    {
        string temp1;
        for(int i = 0; i < ingredientsComponents.Length; i++)
        {

            if (str == ingredientsComponents[i].ToString())
            {
                result = str;
                return result;
            }
            else
            {
                result = "plate";
            }
               
            
        }
        return result;

        
    }


    ArrayList checkTag()
    {

        string ingredients1 = gameObject.tag;
        // Debug.Log("ttttttttttttttttttt" + ingredients1);
        ArrayList AL = new ArrayList();
       

        ArrayList salad = new ArrayList() { "cheese", "lettuce", "tomato" };
        ArrayList cheeseBurger = new ArrayList() { "cheese", "bread", "steak" };
        ArrayList steakSalad = new ArrayList() { "steak", "lettuce", "tomato" };
        ArrayList sandwich = new ArrayList() { "bread", "lettuce", "cheese" };
        ArrayList lettuceBurger = new ArrayList() { "bread", "lettuce", "steak" };
        ArrayList tomatoSandwich = new ArrayList() { "bread", "tomato", "lettuce" };
        ArrayList cheeseSteak = new ArrayList() { "cheese", "steak", "lettuce" };
        ArrayList tomatoBurger = new ArrayList() { "bread", "tomato", "steak" };
        ArrayList tomatoSteak = new ArrayList() { "tomato", "steak", "cheese" };

        if (gameObject.tag == "plate_tomato_lettuce_cheese")
            return salad;
        else if (gameObject.tag == "plate_cheeseburger")
            return cheeseBurger;
        else if (gameObject.tag == "plate_steak_salad")
            return steakSalad;
        else if (gameObject.tag == "plate_sandwich")
            return sandwich;
        else if (gameObject.tag == "plate_lettuce_burger")
            return lettuceBurger;
        else if (gameObject.tag == "plate_tomato_sandwich")
            return tomatoSandwich;
        else if (gameObject.tag == "plate_CheeseSteak")
            return cheeseSteak;
        else if (gameObject.tag == "plate_tomato_burger")
            return tomatoBurger;
        else if (gameObject.tag == "plate_TomatoSteak")
            return tomatoSteak;
        else if (gameObject.tag == "Plate" || gameObject.tag == "plate_spawner")
            return AL;
        else
        {
            string[] ingredient = gameObject.tag.Split('_');

            for (int i = 0; i < ingredient.Length; i++)
            {
                string temp1 = checkIngredient(ingredient[i]);
                if (temp1 != "plate")
                    AL.Add(temp1);

            }




        }

        
        return AL;

    }







}
