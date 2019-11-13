using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlateSwapper : MonoBehaviour
{

    public Material cut_tomato_mat;
    public Material cut_lettuce_mat;
    public Material cut_cheese_mat;
    public Material bread_mat;
    public Material steak_mat;
    public Material bread_steak_mat;
    public Material cut_steak_lettuce_mat;
    public Material cut_steak_cheese_mat;
    public Material cut_bread_lettuce_mat;
    public Material cut_bread_cheese_mat;
    public Material cheese_burger_mat;
    public Material cut_tomato_lettuce_mat;
    public Material cut_tomato_cheese_mat;
    public Material cut_lettuce_cheese_mat;
    public Material salad_mat;
    public Material Plate_Steak;
 
    public GameObject plate;

    private float min_dist = 1.0f;
   
     // check for table we're on
      private GameObject findTable()
      {
          float closest_distance = Mathf.Infinity;
          GameObject[] gos = GameObject.FindGameObjectsWithTag("normal_table");
          GameObject closest = null;
          Vector3 position = transform.position;
          foreach (GameObject go in gos)
          {
            Vector3 diff = go.GetComponent<Transform>().position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < closest_distance && curDistance < min_dist)
            {
              closest = go;
              closest_distance = curDistance;
            }
          }

          return closest;
      }

    // tell our table to update
    private void updateTable(GameObject table, GameObject new_plate)
    {
        table.GetComponent<NormalTable>().putOnTable(new_plate);
    }
    private IEnumerator OnTriggerEnter(Collider col)
    {
        //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
        if (col.gameObject.GetComponent<Renderer>().tag == "cut_tomato")
        {

            if (plate.gameObject.GetComponent<Renderer>().tag == "Plate")
            {
                //Debug.log(col.gameObject.GetComponent<Renderer>().tag);
                plate.gameObject.GetComponent<Renderer>().material = cut_tomato_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_tomato";
                //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_lettuce")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_tomato_lettuce_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_tomato_lettuce";
                //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_cheese")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_tomato_cheese_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_tomato_cheese";
                //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_tomato_cheese" || plate.gameObject.GetComponent<Renderer>().tag == "plate_tomato_lettuce"
                || plate.gameObject.GetComponent<Renderer>().tag == "plate_lettuce_cheese")
            {
                plate.gameObject.GetComponent<Renderer>().material = salad_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_tomato_lettuce_cheese";
                //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            else
            {
                Destroy(col.gameObject);
            }

        }
        else if (col.gameObject.GetComponent<Renderer>().tag == "cut_lettuce")
        {
            yield return new WaitForSeconds(0.01f);
            if (plate.gameObject.GetComponent<Renderer>().tag == "Plate")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_lettuce_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_lettuce";
                //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_cheese")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_lettuce_cheese_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_lettuce_cheese";
                //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_tomato")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_tomato_lettuce_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_tomato_lettuce";
                //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_bread")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_bread_lettuce_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_bread_lettuce";
                //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            //else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_bread_cheese")
            //{
            //    plate.gameObject.GetComponent<Renderer>().material = cut_bread_lettuce_cheese_mat;
            //    plate.gameObject.GetComponent<Renderer>().tag = "plate_bread_cheese_lettuce";
            //    //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
            //    updateTable(findTable(), plate);
            //    Destroy(col.gameObject);
            //}
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_steak")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_steak_lettuce_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_lettuce_steak";
                //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            //else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_cheeseburger")
            //{
            //    plate.gameObject.GetComponent<Renderer>().material = burger_mat;
            //    plate.gameObject.GetComponent<Renderer>().tag = "plate_burger";
            //    //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
            //    updateTable(findTable(), plate);
            //    Destroy(col.gameObject);
            //}
            //else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_cheese_steak")
            //{
            //    plate.gameObject.GetComponent<Renderer>().material = cut_steak_lettuce_cheese_mat;
            //    plate.gameObject.GetComponent<Renderer>().tag = "plate_cheese_lettuce_steak";
            //    //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
            //    updateTable(findTable(), plate);
            //    Destroy(col.gameObject);
            //}
            //else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_bread_cheese_steak")
            //{
            //    plate.gameObject.GetComponent<Renderer>().material = burger_mat;
            //    plate.gameObject.GetComponent<Renderer>().tag = "plate_burger";
            //    //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
            //    updateTable(findTable(), plate);
            //    Destroy(col.gameObject);
            //}
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_tomato_cheese" || plate.gameObject.GetComponent<Renderer>().tag == "plate_tomato_lettuce"
                || plate.gameObject.GetComponent<Renderer>().tag == "plate_lettuce_cheese")
            {
                plate.gameObject.GetComponent<Renderer>().material = salad_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_tomato_lettuce_cheese";
                //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            else
            {
                Destroy(col.gameObject);
            }

        }
        else if (col.gameObject.GetComponent<Renderer>().tag == "cut_cheese")
        {
            yield return new WaitForSeconds(0.01f);
            if (plate.gameObject.GetComponent<Renderer>().tag == "Plate")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_cheese_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_cheese";
                //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_lettuce")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_lettuce_cheese_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_lettuce_cheese";
                //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_tomato")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_tomato_cheese_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_tomato_cheese";
                //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);

            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_bread")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_bread_cheese_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_bread_cheese";
                //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_steak")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_steak_cheese_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_cheese_steak";
                //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            //else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_lettuce_steak")
            //{
            //    plate.gameObject.GetComponent<Renderer>().material = cut_steak_lettuce_cheese_mat;
            //    plate.gameObject.GetComponent<Renderer>().tag = "plate_cheese_lettuce_steak";
            //    //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
            //    updateTable(findTable(), plate);
            //    Destroy(col.gameObject);
            //}
            //else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_bread_lettuce_steak")
            //{
            //    plate.gameObject.GetComponent<Renderer>().material = burger_mat;
            //    plate.gameObject.GetComponent<Renderer>().tag = "plate_burger";
            //    //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
            //    updateTable(findTable(), plate);
            //    Destroy(col.gameObject);
            //}
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_bread_steak")
            {
                plate.gameObject.GetComponent<Renderer>().material = cheese_burger_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_cheeseburger";
                //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            //else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_bread_lettuce")
            //{
            //    plate.gameObject.GetComponent<Renderer>().material = cut_bread_lettuce_cheese_mat;
            //    plate.gameObject.GetComponent<Renderer>().tag = "plate_bread_cheese_lettuce";
            //    //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
            //    updateTable(findTable(), plate);
            //    Destroy(col.gameObject);
            //}
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_tomato_cheese" || plate.gameObject.GetComponent<Renderer>().tag == "plate_tomato_lettuce"
                || plate.gameObject.GetComponent<Renderer>().tag == "plate_lettuce_cheese")
            {
                plate.gameObject.GetComponent<Renderer>().material = salad_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_tomato_lettuce_cheese";
                //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            else
            {
                Destroy(col.gameObject);
            }

        }
        else if (col.gameObject.GetComponent<Renderer>().tag == "bread")
        {
            yield return new WaitForSeconds(0.01f);
            if (plate.gameObject.GetComponent<Renderer>().tag == "Plate")
            {
                plate.gameObject.GetComponent<Renderer>().material = bread_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_bread";
                //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_lettuce")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_bread_lettuce_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_bread_lettuce";
                //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_cheese")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_bread_cheese_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_bread_cheese";
                //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);

            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_steak")
            {
                plate.gameObject.GetComponent<Renderer>().material = bread_steak_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_bread_steak";
                //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_cheese_steak")
            {
                plate.gameObject.GetComponent<Renderer>().material = cheese_burger_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_cheeseburger";
                //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            //else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_lettuce_steak")
            //{
            //    plate.gameObject.GetComponent<Renderer>().material = cut_bread_steak_lettuce_mat;
            //    plate.gameObject.GetComponent<Renderer>().tag = "plate_bread_lettuce_steak";
            //    //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
            //    updateTable(findTable(), plate);
            //    Destroy(col.gameObject);
            //}
            //else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_cheese_lettuce_steak")
            //{
            //    plate.gameObject.GetComponent<Renderer>().material = burger_mat;
            //    plate.gameObject.GetComponent<Renderer>().tag = "plate_burger";
            //    //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
            //    updateTable(findTable(), plate);
            //    Destroy(col.gameObject);
            //}
            //else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_lettuce_cheese")
            //{
            //    plate.gameObject.GetComponent<Renderer>().material = cut_bread_lettuce_cheese_mat;
            //    plate.gameObject.GetComponent<Renderer>().tag = "plate_bread_cheese_lettuce";
            //    //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
            //    updateTable(findTable(), plate);
            //    Destroy(col.gameObject);
            //}
        }

        else if (col.gameObject.GetComponent<Renderer>().tag == "cooked_steak")
        {
            yield return new WaitForSeconds(0.01f);
            if (plate.gameObject.GetComponent<Renderer>().tag == "Plate")
            {
                plate.gameObject.GetComponent<Renderer>().material = Plate_Steak;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_steak";
                //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_lettuce")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_steak_lettuce_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_lettuce_steak";
                //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_cheese")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_steak_cheese_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_cheese_steak";
                //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);

            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_bread")
            {
                plate.gameObject.GetComponent<Renderer>().material = bread_steak_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_bread_steak";
                //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_bread_cheese")
            {
                plate.gameObject.GetComponent<Renderer>().material = cheese_burger_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_cheeseburger";
                //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            //else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_bread_lettuce")
            //{
            //    plate.gameObject.GetComponent<Renderer>().material = cut_bread_steak_lettuce_mat;
            //    plate.gameObject.GetComponent<Renderer>().tag = "plate_bread_lettuce_steak";
            //    //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
            //    updateTable(findTable(), plate);
            //    Destroy(col.gameObject);
            //}
            //else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_bread_cheese_lettuce")
            //{
            //    plate.gameObject.GetComponent<Renderer>().material = burger_mat;
            //    plate.gameObject.GetComponent<Renderer>().tag = "plate_burger";
            //    //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
            //    updateTable(findTable(), plate);
            //    Destroy(col.gameObject);
            //}
            //else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_lettuce_cheese")
            //{
            //    plate.gameObject.GetComponent<Renderer>().material = cut_steak_lettuce_cheese_mat;
            //    plate.gameObject.GetComponent<Renderer>().tag = "plate_cheese_lettuce_steak";
            //    //Debug.log(plate.gameObject.GetComponent<Renderer>().tag);
            //    updateTable(findTable(), plate);
            //    Destroy(col.gameObject);
            //}
            else
            {
                Destroy(col.gameObject);
            }

        }
    }
}
